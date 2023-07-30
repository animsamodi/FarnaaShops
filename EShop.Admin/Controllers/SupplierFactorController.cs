using System;
using System.Collections.Generic;
using System.Linq;
using EndPoint.Web.Utilities;
using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class SupplierFactorController : BaseAdminController
    {
        private ISupplierFactorService _SupplierFactorService;
        private ISupplierService _supplierService;
        private ISupplierFactorProductService _supplierFactorProductService;
        private IWarehouseProductService _warehouseProductService;
        private IProductService _productService;
        private IVariantService _variantService;
        private IWarehouseService _warehouseService;
        private IUserService _userService;

        public SupplierFactorController(ISupplierFactorService SupplierFactorService, ISupplierService supplierService, ISupplierFactorProductService supplierFactorProductService, IProductService productService, IVariantService variantService, IWarehouseProductService warehouseProductService, IWarehouseService warehouseService)
        {
            _SupplierFactorService = SupplierFactorService;
            _supplierService = supplierService;
            _supplierFactorProductService = supplierFactorProductService;
            _productService = productService;
            _variantService = variantService;
            _warehouseProductService = warehouseProductService;
            _warehouseService = warehouseService;
        }


        public ActionResult IndexAll()
        {
             var res = _SupplierFactorService.GetListForAdmin(null);
            return View(res);
        }
        public ActionResult Index()
        {
             var res = _SupplierFactorService.GetListForAdmin(EnumSupplierFactorStatus.Register);
            return View(res);
        }
        public ActionResult IndexConfirmTamin()
        {
             var res = _SupplierFactorService.GetListForAdmin(EnumSupplierFactorStatus.ConfirmTamin);
            return View(res);
        }
        public ActionResult IndexAnbar()
        {
             var res = _SupplierFactorService.GetListForAdmin(EnumSupplierFactorStatus.ConfirmTamin);
            return View(res);
        }
        public ActionResult IndexConfirmAnbar()
        {
             var res = _SupplierFactorService.GetListForAdmin(EnumSupplierFactorStatus.TaeedAnbar);
            return View(res);
        }
        public IActionResult Create()
        {
            ViewBag.Suppliers = _supplierService.GetListForAdmin();

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(SupplierFactor model)
        {

            ViewBag.Suppliers = _supplierService.GetListForAdmin();

            if (!ModelState.IsValid)
                return View(model);

            bool res = _SupplierFactorService.Add(model);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult CreateSupplierFactorProduct(int id)
        {
            ViewBag.Products = _variantService.GetListVariants("-1").Select(x => new SelectListItem { Text = x.ProductFaTitle + " - " + x.ProductOption + " - " + x.Guarantee, Value = x.VariantId.ToString() }).ToList();
             ViewBag.SupplierFactorId = id;
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CreateSupplierFactorProduct(SupplierFactorProduct model)
        {

            ViewBag.Products = _variantService.GetListVariants("-1").Select(x => new SelectListItem { Text = x.ProductFaTitle + " - " + x.ProductOption + " - " + x.Guarantee, Value = x.VariantId.ToString() }).ToList();
            ViewBag.SupplierFactorId = model.SupplierFactorId;
            model.Id = 0;
            if (!ModelState.IsValid)
                return View(model);

            bool res = _supplierFactorProductService.Add(model);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(ListSupplierFactorProducts), new { id = model.SupplierFactorId });
        }


        public IActionResult Delete(int id)
        {
            var data = _SupplierFactorService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
            bool res = _SupplierFactorService.Delete(data);
            
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));

        }
         public IActionResult DeleteSupplierFactorProduct(int id)
        {
            var data = _supplierFactorProductService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
            bool res = _supplierFactorProductService.Delete(data);
            
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(ListSupplierFactorProducts),new {id=data.SupplierFactorId});

        }
        public IActionResult Edit(int id)
        {
            ViewBag.Suppliers = _supplierService.GetListForAdmin();

            var data = _SupplierFactorService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(Index));
            }
         
            return View(data);
        }
        public IActionResult ConfirmSupplierFactorAnbar(int id)
        {
 
            var data = _SupplierFactorService.FindById(id);
                var countProducts = data.SupplierFactorProducts.Sum(c => c.Count) ;
                var countRegisterProducts = data.SupplierFactorProducts.Sum(c => c.WarehouseProducts.Count);
                if (countRegisterProducts < countProducts)
                {
                    TempData["ErrorMessage"] =
                        $"متاسفانه امکان تایید این فاکتور وجود ندارد. شما {countProducts - countRegisterProducts} محصول از این فاکتور را ثبت نکرده اید";
                    return RedirectToAction("IndexAnbar");

            }
            data.Status = EnumSupplierFactorStatus.TaeedAnbar;
            var res = _SupplierFactorService.Update(data);
         
            return RedirectToAction("IndexConfirmAnbar");
        }
        public IActionResult ConfirmSupplierFactorTamin(int id)
        {
 
            var data = _SupplierFactorService.FindById(id);
            data.Status = EnumSupplierFactorStatus.ConfirmTamin;
            var res = _SupplierFactorService.Update(data);
         
            return RedirectToAction("IndexConfirmTamin");
        }
        public IActionResult ListSupplierFactorProducts(int id)
        {
            ViewBag.Suppliers = _supplierService.GetListForAdmin();
            ViewBag.SupplierFactorId = id;

            var res = _supplierFactorProductService.GetFactorProducts(id);
         
            return View(res);
        }
        public IActionResult ListSupplierFactorProductsShow(int id)
        {
            ViewBag.Suppliers = _supplierService.GetListForAdmin();
            ViewBag.SupplierFactorId = id;

            var res = _supplierFactorProductService.GetFactorProducts(id);
         
            return View(res);
        }
        public IActionResult ListSupplierFactorProductsAnbar(int id)
        {
            ViewBag.Suppliers = _supplierService.GetListForAdmin();
            ViewBag.SupplierFactorId = id;

            var res = _supplierFactorProductService.GetFactorProducts(id);
         
            return View(res);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(SupplierFactor model)
        {
            ViewBag.Suppliers = _supplierService.GetListForAdmin();


            if (!ModelState.IsValid)
                return View(model);


             
            bool res = _SupplierFactorService.Update(model);
            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction(nameof(Index));
        }


        #region SupplierFactorProduct

        public IActionResult ListSupplierFactorWarehouseProduct(int id)
        {

            ViewBag.Warehouses = _warehouseService.GetListForAdmin();
            ViewBag.SupplierFactorProduct = _supplierFactorProductService.FindById(id);
            var res = _warehouseProductService.GetListByFactorProductId(id);
            return View(res);
        }
        public IActionResult DeleteWarehouseProductIMEI(int id)
        {

            var data = _warehouseProductService.FindById(id);
            var res = _warehouseProductService.Delete(data);
            return RedirectToAction("ListSupplierFactorWarehouseProduct", new { id = data.SupplierFactorProductId });
        }

        public IActionResult AddWarehouseProductIMEI(CreateWarehouseProductIMEIViewModel  model)
        {
            var listAdd = new List<WarehouseProduct>();
            var supplierFactorProduct = _supplierFactorProductService.FindById(model.supplierFactorProductId);
            var userId = ClaimUtility.GetUserId(User)??0;
           

            var countLastRegister = _warehouseProductService.GetCountCodeRegistered(model.supplierFactorProductId);
            if (countLastRegister >= supplierFactorProduct.Count)
            {
                TempData["ErrorMessage"] = $"شما {countLastRegister} کالا برای این محصول ثبت کرده اید و نمیتوانید محصول بیشتری ثبت کنید";
                return RedirectToAction("ListSupplierFactorWarehouseProduct", new { id = model.supplierFactorProductId });

            } //
            var checkExistIMEI = _warehouseProductService.CheckExistIMEI(model.IMEI);
            if (checkExistIMEI != null)
            {
                TempData["ErrorMessage"] =
                    $" کد {model.IMEI} قبلا برای {checkExistIMEI.SupplierFactorProduct.Variant.Product.EnTitle} توسط {checkExistIMEI.DeliveryUser.FullName} ثبت شده است";
                return RedirectToAction("ListSupplierFactorWarehouseProduct", new { id = model.supplierFactorProductId });

            }
            //foreach (var imei in model.IMEIList)
            //{
            //    WarehouseProduct warehouseProduct = new WarehouseProduct
            //    {
            //        Code = supplierFactorProduct.Code,
            //        DeliveryUserId = userId,
            //        IMEI = imei,
            //        DeliveryDate = DateTime.Now.GetShamsiDate(),
            //        DeliveryTime = DateTime.Now.ToShortTimeString(),
            //        SupplierFactorProductId = model.supplierFactorProductId,

            //    };
            //    warehouseProduct.SetCreateDefaultValue(userId);
            //    listAdd.Add(warehouseProduct);
            //}

            //var res = _warehouseProductService.AddListWarehouseProduct(listAdd);
            if (!string.IsNullOrEmpty(model.IMEI))
            {
                WarehouseProduct warehouseProduct = new WarehouseProduct
                {
                    Code = supplierFactorProduct.Code,
                    DeliveryUserId = userId,
                    IMEI = model.IMEI,
                    DeliveryDate = DateTime.Now.GetShamsiDate(),
                    DeliveryTime = DateTime.Now.ToShortTimeString(),
                    SupplierFactorProductId = model.supplierFactorProductId,
                    WarehouseId = model.WarehouseId

                };
                warehouseProduct.SetCreateDefaultValue(userId);


                var res = _warehouseProductService.Add(warehouseProduct);
            }
             

            return RedirectToAction("ListSupplierFactorWarehouseProduct",new{id=model.supplierFactorProductId});
        }

        

        #endregion

    }
}