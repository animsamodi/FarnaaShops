namespace EShop.Core.ExtensionMethods
{
    public static class PostPrice
    {
        public static int GetPostPrice(this int weight)
        {
            if (weight < 501)
                return 8000;
            if (weight < 1000)
                return 8500;

            return 8000;

        }
    }
}
