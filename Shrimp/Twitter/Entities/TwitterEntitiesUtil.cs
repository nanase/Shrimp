using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shrimp.Twitter.Entities
{
    class TwitterEntitiesUtil
    {
        private static string getURLPosition ( List<TwitterEntitiesURLs> data, int num )
        {
            string res = null;
            if ( data == null )
                return null;
            foreach ( TwitterEntitiesURLs t in data )
            {
                if ( t.indices[0] <= num && t.indices[1] >= num )
                {
                    res = t.url;
                    break;
                }
            }
            return res;
        }

        private static string getMentionsPosition ( List<TwitterEntitiesUserMentions> data, int num )
        {
            string res = null;
            if ( data == null )
                return null;
            foreach ( TwitterEntitiesUserMentions t in data )
            {
                if ( t.indices[0] <= num && t.indices[1] >= num )
                {
                    res = t.screen_name;
                    break;
                }
            }
            return res;
        }

        private static string getHashTagsPosition ( List<TwitterEntitiesHashTags> data, int num )
        {
            string res = null;
            if ( data == null )
                return null;
            foreach ( TwitterEntitiesHashTags t in data )
            {
                if ( t.indices[0] <= num && t.indices[1] >= num )
                {
                    res = t.text;
                    break;
                }
            }
            return res;
        }

        private static string[] getMediaPosition ( List<TwitterEntitiesMedia> data, int num )
        {
            string[] res = null;
            if ( data == null )
                return null;
            foreach ( TwitterEntitiesMedia t in data )
            {
                if ( t.indices[0] <= num && t.indices[1] >= num )
                {
                    res = new string [2];
                    res[0] = t.media_url;
                    res[1] = t.url;
                    break;
                }
            }
            return res;
        }

        /// <summary>
        /// URLをexpand
        /// </summary>
        /// <param name="text"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string getCorrectURL ( string text, dynamic json )
        {
            if ( json.IsDefined ( "urls" ) )
            {
                foreach ( dynamic t in json.urls )
                {
                    text = text.Replace ( t.url, t.expanded_url );
                }
            }
            if ( json.IsDefined ( "media" ) )
            {
                foreach ( dynamic t in json.media )
                {
                    text = text.Replace ( t.url, t.media_url );
                }
            }
            return text;
        }

        public static TwitterEntitiesPosition getEntitiesPosition ( TwitterEntities entities, int num )
        {
            if ( entities == null )
                return null;

            string pos = null;
            pos = TwitterEntitiesUtil.getURLPosition ( entities.urls, num );
            if ( pos != null )
            {
                return new TwitterEntitiesPosition ( pos, "url" );
            }
            pos = TwitterEntitiesUtil.getMentionsPosition ( entities.user_mentions, num );
            if ( pos != null )
            {
                return new TwitterEntitiesPosition ( pos, "mention" );
            }
            pos = TwitterEntitiesUtil.getHashTagsPosition ( entities.hashtags, num );
            if ( pos != null )
            {
                return new TwitterEntitiesPosition ( pos, "hashtags" );
            }

            string[] pos2 = TwitterEntitiesUtil.getMediaPosition ( entities.media, num );
            if ( pos2 != null )
            {
                return new TwitterEntitiesPosition ( pos2[1], "media" );
            }
            return null;
        }
    }
}
