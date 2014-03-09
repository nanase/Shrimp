using System.Collections.Generic;
using System.Drawing;
using Shrimp.Module.Parts;
using Shrimp.Twitter.Entities;
using Shrimp.Twitter.Status;
namespace Shrimp.ControlParts.Timeline.Draw.Cells
{
    /// <summary>
    /// セルのインターフェイスです
    /// </summary>
    interface IDrawCell
    {
        void DrawInitialize(Graphics g, Point offset, DrawCellSize drawCellSize, ControlParts.Timeline.Draw.TweetDraw.SetClickLinkDelegate SetClickLink, int maxWidth, Point MouseHover);
        /// <summary>
        /// 背景を描画
        /// </summary>
        void DrawBackground(Brush brush);
        /// <summary>
        /// アイコンを描画
        /// </summary>
        void DrawIcon(Bitmap image, string screen_name);
        /// <summary>
        /// 名前を描画
        /// </summary>
        void DrawName(string screen_name);
        /// <summary>
        /// 鍵アカウントのアイコン描画
        /// </summary>
        void DrawProtectedIcon();
        /// <summary>
        /// 時間描画
        /// </summary>
        void DrawTime(bool isUseCellInfo, decimal tweet_id);
        /// <summary>
        /// テキストを描画
        /// </summary>
        void DrawText(TwitterEntities entities, int[] selTextPosition, bool isTextBold);
        void DrawNormalText();
        /// <summary>
        /// イメージ描画
        /// </summary>
        void DrawImage(List<TwitterEntitiesMedia> media);
        /// リツイートを描画
        /// </summary>
        void DrawRetweetNotify(string screen_name);
        /// <summary>
        /// Viaを描画
        /// </summary>
        void DrawSource(string source);
        /// <summary>
        /// ボタン
        /// </summary>
        /// <param name="SetClickLink"></param>
        void DrawButtons(TwitterStatus tweet, bool CanReply, bool CanFavorite, bool CanRetweet, bool isAlreadyRetweeted, bool isAlreadyFaved);
    }
}
