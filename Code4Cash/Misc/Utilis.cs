using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using Code4Cash.Data.Models.Entities.Base;
using Code4Cash.Data.Models.ViewModels.Base;
using Code4Cash.Misc.Attributes;

namespace Code4Cash.Misc
{
    public static class Utilis
    {
        public static bool IsValidSelector(string selector)
        {
            Guid tmp;
            return Guid.TryParse(selector, out tmp);
        }


        //http://stackoverflow.com/questions/11454004/calculate-a-md5-hash-from-a-string
        public static string CalculateMd5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                var inputBytes = Encoding.ASCII.GetBytes(input);
                var hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                var sb = new StringBuilder();
                foreach (var t in hashBytes)
                {
                    sb.Append(t.ToString("X2"));
                }
                return sb.ToString().ToLower();
            }
        }

        public static string GenerateRandomString(int length = 20)
        {
            var str = "";
            while (str.Length < length)
            {
                str += Guid.NewGuid().ToString().ToLower().Replace("-", "");
            }
            return str.Substring(0, length);
        }
    }
}