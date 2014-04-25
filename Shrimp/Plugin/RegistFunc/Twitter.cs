using System.Threading.Tasks;
using Shrimp.ControlParts.Timeline;

namespace Shrimp.Plugin.RegistFunc
{
    public class Twitter
    {
        private TimelineControl.OnUseTwitterAPIDelegate onUseTwitterAPI;

        public Twitter ( TimelineControl.OnUseTwitterAPIDelegate onUseTwitterAPI )
        {
            this.onUseTwitterAPI = onUseTwitterAPI;
        }

        /// <summary>
        /// Luaでtweetを呼び出すと呼ばれます。
        /// </summary>
        /// <param name="str"></param>
        public void update ( string str, decimal in_reply_to_status_id )
        {
            //  ツイート処理
            Task.Factory.StartNew ( () =>
            {
                if ( this.onUseTwitterAPI != null )
                    this.onUseTwitterAPI.Invoke ( this, new object[] { "tweet", new object[] { str, in_reply_to_status_id }, null } );
            } );
        }

        /// <summary>
        /// Luaでtweetを呼び出すと呼ばれます。
        /// </summary>
        /// <param name="str"></param>
        public void favorite ( decimal id )
        {
            //  ツイート処理
            Task.Factory.StartNew ( () =>
            {
                if ( this.onUseTwitterAPI != null )
                    this.onUseTwitterAPI.Invoke ( this, new object[] { "fav", id, null } );
            } );
        }

        /// <summary>
        /// Luaでtweetを呼び出すと呼ばれます。
        /// </summary>
        /// <param name="str"></param>
        public void unfavorite ( decimal id )
        {
            //  ツイート処理
            Task.Factory.StartNew ( () =>
            {
                if ( this.onUseTwitterAPI != null )
                    this.onUseTwitterAPI.Invoke ( this, new object[] { "unfav", id, null } );
            } );
        }

        /// <summary>
        /// Luaでtweetを呼び出すと呼ばれます。
        /// </summary>
        /// <param name="str"></param>
        public void retweet ( decimal id )
        {
            //  ツイート処理
            Task.Factory.StartNew ( () =>
            {
                if ( this.onUseTwitterAPI != null )
                    this.onUseTwitterAPI.Invoke ( this, new object[] { "retweet", id, null } );
            } );
        }

        /// <summary>
        /// Luaでupdate_profileを呼び出すと呼ばれます
        /// </summary>
        /// <param name="str"></param>
        public void update_profile ( string name, string url, string location, string description )
        {
            //  プロフィール変更
            Task.Factory.StartNew ( () =>
            {
                if ( this.onUseTwitterAPI != null )
                    this.onUseTwitterAPI.Invoke ( this, new object[] { "update_profile", new object[] {
                name, url, location, description }, null } );
            } );
        }
    }
}
