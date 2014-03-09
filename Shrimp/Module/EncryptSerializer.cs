using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Security.Cryptography;
using System.IO;

namespace Shrimp.Module
{
    class EncryptSerializer
    {
        private static string saltString = "unowenwasher?";
        private static string pwString = "|wvwl|}{jahllpqk~qt}";

        static EncryptSerializer()
        {
            //  静的コンストラクタ
            string tmp = "";
            for (int i = 0; i < pwString.Length; i++)
            {
                tmp += (char)((int)pwString[i] ^ 24);
            }
            pwString = tmp;
        }

        /// <summary>
        /// パスワードから共有キーと初期化ベクタを生成する
        /// </summary>
        /// <param name="password">基になるパスワード</param>
        /// <param name="keySize">共有キーのサイズ（ビット）</param>
        /// <param name="key">作成された共有キー</param>
        /// <param name="blockSize">初期化ベクタのサイズ（ビット）</param>
        /// <param name="iv">作成された初期化ベクタ</param>
        private static void GenerateKeyFromPassword (
            int keySize, out byte[] key, int blockSize, out byte[] iv )
        {
            //パスワードから共有キーと初期化ベクタを作成する
            //saltを決める
            byte[] salt = System.Text.Encoding.UTF8.GetBytes(saltString);
            //Rfc2898DeriveBytesオブジェクトを作成する
            System.Security.Cryptography.Rfc2898DeriveBytes deriveBytes =
                new System.Security.Cryptography.Rfc2898DeriveBytes ( pwString, salt );
            //反復処理回数を指定する デフォルトで1000回
            deriveBytes.IterationCount = 1000;

            //共有キーと初期化ベクタを生成する
            key = deriveBytes.GetBytes ( keySize / 8 );
            iv = deriveBytes.GetBytes ( blockSize / 8 );
        }

        private static char[] GenerateSalt()
        {
            Random rnd = new Random();
            char[] destChar = new char[8];
            for (int i = 0; i < destChar.Length; i++)
            {
                destChar[i] = (char)rnd.Next(0, 255);
            }
            return destChar;
        }

        /// <summary>
        /// データセットを暗号化する
        /// </summary>
        /// <param name="filePath">保存するファイルパス</param>
        /// <param name="ds">暗号化対象データセット</param>
        /// <param name="key">暗号キー</param>
        /// <param name="IV">初期化ベクタ</param>
        public static void Encrypt ( string filePath, Type type, object ds )
        {
            XmlSerializer ser = new XmlSerializer ( type );
            using ( SymmetricAlgorithm sa = new RijndaelManaged () )
            {
                byte[] key, IV;
                GenerateKeyFromPassword ( sa.KeySize, out key, sa.BlockSize, out IV );
                ICryptoTransform encryptor = sa.CreateEncryptor ( key, IV );
                using ( FileStream msEncrypt = new FileStream ( filePath, FileMode.Create ) )
                {
                    using ( CryptoStream csEncrypt = new CryptoStream ( msEncrypt, encryptor, CryptoStreamMode.Write ) )
                    {
                        ser.Serialize ( csEncrypt, ds );
                    }
                }
            }
        }

        /// <summary>
        /// データセットを復号する
        /// </summary>
        /// <param name="filePath">復号するファイルパス</param>
        /// <param name="key">暗号キー</param>
        /// <param name="IV">初期化ベクタ</param>
        /// <returns>復号したデータセット</returns>
        public static object Decrypt ( string filePath, Type type )
        {
            object decrypted = null;
            XmlSerializer ser = new XmlSerializer(type);
            using (SymmetricAlgorithm sa = new RijndaelManaged())
            {
                byte[] key, IV;
                GenerateKeyFromPassword (sa.KeySize, out key, sa.BlockSize, out IV );
                ICryptoTransform decryptor = sa.CreateDecryptor(key, IV);
                using (FileStream msDecrypt = new FileStream(filePath, FileMode.Open))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        decrypted = ser.Deserialize(csDecrypt);
                    }
                }
            }
            return decrypted;
        }
    }
}
