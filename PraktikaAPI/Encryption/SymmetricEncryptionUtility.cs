using PraktikaAPI.Properties;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace PraktikaAPI.Encryption
{
    public static class SymmetricEncryptionUtility
    {
        public static void GenerateKey()
        {
            // Создать алгоритм
            SymmetricAlgorithm? algorithm = SymmetricAlgorithm.Create("DES");
            algorithm.GenerateKey();

            byte[] key = algorithm.Key;

            key = ProtectedData.Protect(key, null, DataProtectionScope.LocalMachine);

            using (FileStream fs = new FileStream(Properties.Resources.key, FileMode.Create))
            {
                fs.Write(key, 0, key.Length);
            }
        }

        public static void ReadKey(SymmetricAlgorithm algorithm)
        {
            byte[] key = new byte[Resources.key.Length];

            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames()
              .Single(str => str.EndsWith("key.config"));

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                stream.Read(key, 0, (int)Properties.Resources.key.Length);
            }

            algorithm.Key = ProtectedData.Unprotect(key, null, DataProtectionScope.LocalMachine);
        }

        public static byte[] EncryptData(string data)
        {
            byte[] clearData = Encoding.UTF8.GetBytes(data);

            SymmetricAlgorithm algorithm = SymmetricAlgorithm.Create("DES");
            ReadKey(algorithm);

            MemoryStream target = new MemoryStream();

            algorithm.GenerateIV();
            target.Write(algorithm.IV, 0, algorithm.IV.Length);

            CryptoStream cs = new CryptoStream(target, algorithm.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(clearData, 0, clearData.Length);
            cs.FlushFinalBlock();

            return target.ToArray();
        }

        public static string DecryptData(byte[] data)
        {
            SymmetricAlgorithm algorithm = SymmetricAlgorithm.Create("DES");
            ReadKey(algorithm);

            MemoryStream target = new MemoryStream();

            int readPos = 0;
            byte[] IV = new byte[algorithm.IV.Length];
            Array.Copy(data, IV, IV.Length);
            algorithm.IV = IV;
            readPos += algorithm.IV.Length;

            CryptoStream cs = new CryptoStream(target, algorithm.CreateDecryptor(),
                CryptoStreamMode.Write);
            cs.Write(data, readPos, data.Length - readPos);
            cs.FlushFinalBlock();

            return Encoding.UTF8.GetString(target.ToArray());
        }
    }
}