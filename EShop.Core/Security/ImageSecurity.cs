using Microsoft.AspNetCore.Http;

namespace EShop.Core.Security
{
   public static class ImageSecurity
    {
        public static bool Imagevalidator(IFormFile file)
        {
            if (file.Length > 0 && file != null)
            {
                try
                {
                    System.Drawing.Image.FromStream(file.OpenReadStream());
                    return true;
                }
                catch 
                {
                    return false;
                }
            }
            return false;
        }
    }
}
