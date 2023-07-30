using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EndPoint.Web.Utilities;
using EShop.Admin.Helper;
using EShop.Core.ExtensionMethods;
using EShop.Core.Helpers;
using EShop.Core.Sender;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities.Address;
using EShop.DataLayer.Entities.Order;
using EShop.DataLayer.Enum;
using EShop.Logging.AuditLog.Models;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class OrderController : BaseAdminController
    {
        private IOrderService _orderService;
        private IShipmentService _shipmentService;
        private IAddressService _addressService;
        private IUserService _userService;
        private ICrmService _crmService;
        private IPaymentDetialService _paymentDetialService;
        private ICategoryService _categoryService;
        private ISmsSender _smsService;
        private ISupplierFactorProductService _supplierFactorProductService;
        private IWarehouseProductService _warehouseProductService;
        public OrderController(IOrderService orderService, ISmsSender smsService,
            IShipmentService shipmentService, IAddressService addressService,
            IUserService userService, ICrmService crmService, IPaymentDetialService paymentDetialService,
            ICategoryService categoryService, Logging.AuditLog.IAuditService logger
            , IHttpContextAccessor contextAccessor, ISupplierFactorProductService supplierFactorProductService, IWarehouseProductService warehouseProductService) : base(logger, contextAccessor)
        {
            _orderService = orderService;
            _shipmentService = shipmentService;
            _userService = userService;
            _crmService = crmService;
            _paymentDetialService = paymentDetialService;
            _categoryService = categoryService;
            _supplierFactorProductService = supplierFactorProductService;
            _warehouseProductService = warehouseProductService;
            _addressService = addressService;
            _smsService = smsService;
        }


        #region Order

        public IActionResult IndexInPerson(UserOrderSearchAdmin search, bool? delivered)
        {
            if (search.PageNumber == null)
                search.PageNumber = 1;
            if (search.PSDate == null && search.PEDate == null)
            {
                search.PSDate = DateTime.Now.GetShamsiDate();
                search.PEDate = DateTime.Now.AddDays(1).GetShamsiDate();
            }
            search.Shipment = "حضوری";
            search.Delivered = delivered;
            var res = _orderService.GetListUserOrderAdmin(search, false);

            ViewBag.count = res.Item1;
            ViewBag.PageNumber = search.PageNumber;

            if (search.PSDate != null)
                search.PSDate = search.PSDate.EnglishToPersian();
            if (search.PEDate != null)
                search.PEDate = search.PEDate.EnglishToPersian();
            ViewBag.Search = search;
            return View(res.Item2);
        }
        public IActionResult Index(UserOrderSearchAdmin search)
        {
            if (search.PageNumber == null)
                search.PageNumber = 1;
            if (search.PSDate == null && search.PEDate == null)
            {
                search.PSDate = DateTime.Now.GetShamsiDate();
                search.PEDate = DateTime.Now.AddDays(1).GetShamsiDate();
            }
            ViewBag.Shipments = _shipmentService.GetActiveShipments();

            var res = _orderService.GetListUserOrderAdmin(search, false);

            ViewBag.count = res.Item1;
            ViewBag.PageNumber = search.PageNumber;

            if (search.PSDate != null)
                search.PSDate = search.PSDate.EnglishToPersian();
            if (search.PEDate != null)
                search.PEDate = search.PEDate.EnglishToPersian();
            ViewBag.Search = search;
            return View(res.Item2);
        }
        public IActionResult OrderPrice(UserOrderSearchAdmin search)
        {
            if (search.PageNumber == null)
                search.PageNumber = 1;
            if (search.PSDate == null && search.PEDate == null)
            {
                search.PSDate = DateTime.Now.GetShamsiDate();
                search.PEDate = DateTime.Now.AddDays(1).GetShamsiDate();
            }
            ViewBag.Shipments = _shipmentService.GetActiveShipments();

            var res = _orderService.GetListUserOrderAdmin(search, false);

            ViewBag.count = res.Item1;
            ViewBag.PageNumber = search.PageNumber;

            if (search.PSDate != null)
                search.PSDate = search.PSDate.EnglishToPersian();
            if (search.PEDate != null)
                search.PEDate = search.PEDate.EnglishToPersian();
            ViewBag.Search = search;
            return View(res.Item2);
        }
        public IActionResult IndexAnbar(UserOrderSearchAdmin search)
        {
            if (search.PageNumber == null)
                search.PageNumber = 1;
            if (search.PSDate == null && search.PEDate == null)
            {
                search.PSDate = DateTime.Now.GetShamsiDate();
                search.PEDate = DateTime.Now.AddDays(1).GetShamsiDate();
            }
            ViewBag.Shipments = _shipmentService.GetActiveShipments();

            search.WaitConfairmAnbar = true;

            var res = _orderService.GetListUserOrderAdmin(search);

            ViewBag.count = res.Item1;
            ViewBag.PageNumber = search.PageNumber;

            if (search.PSDate != null)
                search.PSDate = search.PSDate.EnglishToPersian();
            if (search.PEDate != null)
                search.PEDate = search.PEDate.EnglishToPersian();
            ViewBag.Search = search;
            return View(res.Item2);
        }
        public IActionResult IndexAnbarConfirm(UserOrderSearchAdmin search)
        {
            if (search.PageNumber == null)
                search.PageNumber = 1;
            if (search.PSDate == null && search.PEDate == null)
            {
                search.PSDate = DateTime.Now.GetShamsiDate();
                search.PEDate = DateTime.Now.AddDays(1).GetShamsiDate();
            }
            ViewBag.Shipments = _shipmentService.GetActiveShipments();

            search.ConfairmAnbar = true;

            var res = _orderService.GetListUserOrderAdmin(search);

            ViewBag.count = res.Item1;
            ViewBag.PageNumber = search.PageNumber;

            if (search.PSDate != null)
                search.PSDate = search.PSDate.EnglishToPersian();
            if (search.PEDate != null)
                search.PEDate = search.PEDate.EnglishToPersian();
            ViewBag.Search = search;
            return View(res.Item2);
        }
        public IActionResult PrintOrder(long id)
        {

            var res = _orderService.GetOrderForPrintAdmin(id);
            if (res == null)
                return RedirectToAction("OrderDetailAnbar", new { id = id });
            return View(res);
        }
        public IActionResult ConfirmOrder(long id)
        {

            var order = _orderService.GetOrderById(id);

            if (order.OrderDetails.Any(c => c.WarehouseProductId == null))
            {
                TempData["ErrorMessage"] = $"لطفا همه ی کد های محصولات را ثبت کنید";
                return RedirectToAction("OrderDetailAnbar", new { id = id});

            }

            order.OrderStatus = EnumOrderStatus.OutAnbar;
            var res = _orderService.EditOrder(order);

            return RedirectToAction(nameof(IndexAnbar));
        }
        public IActionResult AddOrderProductIMEI(CreateOrderProductImeiViewModel model)
        {

            var oredr = _orderService.GetOrderAndDetailsById(model.OrderId);
            var warehouseProduct = _warehouseProductService.GetFreeProductByIMEI(model.IMEI);
            if (warehouseProduct == null)
            {
                TempData["ErrorMessage"] = $"کد ثبت شده شما موجود نمی باشد";
                return RedirectToAction("OrderDetailAnbar", new { id = model.OrderId });

            }

            var productInOrder =
                oredr.OrderDetails.FirstOrDefault(c => c.VariantId == warehouseProduct.SupplierFactorProduct.VariantId && c.WarehouseProductId == null);
            if (productInOrder == null)
            {
                TempData["ErrorMessage"] = $"کد ثبت شده شما در این سفارش موجود نمی باشد";
                return RedirectToAction("OrderDetailAnbar", new { id = model.OrderId });
            }
            var userId = ClaimUtility.GetUserId(User) ?? 0;


            warehouseProduct.BuyerUserId = oredr.UserId;
            warehouseProduct.ClearanceUserId = userId;
            warehouseProduct.ClearanceDate = DateTime.Now.GetShamsiDate();
            warehouseProduct.ClearanceTime = DateTime.Now.ToShortTimeString();
            warehouseProduct.OrderDetailId = productInOrder.Id;
            warehouseProduct.FinalPrice = productInOrder.SumPriceAfterDiscount;
            warehouseProduct.IsUse = true;

            var res = _warehouseProductService.Update(warehouseProduct);

            //
             productInOrder.WarehouseProductId = warehouseProduct.Id;
             res = _orderService.UpdateOrderDetails(productInOrder);

            if (res)
                TempData["SuccessMessage"] = $" کد ثبت شده شما برای این سفارش ثبت شد";
            else
                TempData["ErrorMessage"] = $" خطا در انجام عملیات, لطفا دوباره تست کنید";


            return RedirectToAction("OrderDetailAnbar", new { id = model.OrderId });
        }
        
        public IActionResult DeleteOrderDetailIMEI(long orderId ,long id)
        {

             var warehouseProduct = _warehouseProductService.GetProductByorderDetailId(id);
            if (warehouseProduct == null)
            {
                TempData["ErrorMessage"] = $"برای این محصول کدی ثبت نشده است";
                return RedirectToAction("OrderDetailAnbar", new { id = orderId });

            }

           
            var userId = ClaimUtility.GetUserId(User) ?? 0;


            warehouseProduct.BuyerUserId = null;
            warehouseProduct.ClearanceUserId = null;
            warehouseProduct.ClearanceDate = null;
            warehouseProduct.ClearanceTime = null;
            warehouseProduct.OrderDetailId = null;
            warehouseProduct.FinalPrice = 0;
            warehouseProduct.IsUse = false;

            var res = _warehouseProductService.Update(warehouseProduct);
            //
            var productInOrder = _orderService.GetOrderDetailsById(id);
            productInOrder.WarehouseProductId = null;
            res = _orderService.UpdateOrderDetails(productInOrder);

            if (res)
                TempData["SuccessMessage"] = $" کد با موفقیت حذف شد";
            else
                TempData["ErrorMessage"] = $" خطا در انجام عملیات, لطفا دوباره تست کنید";


            return RedirectToAction("OrderDetailAnbar", new { id = orderId });
        }

        public IActionResult ListUnsendOrder(UserOrderSearchAdmin search)
        {
            if (search.PageNumber == null)
                search.PageNumber = 1;
            if (search.PSDate == null && search.PEDate == null)
            {
                search.PSDate = DateTime.Now.GetShamsiDate();
                search.PEDate = DateTime.Now.AddDays(1).GetShamsiDate();
            }
            ViewBag.Shipments = _shipmentService.GetActiveShipments();
            //
            search.SendTadbir = DataLayer.Enum.EnumYesNo.No;
            search.OrderStatus = DataLayer.Enum.EnumOrderStatus.InProccess;
            //
            var res = _orderService.GetListUserOrderAdmin(search);

            ViewBag.count = res.Item1;
            ViewBag.PageNumber = search.PageNumber;

            if (search.PSDate != null)
                search.PSDate = search.PSDate.EnglishToPersian();
            if (search.PEDate != null)
                search.PEDate = search.PEDate.EnglishToPersian();
            ViewBag.Search = search;
            return View(res.Item2);
        }
        public IActionResult IndexColleague(UserOrderSearchAdmin search)
        {
            if (search.PageNumber == null)
                search.PageNumber = 1;
            if (search.PSDate == null && search.PEDate == null)
            {
                search.PSDate = DateTime.Now.GetShamsiDate();
                search.PEDate = DateTime.Now.AddDays(1).GetShamsiDate();
            }
            ViewBag.Shipments = _shipmentService.GetActiveShipments();
            search.IsColleaugeOrder = false;

            var res = _orderService.GetListUserOrderAdmin(search, true);

            ViewBag.count = res.Item1;
            ViewBag.PageNumber = search.PageNumber;

            if (search.PSDate != null)
                search.PSDate = search.PSDate.EnglishToPersian();
            if (search.PEDate != null)
                search.PEDate = search.PEDate.EnglishToPersian();
            ViewBag.Search = search;
            return View(res.Item2);
        }
        public IActionResult OrderPriceColleauge(UserOrderSearchAdmin search)
        {
            if (search.PageNumber == null)
                search.PageNumber = 1;
            if (search.PSDate == null && search.PEDate == null)
            {
                search.PSDate = DateTime.Now.GetShamsiDate();
                search.PEDate = DateTime.Now.AddDays(1).GetShamsiDate();
            }
            ViewBag.Shipments = _shipmentService.GetActiveShipments();


            var res = _orderService.GetListUserOrderAdmin(search, true);

            ViewBag.count = res.Item1;
            ViewBag.PageNumber = search.PageNumber;

            if (search.PSDate != null)
                search.PSDate = search.PSDate.EnglishToPersian();
            if (search.PEDate != null)
                search.PEDate = search.PEDate.EnglishToPersian();
            ViewBag.Search = search;
            return View(res.Item2);
        }

        public IActionResult EditOrder(long id)
        {
            var res = _orderService.GetOrderById(id);
            return View(res);
        }
        public IActionResult OrderDetailAnbar(long id)
        {
            var res = _orderService.GetOrderAndDetailsById(id);
            return View(res);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult EditOrder(Order order)
        {

            if (!ModelState.IsValid)
                return View(order);
            var data = _orderService.GetOrderById(order.Id);
            data.ClientName = order.ClientName;
            data.ClientTel = order.ClientTel;
            data.ClientNatioalCode = order.ClientNatioalCode;
            data.ClientAddress = order.ClientAddress;
            data.ClientPostalCode = order.ClientPostalCode;
            data.RecipientName = order.RecipientName;
            data.RecipientTel = order.RecipientTel;
            data.RecipientAddress = order.RecipientAddress;
            data.RecipientPostalCode = order.RecipientPostalCode;
            data.ShipmentTitle = order.ShipmentTitle;
            var res = _orderService.EditOrder(data);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction("ListUnsendOrder");
        }
        public IActionResult OrderCrmLog(long id)
        {
            var res = _crmService.GetCrmLogByOrderId(id);
            return View(res);
        }

        public IActionResult ChangeOrderState()
        {
            ViewBag.Shipmmets = _shipmentService.GetShipmentForAdmin();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ChangeOrderState(ChangeOrderStateViewModel model)
        {
            ViewBag.Shipmmets = _shipmentService.GetShipmentForAdmin();

            if (!ModelState.IsValid)
                return View(model);
            try
            {
                model.FactorNo = new List<string>();
                Guid guid = Guid.NewGuid();
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/fileUploads",
                    guid + ".xls");

                if (model.File.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") || model.File.ContentType.Equals("application/vnd.ms-excel"))
                {
                    try
                    {
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.File.CopyToAsync(fileStream);
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
                                    string FactorNo = "";
                                    FactorNo = valueArray[row, MapCol("FactorNo")]?.ToString();

                                    if (!string.IsNullOrEmpty(FactorNo) && !string.IsNullOrEmpty(FactorNo))
                                    {
                                        model.FactorNo.Add(FactorNo);

                                    }
                                }

                            }

                        }

                        if (model.FactorNo.Any())
                        {
                            var orders = _orderService.ChangeOrderState(model);
                            foreach (var order in orders)
                            {
                                BackgroundJob.Enqueue(() => _smsService.SendSms(order.Item2, "OrderStateAlert", order.Item1.ToString(), null, null, null, model.OrderStatus.ToDisplay()));
                            }

                        }
                    }
                    catch (Exception e)
                    {
                        var str = e.Message;
                        TempData["XlsConvertErrorMsg"] = "Converting fail, check if your data is correct";
                        TempData["res"] = "faild";

                        return View(model);
                    }

                }
                else
                {
                    TempData["res"] = "faild";

                    return View(model);
                }
                TempData["res"] = "success";

                return View(); ;

            }
            catch (Exception e)
            {
                TempData["res"] = "faild";

                return View(model);
            }
        }

        public IActionResult UploadPostCode()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> UploadPostCode(PostCodeViewModel model)
        {

            if (!ModelState.IsValid)
                return View(model);
            try
            {
                model.FactorPostModels = new List<FactorPostModel>();
                Guid guid = Guid.NewGuid();
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/fileUploads",
                    guid + ".xls");

                if (model.File.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") || model.File.ContentType.Equals("application/vnd.ms-excel"))
                {
                    try
                    {
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.File.CopyToAsync(fileStream);
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
                                    FactorPostModel factorPostModel = new FactorPostModel();
                                    factorPostModel.FactorNo = valueArray[row, MapCol("FactorNo")]?.ToString();
                                    factorPostModel.PostCode = valueArray[row, MapCol("PostCode")]?.ToString();

                                    if (!string.IsNullOrEmpty(factorPostModel.FactorNo) && !string.IsNullOrEmpty(factorPostModel.PostCode))
                                    {
                                        model.FactorPostModels.Add(factorPostModel);

                                    }
                                }

                            }



                        }


                        if (model.FactorPostModels.Any())
                        {
                            _orderService.UploadPostCode(model);
                        }
                    }
                    catch (Exception e)
                    {
                        var str = e.Message;
                        TempData["XlsConvertErrorMsg"] = "Converting fail, check if your data is correct";
                        TempData["res"] = "faild";

                        return View(model);
                    }

                }
                else
                {
                    TempData["res"] = "faild";

                    return View(model);
                }
                TempData["res"] = "success";
                return View(); ;

            }
            catch (Exception e)
            {
                TempData["res"] = "faild";

                return View(model);
            }
        }



        public IActionResult ConvertPreFactorToFactor()
        {
            return View();
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
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ConvertPreFactorToFactor(ConvertPreFactorToFactorViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                model.PreFactorFactors = new List<PreFactorFactorModel>();
                Guid guid = Guid.NewGuid();
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/fileUploads",
                    guid + ".xls");

                if (model.File.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") || model.File.ContentType.Equals("application/vnd.ms-excel"))
                {
                    try
                    {
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.File.CopyToAsync(fileStream);
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
                                    var p = new PreFactorFactorModel();
                                    p.FactorNo = valueArray[row, MapCol("FactorNo")]?.ToString();
                                    p.PreFactorNo = valueArray[row, MapCol("PreFactorNo")]?.ToString();

                                    if (!string.IsNullOrEmpty(p.FactorNo) && !string.IsNullOrEmpty(p.PreFactorNo))
                                    {
                                        model.PreFactorFactors.Add(p);

                                    }
                                }

                            }



                        }


                        if (model.PreFactorFactors.Any())
                        {
                            _orderService.ConverPreFactorToFactor(model);
                        }
                    }
                    catch (Exception e)
                    {
                        var str = e.Message;
                        TempData["XlsConvertErrorMsg"] = "Converting fail, check if your data is correct";
                        TempData["res"] = "faild";

                        return View(model);
                    }

                }
                else
                {
                    TempData["res"] = "faild";

                    return View(model);
                }
                TempData["res"] = "success";
                return View(); ;

            }
            catch (Exception e)
            {
                TempData["res"] = "faild";

                return View(model);
            }
        }
        public IActionResult Payment()
        {
            var res = _orderService.GetPaymentDetailsForAdmin();
            return View(res);
        }
        public IActionResult PaymentListContainer(UserPaymentSearchAdmin search)
        {

            return View();
        }
        public IActionResult PaymentList(string RefId = "", string PSDate = "", string PEDate = "", long? OrderId = null, int PageNumber = 1)
        {
            var content = _orderService.GetListUserPaymentAdmin(RefId, PSDate, PEDate, OrderId, PageNumber, 15);
            ViewBag.count = content.Item1;
            ViewBag.RefId = RefId;
            ViewBag.PSDate = PSDate;
            ViewBag.PEDate = PEDate;
            ViewBag.OrderId = OrderId;
            ViewBag.PageNumber = PageNumber;

            return View("~/Views/Order/PaymentList.cshtml", content.Item2);
        }


        public IActionResult BlockPostCode()
        {
            var res = _addressService.GetBlockPostCode();
            return View(res);
        }
        public IActionResult OrderLimit()
        {
            var res = _orderService.GetListOrderLimit();
            return View(res);
        }

        public IActionResult CreateOrderLimit()
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CreateOrderLimit(OrderLimit orderLimit)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();

            if (!ModelState.IsValid)
                return View(orderLimit);

            TempData["res"] = "faild";
            if (_orderService.CreateOrderLimit(orderLimit))
            {
                _logger.CreateAuditScope(new AuditLog<OrderLimit>()
                {
                    Modifier = _userId,
                    Action = Command.Create,
                    Entite = orderLimit,
                });
                TempData["res"] = "success";
            }

            return RedirectToAction("OrderLimit");
        }
        public IActionResult EditOrderLimit(long id)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            var res = _orderService.GetOrderLimitById(id);
            return View(res);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult EditOrderLimit(OrderLimit orderLimit)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();

            if (!ModelState.IsValid)
                return View(orderLimit);

            TempData["res"] = "faild";
            if (_orderService.EditOrderLimit(orderLimit))
            {
                _logger.CreateAuditScope(new AuditLog<OrderLimit>()
                {
                    Modifier = _userId,
                    Action = Command.Update,
                    Entite = orderLimit,
                });
                TempData["res"] = "success";
            }

            return RedirectToAction("OrderLimit");
        }
        public IActionResult DeleteOrderLimit(long id)
        {

            TempData["res"] = "faild";
            if (_orderService.DeleteOrderLimit(id))
            {
                _logger.CreateAuditScope(new AuditLog<long>()
                {
                    Modifier = _userId,
                    Action = Command.Remove,
                    Entite = id,
                });
                TempData["res"] = "success";
            }

            return RedirectToAction("OrderLimit");
        }
        public IActionResult CreateBlockPostCode()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CreateBlockPostCode(BlockPostalCode postalCode)
        {
            if (!ModelState.IsValid)
                return View(postalCode);

            TempData["res"] = "faild";
            if (_addressService.CreateBlockPostCode(postalCode))
            {
                _logger.CreateAuditScope(new AuditLog<BlockPostalCode>()
                {
                    Modifier = _userId,
                    Action = Command.Create,
                    Entite = postalCode,
                });
                TempData["res"] = "success";
            }

            return RedirectToAction("BlockPostCode");
        }
        public IActionResult DeleteBlockPostCode(long id)
        {

            TempData["res"] = "faild";
            if (_addressService.DeleteBlockPostCode(id))
            {
                _logger.CreateAuditScope(new AuditLog<long>()
                {
                    Modifier = _userId,
                    Action = Command.Remove,
                    Entite = id,
                });
                TempData["res"] = "success";
            }

            return RedirectToAction("BlockPostCode");
        }
        public IActionResult SendToCrm(long id)
        {

            var res = SendOrderToCrm(id);
            //

            //
            TempData["res"] = res ? "success" : "faild";

            var order = _orderService.GetOrderById(id);

            if (order.IsColleauge)
                return RedirectToAction("IndexColleague");
            else
                return RedirectToAction("Index");
        }
        public string GetDetail(long id)
        {
            var res = "";

            //FactorDetail
            //

            try
            {

                CrmImplement crmImplement = new CrmImplement(_crmService);


                var token = crmImplement.GetToken();


                if (!string.IsNullOrEmpty(token))
                {
                    var result = crmImplement.FactorDetail(token, id, 0);
                }


            }
            catch (Exception e)
            {
                return "";
            }

            //


            return res;
        }
        private bool SendOrderToCrm(long orderId)
        {
            try
            {

                CrmImplement crmImplement = new CrmImplement(_crmService);
                var crmOrder = _orderService.GetOrderById(orderId);
                crmOrder.CrmSendCode = CodeGenerator.CrmOrderCodeGenerator();
                _orderService.UpdateOrder(crmOrder);
                var user = _userService.GetUserById(crmOrder.UserId);
                var legal = _userService.GetUserLegalByUserId(crmOrder.UserId);
                var crmOrderDetails = _orderService.GetOrderDetailsForCrm(orderId);
                var token = crmImplement.GetToken(crmOrder.UserId, crmOrder.CrmSendCode);
                var payDet = _paymentDetialService.GetOnliyTypePaymentDetail(orderId);
                var payDetCredit = _paymentDetialService.GetCreditTypePaymentDetail(orderId);
                var pSPDesc = "ندارد";
                try
                {
                    if (payDet != null)
                    {
                        pSPDesc = "کد تراکنش : " + payDet.SaleReferenceId.ToString() + " - ";
                    }
                    pSPDesc += (string.IsNullOrEmpty(crmOrder.ShipmentTitle)
                        ? "ندارد"
                        : crmOrder.ShipmentTitle);
                    if (payDetCredit != null)
                    {
                        pSPDesc += $" - مبلغ {payDetCredit.Price.ToString("N0")} ریال توسط کاربر به صورت اعتباری پردات شده است";
                    }
                    if (user.IsColleague)
                    {
                        pSPDesc += " - **فروش ویژه همکار**";
                    }

                    if (crmOrder.NeedUseDeliveryCode == true)
                    {
                        pSPDesc += " - کد تحویل : " + crmOrder.InPersonCode;

                    }
                }
                catch (Exception e)
                {

                }

                if (!string.IsNullOrEmpty(token))
                {
                    var rseInit = true;
                    foreach (var orderDetail in crmOrderDetails)
                    {
                        try
                        {
                            var provinceCity = crmOrder.ClientAddress.Split(",");
                            InitializeFactor factor = new InitializeFactor
                            {
                                token = token,
                                pSPDate = DateTime.Now,
                                pSPReference = (int)crmOrder.CrmSendCode,
                                pPrjCode = "0",
                                pRCVRName = string.IsNullOrEmpty(crmOrder.RecipientName) ? "ندارد" : crmOrder.RecipientName,
                                pRCVRPhone = crmOrder.RecipientTel,
                                pRCVRPostalCode = crmOrder.RecipientPostalCode,
                                pRCVRAddress = string.IsNullOrEmpty(crmOrder.RecipientAddress) ? "ندارد" : crmOrder.RecipientAddress.Replace("\r\n", ""),
                                pCustNationalCode = user.IsHoghughi && legal != null && !string.IsNullOrEmpty(legal.ShenaseMeli) ? legal.ShenaseMeli : user.NatioalCode,
                                pCustName = user.IsHoghughi ? "-" : crmOrder.ClientName,
                                pCustPhone = crmOrder.ClientTel,
                                pCustAddress = string.IsNullOrEmpty(crmOrder.ClientAddress) ? "ندارد" : crmOrder.ClientAddress.Replace("\r\n", ""),
                                pCustProvince = provinceCity[0].Replace("\r\n", ""),
                                pCustCity = provinceCity[1].Replace("\r\n", ""),
                                pCustPostalCode = string.IsNullOrEmpty(crmOrder.ClientPostalCode) ? "ندارد" : crmOrder.ClientPostalCode,
                                pDiscount = 0,
                                pCostAmount1 = crmOrder.ShipmentPrice,
                                pCostAmount2 = 0,
                                pCostAmount3 = 0,
                                pCostAmount4 = 0,
                                pSPDesc = pSPDesc,
                                pMerchandiseCode = orderDetail.ProductSpecCode.PersianToEnglish(),
                                pAmount = orderDetail.Count,
                                pUnitPrice = (float)Math.Round(orderDetail.Price / 1.09, 0),
                                pCashValue = crmOrder.AmountPayable,
                                pCashDesc = "ندارد",
                                PMerchDesc = orderDetail.ProductColor + " , " + orderDetail.ProductGaranty,
                                pRemainder = 0,
                                pQId = orderDetail.OrderId,
                                //
                                pCustFamily = user.IsHoghughi && legal != null && !string.IsNullOrEmpty(legal.CompanyName) ? legal.CompanyName : "-",
                                pCustType = user.IsHoghughi ? 6 : 1,
                                pCDesc = "-",
                                pSubscriptionNo = user.IsHoghughi && legal != null && !string.IsNullOrEmpty(legal.CodePostiNeshaniMahaleKar) ? legal.CodePostiNeshaniMahaleKar : "-",
                                pGroupName = "-",
                                pAddress2 = user.IsHoghughi && legal != null && !string.IsNullOrEmpty(legal.NeshaniMahaleKar) ? legal.NeshaniMahaleKar : "-",
                                pCEOEmail = user.IsHoghughi && legal != null && !string.IsNullOrEmpty(legal.TelephoneSabet) ? legal.TelephoneSabet : "-",
                                pWhatsapp = user.IsHoghughi && legal != null && !string.IsNullOrEmpty(legal.TarikhTasis) ? legal.TarikhTasis : "-",
                                pTelegram = "-",
                                pLinkdin = "-",
                                pFacebook = "-",
                                pManuFactoryNo = user.IsHoghughi && legal != null && !string.IsNullOrEmpty(legal.CodeNaghshTajer) ? legal.CodeNaghshTajer : "-",
                                pWebSite = user.IsHoghughi && legal != null && !string.IsNullOrEmpty(legal.CodeEghtesadi) ? legal.CodeEghtesadi : "-",
                            };
                            var res = crmImplement.InitializeFactorInfo(factor, crmOrder.UserId, crmOrder.Id, crmOrder.CrmSendCode);
                            if (!res)
                            {
                                rseInit = res;
                                return false;
                            }
                        }
                        catch (Exception e)
                        {
                            return false;

                        }

                    }

                    if (rseInit)
                    {
                        ValidityAndImportInvoce validityAndImportInvoce = new ValidityAndImportInvoce
                        {
                            token = token,
                            pSPReference = (int)crmOrder.CrmSendCode,
                            pQId = crmOrder.Id,
                        };
                        var validityAndImportInvoceRes = crmImplement.ValidityAndImportInvoce(validityAndImportInvoce, crmOrder.UserId, crmOrder.Id, crmOrder.CrmSendCode);
                        if (validityAndImportInvoceRes > 0)
                        {
                            _orderService.SendOrderToCrm(orderId, validityAndImportInvoceRes);
                            return true;

                        }
                        return false;

                    }
                    return false;

                }

                return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        #endregion

        #region Shipment
        [HttpPost]
        public IActionResult GetCity(int id)
        {
            return Json(_addressService.GetCityByProvinceId(id));
        }
        public IActionResult Shipment()
        {
            var res = _shipmentService.GetShipmentForAdmin();
            return View(res);
        }
        public IActionResult CreateShipment()
        {
            ViewBag.provincelist = _addressService.GetProvince();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CreateShipment(Shipment shipment)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.provincelist = _addressService.GetProvince();
                return View(shipment);
            }


            TempData["res"] = "faild";
            if (_shipmentService.CreateShipment(shipment))
            {
                _logger.CreateAuditScope(new AuditLog<Shipment>()
                {
                    Modifier = _userId,
                    Action = Command.Create,
                    Entite = shipment,
                });
                TempData["res"] = "success";
            }

            return RedirectToAction(nameof(Shipment));
        }
        public IActionResult EditShipment(int id)
        {
            Shipment shipment = _shipmentService.FindShipmentById(id);
            if (shipment == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Shipment));
            }

            ViewBag.provincelist = _addressService.GetProvince();

            return View(shipment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult EditShipment(Shipment shipment)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.provincelist = _addressService.GetProvince();

                return View(shipment);
            }
            TempData["res"] = "faild";
            if (_shipmentService.EditShipment(shipment))
            {
                _logger.CreateAuditScope(new AuditLog<Shipment>()
                {
                    Modifier = _userId,
                    Action = Command.Update,
                    Entite = shipment,
                });
                TempData["res"] = "success";
            }
            else
            {
                TempData["res"] = "faild";
                ViewBag.provincelist = _addressService.GetProvince();

                return View(shipment);
            }
            return RedirectToAction(nameof(Shipment));
        }

        #endregion

    }
}