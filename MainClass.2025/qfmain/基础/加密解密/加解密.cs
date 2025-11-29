using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace qfmain
{
    public class 加解密
    {


        /// <summary>
        /// 加密数据，采用对称加密的方式
        /// </summary>
        /// <param name="pToEncrypt">待加密的数据</param>
        /// <param name="Password">密钥，长度为8，英文或数字</param>
        /// <returns>加密后的数据</returns>
        public virtual  string 加密_对称式(string pToEncrypt, string Password)
        {
            string aisdhaisdhwdb = Password;
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
            des.Key = Encoding.ASCII.GetBytes(aisdhaisdhwdb);
            des.IV = Encoding.ASCII.GetBytes(aisdhaisdhwdb);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }


        /// <summary>
        /// 解密过程，使用的是对称的加密
        /// </summary>
        /// <param name="pToDecrypt">等待解密的字符</param>
        /// <param name="password">密钥，长度为8，英文或数字</param>
        /// <returns>返回原密码，如果解密失败，返回‘解密失败’</returns>
        public virtual string 解密_对称式(string pToDecrypt, string password)
        {
            if (pToDecrypt == "") return pToDecrypt;
            string zxcawrafdgegasd = password;
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            des.Key = Encoding.ASCII.GetBytes(zxcawrafdgegasd);
            des.IV = Encoding.ASCII.GetBytes(zxcawrafdgegasd);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            cs.Dispose();
            return Encoding.Default.GetString(ms.ToArray());
        }



        #region 文本加解密_自定义,简单的



        /// <summary>
        /// 加密字符串,密钥必须由字符串组成
        /// </summary>
        /// <param name="word">被加密字符串</param>
        /// <param name="key">密钥必须由字符串组成</param>
        /// <returns>加密后字符串</returns>
        public virtual string 文本加密(string word, string key)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(key, "^[a-zA-Z]*$"))
            {
                throw new Exception("key 必须由字母组成");
            }

            key = key.ToLower();

            //逐个字符加密字符串
            char[] s = word.ToCharArray();
            for (int i = 0; i < s.Length; i++)
            {
                char a = word[i];
                char b = key[i % key.Length];
                s[i] = EncryptChar(a, b);
            }

            return new string(s);

        }

        /// <summary>
        /// 加密单个字符
        /// </summary>
        /// <param name="a">被加密字符</param>
        /// <param name="b">密钥</param>
        /// <returns>加密后字符</returns>
        private  char EncryptChar(char a, char b)
        {
            int c = a + b - 'a';

            if (a >= '0' && a <= '9') //字符0-9的转换
            {
                while (c > '9') c -= 10;
            }
            else if (a >= 'a' && a <= 'z') //字符a-z的转换
            {
                while (c > 'z') c -= 26;
            }
            else if (a >= 'A' && a <= 'Z') //字符A-Z的转换
            {
                while (c > 'Z') c -= 26;
            }

            else return a; //不再上面的范围内，不转换直接返回

            return (char)c; //返回转换后的字符
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="word">被解密字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>解密后字符串</returns>
        public virtual string 文本解密(string word, string key)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(key, "^[a-zA-Z]*$"))
            {
                throw new Exception("key 必须由字母组成");
            }

            key = key.ToLower();

            //逐个字符解密字符串
            char[] s = word.ToCharArray();
            for (int i = 0; i < s.Length; i++)
            {
                char a = word[i];
                char b = key[i % key.Length];
                s[i] = DecryptChar(a, b);
            }

            return new string(s);
        }

        /// <summary>
        /// 解密单个字符
        /// </summary>
        /// <param name="a">被解密字符</param>
        /// <param name="b">密钥</param>
        /// <returns>解密后字符</returns>
        private char DecryptChar(char a, char b)
        {
            int c = a - b + 'a';

            if (a >= '0' && a <= '9') //字符0-9的转换
            {
                while (c < '0') c += 10;
            }
            else if (a >= 'a' && a <= 'z') //字符a-z的转换
            {
                while (c < 'a') c += 26;
            }
            else if (a >= 'A' && a <= 'Z') //字符A-Z的转换
            {
                while (c < 'A') c += 26;
            }
            else return a; //不再上面的范围内，不转换直接返回

            return (char)c; //返回转换后的字符
        }


        #endregion



        /// <summary>
        /// 使用DES加密指定字符串
        /// </summary>
        /// <param name="encryptStr">待加密的字符串</param>
        /// <param name="key">密钥(最大长度8)</param>
        /// <param name="IV">初始化向量(最大长度8)</param>
        /// <returns>加密后的字符串</returns>
        public virtual string 文本加密_1(string encryptStr, string key, string IV)
        {
            //将key和IV处理成8个字符
            key += "12345678";
            IV += "12345678";
            key = key.Substring(0, 8);
            IV = IV.Substring(0, 8);
            SymmetricAlgorithm sa;
            ICryptoTransform ict;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;
            sa = new DESCryptoServiceProvider();
            sa.Key = Encoding.UTF8.GetBytes(key);
            sa.IV = Encoding.UTF8.GetBytes(IV);
            ict = sa.CreateEncryptor();
            byt = Encoding.UTF8.GetBytes(encryptStr);
            ms = new MemoryStream();
            cs = new CryptoStream(ms, ict, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            //加上一些干扰字符
            string retVal = Convert.ToBase64String(ms.ToArray());
            System.Random ra = new Random();
            for (int i = 0; i < 8; i++)
            {
                int radNum = ra.Next(36);
                char radChr = Convert.ToChar(radNum + 65);//生成一个随机字符
                retVal = retVal.Substring(0, 2 * i + 1) + radChr.ToString() + retVal.Substring(2 * i + 1);
            }
            return retVal;
        }

        /// <summary>
        /// 使用DES解密指定字符串
        /// </summary>
        /// <param name="encryptedValue">待解密的字符串</param>
        /// <param name="key">密钥(最大长度8)</param>
        /// <param name="IV">初始化向量(最大长度8)</param>
        /// <returns>解密后的字符串</returns>
        public virtual string 文本解密_1(string encryptedValue, string key, string IV)
        {
            //去掉干扰字符
            string tmp = encryptedValue;
            if (tmp.Length < 16)
            {
                return "";
            }
            for (int i = 0; i < 8; i++)
            {
                tmp = tmp.Substring(0, i + 1) + tmp.Substring(i + 2);
            }
            encryptedValue = tmp;
            //将key和IV处理成8个字符
            key += "12345678";
            IV += "12345678";
            key = key.Substring(0, 8);
            IV = IV.Substring(0, 8);
            SymmetricAlgorithm sa;
            ICryptoTransform ict;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;
            try
            {
                sa = new DESCryptoServiceProvider();
                sa.Key = Encoding.UTF8.GetBytes(key);
                sa.IV = Encoding.UTF8.GetBytes(IV);
                ict = sa.CreateDecryptor();
                byt = Convert.FromBase64String(encryptedValue);
                ms = new MemoryStream();
                cs = new CryptoStream(ms, ict, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                cs.Close();
                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (System.Exception)
            {
                return "";
            }
        }

        public virtual string 文本加密_简短(string pwd)
        {
            char[] arrChar = pwd.ToCharArray();
            string strChar = "";
            for (int i = 0; i < arrChar.Length; i++)
            {
                arrChar[i] = Convert.ToChar(arrChar[i] + 3);
                strChar = strChar + arrChar[i].ToString();
            }
            return strChar;
        }

        public virtual string 文本解密_简短(string pwd)
        {
            char[] arrChar = pwd.ToCharArray();
            string strChar = "";
            for (int i = 0; i < arrChar.Length; i++)
            {
                arrChar[i] = Convert.ToChar(arrChar[i] - 3);
                strChar = strChar + arrChar[i].ToString();
            }
            return strChar;
        }





        #region md5










        /// <summary>
        /// MD5加密字符串
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns>md5加密结果</returns>
        public virtual string MD5_加密字符串(string input)
        { // input = "123456z";
            var md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] result = md5.ComputeHash(data);
            String ret = ""; ;
            for (int i = 0; i < result.Length; i++)
                ret += result[i].ToString("X2");
            return ret;
        }

        /// <summary>
        /// MD5加密byte
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>md5加密结果</returns>
        public virtual string MD5_加密byte(byte[] bytes)
        {
            var md5 = new MD5CryptoServiceProvider();
            byte[] hash = md5.ComputeHash(bytes, 0, bytes.Length);
            StringBuilder sb = new StringBuilder();
            foreach (byte value in hash)
            {
                sb.AppendFormat("{0:x2}", value);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 读取文件MD5值
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>MD5</returns>
        public virtual string FileMd5_读取文件MD5值(string filePath)
        {
            var cfilePath = filePath + "e";
            if (File.Exists(cfilePath))
                File.Delete(cfilePath);
            File.Copy(filePath, cfilePath);//复制一份，防止占用


            if (File.Exists(cfilePath))
            {
                var buffer = File.ReadAllBytes(cfilePath);
                System.IO.File.Delete(cfilePath);
                return MD5_加密byte(buffer);
            }
            else
            {
                throw new Exception("读取文件MD5出错!");
            }
        }






        public virtual string MD5_加密16位(string Str)
        {
            var md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(Str)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }






        public virtual string MD5_加密32位(string Str, string psd)
        {


            string cl = Str;
            MD5 md5 = MD5.Create(); //实例化一个md5对像
                                    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                psd = psd + s[i].ToString("X");
            }
            return psd;
        }









        public virtual string MD5_加密64位(string Str)
        {
            string cl = Str;
            //string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
                                    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            return Convert.ToBase64String(s);
        }








        #endregion MD5


        #region 文本 简单 加密 (跟精易模块一致)
        /// <summary>
        /// 文本_加密 简单加密
        /// </summary>
        /// <param name="str">待加密的文本</param>
        /// <param name="pass">加密的密码</param>
        /// <returns></returns>
        public virtual string 文本加密_精易(string str, string pass, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            byte[] bin = encoding.GetBytes(str);
            List<byte> list = new List<byte>();
            for (int i = 0; i < bin.Length; i++)
            {
                var ch = (byte)(bin[i] ^ 3600);
                list.Add(ch);
            }


            string md5 = MD5_加密字符串(pass).Substring(2, 9);

            string hex =new 进制 (). ByteTo十六进制(list.ToArray());


            return hex + md5.ToUpper();
        }

        /// <summary>
        /// 文本解密 (对应易语言模块)
        /// </summary>
        /// <param name="str">待解密的文本</param>
        /// <param name="pass">解密的密文</param>
        /// <returns></returns>
        public virtual string 文本解密_精易(string str, string pass, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            string md5 = MD5_加密字符串(pass).Substring(2, 9).ToUpper();
            if (!str.EndsWith(md5))
            {
                return "";
            }

            string item = str.Substring(0, str.Length - 9);

            byte[] bin = new 进制 (). 十六进制ToByte(item);
            List<byte> list = new List<byte>();
            for (int i = 0; i < bin.Length; i++)
            {
                var ch = (byte)(bin[i] ^ 3600);
                list.Add(ch);
            }

            string html = encoding.GetString(list.ToArray());

            return html;
        }

        #endregion


        #region Des

        public virtual byte[] Des加密(string input, string key,
                                        CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        {
            if (key.Length > 8) key = key.Substring(0, 8);

            if (key.Length != 8)//必须是8位数的密码 不足我们补全下
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 8 - key.Length; i++)
                {
                    sb.Append("0");
                }

                key = key + sb.ToString();

            }

            try
            {

                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(input);
                    des.Mode = mode;
                    des.Padding = padding;

                    des.Key = ASCIIEncoding.UTF8.GetBytes(key);
                    des.IV = ASCIIEncoding.UTF8.GetBytes(key);
                    using (var ms = new System.IO.MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(bytes, 0, bytes.Length);
                            cs.FlushFinalBlock();
                        }
                        return ms.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new byte[0];
            }

        }


        public virtual byte[] Des解密(byte[] input, string key,
                                        CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
        {

            if (key.Length > 8) key = key.Substring(0, 8);

            if (key.Length != 8)//必须是8位数的密码 不足我们补全下
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 8 - key.Length; i++)
                {
                    sb.Append("0");
                }

                key = key + sb.ToString();

            }


            try
            {


                using (var des = new DESCryptoServiceProvider())
                {
                    des.Mode = mode;
                    des.Padding = padding;
                    des.Key = ASCIIEncoding.UTF8.GetBytes(key);
                    des.IV = ASCIIEncoding.UTF8.GetBytes(key);
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(input, 0, input.Length);
                            cs.FlushFinalBlock();

                            ms.ToArray();
                            return ms.ToArray();
                        }
                    }
                }
            }
            catch
            {
                return new byte[0];
            }
        }

        #endregion Des



        #region sha1


        /// <summary>
        /// SHA1 加密，返回大写字符串
        /// </summary>
        /// <param name="content">需要加密字符串</param>
        /// <returns>返回40位UTF8 大写</returns>
        public virtual string SHA1加密(string content)
        {
            return SHA1加密2(content, Encoding.UTF8);
        }



        /// <summary>
        /// SHA1 加密，返回大写字符串
        /// </summary>
        /// <param name="content">需要加密字符串</param>
        /// <param name="encode">指定加密编码</param>
        /// <returns>返回40位大写字符串</returns>
        public virtual string SHA1加密2(string content, Encoding encode)
        {
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] bytes_in = encode.GetBytes(content);
                byte[] bytes_out = sha1.ComputeHash(bytes_in);
                sha1.Dispose();
                string result = BitConverter.ToString(bytes_out);
                result = result.Replace("-", "");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("SHA1加密出错：" + ex.Message);
            }
        }

        #endregion








        #region Aes

        private readonly string Default_AES_Key = "@#kim123";

        private byte[] Keys =
        { 0x41,
      0x72,
      0x65,
      0x79,
      0x6F,
      0x75,
      0x6D,
      0x79,
      0x53,
      0x6E,
      0x6F,
      0x77,
      0x6D,
      0x61,
      0x6E,
      0x3F
    };

        /// <summary>
        /// 对称加密算法AES RijndaelManaged加密(RijndaelManaged（AES）算法是块式加密算法)
        /// </summary>
        /// <param name="encryptString">待加密字符串</param>
        /// <returns>加密结果字符串</returns>
        public virtual string AES加密(string encryptString, string 密码)
        {
            return AES加密2(encryptString, 密码);
        }

        /// <summary>
        /// 对称加密算法AES RijndaelManaged加密(RijndaelManaged（AES）算法是块式加密算法)
        /// </summary>
        /// <param name="encryptString">待加密字符串</param>
        /// <param name="encryptKey">加密密钥，须半角字符</param>
        /// <returns>加密结果字符串</returns>
        public virtual string AES加密2(string encryptString, string encryptKey, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            encryptKey = GetSubString(encryptKey, 32, "");
            encryptKey = encryptKey.PadRight(32, ' ');
            var rijndaelProvider = new RijndaelManaged
            {
                Key = encoding.GetBytes(encryptKey.Substring(0, 32)),
                IV = Keys
            };
            ICryptoTransform rijndaelEncrypt = rijndaelProvider.CreateEncryptor();
            byte[] inputData = encoding.GetBytes(encryptString);
            byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);
            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// 对称加密算法AES RijndaelManaged解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <returns>解密成功返回解密后的字符串,失败返源串</returns>
        public virtual string AES解密(string decryptString, string 密码)
        {
            return AES解密2(decryptString, 密码);
        }

        /// <summary>
        /// 对称加密算法AES RijndaelManaged解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串,失败返回空</returns>
        public virtual string AES解密2(string decryptString, string decryptKey, Encoding encoding = null)
        {
            try
            {
                if (encoding == null)
                {
                    encoding = Encoding.UTF8;
                }

                decryptKey = GetSubString(decryptKey, 32, "");
                decryptKey = decryptKey.PadRight(32, ' ');
                var rijndaelProvider = new RijndaelManaged()
                {
                    Key = encoding.GetBytes(decryptKey),
                    IV = Keys
                };
                ICryptoTransform rijndaelDecrypt = rijndaelProvider.CreateDecryptor();
                byte[] inputData = Convert.FromBase64String(decryptString);
                byte[] decryptedData = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);
                return encoding.GetString(decryptedData);
            }
            catch
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 按字节长度(按字节,一个汉字为2个字节)取得某字符串的一部分
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="length">所取字符串字节长度</param>
        /// <param name="tailString">附加字符串(当字符串不够长时，尾部所添加的字符串，一般为"...")</param>
        /// <returns>某字符串的一部分</returns>
        private string GetSubString(string sourceString, int length, string tailString)
        {
            return GetSubString(sourceString, 0, length, tailString);
        }

        /// <summary>
        /// 按字节长度(按字节,一个汉字为2个字节)取得某字符串的一部分
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="startIndex">索引位置，以0开始</param>
        /// <param name="length">所取字符串字节长度</param>
        /// <param name="tailString">附加字符串(当字符串不够长时，尾部所添加的字符串，一般为"...")</param>
        /// <returns>某字符串的一部分</returns>
        private string GetSubString(string sourceString, int startIndex, int length, string tailString)
        { //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
            if (System.Text.RegularExpressions.Regex.IsMatch(sourceString, "[\u0800-\u4e00]+") || System.Text.RegularExpressions.Regex.IsMatch(sourceString, "[\xAC00-\xD7A3]+"))
            { //当截取的起始位置超出字段串长度时
                if (startIndex >= sourceString.Length)
                {
                    return string.Empty;
                }

                return sourceString.Substring(startIndex, length + startIndex > sourceString.Length ? (sourceString.Length - startIndex) : length);
            }

            //中文字符，如"中国人民abcd123"
            if (length <= 0)
            {
                return string.Empty;
            }

            byte[] bytesSource = Encoding.Default.GetBytes(sourceString);

            //当字符串长度大于起始位置
            if (bytesSource.Length > startIndex)
            {
                int endIndex = bytesSource.Length;

                //当要截取的长度在字符串的有效长度范围内
                if (bytesSource.Length > (startIndex + length))
                {
                    endIndex = length + startIndex;
                }
                else
                { //当不在有效范围内时,只取到字符串的结尾
                    length = bytesSource.Length - startIndex;
                    tailString = "";
                }

                var anResultFlag = new int[length];
                int nFlag = 0;
                //字节大于127为双字节字符
                for (int i = startIndex; i < endIndex; i++)
                {
                    if (bytesSource[i] > 127)
                    {
                        nFlag++;
                        if (nFlag == 3)
                        {
                            nFlag = 1;
                        }
                    }
                    else
                    {
                        nFlag = 0;
                    }

                    anResultFlag[i] = nFlag;
                }

                //最后一个字节为双字节字符的一半
                if ((bytesSource[endIndex - 1] > 127) && (anResultFlag[length - 1] == 1))
                {
                    length++;
                }

                byte[] bsResult = new byte[length];
                Array.Copy(bytesSource, startIndex, bsResult, 0, length);
                var myResult = Encoding.Default.GetString(bsResult);
                myResult += tailString;
                return myResult;
            }

            return string.Empty;
        }

        /// <summary>
        /// 加密文件流
        /// </summary>
        /// <param name="fs">需要加密的文件流</param>
        /// <param name="decryptKey">加密密钥</param>
        /// <returns>加密流</returns>
        public virtual CryptoStream AES加密文件流(FileStream fs, string decryptKey, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            decryptKey = GetSubString(decryptKey, 32, "");
            decryptKey = decryptKey.PadRight(32, ' ');
            var rijndaelProvider = new RijndaelManaged()
            {
                Key = encoding.GetBytes(decryptKey),
                IV = Keys
            };
            ICryptoTransform encrypto = rijndaelProvider.CreateEncryptor();
            return new CryptoStream(fs, encrypto, CryptoStreamMode.Write);
        }

        /// <summary>
        /// 解密文件流
        /// </summary>
        /// <param name="fs">需要解密的文件流</param>
        /// <param name="decryptKey">解密密钥</param>
        /// <returns>加密流</returns>
        public virtual CryptoStream AES解密文件流(FileStream fs, string decryptKey, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            decryptKey = GetSubString(decryptKey, 32, "");
            decryptKey = decryptKey.PadRight(32, ' ');
            var rijndaelProvider = new RijndaelManaged()
            {
                Key = encoding.GetBytes(decryptKey),
                IV = Keys
            };
            ICryptoTransform decrypto = rijndaelProvider.CreateDecryptor();
            return new CryptoStream(fs, decrypto, CryptoStreamMode.Read);
        }

        /// <summary>
        /// 对指定文件AES加密
        /// </summary>
        /// <param name="input">源文件流</param>
        /// <param name="outputPath">输出文件路径</param>
        public virtual void AES加密流到文件(FileStream input, string outputPath)
        {
            using (FileStream fren = new FileStream(outputPath, FileMode.Create))
            {
                CryptoStream enfr = AES加密文件流(fren, Default_AES_Key);
                byte[] bytearrayinput = new byte[input.Length];
                input.Read(bytearrayinput, 0, bytearrayinput.Length);
                enfr.Write(bytearrayinput, 0, bytearrayinput.Length);
            }
        }

        /// <summary>
        /// 对指定的文件AES解密
        /// </summary>
        /// <param name="input">源文件流</param>
        /// <param name="outputPath">输出文件路径</param>
        public virtual void AES解密流到文件(FileStream input, string outputPath)
        {
            FileStream frde = new FileStream(outputPath, FileMode.Create);
            CryptoStream defr = AES解密文件流(input, Default_AES_Key);
            byte[] bytearrayoutput = new byte[1024];
            while (true)
            {
                var count = defr.Read(bytearrayoutput, 0, bytearrayoutput.Length);
                frde.Write(bytearrayoutput, 0, count);
                if (count < bytearrayoutput.Length)
                {
                    break;
                }
            }
        }


        #endregion


        #region Base64加密解密

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        ///    <param name="encoding"></param>
        /// <returns>加密后的数据</returns>
        public virtual string Base64加密(string str, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            byte[] encbuff = encoding.GetBytes(str);
            return Convert.ToBase64String(encbuff);
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="str">需要解密的字符串</param>
        ///  <param name="encoding"></param>
        /// <returns>解密后的数据</returns>
        public virtual string Base64解密(string str, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            byte[] decbuff = Convert.FromBase64String(str);
            return encoding.GetString(decbuff);
        }

        #endregion



        /// <summary>
        /// SHA256函数
        /// </summary>
        /// <param name="str">原始字符串</param>
        ///  <param name="encoding"></param>
        /// <returns>SHA256结果(16进制字节)</returns>
        public virtual string SHA256(string str, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            byte[] sha256Data = encoding.GetBytes(str);
            var sha256 = new SHA256Managed();
            byte[] result = sha256.ComputeHash(sha256Data);

            return new 进制().ByteTo十六进制(result).ToLower();

            //return Convert.ToBase64String(result); //返回base64
        }




        #region my加解密


        /// <summary>
        /// 只是0~9,A~Z做混合替换
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public virtual char my加密(char str)
        {
            switch (str)
            {
                case '0':
                    return 'A';
                case '1':
                    return 'B';
                case '2':
                    return 'C';
                case '3':
                    return 'D';
                case '4':
                    return 'E';
                case '5':
                    return 'F';
                case '6':
                    return 'G';
                case '7':
                    return 'H';
                case '8':
                    return 'I';
                case '9':
                    return 'J';
                case 'A':
                    return 'K';
                case 'B':
                    return 'L';
                case 'C':
                    return 'M';
                case 'D':
                    return 'N';
                case 'E':
                    return 'O';
                case 'F':
                    return 'P';
                case 'G':
                    return 'Q';
                case 'H':
                    return 'R';
                case 'I':
                    return 'S';
                case 'J':
                    return 'T';
                case 'K':
                    return 'U';
                case 'L':
                    return 'V';
                case 'M':
                    return 'W';
                case 'N':
                    return 'X';
                case 'O':
                    return 'Y';
                case 'P':
                    return 'Z';
                case 'Q':
                    return '0';
                case 'R':
                    return '1';
                case 'S':
                    return '2';
                case 'T':
                    return '3';
                case 'U':
                    return '4';
                case 'V':
                    return '5';
                case 'W':
                    return '6';
                case 'X':
                    return '7';
                case 'Y':
                    return '8';
                case 'Z':
                    return '9';
                default:
                    return ' ';
            }
        }

        /// <summary>
        /// 只是0~9,A~Z做混合替换
        /// </summary>
        /// <param name='str'></param>
        /// <returns></returns>
        public virtual char my解密(char str)
        {
            switch (str)
            {
                case 'A':
                    return '0';
                case 'B':
                    return '1';
                case 'C':
                    return '2';
                case 'D':
                    return '3';
                case 'E':
                    return '4';
                case 'F':
                    return '5';
                case 'G':
                    return '6';
                case 'H':
                    return '7';
                case 'I':
                    return '8';
                case 'J':
                    return '9';
                case 'K':
                    return 'A';
                case 'L':
                    return 'B';
                case 'M':
                    return 'C';
                case 'N':
                    return 'D';
                case 'O':
                    return 'E';
                case 'P':
                    return 'F';
                case 'Q':
                    return 'G';
                case 'R':
                    return 'H';
                case 'S':
                    return 'I';
                case 'T':
                    return 'J';
                case 'U':
                    return 'K';
                case 'V':
                    return 'L';
                case 'W':
                    return 'M';
                case 'X':
                    return 'N';
                case 'Y':
                    return 'O';
                case 'Z':
                    return 'P';
                case '0':
                    return 'Q';
                case '1':
                    return 'R';
                case '2':
                    return 'S';
                case '3':
                    return 'T';
                case '4':
                    return 'U';
                case '5':
                    return 'V';
                case '6':
                    return 'W';
                case '7':
                    return 'X';
                case '8':
                    return 'Y';
                case '9':
                    return 'Z';
                default:
                    return ' ';
            }
        }

        /// <summary>
        /// 只是0~9,A~Z做混合替换
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public virtual string my加密(string str)
        {
            List<char> lst = new List<char>();
            foreach (var s in str)
            {
                char c = my加密(s);
                lst.Add(c);
            }
            return new string(lst.ToArray());
        }


        /// <summary>
        /// 只是0~9,A~Z做混合替换
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public virtual string my解密(string str)
        {
            List<char> lst = new List<char>();
            foreach (var s in str)
            {
                char c = my解密(s);
                lst.Add(c);
            }
            return new string(lst.ToArray());
        }



        #endregion


    }
}
