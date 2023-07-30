using System;
using System.IO;
using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
using Microsoft.AspNetCore.Http;

namespace EShop.Admin.Helper
{
    public static class ImageEx
    {
        #region Base Variable Definition

        private static readonly bool SaveWebP = Convert.ToBoolean(ConfigurationManager.AppSetting["WebP:IsSave"]);
        private static readonly int Quality = Convert.ToInt32(ConfigurationManager.AppSetting["WebP:Quality"]);
 
        #endregion

        public static string SaveImage(this IFormFile imgfile, string filename, string path,bool SaveWebPLocal=true)
        {
            var basePath = path;
            if (String.IsNullOrEmpty(filename))
            {
                filename = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imgfile.FileName);
            }

            path = Path.Combine(Directory.GetCurrentDirectory(), path, filename);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                imgfile.CopyTo(stream);
            }
            if (SaveWebP && SaveWebPLocal)
            {
                string webPFileName = Path.GetFileNameWithoutExtension(filename) + ".webp";
                string webPImagePath = Path.Combine(Directory.GetCurrentDirectory(), basePath, webPFileName);
                using (FileStream webPFileStream = new FileStream(webPImagePath, FileMode.Create))
                {
                    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                    {
                        var bitmapfile = imgfile.OpenReadStream();
                        imageFactory.Load(bitmapfile)
                            .Format(new WebPFormat())
                            .Quality(Quality)
                            .Save(webPFileStream);
                    }
                }
                return webPFileName;
            }

            return filename;

        }
    }
}