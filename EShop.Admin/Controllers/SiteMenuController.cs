using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Admin.Helper;
using EShop.Core.Security;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Enum;
using Infrastructure.ExternalApi.ImageServer;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class SiteMenuController : BaseAdminController
    {
        private ISiteMenuService _SiteMenuService;
        private ICategoryService _categoryService;
        private IBrandService _brandService;
        IProductSeriService _productSeriService;
        private ICategoryMainService _categoryMainService;
        private IImageUploadService _imageUploadService;

        public SiteMenuController(ISiteMenuService siteMenuService,
          ICategoryService categoryService,
          IBrandService brandService,
          IProductSeriService productSeriService, ICategoryMainService categoryMainService, IImageUploadService imageUploadService)
        {
            _SiteMenuService = siteMenuService;
            _categoryService = categoryService;
            _brandService = brandService;
            _productSeriService = productSeriService;
            _categoryMainService = categoryMainService;
            _imageUploadService = imageUploadService;
        }


        public ActionResult IndexPlus()
        {
            var res = _SiteMenuService.GetListForAdmin(typeSystem: EnumTypeSystem.FarnaaPlus);
            return View(res);
        }
        public ActionResult Index()
        {
            var res = _SiteMenuService.GetListForAdmin(typeSystem: EnumTypeSystem.Farnaa);
            return View(res);
        }
        public IActionResult Create(EnumTypeSystem typeSystem)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            ViewBag.ParentList = _SiteMenuService.GetListForAdmin(typeSystem);
            //ViewBag.SeriList = _productSeriService.GetListForAdmin();
            ViewBag.TypeSystem = typeSystem;
            return View();

        } 
        public IActionResult CreateAuto(EnumTypeSystem typeSystem)
        {
            var siteMenuList = new List<SiteMenu>();
            //remove Exist SiteMenu
            var existSiteMenus = _SiteMenuService.GetListForDelete(typeSystem);
           
                _SiteMenuService.DeleteRange(existSiteMenus);
            
            var categories = _categoryService.GetCategoriesForTree();
            //
            Random rnd = new Random();

            var lstMainCat1 = categories.Where(c => c.ParentId == null).ToList();
            int sort = 0;
            foreach (var mainCat1 in lstMainCat1)
            {
                var menu = new SiteMenu
                {
                    Link = "#",
                    IsActive = true,
                    IsDelete = false,
                    Sort = sort,
                    Title = mainCat1.FaTitle,
                    Type = EnumTypeMenu.Main,
                    Code = rnd.Next(10000, 99999),
                    TypeSystem = typeSystem
                };
                siteMenuList.Add(menu);
                //_SiteMenuService.Add(menu);
                sort++;
                int sort2 = 0;
                //
                var lstSubCat1 = categories.Where(c => c.ParentId == mainCat1.Id).ToList();
                foreach (var subCat1 in lstSubCat1)
                {
                    var menu2 = new SiteMenu
                    {
                        ParentCode= menu.Code,
                        Link = "/category/" + subCat1.EnTitle.Trim().ToLower().Replace(" ","-"),
                        IsActive = true,
                        IsDelete = false,
                        Sort = sort2,
                        Title = subCat1.FaTitle,
                        Type = EnumTypeMenu.SubMenu,
                        Code = rnd.Next(10000, 99999),
                        TypeSystem = typeSystem
                    };
                    siteMenuList.Add(menu2);
                   // _SiteMenuService.Add(menu2);
                   // menu.SiteMenuParent = menu2;
                    sort2++;
                    int sort3 = 0;
                    //
                    var lstSubCat2 = categories.Where(c => c.ParentId == subCat1.Id).ToList();
                    if (lstSubCat2.Any())
                    {
                        foreach (var subCat2 in lstSubCat2)
                        {
                            var menu3 = new SiteMenu
                            {
                                ParentCode = menu2.Code,
                                Link = menu2.Link + "/" + subCat2.EnTitle.Trim().ToLower().Replace(" ", "-"),
                                IsActive = true,
                                IsDelete = false,
                                Sort = sort3,
                                Title = subCat2.FaTitle,
                                Type = EnumTypeMenu.SubMenu,
                                Code = rnd.Next(10000, 99999),
                                TypeSystem = typeSystem
                            };
                            siteMenuList.Add(menu3);
                            //_SiteMenuService.Add(menu3);
                            //menu2.SiteMenuParent = menu3;

                            sort3++;
                            int sort4 = 0;
                            //
                            var brands = _brandService.GetBrandByCategoryId(subCat2.Id);
                            foreach (var brand in brands)
                            {
                                var brandMenu = new SiteMenu
                                {
                                    ParentCode = menu3.Code,
                                    Link = menu3.Link + "/" + brand.EnTitle.Trim().ToLower().Replace(" ", "-"),
                                    IsActive = true,
                                    IsDelete = false,
                                    Sort = sort4,
                                    Title = /*subCat2.FaTitle + " " +*/ brand.FaTitle,
                                    Type = EnumTypeMenu.SubMenu,
                                    Code = rnd.Next(10000, 99999),
                                    TypeSystem = typeSystem
                                };
                                siteMenuList.Add(brandMenu);
                               // _SiteMenuService.Add(brandMenu);
                                //menu3.SiteMenuParent = brandMenu;
                                sort4++;
                            }
                        }

                    }
                    else
                    {
                        var parentBrandMenu = new SiteMenu
                            {
                                ParentCode = menu2.Code,
                                Link = "/category/" + subCat1.EnTitle.Trim().ToLower().Replace(" ", "-"),
                                IsActive = true,
                                IsDelete = false,
                                Sort = sort3,
                                Title =/*" همه ی " +*/ subCat1.FaTitle ,
                                Type = EnumTypeMenu.SubMenu,
                                Code = rnd.Next(10000, 99999),
                                TypeSystem = typeSystem
                        };
                        siteMenuList.Add(parentBrandMenu);

                        var brands = _brandService.GetBrandByCategoryId(subCat1.Id);
                        foreach (var brand in brands)
                        {
                            var brandMenu = new SiteMenu
                            {
                                ParentCode = parentBrandMenu.Code,
                                Link = "/category/" + subCat1.EnTitle.Trim().ToLower().Replace(" ", "-") + "/" + brand.EnTitle.Trim().ToLower().Replace(" ", "-"),
                                IsActive = true,
                                IsDelete = false,
                                Sort = sort3,
                                Title = /*subCat1.FaTitle + " " +*/ brand.FaTitle,
                                Type = EnumTypeMenu.SubMenu,
                                Code = rnd.Next(10000, 99999),
                                TypeSystem = typeSystem
                            };
                            siteMenuList.Add(brandMenu);
                            //_SiteMenuService.Add(brandMenu);
                            //menu2.SiteMenuParent = brandMenu;
                            sort3++;
                        }
                    }
                }

                
            }
            _SiteMenuService.AddRange(siteMenuList);


           

            var mainCategories = _categoryMainService.GetListForUser();



            //List<SearchCategoryViewModel> categories = new List<SearchCategoryViewModel>();

            //var subcategory = _categoryService.GetAllSubCategory();
            //var category = _categoryService.GetAllCategory().Where(c => c.Id != 29).ToList();

            //foreach (var item in category)
            //{
            //    var subid = subcategory.Where(c => c.SubId == item.Id);

            //    if (subid.Count() > 0)
            //    {
            //        foreach (var item2 in subid)
            //        {
            //            categories.Add(new SearchCategoryViewModel
            //            {
            //                CategoryId = item.Id,
            //                ParentId = item2.ParentId,
            //                Title = item.FaTitle,
            //                haveChild = subcategory.Any(c => c.ParentId == item.Id)
            //                ,
            //                CategoryEnTitle = item.EnTitle
            //            });
            //        }
            //    }
            //    else
            //    {
            //        categories.Add(new SearchCategoryViewModel
            //        {
            //            CategoryId = item.Id,
            //            ParentId = null,
            //            Title = item.FaTitle,
            //            haveChild = subcategory.Any(c => c.ParentId == item.Id),
            //            CategoryEnTitle = item.EnTitle
            //        });
            //    }
            //}
            //var r = new Tuple<List<MainMenuShowViewModel>, List<CategotyMain>,
            //    List<SearchCategoryViewModel>, Dictionary<long, List<Brand>>>(value, mainCategories, categories, brands));

            if (typeSystem == EnumTypeSystem.Farnaa)
            {
                return RedirectToAction(nameof(Index));

            }
            else
            {
                return RedirectToAction(nameof(IndexPlus));

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(SiteMenu model)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            ViewBag.ParentList = _SiteMenuService.GetListForAdmin(model.TypeSystem);

            //ViewBag.SeriList = _productSeriService.GetListForAdmin();
            if (model.IconImg != null)
            {
                if (ImageSecurity.Imagevalidator(model.IconImg))
                {
                  //  model.Icon = model.IconImg.SaveImage("", "wwwroot/uploads");
                    model.Icon = _imageUploadService.Upload(model.IconImg);
                    ;
                }
                else
                {
                    ModelState.AddModelError("Image", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }
            Random rnd = new Random();
            model.Code = rnd.Next(10000, 99999);



            bool res = _SiteMenuService.Add(model);
            TempData["res"] = res ? "success" : "faild";
            if (model.TypeSystem == EnumTypeSystem.Farnaa)
            {
                return RedirectToAction(nameof(Index));

            }
            else
            {
                return RedirectToAction(nameof(IndexPlus));

            }
        }


        public IActionResult Delete(int id)
        {
            var data = _SiteMenuService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                if (data.TypeSystem == EnumTypeSystem.Farnaa)
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    return RedirectToAction(nameof(IndexPlus));

                }
            }
            bool res = _SiteMenuService.Delete(data);

            TempData["res"] = res ? "success" : "faild";
            if (data.TypeSystem == EnumTypeSystem.Farnaa)
            {
                return RedirectToAction(nameof(Index));

            }
            else
            {
                return RedirectToAction(nameof(IndexPlus));

            }
        }
        public IActionResult Edit(int id, EnumTypeSystem typeSystem)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            ViewBag.ParentList = _SiteMenuService.GetListForAdmin(typeSystem);
            //ViewBag.SeriList = _productSeriService.GetListForAdmin();

            var data = _SiteMenuService.FindById(id);
            if (data == null)
            {
                TempData["res"] = "faild";
                if (data.TypeSystem == EnumTypeSystem.Farnaa)
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    return RedirectToAction(nameof(IndexPlus));

                }
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(SiteMenu model)
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            ViewBag.ParentList = _SiteMenuService.GetListForAdmin(model.TypeSystem);

            //ViewBag.SeriList = _productSeriService.GetListForAdmin();
            var menu = _SiteMenuService.GetById(model.Id);
            menu.Link = model.Link;
            menu.Type = model.Type;
            menu.CategoryId = model.CategoryId;
            menu.ParentCode = model.ParentCode;
            menu.BrandId = model.BrandId;
            menu.SeriId = model.SeriId;
            menu.Title = model.Title;
            menu.Sort = model.Sort;
            menu.SaveForNextChange = model.SaveForNextChange;
            menu.IsActive = model.IsActive;
 
            if (!ModelState.IsValid)
                return View(model);
            if (model.IconImg != null)
            {
                if (ImageSecurity.Imagevalidator(model.IconImg))
                {
                   // menu.Icon = model.IconImg.SaveImage("", "wwwroot/uploads");
                    menu.Icon = _imageUploadService.Upload(model.IconImg);
                    ;
                }
                else
                {
                    ModelState.AddModelError("Image", "لطفا یک فایل درست انتحاب کنید");
                    return View(model);
                }
            }


            bool res = _SiteMenuService.Update(menu);
            TempData["res"] = res ? "success" : "faild";
            if (model.TypeSystem == EnumTypeSystem.Farnaa)
            {
                return RedirectToAction(nameof(Index));

            }
            else
            {
                return RedirectToAction(nameof(IndexPlus));

            }
        }

    }
}