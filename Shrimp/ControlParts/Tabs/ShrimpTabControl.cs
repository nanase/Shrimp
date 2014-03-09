using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.VisualStyles;
using Microsoft.VisualBasic;
using Shrimp.Module.StringUtil;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace Shrimp.ControlParts.Tabs
{
    class ShrimpTabControl : TabControl
    {
        private Font NormalFont, BoldFont;
        // タブの閉じるボタンクリックイベント
        public event EventHandler TabCloseButtonClick;
        public TabControls HoverControl = null;
        public int HoverControlNum = -1;

        public ShrimpTabControl () : base()
        {
            this.Font = new Font ( "Meiryo", 9f );
            this.NormalFont = new Font ( this.Font.Name, 9f, FontStyle.Regular );
            this.BoldFont = new Font ( this.Font.Name, 9f, FontStyle.Bold );
            //Paintイベントで描画できるようにする
            if ( TabRenderer.IsSupported )
                this.SetStyle(ControlStyles.UserPaint, true);
            //ダブルバッファリングを有効にする
            this.DoubleBuffered = true;
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            ////this.SetStyle(ControlStyles.DoubleBuffer, true);
            //リサイズで再描画する
            this.ResizeRedraw = true;
            //this.SetStyle(ControlStyles.ResizeRedraw, true);

            //ControlStyles.UserPaintをTrueすると、
            //SizeModeは強制的にTabSizeMode.Fixedにされる
            this.SizeMode = TabSizeMode.Normal;
            this.ItemSize = new Size(120, 18);
            this.Padding = new Point ( 20, 3 );
            this.Appearance = TabAppearance.Normal;
            this.Alignment = TabAlignment.Top;
            this.Multiline = true;
            this.AllowDrop = true;

            this.DragEnter += new DragEventHandler ( ShrimpTabControl_DragEnter );
        }

        void ShrimpTabControl_DragEnter ( object sender, DragEventArgs e )
        {
            if ( e.Data.GetDataPresent ( typeof ( TabControls ) ) &&
            this.TabPages.Contains ( (TabControls)( e.Data.GetData ( typeof ( TabControls ) ) ) ) )
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        protected override void OnKeyDown ( KeyEventArgs ke )
        {
            base.OnKeyDown ( ke );

            if ( ke.KeyCode == Keys.Left )
            {
                var t = this.SelectedIndex - 1;
                if ( t < 0 )
                    t = 0;
                this.SelectedIndex = t;
                ke.SuppressKeyPress = true;
            }
            else if ( ke.KeyCode == Keys.Right )
            {
                var t = this.SelectedIndex + 1;
                if ( t >= this.TabCount - 1 )
                    t = this.TabCount - 1;
                this.SelectedIndex = t;
                ke.SuppressKeyPress = true;
            }
            else if ( ke.KeyCode == Keys.Up )
            {
                TabControls tab = this.SelectedTab as TabControls;
                if ( tab != null )
                {
                    tab.ScrollUp ();
                }
                ke.SuppressKeyPress = true;
            }
            else if ( ke.KeyCode == Keys.Down )
            {
                TabControls tab = this.SelectedTab as TabControls;
                if ( tab != null )
                {
                    tab.ScrollDown ();
                }
                ke.SuppressKeyPress = true;
            }
            else
            {
                TabControls tab = this.SelectedTab as TabControls;
                if ( tab != null )
                {
                    tab.OnKeyDownFromParent ( ke );
                }
            }
                
        }


        // タブの閉じるボタンクリックイベント
        protected void OnCloseButtonClick ( EventArgs e )
        {
            if ( this.TabCloseButtonClick != null )
            {
                this.TabCloseButtonClick ( this, e );
            }
        }

        /// <summary>
        /// マウスカーソルから、アイテムの位置を検索
        /// </summary>
        /// <param name="Location"></param>
        /// <returns></returns>
        public int MouseCursorPointToItem ( Point Location )
        {
            for ( int i = 0; i < this.TabPages.Count; i++ )
            {
                Rectangle rect = this.GetTabRect ( i );
                var tab = this.TabPages[i] as TabControls;
                if ( rect.Contains ( Location ) )
                {
                    return i;
                }
            }
            return -1;
        }

        protected override void OnMouseMove ( MouseEventArgs e )
        {
            base.OnMouseMove ( e );
            for ( int i = 0; i < this.TabPages.Count; i++ )
            {
                Rectangle rect = this.GetTabRect ( i );
                var tab = this.TabPages[i] as TabControls;
                if ( rect.Contains ( e.Location ) )
                {
                    this.HoverControlNum = i;
                    this.HoverControl = tab;
                    break;
                }
            }

            if ( HoverControlNum >= 0 )
            {
                if ( !HoverControl.isLock )
                {
                    Rectangle rect = this.GetTabCloseButtonRect ( HoverControlNum );
                    if ( rect.Contains ( e.Location ) )
                    {
                        this.Cursor = Cursors.Hand;
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                    }
                }

                if ( e.Button == MouseButtons.Left && this.HoverControl != null )
                {
                    this.DoDragDrop ( this.HoverControl, DragDropEffects.Move );
                }
                this.Invalidate ();
            }
        }

        // OnMouseUp
        protected override void OnMouseUp ( MouseEventArgs e )
        {
            if ( HoverControlNum >= 0 && !HoverControl.isLock )
            {
                Rectangle rect = this.GetTabCloseButtonRect ( HoverControlNum );
                if ( rect.Contains ( e.Location ) )
                {
                    this.OnCloseButtonClick ( new EventArgs () );
                    this.Invalidate ( rect );
                }
            }

            base.OnMouseUp ( e );

        }

        // タブの閉じるボタン場所を取得
        private Rectangle GetTabCloseButtonRect ( int index )
        {
            Rectangle rect = this.GetTabRect ( index );
            rect.X = rect.Right - 20;
            rect.Y = rect.Top + 2;
            rect.Width = 16;
            rect.Height = 16;

            return rect;
        }

        // タブに閉じるボタンを描画
        private void DrawTabCloseButton ( Graphics g, Rectangle rect )
        {
            ControlPaint.DrawCaptionButton ( g, rect, CaptionButton.Close, ButtonState.Flat );
            g.Dispose ();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if ( !TabRenderer.IsSupported )
            {
                throw new Exception ( "カスタムタブコントロールを描画することができません" );
            }

            //TabControlの背景を塗る
            e.Graphics.FillRectangle(SystemBrushes.Control, this.ClientRectangle);

            if (this.TabPages.Count == 0)
                return;

            //TabPageの枠を描画する
            TabControls page = this.SelectedTab as TabControls;
            if ( page == null )
                return;
            Rectangle pageRect = new Rectangle(
                page.Bounds.X - 2,
                page.Bounds.Y - 2,
                page.Bounds.Width + 5,
                page.Bounds.Height + 5);
            TabRenderer.DrawTabPage(e.Graphics, pageRect);

            //タブを描画する
            for (int i = 0; i < this.TabPages.Count; i++)
            {
                page = this.TabPages[i]  as TabControls;
                Rectangle tabRect = this.GetTabRect(i);
                //表示するタブの状態を決定する
                TabItemState state;
                if (!this.Enabled)
                {
                    state = TabItemState.Disabled;
                }
                else if (this.SelectedIndex == i)
                {
                    state = TabItemState.Selected;
                }
                else
                {
                    state = TabItemState.Normal;
                }

                //選択されたタブとページの間の境界線を消すために、
                //描画する範囲を大きくする
                if (this.SelectedIndex == i)
                {
                    if (this.Alignment == TabAlignment.Top)
                    {
                        tabRect.Height += 1;
                    }
                    else if (this.Alignment == TabAlignment.Bottom)
                    {
                        tabRect.Y -= 2;
                        tabRect.Height += 2;
                    }
                    else if (this.Alignment == TabAlignment.Left)
                    {
                        tabRect.Width += 1;
                    }
                    else if (this.Alignment == TabAlignment.Right)
                    {
                        tabRect.X -= 2;
                        tabRect.Width += 2;
                    }
                }

                //画像のサイズを決定する
                Size imgSize;
                if (this.Alignment == TabAlignment.Left ||
                    this.Alignment == TabAlignment.Right)
                {
                    imgSize = new Size(tabRect.Height, tabRect.Width);
                }
                else
                {
                    imgSize = tabRect.Size;
                }

                //Bottomの時はTextを表示しない（Textを回転させないため）
                string tabText = page.Text;
                if (this.Alignment == TabAlignment.Bottom)
                {
                    tabText = "";
                }

                //タブの画像を作成する
                Bitmap bmp = new Bitmap(imgSize.Width, imgSize.Height);
                Graphics g = Graphics.FromImage(bmp);

                //高さに1足しているのは、下にできる空白部分を消すため
                Image img = new Bitmap ( imgSize.Width, imgSize.Height);
                Graphics gg = Graphics.FromImage ( img );
                /*
                LinearGradientBrush gb = new LinearGradientBrush (
                    g.VisibleClipBounds,
                    Color.Red,
                    Color.Gray,
                    LinearGradientMode.Vertical );
                */
                Brush BackBrush = 
                    ( state == TabItemState.Selected ? Setting.TabColors.SelectedTabBackground : 
                    page.isContainUnRead ? Setting.TabColors.ExistUnReadBackgroundColor : Setting.TabColors.NormalTabBackground );
                Brush StringBrush =
                    ( state == TabItemState.Selected ? Setting.TabColors.SelectedTabString :
                    page.isContainUnRead ? Setting.TabColors.ExistUnReadStringColor : Setting.TabColors.NormalTabString );
                bool isBold = 
                    ( state == TabItemState.Selected ? Setting.TabColors.isBoldSelectedTab : 
                    page.isContainUnRead ? Setting.TabColors.isBoldUnRead : Setting.TabColors.isBoldNormalTab );
                gg.FillRectangle ( BackBrush, new Rectangle ( 2, 2, imgSize.Width - 4, imgSize.Height - 2 ) );
                gg.Dispose ();

                TabRenderer.DrawTabItem ( g,
                            new Rectangle ( 0, 0, bmp.Width, bmp.Height ),
                            "",
                            null, img,
                            new Rectangle ( 0, 0, img.Width, img.Height ), false,
                            state );

                if ( i == SelectedIndex )
                    g.FillRectangle ( Brushes.Yellow, 
                        new Rectangle ( 2, ( this.Alignment == TabAlignment.Bottom ? bmp.Height - 3 : 0 ),
                            bmp.Width - 2, 3 ) );
                if ( HoverControlNum == i && !HoverControl.isLock )
                    DrawTabCloseButton ( g, new Rectangle ( bmp.Width - 22, 2, 20, bmp.Height - 2 ) );

                if ( this.Alignment == TabAlignment.Left || this.Alignment == TabAlignment.Right )
                {
                    StringFormat sf = new StringFormat ();
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Center;
                    var text = page.Text;
                    g = Graphics.FromImage ( bmp );
                    g.DrawString ( text,
                        ( isBold ? this.BoldFont : this.NormalFont ),
                        StringBrush,
                        new RectangleF ( 2, 2, bmp.Width, bmp.Height ),
                        sf );
                    sf.Dispose ();
                }
                g.Dispose();


                //画像を回転する
                if ( this.Alignment == TabAlignment.Bottom )
                {
                    bmp.RotateFlip ( RotateFlipType.Rotate180FlipNone );
                }
                else if ( this.Alignment == TabAlignment.Left )
                {
                    bmp.RotateFlip ( RotateFlipType.Rotate270FlipNone );
                }
                else if ( this.Alignment == TabAlignment.Right )
                {
                    bmp.RotateFlip ( RotateFlipType.Rotate90FlipNone );
                }

                if ( this.Alignment != TabAlignment.Left && this.Alignment != TabAlignment.Right )
                {
                    StringFormat sf2 = new StringFormat ();
                    sf2.Alignment = StringAlignment.Near;
                    sf2.LineAlignment = StringAlignment.Center;
                    var text2 = page.Text;
                    g = Graphics.FromImage ( bmp );
                    g.DrawString ( text2,
                        ( isBold ? this.BoldFont : this.NormalFont ),
                        StringBrush,
                        new RectangleF ( 2, 3, bmp.Width, bmp.Height ),
                        sf2 );
                    sf2.Dispose ();
                }

                //画像を描画する
                e.Graphics.DrawImage(bmp, tabRect.X, tabRect.Y, bmp.Width, bmp.Height);
                bmp.Dispose();
                g.Dispose ();
            }
        }

        /// <summary>
        /// フォントの設定
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="Msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport ( "user32.dll" )]
        private static extern IntPtr SendMessage ( IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam );

        private const int WM_SETFONT = 0x30;
        private const int WM_FONTCHANGE = 0x1d;

        protected override void OnCreateControl ()
        {
            base.OnCreateControl ();
            this.OnFontChanged ( EventArgs.Empty );
        }

        protected override void OnFontChanged ( EventArgs e )
        {
            base.OnFontChanged ( e );
            IntPtr hFont = this.Font.ToHfont ();
            SendMessage ( this.Handle, WM_SETFONT, hFont, (IntPtr)( -1 ) );
            SendMessage ( this.Handle, WM_FONTCHANGE, IntPtr.Zero, IntPtr.Zero );
            this.UpdateStyles ();
            this.ItemSize = new Size ( 0, 18 );
        }
    }
}
