using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Entities.Ofogh;
using EShop.DataLayer.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class OfoghController : BaseAdminController
    {
        private IOfoghService _ofoghService;

        public OfoghController(IOfoghService ofoghService, Logging.AuditLog.IAuditService logger,
            IHttpContextAccessor contextAccessor
        ) : base(logger,contextAccessor)
        {
            _ofoghService = ofoghService;
        }

        public ActionResult IndexHamkar()
        {
            var res = _ofoghService.GetListForAdminKamkar();
            return View(res);
        }


        public IActionResult UploadHamkar()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> UploadHamkar(IFormFile File)
        {
            //TempData["XlsConvertErrorMsg"] = "Converting fail, check if your data is correct";
            //TempData["res"] = "faild";

            //return View();

            try
            {
                Guid guid = Guid.NewGuid();
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/fileUploads",
                    guid + ".xls");

                if (File.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") || File.ContentType.Equals("application/vnd.ms-excel"))
                {
                    List<OfoghHistory> ofoghHistories = new List<OfoghHistory>();
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
                               // int maxColumns = workSheet.Dimension.End.Column;
                                int maxColumns = 18;

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
                                    ofoghHistories.Add(new OfoghHistory
                                    {
                                        TypeOfogh = EnumTypeOfogh.Hamkar,
                                        StatusOfogh = EnumStatusOfogh.Wating,
                                        CountTry = 0,
                                        LastTryDate = DateTime.Now,
                                        LastTryTime = DateTime.Now.ToShortTimeString(),
                                        TarikhSanad = valueArray[row, 0]?.ToString(),
                                        ShomareSuratHesab = valueArray[row, 1]?.ToString(),
                                        ShenaseMeli = valueArray[row, 2]?.ToString(),
                                        CodeNaghs = valueArray[row, 3]?.ToString(),
                                        NameKharidar = valueArray[row, 4]?.ToString(),
                                        Mobile = valueArray[row, 5]?.ToString(),
                                        CodePostiMabda = valueArray[row, 6]?.ToString(),
                                        CodePostiMaghsad = valueArray[row, 7]?.ToString(),
                                        ShomareGharardadBurs = valueArray[row, 8]?.ToString(),
                                        VaziyatHaml = valueArray[row, 9]?.ToString(),
                                        ShomareBarname = valueArray[row, 10]?.ToString(),
                                        TarikhBarname = valueArray[row, 11]?.ToString(),
                                        SerialBarname = valueArray[row, 12]?.ToString(),
                                        Sharh = valueArray[row, 13]?.ToString(),
                                        ShenaseKala = valueArray[row, 14]?.ToString(),
                                        Tedad = valueArray[row, 15]?.ToString(),
                                        Mablagh = valueArray[row, 16]?.ToString(),
                                        Takhfif = valueArray[row, 17]?.ToString(),
                                        Maliyat = valueArray[row, 18]?.ToString(),

                                    });
                                }

                            }



                        }


                        if (ofoghHistories.Any())
                        {
                            _ofoghService.AddRange(ofoghHistories);
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

        public ActionResult IndexKhord()
        {
            var res = _ofoghService.GetListForAdminKhord();
            return View(res);
        }


        public IActionResult UploadKhord()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> UploadKhord(IFormFile File)
        {
            //TempData["XlsConvertErrorMsg"] = "Converting fail, check if your data is correct";
            //TempData["res"] = "faild";

            //return View();

            try
            {
                Guid guid = Guid.NewGuid();
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/fileUploads",
                    guid + ".xls");

                if (File.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") || File.ContentType.Equals("application/vnd.ms-excel"))
                {
                    List<OfoghHistory> ofoghHistories = new List<OfoghHistory>();
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
                                    ofoghHistories.Add(new OfoghHistory
                                    {
                                        TypeOfogh = EnumTypeOfogh.KhordeForush,
                                        StatusOfogh = EnumStatusOfogh.Wating,
                                        CountTry = 0,
                                        LastTryDate = DateTime.Now,
                                        LastTryTime = DateTime.Now.ToShortTimeString(),
                                        TarikhSanad = valueArray[row, 0]?.ToString(),
                                        ShomareSuratHesab = valueArray[row, 1]?.ToString(),
                                        ShenaseMeli = valueArray[row, 2]?.ToString(),
                                        NameKharidar = valueArray[row, 3]?.ToString(),
                                        Mobile = valueArray[row, 4]?.ToString(),
                                        CodePostiMabda = valueArray[row, 5]?.ToString(),
                                        Sharh = valueArray[row, 6]?.ToString(),
                                        ShenaseKala = valueArray[row, 7]?.ToString(),
                                        ShenaseRahgiri = valueArray[row, 8]?.ToString(),
                                        Mablagh = valueArray[row, 9]?.ToString(),

                                    });
                                }

                            }



                        }


                        if (ofoghHistories.Any())
                        {
                            _ofoghService.AddRange(ofoghHistories);
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



    }




}