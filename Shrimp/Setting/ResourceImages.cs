using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Drawing;
using Shrimp.Module;

namespace Shrimp.Setting
{
    class ResourceImages
    {
        /// <summary>
        /// 静的コンストラクタ
        /// </summary>
        static ResourceImages ()
        {
            // コピーしたMemoryStreamからBitmapを作成し、背景に設定
            Fav = new ButtonImage (
                Image.FromStream ( getResourceFile ( "Shrimp.Images.c_fav.png" ) ),
                Image.FromStream ( getResourceFile ( "Shrimp.Images.fav.png" ) )
            );

            // コピーしたMemoryStreamからBitmapを作成し、背景に設定
            UnFav = new ButtonImage (
                Image.FromStream ( getResourceFile ( "Shrimp.Images.c_unfav.png" ) ),
                Image.FromStream ( getResourceFile ( "Shrimp.Images.unfav.png" ) )
            );

            // コピーしたMemoryStreamからBitmapを作成し、背景に設定
            Retweet = new ButtonImage (
                Image.FromStream ( getResourceFile ( "Shrimp.Images.c_retweet.png" ) ),
                Image.FromStream ( getResourceFile ( "Shrimp.Images.retweet.png" ) )
            );

            // コピーしたMemoryStreamからBitmapを作成し、背景に設定
            Reply = new ButtonImage (
                Image.FromStream ( getResourceFile ( "Shrimp.Images.c_reply.png" ) ),
                Image.FromStream ( getResourceFile ( "Shrimp.Images.reply.png" ) )
            );

            Protected = Image.FromStream ( getResourceFile ( "Shrimp.Images.protected.png" ) );
            In_Reply_To_Status_ID_Arrow = Image.FromStream ( getResourceFile ( "Shrimp.Images.in_reply_status_id_arrow.png" ) );
            LoadingImage = Image.FromStream ( getResourceFile ( "Shrimp.Images.loading.png" ) );
            UserImage = Image.FromStream ( getResourceFile ( "Shrimp.Images.user.png" ) );
            SearchImage = Image.FromStream ( getResourceFile ( "Shrimp.Images.search.png" ) );
            RepliesImage = Image.FromStream ( getResourceFile ( "Shrimp.Images.replies.png" ) );
            FavsImage = Image.FromStream ( getResourceFile ( "Shrimp.Images.favs.png" ) );
            BlockImage = Image.FromStream ( getResourceFile ( "Shrimp.Images.block.png" ) );
            BookmarkImage = Image.FromStream ( getResourceFile ( "Shrimp.Images.bookmark.png" ) );
            RemoveImage = Image.FromStream ( getResourceFile ( "Shrimp.Images.remove.png" ) );
            ConversationImage = Image.FromStream ( getResourceFile ( "Shrimp.Images.conversation.png" ) );
            TextImage = Image.FromStream ( getResourceFile ( "Shrimp.Images.text.png" ) );
        }

        /// <summary>
        /// リソースファイルのデータを取得
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static MemoryStream getResourceFile ( string path )
        {
            //現在のコードを実行しているAssemblyを取得
            Assembly assm =
                Assembly.GetExecutingAssembly ();
            int len = 0;
            BinaryReader reader;
            MemoryStream memory;
            Stream stream;

            stream = assm.GetManifestResourceStream ( path );
            len = (int)stream.Length;
            reader = new BinaryReader ( stream );
            memory = new MemoryStream ( len );

            memory.Write ( reader.ReadBytes ( len ), 0, len );
            return memory;
        }

        /// <summary>
        /// ふぁぼ画像
        /// </summary>
        public static ButtonImage Fav
        {
            get;
            set;
        }

        /// <summary>
        /// あんふぁぼ画像
        /// </summary>
        public static ButtonImage UnFav
        {
            get;
            set;
        }

        /// <summary>
        /// リツイート画像
        /// </summary>
        public static ButtonImage Retweet
        {
            get;
            set;
        }

        /// <summary>
        /// リプライ画像
        /// </summary>
        public static ButtonImage Reply
        {
            get;
            set;
        }

        /// <summary>
        /// カギ垢のアイコン
        /// </summary>
        public static Image Protected
        {
            get;
            set;
        }

        /// <summary>
        /// リプライ一覧のやじるし
        /// </summary>
        public static Image In_Reply_To_Status_ID_Arrow
        {
            get;
            set;
        }

        /// <summary>
        /// ロード中の画像
        /// </summary>
        public static Image LoadingImage
        {
            get;
            set;
        }

        /// <summary>
        /// ユーザーの画像
        /// </summary>
        public static Image UserImage
        {
            get;
            set;
        }

        /// <summary>
        /// 検索の画像
        /// </summary>
        public static Image SearchImage
        {
            get;
            set;
        }

        /// <summary>
        /// リプライ
        /// </summary>
        public static Image RepliesImage
        {
            get;
            set;
        }

        /// <summary>
        /// お気に入りタイムライン
        /// </summary>
        public static Image FavsImage
        {
            get;
            set;
        }


        /// <summary>
        /// ブロック
        /// </summary>
        public static Image BlockImage
        {
            get;
            set;
        }

        /// <summary>
        /// ブックマーク
        /// </summary>
        public static Image BookmarkImage
        {
            get;
            set;
        }

        /// <summary>
        /// ツイートの削除アイコン
        /// </summary>
        public static Image RemoveImage
        {
            get;
            set;
        }

        /// <summary>
        /// 会話アイコン
        /// </summary>
        public static Image ConversationImage
        {
            get;
            set;
        }

        /// <summary>
        /// アイコン
        /// </summary>
        public static Image TextImage
        {
            get;
            set;
        }

        public static Image BackgroundImage
        {
            get;
            set;
        }
    }
}
