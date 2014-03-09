using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Shrimp.Twitter.Status;

namespace Shrimp.Query
{
    /// <summary>
    /// クエリパーサー
    /// </summary>
    class QueryParser
    {
        private Regex parserRegex;

        public QueryParser ()
        {
            this.parserRegex = new Regex ( @"(?<left>[\w\d\.]*)\s*(?<if>(!=|==|>=|<=|\/=|>|<))\s*(?<right>[\w\d\.]*)", 
                RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }

        public bool isCorrect ( string userRegex )
        {
            return this.parserRegex.IsMatch ( "" );
        }

        private bool ifMatch ( string ifs, object left, object right )
        {
            if ( ifs == "==" )
            {
                string l = left as string;
                string r = right as string;
                return l == r;
            }
            else if ( ifs == "/=" )
            {
                string l = left as string;
                string r = right as string;
                if ( r == null )
                    r = "";
                return l.IndexOf ( r ) >= 0;
            }
            else if ( ifs == "!=" )
            {
                string l = left as string;
                string r = right as string;
                return l != r;
            }
            else if ( ifs == ">=" )
            {
                decimal l = 0, r = 0;
                if ( left is decimal )
                    l = (decimal)left;
                if ( right is decimal )
                    r = (decimal)right;
                return (l >= r);
            }
            else if ( ifs == "<=" )
            {
                decimal l = 0, r = 0;
                if ( left is decimal )
                    l = (decimal)left;
                if ( right is decimal )
                    r = (decimal)right;
                return ( l <= r );
            }
            else if ( ifs == ">" )
            {
                decimal l = 0, r = 0;
                if ( left is decimal )
                    l = (decimal)left;
                if ( right is decimal )
                    r = (decimal)right;
                return ( l > r );
            }
            else if ( ifs == "<" )
            {
                decimal l = 0, r = 0;
                if ( left is decimal )
                    l = (decimal)left;
                if ( right is decimal )
                    r = (decimal)right;
                return ( l < r );
                //return left => right;
            }
            return false;
        }

        private object parseStr ( string str, TwitterStatus status )
        {
            if ( str == "text" )
                return (status.DynamicTweet.text == null ? "" : status.DynamicTweet.text );
            if ( str == "source" )
                return ( status.DynamicTweet.source == null ? "" : status.DynamicTweet.source );
            if ( str == "source_url" )
                return ( status.DynamicTweet.source_url == null ? "" : status.DynamicTweet.source_url );
            if ( str == "user_id" )
                return status.DynamicTweet.user.id;
            if ( str == "id" )
                return status.DynamicTweet.id;
            if ( str == "in_reply_to_status_id" )
                return status.DynamicTweet.in_reply_to_status_id;
            if ( str == "screen_name" )
                return (status.DynamicTweet.user.screen_name == null ? "" : status.DynamicTweet.user.screen_name );
            if ( str == "name" )
                return (status.DynamicTweet.user.name == null ? "" : status.DynamicTweet.user.name );

            decimal test1 = 0;
            if ( decimal.TryParse ( str, out test1 ) )
                return test1;
            return str;
        }


        private bool parseMatch ( Match match, TwitterStatus status )
        {
            if ( match == null || status == null )
                return false;
            if ( match.Success )
            {
                object left = parseStr ( match.Groups["left"].Value, status );
                object right = parseStr ( match.Groups["right"].Value, status );
                return ifMatch ( match.Groups["if"].Value, left, right );
            }
            return false;
        }

        public bool isMatch ( string userRegex, TwitterStatus status )
        {
            bool isOrAndUse = false;
            string[] andParam = userRegex.Split ( '&' );
            if ( andParam.Length > 1 )
            {
                //  条件判定
                for ( int i = 0; i < andParam.Length / 2; i += 2 )
                {
                    bool a = false, b = false;
                    Match match = this.parserRegex.Match ( andParam[i] );
                    if ( !match.Success )
                        return false;
                    a = parseMatch ( match, status );
                    match = this.parserRegex.Match ( andParam[i + 1] );
                    if ( !match.Success )
                        return false;
                    b = parseMatch ( match, status );
                    if ( !( a && b ) )
                        return false;
                }
                isOrAndUse = true;
            }

            string[] orParam = userRegex.Split ( '|' );
            if ( orParam.Length > 1 )
            {
                //  条件判定
                for ( int i = 0; i < orParam.Length / 2; i += 2 )
                {
                    bool a = false, b = false;
                    Match match = this.parserRegex.Match ( orParam[i] );
                    if ( !match.Success )
                        return false;
                    a = parseMatch ( match, status );
                    match = this.parserRegex.Match ( orParam[i + 1] );
                    if ( !match.Success )
                        return false;
                    b = parseMatch ( match, status );
                    if ( !( a || b ) )
                        return false;
                }
                isOrAndUse = true;
            }

            if ( !isOrAndUse )
            {
                Match match2 = this.parserRegex.Match ( userRegex );
                if ( !match2.Success )
                    return false;
                return parseMatch ( match2, status );
            }
            return true;
        }
    }
}
