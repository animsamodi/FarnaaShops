using System.Collections.Generic;
using System.IO;
using System.Linq;
using EShop.Core.ViewModels;
using Infrastructure.ExternalApi.ImageServer;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;

namespace EShop.Core.ExternalApi.ImageServer
{
    public class ImageUploadService : IImageUploadService
    {
        
        private readonly MediaConfiguration _mediaConfiguration;

        public ImageUploadService(MediaConfiguration mediaConfiguration)
        {
            _mediaConfiguration = mediaConfiguration;
        }

        public string Upload(IFormFile file,bool compress = true)
        {
          //  imgname = _imageUploadService.Upload(product.ImgName);

            var client = new RestClient($"{_mediaConfiguration.Url}?apikey={_mediaConfiguration.Key}&compress="+compress);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            List<IFormFile> files = new List<IFormFile>
            {
                file
            };
            foreach (var item in files)
            {
                byte[] bytes;
                using (var ms = new MemoryStream())
                {
                    item.CopyToAsync(ms).Wait();
                    bytes = ms.ToArray();
                }
                request.AddFile(item.FileName, bytes, item.FileName, item.ContentType);
            }

            request.AddParameter("compress",  true);



            IRestResponse response = client.ExecuteAsync(request).Result;
            UploadDto  upload = JsonConvert.DeserializeObject<UploadDto>(response.Content);
            return upload.FileNameAddress.FirstOrDefault();

        }
        public List<string> UploadList(List<IFormFile> files, bool compress)
        {
            var client = new RestClient($"{_mediaConfiguration.Url}?apikey={_mediaConfiguration.Key}");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            foreach (var item in files)
            {
                byte[] bytes;
                using (var ms = new MemoryStream())
                {
                    item.CopyToAsync(ms);
                    bytes = ms.ToArray();
                }
                request.AddFile(item.FileName, bytes, item.FileName, item.ContentType);
            }
            request.AddParameter("compress", compress);


            IRestResponse response = client.Execute(request);
            UploadDto  upload = JsonConvert.DeserializeObject<UploadDto>(response.Content);
            return upload.FileNameAddress;

        }
    }
}