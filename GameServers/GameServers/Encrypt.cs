using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace GameServers
{
    class Encrypt
    {
        #region md5加密
        /// <summary>
        /// md5 用于密码加密
        /// </summary>
        /// <param name="strSource">源串</param>
        /// <param name="len">加密长度</param>
        /// <returns></returns>
        public static string MD5Encrypt(string strSource, int len)
        {
            byte[] strBytes = Encoding.ASCII.GetBytes(strSource);
            byte[] hashValues = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(strBytes);
            var stringBuilder = new StringBuilder();
            switch (len)
            {
                case 16:
                    for (int i = 4; i < 16; i++)
                        stringBuilder.Append(hashValues[i].ToString("x2"));
                    break;
                case 32:
                    for (int i = 0; i < 16; i++)
                        stringBuilder.Append(hashValues[i].ToString("x2"));
                    break;
                default:
                    for (int i = 0; i < hashValues.Length; i++)
                        stringBuilder.Append(hashValues[i].ToString("x2"));
                    break;
            }
            return stringBuilder.ToString();
        }
        #endregion md5加密

        #region DES加密
        /// <summary>
        /// DES加密  8位密钥
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public static string DesEncrypt(string strSource)
        {
            return DesEncrypt(strSource, "He...Hzy");
        }

        public static string DesEncrypt(string strSource, string sKey)
        {
            try
            {
                var des = new DESCryptoServiceProvider();
                byte[] byteArray;
                byteArray = Encoding.Default.GetBytes(strSource);
                des.Key = Encoding.ASCII.GetBytes(sKey);
                des.IV = Encoding.ASCII.GetBytes(sKey);
                var ms = new MemoryStream();
                using (var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(byteArray, 0, byteArray.Length);
                    cs.FlushFinalBlock();
                }
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var t in ms.ToArray())
                {
                    stringBuilder.AppendFormat("{0:X2}", t);
                }
                return stringBuilder.ToString();
            }
            catch
            {
                throw new Exception("DesEncrypt Error!");
            }
        }
        #endregion DES加密

        #region DES解密
        public static string DesDecrypt(string text)
        {
            return DesDecrypt(text, "He...Hzy");
        }

        public static string DesDecrypt(string text, string sKey)
        {
            try
            {
                var des = new DESCryptoServiceProvider();
                int len;
                len = text.Length / 2;
                var byteArray = new byte[len];
                int temp = 0;
                for (int i = 0; i < len; i++)
                {
                    temp = Convert.ToInt32(text.Substring(i * 2, 2), 16);
                    byteArray[i] = (byte)temp;
                }
                des.Key = Encoding.ASCII.GetBytes(sKey);
                des.IV = Encoding.ASCII.GetBytes(sKey);
                var ms = new MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(byteArray, 0, byteArray.Length);
                    cs.FlushFinalBlock();
                    return Encoding.Default.GetString(ms.ToArray());
                }
            }
            catch
            {
                throw new Exception("Decrypt Error!");
            }
        }
        #endregion DES解密
    }
}
