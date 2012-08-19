using System;
using System.Security.Cryptography;
using System.Text;

namespace Conversus.Impl
{
    public enum LicenseType
    {
        Lite = 3,
        Medium = 7,
        Big = 10,
        Large = 20
    }

    public class EncryptionManager
    {
        private const string Key = "image";

        public static string GetEncryptedKey(string companyName, LicenseType licenseType)
        {
            return EncryptString(licenseType.ToString() + companyName);
        }

        public static LicenseType? TestLicense(string key, string company)
        {
            var licValues = Enum.GetValues(typeof(LicenseType));

            foreach (LicenseType licValue in licValues)
            {
                var testString = GetEncryptedKey(company, licValue);
                if (testString == key)
                    return licValue;
            }

            return null;
        }

        private static string EncryptString(string stringToEncrypt)
        {
            byte[] results;
            var utf8 = new UTF8Encoding();
            byte[] tdesKey;
            
            using (var hashProvider = new MD5CryptoServiceProvider())
            {
                tdesKey = hashProvider.ComputeHash(utf8.GetBytes(Key));
            }

            using (var tdesAlgorithm = new TripleDESCryptoServiceProvider())
            {
                tdesAlgorithm.Key = tdesKey;
                tdesAlgorithm.Mode = CipherMode.ECB;
                tdesAlgorithm.Padding = PaddingMode.PKCS7;

                byte[] dataToEncrypt = utf8.GetBytes(stringToEncrypt);

                try
                {
                    ICryptoTransform encryptor = tdesAlgorithm.CreateEncryptor();
                    results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
                }
                finally
                {
                    tdesAlgorithm.Clear();
                }
            }

            return Convert.ToBase64String(results);
        }
    }
}
