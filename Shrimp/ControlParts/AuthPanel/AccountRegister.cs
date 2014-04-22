using System;
using System.Diagnostics;
using System.Windows.Forms;
using Shrimp.Twitter;
using Shrimp.Twitter.REST.Authorize;

namespace Shrimp.ControlParts.AuthPanel
{
    /// <summary>
    /// アカウント認証画面
    /// </summary>
    public partial class AccountRegister : Form
    {
        #region -- Private Fields --
        private TwitterInfo destTwitter;
        private OAuthorize oauth = new OAuthorize ();
        #endregion

        #region -- Public Delegates --
        public delegate void CompletedAuthorizeTwitterDelegate ( object sender, TwitterInfo data );
        #endregion

        #region -- Public Events --
        public event CompletedAuthorizeTwitterDelegate CompletedAuthorizeTwitter;
        #endregion

        #region -- Public Properties --
        public string URLLabel
        {
            get { return this.accountURLLabel.Text; }
            set
            {
                if ( this.accountURLLabel.InvokeRequired )
                {
                    this.accountURLLabel.Invoke ( (MethodInvoker)delegate ()
                    {
                        this.accountURLLabel.Text = value;
                    } );
                }
                else
                {
                    this.accountURLLabel.Text = value;
                }
            }
        }

        public bool URLLabelEnable
        {
            get { return this.accountURLLabel.Enabled; }
            set
            {
                if ( this.accountURLLabel.InvokeRequired )
                {
                    this.accountURLLabel.Invoke ( (MethodInvoker)delegate ()
                    {
                        this.accountURLLabel.Enabled = value;
                    } );
                }
                else
                {
                    this.accountURLLabel.Enabled = value;
                }
            }
        }
        #endregion

        #region -- Constructors --
        public AccountRegister ( string consumer_key, string consumer_secret_key )
        {
            if ( consumer_key == null || consumer_secret_key == null )
                throw new NullReferenceException ( "アカウント認証画面でコンシューマーキーがnullです" );
            InitializeComponent ();
            this.destTwitter = new TwitterInfo ( consumer_key, consumer_secret_key );
        }
        #endregion

        #region -- Destructor --
        // TODO: デストラクタではなく IDispose に記述したほうがよさそう
        ~AccountRegister ()
        {
            oauth.loadCompletedEvent -= new Twitter.REST.TwitterWorker.loadCompletedEventHandler ( oauth_loadCompletedEvent );
        }
        #endregion

        #region -- Private Methods --
        private void AccountRegister_Load ( object sender, EventArgs e )
        {
            oauth.loadCompletedEvent += new Twitter.REST.TwitterWorker.loadCompletedEventHandler ( oauth_loadCompletedEvent );
            oauth.RequestToken ( this.destTwitter );
        }

        private void oauth_loadCompletedEvent ( object sender, Twitter.REST.TwitterCompletedEventArgs e )
        {
            if ( e.raw_data.Uri.OriginalString.IndexOf ( "oauth/request_token" ) >= 0 )
            {
                if ( e.data != null )
                {
                    //
                    string[] tmp = (string[])e.data;
                    this.URLLabel = "https://api.twitter.com/oauth/authorize?oauth_token=" + tmp[0] + "";
                    this.URLLabelEnable = true;
                    this.destTwitter.RequestTokenKey = tmp[0];
                    this.destTwitter.RequestTokenSecret = tmp[1];
                }
                else
                {
                    this.URLLabel = "トークンの取得に失敗しました。ネットワークに繋がっているか、コンピュータの時刻が狂っていないかどうかを確認してください";
                }
            }
            if ( e.raw_data.Uri.OriginalString.IndexOf ( "oauth/access_token" ) >= 0 )
            {
                if ( e.data != null )
                {
                    //
                    string[] tmp = (string[])e.data;
                    this.destTwitter.AccessTokenKey = tmp[0];
                    this.destTwitter.AccessTokenSecret = tmp[1];
                    this.destTwitter.UserId = Decimal.Parse ( tmp[2] );
                    this.destTwitter.ScreenName = tmp[3];
                    this.Tag = this.destTwitter;
                    if ( CompletedAuthorizeTwitter != null )
                        CompletedAuthorizeTwitter.Invoke ( this, (TwitterInfo)this.destTwitter.Clone () );
                    if ( this.InvokeRequired )
                    {
                        this.Invoke ( (MethodInvoker)delegate ()
                        {
                            this.Close ();
                        } );
                    }
                    else
                    {
                        this.Close ();
                    }
                }
                else
                {
                    MessageBox.Show ( "アクセストークンの取得に失敗しました。再度1からトークンを取得し直してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    this.URLLabelEnable = false;
                    this.PinBoxInitialize ();
                    this.destTwitter = new TwitterInfo ( this.destTwitter.ConsumerKey, this.destTwitter.ConsumerSecret );
                    oauth.RequestToken ( this.destTwitter );
                }
            }
        }

        private void PinBoxInitialize ()
        {
            this.Invoke ( (MethodInvoker)delegate ()
            {
                this.Pinbox.ResetText ();
                this.Pinbox.Enabled = false;
            } );
        }

        /// <summary>
        /// テキスト変更
        /// </summary>
        private void Pinbox_TextChanged ( object sender, EventArgs e )
        {
            this.AuthorizeButton.Enabled = (this.Pinbox.Text.Length != 0);
        }

        /// <summary>
        /// ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AuthorizeButton_Click ( object sender, EventArgs e )
        {
            if ( this.Pinbox.Text.Length == 0 )
                return;

            this.AuthorizeButton.Enabled = false;
            oauth.AccessToken ( destTwitter, this.Pinbox.Text );
        }

        /// <summary>
        /// リクエストトークンのURLを張る
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accountURLLabel_LinkClicked ( object sender, LinkLabelLinkClickedEventArgs e )
        {
            Process.Start ( this.URLLabel );
            if ( this.Pinbox.InvokeRequired )
            {
                this.Pinbox.Invoke ( (MethodInvoker)delegate ()
                {
                    this.Pinbox.Enabled = true;
                } );
            }
            else
            {
                this.Pinbox.Enabled = true;
            }
        }

        private void Pinbox_KeyDown ( object sender, KeyEventArgs e )
        {
            if ( e.KeyCode == Keys.Enter )
                AuthorizeButton_Click ( null, null );
        }
        #endregion
    }
}
