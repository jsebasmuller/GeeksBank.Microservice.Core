using System;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace GeeksBank.Core.Domain.Helpers
{
    public static class AESExtension
    {
        public static string _key;

        public static string Encrypt(string plainText)
        {
            var key = Encoding.UTF8.GetBytes(_key);
            var iv = Encoding.UTF8.GetBytes(_key.Substring(0, 16));
            string result = "";
            using (var aes = RijndaelManaged.Create())
            {
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;
                aes.Key = key;
                aes.IV = iv;
                using (var transform = aes.CreateEncryptor())
                {
                    var toEncodeByte = Encoding.UTF8.GetBytes(plainText);
                    var encrypted = transform.TransformFinalBlock(toEncodeByte, 0, toEncodeByte.Length);
                    result = ByteArrayToString(encrypted);
                }
            }
            return result;
        }

        public static string Decrypt(string encryptedHex)
        {
            var key = Encoding.UTF8.GetBytes(_key);
            var iv = Encoding.UTF8.GetBytes(_key.Substring(0, 16));
            string result = "";
            using (var aes = RijndaelManaged.Create())
            {
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;
                aes.Key = key;
                aes.IV = iv;
                using (var transform = aes.CreateDecryptor())
                {
                    var toEncodeByte = StringToByteArray(encryptedHex);
                    var decrypted = transform.TransformFinalBlock(toEncodeByte, 0, toEncodeByte.Length);
                    result = Encoding.UTF8.GetString(decrypted);
                }
            }
            return result;
        }

        public static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static string MD5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }
    }
}