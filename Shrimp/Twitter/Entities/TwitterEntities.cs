using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shrimp.Twitter.Entities;
using Shrimp.Module;
using System.Text.RegularExpressions;

namespace Shrimp.Twitter.Entities
{
    public class TwitterEntities
    {
        #region コンストラクタ
        public TwitterEntities ( dynamic raw_data )
        {
            if ( raw_data.IsDefined ( "hashtags" ) )
            {
                this.hashtags = new List<TwitterEntitiesHashTags> ();
                foreach ( dynamic t in raw_data.hashtags )
                {
                    this.hashtags.Add ( new TwitterEntitiesHashTags ( t ) );
                }
            }
            if ( raw_data.IsDefined ( "urls" ) )
            {
                this.urls = new List<TwitterEntitiesURLs> ();
                foreach ( dynamic t in raw_data.urls )
                {
                    this.urls.Add ( new TwitterEntitiesURLs ( t ) );
                }
            }
            if ( raw_data.IsDefined ( "user_mentions" ) )
            {
                this.user_mentions = new List<TwitterEntitiesUserMentions> ();
                foreach ( dynamic t in raw_data.user_mentions )
                {
                    this.user_mentions.Add ( new TwitterEntitiesUserMentions ( t ) );
                }
            }
            if ( raw_data.IsDefined ( "media" ) )
            {
                this.media = new List<TwitterEntitiesMedia> ();
                foreach ( dynamic t in raw_data.media )
                {
                    this.media.Add ( new TwitterEntitiesMedia ( t ) );
                }
            }
        }

        public TwitterEntities ( string text )
        {
            MatchCollection m = RegexUtil.ParseHashTag ( text );
            if ( m != null && m.Count != 0 )
            {
                this.hashtags = new List<TwitterEntitiesHashTags> ();
                foreach ( Match t in m )
                {
                    this.hashtags.Add ( new TwitterEntitiesHashTags ( t.Value, new int[] { t.Index, t.Index + t.Length - 1 } ) );
                }
            }

            m = RegexUtil.ParseURL ( text );
            if ( m != null && m.Count != 0 )
            {
                this.urls = new List<TwitterEntitiesURLs> ();
                foreach ( Match t in m )
                {
                    bool isThumb = false;
                    string res = ThumURLUtil.getThumbURL ( t.Value, out isThumb );
                    if ( res != null && isThumb )
                    {
                        if ( this.media == null )
                            this.media = new List<TwitterEntitiesMedia> ();
                        this.media.Add ( new TwitterEntitiesMedia ( t.Value, res, new int[] { t.Index, t.Index + t.Length - 1 } ) );
                    }
                    else
                    {
                        this.urls.Add ( new TwitterEntitiesURLs ( t.Value, new int[] { t.Index, t.Index + t.Length - 1 } ) );
                    }
                }
            }

            m = RegexUtil.ParseMention ( text );
            if ( m != null && m.Count != 0 )
            {
                this.user_mentions = new List<TwitterEntitiesUserMentions> ();
                foreach ( Match t in m )
                {
                    this.user_mentions.Add ( new TwitterEntitiesUserMentions ( t.Value, new int[] { t.Index, t.Index + t.Length - 1 } ) );
                }
            }
        }
        #endregion

        /// <summary>
        /// urls
        /// </summary>
        public List<TwitterEntitiesURLs> urls
        {
            get;
            set;
        }

        /// <summary>
        /// hashtags
        /// </summary>
        public List<TwitterEntitiesHashTags> hashtags
        {
            get;
            set;
        }

        /// <summary>
        /// media
        /// </summary>
        public List<TwitterEntitiesMedia> media
        {
            get;
            set;
        }

        /// <summary>
        /// user_mentions
        /// </summary>
        public List<TwitterEntitiesUserMentions> user_mentions
        {
            get;
            set;
        }
    }
}
