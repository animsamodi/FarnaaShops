
using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
 using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
 
namespace EShop.Core.ExtensionMethods
{
  public static class ImageEx
    {
        public static void DeleteImage(this string filename,string path)
        {
            if (filename != null)
            {
                string imgpath = Path.Combine(Directory.GetCurrentDirectory(), path, filename);
                if (System.IO.File.Exists(imgpath))
                {
                    System.IO.File.Delete(imgpath);
                }
            }
        }

        public static void Image_resize(this string input_Image_Path, string output_Image_Path, int new_width,int new_height)

        {
            const long quality = 50L;
            using (var image = new System.Drawing.Bitmap(System.Drawing.Image.FromFile(input_Image_Path)))
            {

                var resized_Bitmap = new System.Drawing.Bitmap(new_width, new_height);



                using (var graphics = System.Drawing.Graphics.FromImage(resized_Bitmap))

                {

                    graphics.CompositingQuality = CompositingQuality.HighSpeed;

                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    graphics.CompositingMode = CompositingMode.SourceCopy;

                    graphics.DrawImage(image, 0, 0, new_width, new_height);

                    using (var output = System.IO.File.Open(output_Image_Path, FileMode.Create))

                    {

                        var qualityParamId = System.Drawing.Imaging.Encoder.Quality;

                        var encoderParameters = new EncoderParameters(1);

                        encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);

                        var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

                        resized_Bitmap.Save(output, codec, encoderParameters);

                    }

                }

            }

        }

        public static IEnumerable<FileInfo> GetFilesByExtensions(this DirectoryInfo directoryInfo, params string[] extensions)
        {
            var allowedExtensions = new HashSet<string>(extensions, StringComparer.OrdinalIgnoreCase);
            var i = directoryInfo.EnumerateFiles().Where(f => allowedExtensions.Contains(f.Extension));
            return directoryInfo.EnumerateFiles().Where(f => allowedExtensions.Contains(f.Extension));
        }
    }
}
