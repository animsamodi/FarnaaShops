using System.IO;

namespace EShop.Core.ExtensionMethods
{
    public static class DirectoryEx
    {
        public static void CreateDirectory(this string path)
        {
            try
            {
                path = Directory.GetCurrentDirectory() + path;
                if (Directory.Exists(path))
                    return;
                Directory.CreateDirectory(path);
            }
            catch
            {

            }
        }

        public static void DeleteDirectory(this string path)
        {

            path = Directory.GetCurrentDirectory() + path;
            if (Directory.Exists(path))
                Directory.Delete(path,true);


        }
    }
}
