using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ExternalApi.ImageServer
{
    public interface IImageUploadService
    {
       string Upload(IFormFile file, bool compress =true);
        List<string> UploadList(List<IFormFile> files, bool compress);
    }
}
