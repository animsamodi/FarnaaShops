using System;

namespace EShop.Core.Helpers
{
   public static class CodeGenerator
    {
        public static int PhoneActiveCodeGenerator()
        {
            Random rnd = new Random();
            return rnd.Next(10000, 99999);
        } public static string PasswordGenerator()
        {
            Random rnd = new Random();
            return rnd.Next(100000, 999999).ToString();
        }
        public static int CrmOrderCodeGenerator()
        {
            Random rnd = new Random();
            return rnd.Next(100000, 999999);
        }

        public static string CreditTrakingCodeCodeGenerator()
        {
            Random rnd = new Random();
            return rnd.Next(1000000, 9999999).ToString();
        }
    }
}
