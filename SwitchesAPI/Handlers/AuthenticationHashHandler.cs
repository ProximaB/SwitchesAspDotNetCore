using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SwitchesAPI.Handlers
{
    public static class AuthenticationHashHandler
    {
        public static string GenerateSaltedHash (string plainText, string salt)
        {
            byte[] plainText_b = Encoding.ASCII.GetBytes(plainText);
            byte[] salt_b = Encoding.ASCII.GetBytes(salt);


            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes =
                new byte[plainText_b.Length + salt_b.Length];

            for ( int i = 0; i < plainText_b.Length; i++ )
            {
                plainTextWithSaltBytes[i] = plainText_b[i];
            }
            for ( int i = 0; i < salt.Length; i++ )
            {
                plainTextWithSaltBytes[plainText_b.Length + i] = salt_b[i];
            }

            return Convert.ToBase64String(algorithm.ComputeHash(plainTextWithSaltBytes));
        }

        public static bool CompareByteArrays (string array1, string array2)
        {

            byte[] array1_b = Encoding.ASCII.GetBytes(array1);
            byte[] array2_b = Encoding.ASCII.GetBytes(array2);

            if ( array1.Length != array2.Length )
            {
                return false;
            }

            for ( int i = 0; i < array1.Length; i++ )
            {
                if ( array1[i] != array2[i] )
                {
                    return false;
                }
            }

            return true;
        }
    }
}
