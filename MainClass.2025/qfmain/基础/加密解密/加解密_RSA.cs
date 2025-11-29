using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace qfmain
{
    /// <summary>
    /// 非对称加密,安全性高,但效率低
    /// </summary>
    public class 加解密_RSA
    {

        // 生成公钥和私钥（XML格式）
        public (string 公钥, string 私钥) GenerateKeys()
        {
            using (RSA rsa = RSA.Create())
            {
                return (rsa.ToXmlString(false), rsa.ToXmlString(true));
            }
        }

        // 公钥加密
        public virtual string 公钥加密(string plainText, string 公钥)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.FromXmlString(公钥);
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] encryptedBytes = rsa.Encrypt(plainBytes, RSAEncryptionPadding.OaepSHA256);
                return Convert.ToBase64String(encryptedBytes);
            }
        }

        // 私钥解密
        public virtual string 私钥解密(string cipherText, string 私钥)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.FromXmlString(私钥);
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                byte[] decryptedBytes = rsa.Decrypt(cipherBytes, RSAEncryptionPadding.OaepSHA256);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}
