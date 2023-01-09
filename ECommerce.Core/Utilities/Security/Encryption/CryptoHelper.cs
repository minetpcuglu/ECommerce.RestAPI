
using System;
using System.Security.Cryptography;
using System.Text;

namespace ECommerce.Core.Utilities.Security.Encryption
{
    public class CryptoHelper
    {
        const string PassPhrase = "VE*@RI87BEL61Ya61zı87lı00M*+90";
        public string Encrypt(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return message;
            }
            byte[] results;
            UTF8Encoding utf8 = new UTF8Encoding();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] deskey = md5.ComputeHash(utf8.GetBytes(PassPhrase));
            TripleDESCryptoServiceProvider desalg = new TripleDESCryptoServiceProvider();
            desalg.Key = deskey;
            desalg.Mode = CipherMode.ECB;
            desalg.Padding = PaddingMode.PKCS7;
            byte[] encryptData = utf8.GetBytes(message);

            try
            {
                ICryptoTransform encryptor = desalg.CreateEncryptor();
                results = encryptor.TransformFinalBlock(encryptData, 0, encryptData.Length);

            }
            finally
            {
                desalg.Clear();
                md5.Clear();
            }
            return Convert.ToBase64String(results);

        }

        public string Decrypt(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return message;
            }
            byte[] results;
            UTF8Encoding utf8 = new UTF8Encoding();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] deskey = md5.ComputeHash(utf8.GetBytes(PassPhrase));
            TripleDESCryptoServiceProvider desalg = new TripleDESCryptoServiceProvider();
            desalg.Key = deskey;
            desalg.Mode = CipherMode.ECB;
            desalg.Padding = PaddingMode.PKCS7;
            byte[] decryptData = Convert.FromBase64String(message);
            try
            {
                ICryptoTransform decryptor = desalg.CreateDecryptor();
                results = decryptor.TransformFinalBlock(decryptData, 0, decryptData.Length);
            }
            finally
            {
                desalg.Clear();
                md5.Clear();

            }
            return utf8.GetString(results);
        }
    }
}
