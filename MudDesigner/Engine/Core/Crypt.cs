using System.Security.Cryptography;
using System.Text;

namespace MudDesigner.Engine.Core
{
    public class Crypt
    {
        public static byte[] GenerateSaltedHash(string password, byte[] salt)
        {
            var plainText = Encoding.UTF8.GetBytes(password);
            HashAlgorithm algorithm = new SHA256Managed();
            var plainTextWithSaltBytes = new byte[plainText.Length+salt.Length];

            for(var i =0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for(var i =0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }
            var hash = algorithm.ComputeHash(plainTextWithSaltBytes);

            return hash;
        }

        public static byte[] GenerateSalt(int length)
        {
            var salt = new byte[length];
            var random = new RNGCryptoServiceProvider();
            random.GetNonZeroBytes(salt);

            return salt;
        }

        public static bool CrypCompare(byte[] array1, byte[] array2)
        {
            const byte zero = 0;
            var maxLength = array1.Length > array2.Length ? array1.Length : array2.Length;
            var wereEqual = array1.Length == array2.Length;

            var paddedArray1 = new byte[maxLength];
            var paddedArray2 = new byte[maxLength];
            for(var i = 0; i < maxLength; i++)
            {
                paddedArray1[i] = array1.Length > i ? array1[i] : zero;
                paddedArray2[i] = array2.Length > i ? array2[i] : zero;
            }

            var compareResult = true;
            for(var i = 0; i < maxLength; i++)
            {
                compareResult = compareResult & paddedArray1[i] == paddedArray2[i];
            }

            return compareResult & wereEqual;
        }
    }
}