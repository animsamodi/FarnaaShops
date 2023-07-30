using EShop.Admin.Helper;
using EShop.Core.Security;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Entities.Address;
using EShop.DataLayer.Entities.Site;
using EShop.DataLayer.Enum;
using EShop.Logging.AuditLog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Admin.Controllers
{
    //////////[Area("Admin")]
    public class SettingController : BaseAdminController
    {
        private IStaticPageService _staticPageService;
        private IAddressService _addressService;

        public SettingController(IStaticPageService staticPageService, IAddressService addressService
            , Logging.AuditLog.IAuditService logger, IHttpContextAccessor contextAccessor) : base(logger, contextAccessor)
        {
            _staticPageService = staticPageService;
            _addressService = addressService;
        }
        #region MainMenu
        public IActionResult Index() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult ChangeBgNewProduct(IFormFile image)
        {
            try
            {
                if (image != null)
                {
                    if (ImageSecurity.Imagevalidator(image))
                    {
                        image.SaveImage("bg-new.jpg", "wwwroot/uploads");
                         TempData["SuccessMessage"] = "تصویر با موفقیت ویرایش شد";

                    }

                }
            }
            catch (Exception e)
            {
            }





            TempData["ErrorMessage"] = "خطا در تغییر تصویر";
            return RedirectToAction("Index");
        }
        public IActionResult IndexStaticPage()
        {
            var res = _staticPageService.GetListStaticForAdmin();
            return View(res);
        }

        public IActionResult EditStaticPage(long id)
        {
            var res = _staticPageService.GetStaticPageById(id);
            return View(res);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult EditStaticPage(StaticPage staticPage, int typePage)
        {
            staticPage.TypeStaticPage = (EnumTypeStaticPage)typePage;
            if (!ModelState.IsValid)
                return View(staticPage);


            TempData["res"] = "faild";
            if (_staticPageService.EditStaticPage(staticPage))
            {
                _logger.CreateAuditScope(new AuditLog<StaticPage>()
                {
                    Modifier = _userId,
                    Action = Command.Update,
                    Entite = staticPage,
                });
                TempData["res"] = "success";
            }
            return RedirectToAction("IndexStaticPage");
        }
        #endregion

        #region Import

        public IActionResult Import()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Import(IFormFile File)
        {

            try
            {
                Guid guid = Guid.NewGuid();
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/fileUploads",
                    guid + ".xls");

                if (File.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") || File.ContentType.Equals("application/vnd.ms-excel"))
                {
                    List<City> list = new List<City>();
                    try
                    {
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await File.CopyToAsync(fileStream);
                            fileStream.Dispose();
                            ExcelPackage.LicenseContext = LicenseContext.Commercial;
                            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                            using (var package = new ExcelPackage(new FileInfo(filePath)))
                            {

                                ExcelWorksheet workSheet = package.Workbook.Worksheets.FirstOrDefault();
                                object[,] valueArray = workSheet.Cells.GetValue<object[,]>();

                                int maxRows = workSheet.Dimension.End.Row;
                                int maxColumns = workSheet.Dimension.End.Column;

                                System.Data.DataTable dt = new System.Data.DataTable();
                                for (int col = 0; col < maxColumns; col++)
                                {
                                    dt.Columns.Add((string?)valueArray[0, col]);
                                }
                                for (int row = 1; row < maxRows; row++)
                                {
                                    DataRow dr = dt.NewRow();
                                    for (int col = 0; col < maxColumns; col++)
                                    {
                                        var rowCol = valueArray[row, col];
                                        if (rowCol != null) dr[col] = rowCol.ToString()!;
                                    }
                                    dt.Rows.Add(dr);
                                }
                                for (int col = 0; col < maxColumns; col++)
                                {
                                    dictionaryColIndexs.Add((string?)valueArray[0, col], col);
                                }


                                for (int row = 1; row < maxRows; row++)
                                {
                                    var p = new City();
                                    p.Id = Convert.ToInt64(valueArray[row, MapCol("کد شهر")] != null ? valueArray[row, MapCol("کد شهر")].ToString() : "0");
                                    p.TaxCode = Convert.ToInt64(valueArray[row, MapCol("کد مالیاتی")] != null ? valueArray[row, MapCol("کد مالیاتی")].ToString() : "0");
                                    p.CityName = valueArray[row, MapCol("نام شهر")]?.ToString();

                                    if (!string.IsNullOrEmpty(p.CityName) && p.Id != 0 && p.TaxCode != 0)
                                    {
                                        list.Add(p);

                                    }
                                }

                                _addressService.UpdateTaxCodeCity(list);


                            }



                        }


                    }
                    catch (Exception e)
                    {
                        var str = e.Message;
                        TempData["XlsConvertErrorMsg"] = "Converting fail, check if your data is correct";
                        TempData["res"] = "faild";

                        return View();
                    }

                }
                else
                {
                    TempData["res"] = "faild";

                    return View();
                }
                TempData["res"] = "success";
                return View(); ;

            }
            catch (Exception e)
            {
                TempData["res"] = "faild";

                return View();
            }
        }
        Dictionary<string, int> dictionaryColIndexs = new Dictionary<string, int>();
        private int MapCol(string colName)
        {

            if (dictionaryColIndexs.ContainsKey(colName))
            {
                int index = dictionaryColIndexs[colName];
                return index;
            }
            return -1;
        }
        #endregion

    }
}