using System;
using System.IO;
using EShop.Admin.Helper;
using EShop.Core.ExtensionMethods;
using EShop.Core.Security;
using Infrastructure.ExternalApi.ImageServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class FileManagerController : BaseAdminController
    {
        private IImageUploadService _imageUploadService;

        public FileManagerController(IImageUploadService imageUploadService)
        {
            _imageUploadService = imageUploadService;
        }


        [HttpPost]
        public ActionResult UploadImageCkEditor(IFormFile upload, string CKEditorFuncNum, string CKEditor,
            string langCode)
        {
            string vImagePath = String.Empty;
            string vMessage = String.Empty;
            string vFilePath = String.Empty;
            string vOutput = String.Empty;
            try
            {
                 
                if (upload != null)
                {
                    var vFileName = DateTime.Now.ToString("yyyyMMdd-HHMMssff") +
                                    Path.GetExtension(upload.FileName).ToLower();

                    if (ImageSecurity.Imagevalidator(upload))
                    {
                        //vFileName = upload.SaveImage("", "wwwroot/uploads");
                        vFileName = _imageUploadService.Upload(upload);
                        
                        vMessage = "تصویر با موفقیت ذخیره شد";

                    }    
                    vImagePath = Url.Content("https://media.farnaa.com/media/" + vFileName);

                }
            }
            catch
            {
                vMessage = "There was an issue uploading";
            }
            vOutput = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + vImagePath + "\", \"" + vMessage + "\");</script></body></html>";
            return Content(vOutput);
        }
   

        [IgnoreAntiforgeryToken]
        public IActionResult ImageUpload(IFormFile upload)
        {
            string imgname = "";
            if (upload != null)
            {
                if (ImageSecurity.Imagevalidator(upload))
                {
                    //imgname = upload.SaveImage("", "wwwroot/uploads");
                    imgname = _imageUploadService.Upload(upload);

                }
            }

            return Json(new { uploaded = true, url = "https://media.farnaa.com/media/" + imgname });
        }
        [IgnoreAntiforgeryToken]
        [HttpPost]
        [Route("file-upload")]
        public IActionResult ImageUpload2(IFormFile upload)
        {
            string imgname = "";
            if (upload != null)
            {
                if (ImageSecurity.Imagevalidator(upload))
                {
                    //imgname = upload.SaveImage("", "wwwroot/uploads");
                    imgname = _imageUploadService.Upload(upload);
                    ;
                }
            }

            return Json(new { uploaded = true, url = "https://media.farnaa.com/media/" + imgname });
        }

        #region Ckeditor

        public IActionResult CKEditorFileManager()
        {
            return View();
        }

        public IActionResult CkEditorImageViewer(string directory)
        {
            ViewBag.directory = directory;
            return View();
        }

        [HttpPost]
        public IActionResult CKEditorDeleteImage(string directory, string name)
        {
            name.DeleteImage("wwwroot/uploads/" + directory);
            ViewBag.directory = directory;
            return View("CkEditorImageViewer");
        }

        [HttpPost]
        public IActionResult CKEditorCreateFolder(string directory, string foldername)
        {
            ("https://media.farnaa.com/media" + directory + "/" + foldername).CreateDirectory();
            ViewBag.directory = directory;
            return View("CkEditorImageViewer");
        }
        [HttpPost]
        public IActionResult CKEditorDeleteFolder(string directory, string foldername)
        {
            ("https://media.farnaa.com/media" + directory + "/" + foldername).DeleteDirectory();
            ViewBag.directory = directory;
            return View("CkEditorImageViewer");
        }
        public IActionResult CKeditorImageUpload(string directory, IFormFile upload)
        {

            if (upload != null)
            {
                if (ImageSecurity.Imagevalidator(upload))
                {
                    //upload.SaveImage("", "wwwroot/uploads" + directory);
                    _imageUploadService.Upload(upload);
                }
            }

            ViewBag.directory = directory;
            return View("CkEditorImageViewer");
        }
        #endregion

        #region Main

        public IActionResult MainFileManager(string directory = "")
        {
            ViewBag.directory = directory;
            return View();
        }

        public IActionResult MainImageViewer(string directory = "")
        {
            ViewBag.directory = directory;
            return View();
        }

        [HttpPost]
        public IActionResult MainDeleteImage(string directory, string name)
        {
            name.DeleteImage("wwwroot/uploads/" + directory);
            ViewBag.directory = directory;
            return View("MainImageViewer");
        }

        [HttpPost]
        public IActionResult MainCreateFolder(string directory, string foldername)
        {
            ("https://media.farnaa.com/media" + directory + "/" + foldername).CreateDirectory();
            ViewBag.directory = directory;
            return View("MainImageViewer");
        }
        [HttpPost]
        public IActionResult MainDeleteFolder(string directory, string foldername)
        {
            ("https://media.farnaa.com/media" + directory + "/" + foldername).DeleteDirectory();
            ViewBag.directory = directory;
            return View("MainImageViewer");
        }
        public IActionResult MainImageUpload(string directory, IFormFile upload)
        {

            if (upload != null)
            {
                if (ImageSecurity.Imagevalidator(upload))
                {
                   // upload.SaveImage("", "wwwroot/uploads" + directory);
                    _imageUploadService.Upload(upload);

                }
            }

            ViewBag.directory = directory;
            return View("MainImageViewer");
        }
        #endregion
    }
}