using System;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;
using Shrimp.Setting;

namespace Shrimp.Module
{
    /// <summary>
    /// 設定データが格納されたクラスを保存したり読み込みしたりします
    /// </summary>
    class SettingSerializer
    {
        /// <summary>
        /// クラスを保存します
        /// </summary>
        public static void SaveColor()
        {
            try
            {
                ColorOwner settingClass = new ColorOwner();
                settingClass.SaveAll();
                DataContractSerializer ser = new DataContractSerializer(typeof(ColorOwner));
                using (FileStream fs = new FileStream(ShrimpSettings.ColorsDirectory + "/" + settingClass.ColorsData["ProfileName"].profileName + ".shc", FileMode.Create))
                {
                    ser.WriteObject(fs, settingClass);
                }
                settingClass = null;
            }
            catch (Exception e)
            {
                MessageBox.Show("色設定保存エラー:" + e.Message);
            }
        }

        /// <summary>
        /// クラスを読み込みします
        /// </summary>
        /// <returns>読み込んだクラスを返します</returns>
        public static void LoadColor(string file)
        {
            if (File.Exists(file))
            {
                try
                {
                    ColorOwner settingClass = new ColorOwner();
                    DataContractSerializer ser = new DataContractSerializer(typeof(ColorOwner));
                    using (FileStream fs = new FileStream(file, FileMode.Open))
                    {
                        settingClass = (ColorOwner)ser.ReadObject(fs);
                    }
                    settingClass.LoadAll();
                    settingClass = null;
                }
                catch (Exception e)
                {
                    MessageBox.Show("色設定読み込みエラー:" + e.Message);
                }
            }
            else
            {
                return;
            }
            return;
        }

        /// <summary>
        /// クラスを保存します
        /// </summary>
        public static void Save()
        {
            try
            {
                SettingOwner settingClass = new SettingOwner();
                settingClass.SaveAll();
                DataContractSerializer ser = new DataContractSerializer(typeof(SettingOwner));
                using (FileStream fs = new FileStream(ShrimpSettings.SettingPath, FileMode.Create))
                {
                    ser.WriteObject(fs, settingClass);
                }
                settingClass = null;
            }
            catch (Exception e)
            {
                MessageBox.Show("設定保存エラー:" + e.Message);
            }
        }

        /// <summary>
        /// クラスを読み込みします
        /// </summary>
        /// <returns>読み込んだクラスを返します</returns>
        public static void Load()
        {
            if (!Directory.Exists(ShrimpSettings.SettingDirectory))
                Directory.CreateDirectory(ShrimpSettings.SettingDirectory);
            if (!Directory.Exists(ShrimpSettings.ColorsDirectory))
                Directory.CreateDirectory(ShrimpSettings.ColorsDirectory);

            if (File.Exists(ShrimpSettings.SettingPath))
            {
                try
                {
                    SettingOwner settingClass = new SettingOwner();
                    DataContractSerializer ser = new DataContractSerializer(typeof(SettingOwner));
                    using (FileStream fs = new FileStream(ShrimpSettings.SettingPath, FileMode.Open))
                    {
                        settingClass = (SettingOwner)ser.ReadObject(fs);
                    }
                    settingClass.LoadAll();
                    settingClass = null;
                }
                catch (Exception e)
                {
                    MessageBox.Show("設定読み込みエラー:" + e.Message);
                }
            }
            else
            {
                return;
            }
            return;
        }
    }
}
