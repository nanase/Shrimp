using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Shrimp.Setting
{
    class ShrimpSettings
    {
        private static string DefaultPath;
        static ShrimpSettings ()
        {
            DefaultPath = Path.GetDirectoryName ( Assembly.GetExecutingAssembly ().Location );
        }

        /// <summary>
        /// 設定が保存されるディレクトリ名
        /// </summary>
        public static string SettingDirectory
        {
            get
            {
                return DefaultPath + "\\Setting";
            }
        }

        /// <summary>
        /// 色設定が保存されるディレクトリ名
        /// </summary>
        public static string ColorsDirectory
        {
            get
            {
                return SettingDirectory + "\\Colors";
            }
        }

        /// <summary>
        /// プラグインディレクトリ
        /// </summary>
        public static string PluginDirectory
        {
            get
            {
                return DefaultPath + "\\Plugins";
            }
        }

        /// <summary>
        /// タブの設定パス
        /// </summary>
        public static string TabSettingPath
        {
            get
            {
                return SettingDirectory + "\\tab";
            }
        }

        /// <summary>
        /// 設定のパス
        /// </summary>
        public static string SettingPath
        {
            get
            {
                return SettingDirectory + "\\setting";
            }
        }

        /// <summary>
        /// アカウントパス
        /// </summary>
        public static string AccountPath
        {
            get
            {
                return SettingDirectory + "\\account";
            }
        }

        /// <summary>
        /// データベースパス
        /// </summary>
        public static string DatabasePath
        {
            get
            {
                return SettingDirectory + "\\database.db";
            }
        }
    }
}
