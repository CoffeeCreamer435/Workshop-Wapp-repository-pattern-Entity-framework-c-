`using System;

// Laad encryptie libs
using System.Security.Cryptography;
using System.Text;
using BCrypt;

namespace Encryptie_methode_ASP_Core
{
    class Program
    {

        static void Main(string[] args)
        {
            String password = "wachtwoord123";
            String salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            // String salt = CreateSalt(10);

            Console.WriteLine("#----- SHA 512 -----#");
            Console.WriteLine(password + " " + GenerateSHA512String(password));
            Console.WriteLine(password + " " + GenerateSHA512String(password));
            Console.WriteLine(password + " " + salt + " " + GenerateSHA512String(password + salt));

            Console.WriteLine("\n#----- BCrypt -----#");
            Console.WriteLine(password + " " + BCrypt.Net.BCrypt.HashPassword(password));
            Console.WriteLine(password + " " + BCrypt.Net.BCrypt.HashPassword(password));
            Console.WriteLine(password + " " + salt + " " + BCrypt.Net.BCrypt.HashPassword(password));

            Console.WriteLine("\n#----- Enhanced BCrypt -----#");
            Console.WriteLine(password + " " + BCrypt.Net.BCrypt.EnhancedHashPassword(password));
            Console.WriteLine(password + " " + BCrypt.Net.BCrypt.EnhancedHashPassword(password));
            Console.WriteLine(password + " " + salt + " " + BCrypt.Net.BCrypt.EnhancedHashPassword(password));

            Console.WriteLine("\n#----- BCrypt validation -----#");
            Console.WriteLine("Password klopt: " + BCrypt.Net.BCrypt.Verify(password, BCrypt.Net.BCrypt.HashPassword(password)));
        }

        public static string GenerateSHA512String(string inputString)
        {
            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }
       
        public static String CreateSalt(int size)
        {
            var random = RandomNumberGenerator.Create();
            var buffer = new byte[size];
            random.GetBytes(buffer);
            return Convert.ToBase64String(buffer);
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}`
