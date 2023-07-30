namespace EShop.Core.ExtensionMethods
{
    public static class EmailEx
    {
        public static string EmailNormalize(this string email)
        {
            return email.Trim().ToLower();
        }
    }
}
