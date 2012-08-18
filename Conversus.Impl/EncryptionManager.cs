using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Conversus.Impl
{
    public class EncryptionManager
    {
        private const string content = "image";

        public static string EncryptString(string Message, string Passphrase)
        {
            byte[] Results;
            var UTF8 = new System.Text.UTF8Encoding();

            var HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(content));

            var TDESAlgorithm = new TripleDESCryptoServiceProvider();

            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            byte[] DataToEncrypt = UTF8.GetBytes(Message);

            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }

        public static LicenseType? TestLicense(string key, string company)
        {
            var licValues = Enum.GetValues(typeof (LicenseType));

            foreach (LicenseType licValue in licValues)
            {
                var testString = EncryptString(licValue.ToString() + company, "key");
                if (testString == key) return licValue;   
            }

            return null;
        }
    }

    public enum LicenseType
    {
        Lite = 3,
        Medium = 7,
        Big = 10,
        Large = 20
    }
}
