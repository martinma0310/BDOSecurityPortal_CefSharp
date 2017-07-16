using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;


namespace BDOSecurityPortalUtil
{
    public class EncryptBiz
    {
        private string _defaultKey = DateTime.Now.ToString("yyyyMMdd");
        //默认密钥向量
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public string EncryptDES(string encryptString, string encryptKey, bool defaultKey)
        {
            try
            {
                //对称算法实现的必须继承的抽象类
                SymmetricAlgorithm sa;
                //加密转化运算
                ICryptoTransform ct;
                //IO内存流
                MemoryStream ms;
                CryptoStream cs;
                byte[] byt;

                //sa.Mode = CipherMode.CBC;

                sa = new DESCryptoServiceProvider();
                sa.KeySize = 64;
                sa.Padding = PaddingMode.PKCS7;
                sa.Mode = CipherMode.ECB;
                if (defaultKey)
                {
                    sa.Key = ConvertStringToByteArray(_defaultKey);
                    sa.IV = ConvertStringToByteArray(_defaultKey);
                }
                else
                {
                    sa.Key = ConvertStringToByteArray(encryptKey);
                    sa.IV = ConvertStringToByteArray(encryptKey);
                }
                ct = sa.CreateEncryptor();

                byt = ConvertStringToByteArray(encryptString);

                ms = new MemoryStream();
                cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                cs.Close();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception e)
            {
                return "";
            }

            //try
            //{
            //    byte[] rgbKey;
            //    if (defaultKey)
            //    {
            //        rgbKey = Encoding.UTF8.GetBytes(_defaultKey);
            //    }
            //    else
            //    {
            //        rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
            //    }
            //    byte[] rgbIV = Keys;
            //    byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            //    DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
            //    MemoryStream mStream = new MemoryStream();
            //    CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            //    cStream.Write(inputByteArray, 0, inputByteArray.Length);
            //    cStream.FlushFinalBlock();
            //    return Convert.ToBase64String(mStream.ToArray());
            //}
            //catch (Exception ex)
            //{
            //    return encryptString;
            //}
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public string DecryptDES(string decryptString, string decryptKey, bool defaultKey)
        {
            try
            {
                //对称算法实现的必须继承的抽象类
                SymmetricAlgorithm sa;
                //IO内存流
                MemoryStream ms;
                //
                CryptoStream cs;

                byte[] byt;

                sa = new DESCryptoServiceProvider();
                //设置对称算法密钥的长度大小，64位
                sa.KeySize = 64;
                //填充模式，自动增加长度大小
                sa.Padding = PaddingMode.PKCS7;
                //运算模式
                sa.Mode = CipherMode.ECB;
                if (defaultKey)
                {
                    //设置密钥
                    sa.Key = this.ConvertStringToByteArray(_defaultKey);
                    //设置对称算法的初始向量
                    sa.IV = this.ConvertStringToByteArray(_defaultKey);
                }
                else
                {
                    //设置密钥
                    sa.Key = this.ConvertStringToByteArray(decryptKey);
                    //设置对称算法的初始向量
                    sa.IV = this.ConvertStringToByteArray(decryptKey);
                }
                ICryptoTransform desdecrypt = sa.CreateDecryptor();
                byt = Convert.FromBase64String(decryptString);

                ms = new MemoryStream(byt, 0, byt.Length);
                cs = new CryptoStream(ms, desdecrypt, CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            catch (Exception e)
            {
                return "";
            }
            //try
            //{
            //    byte[] rgbKey;
            //    if (defaultKey)
            //    {
            //        rgbKey = Encoding.UTF8.GetBytes(_defaultKey);
            //    }
            //    else
            //    {
            //        rgbKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 8));
            //    }
            //    byte[] rgbIV = Keys;
            //    byte[] inputByteArray = Convert.FromBase64String(decryptString);
            //    DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
            //    MemoryStream mStream = new MemoryStream();
            //    CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            //    cStream.Write(inputByteArray, 0, inputByteArray.Length);
            //    cStream.FlushFinalBlock();
            //    return Encoding.UTF8.GetString(mStream.ToArray());
            //}
            //catch(Exception ex)
            //{
            //    return decryptString;
            //}
        }

        private Byte[] ConvertStringToByteArray(String s)
        {
            return (new UTF8Encoding()).GetBytes(s);
        }

      
    }
}
