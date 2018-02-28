using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Edenrose.Common
{
    public class Encrypt
    {
        public static string EncryptMD5(string strPass)
        {
            HashAlgorithm hashAlg = HashAlgorithm.Create("MD5");
            byte[] pwdData = Encoding.Default.GetBytes(strPass);
            byte[] hash = hashAlg.ComputeHash(pwdData);
            string result = BitConverter.ToString(hash);
            string[] tg = result.Split(new char[] { '-' });
            result = "";
            for (int i = 0; i < tg.Length; i++) result += tg[i];
            return result;
        }
        private const string KEY = "<random value goes here>-BY-eSport5";
        public static string EncryptAndHash(string value)
        {
            MACTripleDES des = new MACTripleDES();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            des.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(KEY));
            string encrypted = Convert.ToBase64String(des.ComputeHash(Encoding.UTF8.GetBytes(value))) + '-' + Convert.ToBase64String(Encoding.UTF8.GetBytes(value));

            return HttpUtility.UrlEncode(encrypted);
        }

        /// <summary>
        /// Returns null if string has been modified since encryption
        /// </summary>
        /// <param name="encoded"></param>
        /// <returns></returns>
        public static string DecryptWithHash(string encoded)
        {
            MACTripleDES des = new MACTripleDES();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            des.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(KEY));

            string decoded = HttpUtility.UrlDecode(encoded);
            // in the act of url encoding and decoding, plus (valid base64 value) gets replaced with space (invalid base64 value). this reverses that.
            decoded = decoded.Replace(" ", "+");
            string value = Encoding.UTF8.GetString(Convert.FromBase64String(decoded.Split('-')[1]));
            string savedHash = Encoding.UTF8.GetString(Convert.FromBase64String(decoded.Split('-')[0]));
            string calculatedHash = Encoding.UTF8.GetString(des.ComputeHash(Encoding.UTF8.GetBytes(value)));

            if (savedHash != calculatedHash) return null;

            return value;
        }
    }
}
