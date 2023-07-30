namespace EShop.Core.ExtensionMethods
{
   public static class RatingEX
    {
        public static string GetRatingRange(this int value)
        {
            if (value > 75)
                return "عالی";

            if (value > 50)
                return "خوب";

            if (value > 25)
                return "معمولی";

            return "بد";
                
        }

        public static string[] GetEvaluation(this string value)
        {
            if (value != null)
            {
                string[] str = value.Split("=");
                return str;
            }

            return new string[] { };
        }
    }
}
