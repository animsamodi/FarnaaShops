using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EShop.Admin.Helper;
using EShop.Core.ExtensionMethods;
using EShop.Core.Security;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Category;
using EShop.Core.ViewModels.Product;
using EShop.DataLayer.Entities.Brand;
using EShop.DataLayer.Entities.Category;
using EShop.DataLayer.Entities.Product;
using EShop.DataLayer.Entities.Property;
using EShop.DataLayer.Entities.Variety;
using EShop.DataLayer.Enum;
using EShop.Logging.AuditLog.Models;
using Ganss.XSS;
using Infrastructure.ExternalApi.ImageServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
     [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None)]
    public class CatalogController : BaseAdminController
    {
        readonly ICategoryService _categoryService;
        readonly IProductService _productService;
        readonly IBrandService _brandService;
        readonly IPropertyService _propertyService;
        readonly IGalleryService _galleryService;
        readonly IReviewService _reviewService;
        readonly IAttributeRatingService _attributeRatingService;
        readonly IVariantService _variantService;
        IProductSeriService _productSeriService;
        private IImageUploadService _imageUploadService;
        public CatalogController(ICategoryService categoryService,
            IProductService productService,
            IBrandService brandService,
            IGalleryService galleryService, 
            IReviewService reviewService,
            IAttributeRatingService attributeRatingService,
            IPropertyService propertyService,
            IVariantService variantService, IProductSeriService productSeriService,Logging.AuditLog.IAuditService logger
            ,IHttpContextAccessor contextAccessor, IImageUploadService imageUploadService)
       
            : base(logger,contextAccessor)
        {

            _categoryService = categoryService;
            _productService = productService;
            _brandService = brandService;
            _galleryService = galleryService;
            _reviewService = reviewService;
            _attributeRatingService = attributeRatingService;
            _propertyService = propertyService;
            _variantService = variantService;
            _productSeriService = productSeriService;
            _imageUploadService = imageUploadService;
        }

        #region Category
        public IActionResult CategoryList()
        {
            return View(_categoryService.GetCategoriesForAdmin());
        }

        public IActionResult DeleteCategory(int id)
        {
            Category category = _categoryService.FindCategoryById(id);
            if (category == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(CategoryList));
            }

            TempData["res"] = "faild";
            if (_categoryService.DeleteCategory(category))
            {
                _logger.CreateAuditScope(new AuditLog<Category>()
                {
                    Modifier = _userId,
                    Action = Command.Remove,
                    Entite = category,
                });
                TempData["res"] = "success";
            }

            return RedirectToAction(nameof(CategoryList));
        }
        public IActionResult CreateCategory()
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddCategory();
            return View();
        }
        public IActionResult ImportProduct()
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

        public IActionResult CreateCategory(CreateCategoryViewModel category)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryList = _categoryService.GetCategoriesForAddCategory();
                return View(category);
            }
            if (_categoryService.IsExistCategoryTitle(0, category.EnTitle))
            {
                ModelState.AddModelError("EnTitle", "این نام تکراری است");
                ViewBag.CategoryList = _categoryService.GetCategoriesForAdd();
                return View(category);
            }
            string imgname = "";
            if (category.Image != null)
            {
                if (ImageSecurity.Imagevalidator(category.Image))
                {
                    //imgname = category.Image.SaveImage("", "wwwroot/uploads");
                    imgname = _imageUploadService.Upload(category.Image);
                    

                }
                else
                {
                    ModelState.AddModelError("Image", "لطفا یک فایل درست انتحاب کنید");
                    return View(category);
                }
            }
            var htmlsanitaizer = new HtmlSanitizer();
            Category cat = new Category
            {
                Descrption = category.Descrption,
                KeyWord = category.KeyWord,
                FaTitle = htmlsanitaizer.Sanitize(category.FaTitle),
                EnTitle = category.EnTitle,
                ImgName = imgname,
                IsMain = true
            };

            TempData["res"] ="faild";
            if (_categoryService.AddCategory(cat, category.ParentList))
            {
                _logger.CreateAuditScope(new AuditLog<Category>()
                {
                    Modifier = _userId,
                    Action = Command.Create,
                    Entite = cat,
                });
                TempData["res"] = "success";
            }            


            return RedirectToAction(nameof(CategoryList));
        }
        public IActionResult EditCategory(int id)
        {
            Category category = _categoryService.FindCategoryById(id);
            if (category == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(CategoryList));
            }
            EditCategoryViewModel cat = new EditCategoryViewModel
            {
                Id = category.Id,
                Descrption = category.Descrption,
                FaTitle = category.FaTitle,
                EnTitle = category.EnTitle,
                CurrentImage = category.ImgName,
                KeyWord = category.KeyWord

            };
            ViewBag.CategoryList = _categoryService.GetCategoriesForAdd();
            ViewBag.ParentList = _categoryService.GetSubCategory(id);
            return View(cat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult EditCategory(EditCategoryViewModel category)
        {
            if (!ModelState.IsValid)
            {
                category.CurrentImage = category.CurrentImage;
                ViewBag.CategoryList = _categoryService.GetCategoriesForAdd();
                ViewBag.ParentList = category.ParentList;
                return View(category);
            }
            if (_categoryService.IsExistCategoryTitle(category.Id, category.EnTitle))
            {
                ModelState.AddModelError("EnTitle", "این نام تکراری است");
                category.CurrentImage = category.CurrentImage;
                ViewBag.CategoryList = _categoryService.GetCategoriesForAdd();
                ViewBag.ParentList = category.ParentList;
                return View(category);
            }
            string imgname = category.CurrentImage == null ? category.CurrentImage : "";
            if (category.Image != null)
            {

                if (ImageSecurity.Imagevalidator(category.Image))
                {
                    try
                    {
                        imgname.DeleteImage("wwwroot/uploads");

                    }
                    catch (Exception e)
                    {

                    }
                    //imgname = category.Image.SaveImage(imgname, "wwwroot/uploads");
                    imgname = _imageUploadService.Upload(category.Image);
                    ;
                }
                else
                {
                    ModelState.AddModelError("Image", "لطفا یک فایل درست انتحاب کنید");
                    category.CurrentImage = category.CurrentImage;
                    ViewBag.CategoryList = _categoryService.GetCategoriesForAdd();
                    ViewBag.ParentList = category.ParentList;
                    return View(category);
                }
            }

            Category cat = new Category()
            {
                Id = category.Id,
                Descrption = category.Descrption,
                FaTitle = category.FaTitle,
                EnTitle = category.EnTitle,
                ImgName = imgname,
                KeyWord = category.KeyWord,
                IsMain = true
            };

            if (_categoryService.UpdateCategory(cat, category.ParentList))
            {
                _logger.CreateAuditScope(new AuditLog<Category>()
                {
                    Modifier = _userId,
                    Action = Command.Update,
                    Entite = cat,
                });
                TempData["res"] = "success";
            }
            else
            {
                TempData["res"] = "faild";
                ViewBag.CategoryList = _categoryService.GetCategoriesForAdd();
                ViewBag.ParentList = category.ParentList;
                return View(category);
            }

            return RedirectToAction(nameof(CategoryList));
        }
        #endregion

        #region Product
        public IActionResult ProductListContainer()
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAdd();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            ViewBag.SeriList = _productSeriService.GetListForAdmin();

            return View("~/Views/catalog/Product/ProductListContainer.cshtml");
        }
        public IActionResult ProductListContainerReview()
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAdd();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            ViewBag.SeriList = _productSeriService.GetListForAdmin();

            return View("~/Views/catalog/Product/ProductListContainerReview.cshtml");
        }
        public IActionResult ProductListContainerReviewPlus()
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAdd();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            ViewBag.SeriList = _productSeriService.GetListForAdmin();

            return View("~/Views/catalog/Product/ProductListContainerReviewPlus.cshtml");
        }
        public IActionResult ProductListContainerPlus()
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAdd();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            ViewBag.SeriList = _productSeriService.GetListForAdmin();

            return View("~/Views/catalog/Product/ProductListContainerPlus.cshtml");
        }
        public IActionResult ProductListContainerColleague()
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAdd();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            ViewBag.SeriList = _productSeriService.GetListForAdmin();

            return View("~/Views/catalog/Product/ProductListContainerColleague.cshtml");
        }
        public IActionResult ProductList(string searchtext = "", int brnad = 0, int category = 0, int state = 0, int pagenumber = 1)
        {
            var content = _productService.GetProductsForAdmin(searchtext, pagenumber, brnad, category, state, 15);
            ViewBag.count = content.Item1;
            ViewBag.searchtext = searchtext;
            ViewBag.brnad = brnad;
            ViewBag.category = category;
            ViewBag.state = state;
            ViewBag.PageNumber = pagenumber;

            return View("~/Views/catalog/Product/ProductList.cshtml", content.Item2);
        }
        public IActionResult ProductListReview(string searchtext = "", int brnad = 0, int category = 0, int state = 0, int pagenumber = 1)
        {
            var content = _productService.GetProductsForAdmin(searchtext, pagenumber, brnad, category, state, 15);
            ViewBag.count = content.Item1;
            ViewBag.searchtext = searchtext;
            ViewBag.brnad = brnad;
            ViewBag.category = category;
            ViewBag.state = state;
            ViewBag.PageNumber = pagenumber;

            return View("~/Views/catalog/Product/ProductListReview.cshtml", content.Item2);
        }
        public IActionResult ProductListReviewPlus(string searchtext = "", int brnad = 0, int category = 0, int state = 0, int pagenumber = 1)
        {
            var content = _productService.GetProductsForAdmin(searchtext, pagenumber, brnad, category, state, 15);
            ViewBag.count = content.Item1;
            ViewBag.searchtext = searchtext;
            ViewBag.brnad = brnad;
            ViewBag.category = category;
            ViewBag.state = state;
            ViewBag.PageNumber = pagenumber;

            return View("~/Views/catalog/Product/ProductListReviewPlus.cshtml", content.Item2);
        }
        public IActionResult ProductListColleague(string searchtext = "", int brnad = 0, int category = 0, int state = 0, int pagenumber = 1)
        {
            var content = _productService.GetProductsForAdminColleague(searchtext, pagenumber, brnad, category, state, 15);
            ViewBag.count = content.Item1;
            ViewBag.searchtext = searchtext;
            ViewBag.brnad = brnad;
            ViewBag.category = category;
            ViewBag.state = state;
            ViewBag.PageNumber = pagenumber;

            return View("~/Views/catalog/Product/ProductListColleague.cshtml", content.Item2);
        }
        public IActionResult ProductListPlus(string searchtext = "", int brnad = 0, int category = 0, int state = 0, int pagenumber = 1)
        {
            var content = _productService.GetProductsForAdminPlus(searchtext, pagenumber, brnad, category, state, 15);
            ViewBag.count = content.Item1;
            ViewBag.searchtext = searchtext;
            ViewBag.brnad = brnad;
            ViewBag.category = category;
            ViewBag.state = state;
            ViewBag.PageNumber = pagenumber;

            return View("~/Views/catalog/Product/ProductListPlus.cshtml", content.Item2);
        }
        public IActionResult CreateProduct()
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            //ViewBag.SeriList = _productSeriService.GetListForAdmin();
            AddProductViewModel product = new AddProductViewModel();


            product.drpColors = _variantService.GetListProductOption().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            product.drpGuarantees = _variantService.GetListGuarantee().Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();

            return View("~/Views/catalog/Product/CreateProduct.cshtml", product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CreateProduct(AddProductViewModel product)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
                ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
                //ViewBag.SeriList = _productSeriService.GetListForAdmin();
                product.drpColors = _variantService.GetListProductOption().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                product.drpGuarantees = _variantService.GetListGuarantee().Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();

                return View("~/Views/catalog/Product/CreateProduct.cshtml", product);
            }
            string imgname = "";
            if (product.ImgName != null)
            {
                if (ImageSecurity.Imagevalidator(product.ImgName))
                {
                    //imgname = product.ImgName.SaveImage("", "wwwroot/uploads");
                     imgname = _imageUploadService.Upload(product.ImgName);
                    //string oldpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", imgname);
                    //string newpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/tumb", imgname);
                    //string newpath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/main", imgname);
                }
                else
                {
                    ModelState.AddModelError("ImgName", "لطفا یک فایل درست انتحاب کنید");
                    ViewBag.CategoryList = _categoryService.GetCategoriesForAdd();
                    ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
                    //ViewBag.SeriList = _productSeriService.GetListForAdmin();
                    product.drpColors = _variantService.GetListProductOption().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                    product.drpGuarantees = _variantService.GetListGuarantee().Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();

                    return View("~/Views/catalog/Product/CreateProduct.cshtml", product);
                }
            }

            Product p = new Product
            {
                FaTitle = product.FaTitle.Trim(),
                EnTitle = product.EnTitle?.Trim() ?? product.FaTitle.Trim(),
                SpecCode = product.SpecCode,
                CommodityId = product.CommodityId,
                ImgName = imgname,
                CategoryId = product.CategoryID,
                BrandId = product.BrandID,
                SeriId  =   product.SeriId,
                IsPublished = product.IsPublished,
                CreateDate = DateTime.Now,
                LastUpdateDate = DateTime.Now,
                KeyWord = product.KeyWord,
                MetaTitle = product.MetaTitle,
                MetaDescription = product.MetaDescription,
                MetaKeywords = product.MetaKeywords,
                Canonical = product.Canonical,
                HeaderTag = product.HeaderTag,
                IsShowPopUp = product.IsShowPopUp,
                PopUpContent = product.PopUpContent,
                Schema = product.Schema,
                Ram = product.Ram,
                Rom = product.Rom,
                IsAvailablesoon = product.IsAvailablesoon
            };
            if (p.IsPublished)
                p.IsAvailablesoon = false;
            var productid = _productService.AddProduct(p);
            if (productid > 0)
            {
                _logger.CreateAuditScope(new AuditLog<Product>()
                {
                    Modifier = _userId,
                    Action = Command.Create,
                    Entite = p,
                });
            }            

            List<long> parentlist = _categoryService.GetParentCategory(product.CategoryID);
            parentlist.Add(product.CategoryID);
            List<ProductCategory> productCategory = new List<ProductCategory>();
            List<BrandCategory> brandCategories = new List<BrandCategory>();
            foreach (var item in parentlist)
            {
                productCategory.Add(new ProductCategory
                {
                    CategoryId = item,
                    ProductId = productid
                });
                brandCategories.Add(new BrandCategory
                {
                    BrandId = product.BrandID,
                    CategoryId = item
                });
            }
            bool res = _categoryService.AddProductCategories(productCategory);
            _categoryService.AddBrandCategories(brandCategories);
            if (product.GuaranteesIds != null)
                foreach (var guarantee in product.GuaranteesIds)
                {
                    if (product.ColorsIds != null)
                        foreach (var color in product.ColorsIds)
                        {
                            var isExit =
                                _variantService.CheckVariantExist(productid, guarantee, color);
                            if (!isExit)
                            {
                                Variant variant = new Variant
                                {
                                    ShopCount = 0,
                                    VoteCount = 0,
                                    TotallySatisfied = 0,
                                    Satisfied = 0,
                                    Neutral = 0,
                                    DisSatisfied = 0,
                                    TotallyDisSatisfied = 0,
                                    ReserveCount = 0,
                                    SellerId = _variantService.GetSellerId(),
                                    Count = 0,
                                    CreateDate = DateTime.Now,
                                    IsDelete = false,
                                    MaxOrderCount = 0,
                                    MaxOrderCountColleague = 10,
                                    SepcialPrice = 0,
                                    Price = 0,
                                    GuaranteeId = guarantee,
                                    ProductOptionId = color,
                                    ProductId = productid
                                };


                                if (ModelState.IsValid)
                                    _variantService.AddVariant(variant);
                            }
                        }
                }

            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction("ProductListContainer");
        }

        public IActionResult ChangeProductCategory()
        {
            var products = _productService.GetAllProduct();
            var cats = _categoryService.GetAllCategory();
            _brandService.RemoveAllBrandCategories();
            foreach (var product in products)
            {
                var catId = _productService.GetProductCategoryId(product.Id);
                var cat = cats.FirstOrDefault(c => c.Id == catId);
                List<ProductCategory> productCategory = new List<ProductCategory>();
                List<BrandCategory> brandCategories = new List<BrandCategory>();

                var isExistProductCategory = _categoryService.IsExistProductCategory(catId, product.Id);
                if (!isExistProductCategory)
                {
                    productCategory.Add(new ProductCategory
                    {
                        CategoryId = catId,
                        ProductId = product.Id,
                        Category = cat,
                        Product = product
                    });
                   
                }
 brandCategories.Add(new BrandCategory
                    {
                        BrandId = product.BrandId,
                        CategoryId = catId
                    });
                List<long> parentlist = _categoryService.GetParentCategory(catId);

                foreach (var item in parentlist)
                {
                    isExistProductCategory = _categoryService.IsExistProductCategory(item, product.Id);
                    if (!isExistProductCategory)
                    {
cat = cats.FirstOrDefault(c => c.Id == item);

                    productCategory.Add(new ProductCategory
                    {
                        CategoryId = item,
                        ProductId = product.Id,
                        Category = cat,
                        Product = product
                    });
                  
                    }  brandCategories.Add(new BrandCategory
                    {
                        BrandId = product.BrandId,
                        CategoryId = item
                    });
                }

                if (productCategory.Count > 0)
                {
                }
                if (brandCategories.Count > 0)
                {
                    bool res = _categoryService.AddBrandCategories(brandCategories);
                }
            }

            TempData["res"] = "success";
            return RedirectToAction("Index","Setting");
        }
 


        public IActionResult EditProduct(long id)
        {
            var product = _productService.FindProductById(id);
            if (product == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction("ProductListContainer");
            }
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            //ViewBag.SeriList = _productSeriService.GetListForAdmin();



            AddProductViewModel slvm = new AddProductViewModel()
            {
                Id = product.Id,
                SpecCode = product.SpecCode,
                BrandID = product.BrandId,
                SeriId = product.SeriId,
                CategoryID = product.CategoryId,
                EnTitle = product.EnTitle ?? product.FaTitle,
                FaTitle = product.FaTitle,
                Img = product.ImgName,
                IsPublished = product.IsPublished,
                CommodityId = product.CommodityId,
                KeyWord = product.KeyWord,
                MetaTitle = product.MetaTitle,
                MetaDescription = product.MetaDescription,
                MetaKeywords = product.MetaKeywords,
                Canonical = product.Canonical,
                HeaderTag = product.HeaderTag,
                IsShowPopUp = product.IsShowPopUp,
                PopUpContent = product.PopUpContent,
                Schema = product.Schema,
                FAQSchema = product.FAQSchema,
                Ram = product.Ram,
                Rom = product.Rom,
                IsAvailablesoon = product.IsAvailablesoon
            };

            List<long> colorsIds = _variantService.GetListProductProductOption(product.Id);
            List<long> guaranteesIds = _variantService.GetListProductGuarantees(product.Id);
            slvm.drpColors = _variantService.GetListProductOption().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            slvm.drpGuarantees = _variantService.GetListGuarantee().Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();
            slvm.GuaranteesIds = guaranteesIds.ToArray();
            slvm.ColorsIds = colorsIds.ToArray();
            return View("~/Views/catalog/Product/EditProduct.cshtml", slvm);

        }


        public IActionResult DeleteProduct(long id)
        {
            var res = _productService.DeleteProduct(id);

            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction("ProductListContainer");


        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult EditProduct(AddProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
                ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
                //ViewBag.SeriList = _productSeriService.GetListForAdmin();
                product.drpColors = _variantService.GetListProductOption().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                product.drpGuarantees = _variantService.GetListGuarantee().Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();

                return View("~/Views/catalog/Product/EditProduct.cshtml", product);

            }
            string filename = product.Img;
            if (product.ImgName != null)
            {
                if (ImageSecurity.Imagevalidator(product.ImgName))
                {
                    try
                    {

                    }
                    catch (Exception e)
                    {
                    }

                    filename = "";
                    //filename = product.ImgName.SaveImage(filename, "wwwroot/uploads");
                    //string oldpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", filename);
                    //string newpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/tumb", filename);
                    //string newpath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/main", filename);
                                   filename = _imageUploadService.Upload(product.ImgName);
                                  
                }
                else
                {
                    ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
                    ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
                    //ViewBag.SeriList = _productSeriService.GetListForAdmin();
                    product.drpColors = _variantService.GetListProductOption().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                    product.drpGuarantees = _variantService.GetListGuarantee().Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();

                    ModelState.AddModelError("DesktopImg", "لطفا یک فایل درست انتحاب کنید");
                    return View("~/Views/catalog/Product/EditProduct.cshtml", product);
                }
            }

            Product p = new Product
            {
                Id = product.Id.Value,
                FaTitle = product.FaTitle.Trim(),
                EnTitle = product.EnTitle?.Trim() ?? product.FaTitle.Trim(),
                SpecCode = product.SpecCode,
                CommodityId = product.CommodityId,
                ImgName = filename,
                CategoryId = product.CategoryID,
                BrandId = product.BrandID,
                SeriId = product.SeriId,
                IsPublished = product.IsPublished,
                LastUpdateDate = DateTime.Now,
                KeyWord = product.KeyWord,
                MetaTitle = product.MetaTitle,
                MetaDescription = product.MetaDescription,
                MetaKeywords = product.MetaKeywords,
                Canonical = product.Canonical,
                HeaderTag = product.HeaderTag,
                IsShowPopUp = product.IsShowPopUp,
                PopUpContent = product.PopUpContent,
                Schema = product.Schema,
                FAQSchema =product.FAQSchema,
                Ram = product.Ram,
                Rom = product.Rom,
                IsAvailablesoon = product.IsAvailablesoon
            };
            if (p.IsPublished)
                p.IsAvailablesoon = false;
            TempData["res"] = "faild";
            if (_productService.UpdateProduct(p))
            {
                _logger.CreateAuditScope(new AuditLog<Product>()
                {
                    Modifier = _userId,
                    Action = Command.Update,
                    Entite =p,
                });
                TempData["res"] = "success";
            }


            _categoryService.RemoveProductCategories(p.Id);
            List<long> parentlist = _categoryService.GetParentCategory(product.CategoryID);
            parentlist.Add(product.CategoryID);
            List<ProductCategory> productCategory = new List<ProductCategory>();
            List<BrandCategory> brandCategories = new List<BrandCategory>();
            foreach (var item in parentlist)
            {
                productCategory.Add(new ProductCategory
                {
                    CategoryId = item,
                    ProductId = p.Id
                });
                brandCategories.Add(new BrandCategory
                {
                    BrandId = product.BrandID,
                    CategoryId = item
                });
            }
             _categoryService.AddProductCategories(productCategory);
            _categoryService.AddBrandCategories(brandCategories);
            var LastGuarantessColors = _variantService.GetLastVariantsByProductId(product.Id.Value);
            if (product.GuaranteesIds != null)
                foreach (var guarantee in product.GuaranteesIds)
                {
                    if (product.ColorsIds != null)
                        foreach (var color in product.ColorsIds)
                        {
                            var varient =
                                _variantService.GetVariantExist(product.Id.Value, guarantee, color);
                            if (varient == null)
                            {
                                Variant variant = new Variant
                                {
                                    ShopCount = 0,
                                    VoteCount = 0,
                                    TotallySatisfied = 0,
                                    Satisfied = 0,
                                    Neutral = 0,
                                    DisSatisfied = 0,
                                    TotallyDisSatisfied = 0,
                                    ReserveCount = 0,
                                    SellerId = _variantService.GetSellerId(),
                                    Count = 0,
                                    CreateDate = DateTime.Now,
                                    IsDelete = false,
                                    MaxOrderCount = 0,
                                    SepcialPrice = 0,
                                    Price = 0,
                                    GuaranteeId = guarantee,
                                    ProductOptionId = color,
                                    ProductId = product.Id.Value
                                };


                                if (ModelState.IsValid)
                                    _variantService.AddVariant(variant);
                            }
                            else
                            {
                                LastGuarantessColors.Remove(varient);
                            }
                        }
                }


            _variantService.DeleteLastVarients(LastGuarantessColors);

            return RedirectToAction("ProductListContainer");
        }
        #endregion

        #region Property
        public IActionResult PropertyGroupList(int pagenumber = 1)
        {
            ViewBag.pagenumber = pagenumber;
            ViewBag.Pagecount = _propertyService.GetPropertyGroupCount();
            return View("~/Views/catalog/Property/PropertyGroupList.cshtml", _propertyService.GetPropertyGroup(pagenumber));
        }


        public IActionResult PropertyNameList()
        {
            return View("~/Views/catalog/Property/PropertyNameList.cshtml", _propertyService.GetPropertyName());
        }
        public IActionResult CreatePropertyGroup()
        {

            return View("~/Views/catalog/Property/CreatePropertyGroup.cshtml");
        }
        public IActionResult CreatePropertyName()
        {
            ViewBag.Groups = _propertyService.GetPropertyGroupForAddNames();
            ViewBag.CategoryList = _categoryService.GetCategoriesForAdd();
            return View("~/Views/catalog/Property/CreatePropertyName.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CreatePropertyGroup(PropertyGroup propertyGroup)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/catalog/Property/CreatePropertyGroup.cshtml", propertyGroup);
            }

            var id = _propertyService.AddPropertyGroup(propertyGroup);

            _logger.CreateAuditScope(new AuditLog<PropertyGroup>()
            {
                Modifier = _userId,
                Action = Command.Create,
                Entite = propertyGroup,
            });

            if (id > 0)
            {
                TempData["res"] = "success";
                return RedirectToAction(nameof(PropertyGroupList));
            }
            TempData["res"] = "faild";
            return RedirectToAction(nameof(PropertyGroupList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CreatePropertyName(PropertyName names, List<int> category)
        {
            ViewBag.Groups = _propertyService.GetPropertyGroupForAddNames();
            ViewBag.Category = _categoryService.GetCategoriesForAdd();

            if (!ModelState.IsValid)
            {
                return View("~/Views/catalog/Property/CreatePropertyName.cshtml", names);
            }
            string iconName = "";
             if (ImageSecurity.Imagevalidator(names.IconImg))
            {
                //names.Icon = names.IconImg.SaveImage("", "wwwroot/uploads");
                names.Icon = _imageUploadService.Upload(names.IconImg);
                ;
            }
            else
            {
                ModelState.AddModelError("Icon", "لطفا یک فایل درست انتحاب کنید");
                return View("~/Views/catalog/Property/CreatePropertyName.cshtml", names);
            }
            _logger.CreateAuditScope(new AuditLog<PropertyName>()
            {
                Modifier = _userId,
                Action = Command.Create,
                Entite = names,
            });

            long id = _propertyService.AddPropertyName(names);
            
            if (id > 0)
            {
                List<PropertyCategory> propertyCategory = new List<PropertyCategory>();
                foreach (var item in category)
                {
                    propertyCategory.Add(new PropertyCategory
                    {
                        PropertyNameId = id,
                        CategoryId = item
                    });
                }

                bool res = _propertyService.AddNameCategory(propertyCategory);
                TempData["res"] = res ? "success" : "faild";
                return RedirectToAction(nameof(PropertyNameList));
            }
            TempData["res"] = "faild";
            return RedirectToAction(nameof(PropertyNameList));
        }

        public IActionResult EditPropertyName(int id)
        {
            PropertyName name = _propertyService.GetPropertyNameForEdit(id);
            name.CurrentIconName = name.Icon;
            if (name == null)
            {
                TempData["res"] = "faild";
                RedirectToAction(nameof(PropertyNameList));
            }
            ViewBag.Groups = _propertyService.GetPropertyGroupForAddNames();
            if (name != null)
            {
                List<GetCategoryForAddViewModel> exceptCategory = name.PropertyCategories.Select(c =>
                    new GetCategoryForAddViewModel
                    {
                        Id = c.CategoryId,
                        Title = c.Category.FaTitle
                    }).ToList();
                var catList = _categoryService.GetCategoriesForAdd();
                ViewBag.CategoryList = catList.Except(exceptCategory);
            }
            else
            {
                ViewBag.CategoryList = _categoryService.GetCategoriesForAdd();
            }

            return View("~/Views/catalog/Property/EditPropertyName.cshtml", name);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult EditPropertyName(PropertyName names, List<int> category)
        {
            ViewBag.Groups = _propertyService.GetPropertyGroupForAddNames();
            ViewBag.CategoryList = _categoryService.GetCategoriesForAdd();

            if (!ModelState.IsValid)
            {
                return View("~/Views/catalog/Property/EditPropertyName.cshtml", names);
            }
            if (names.IconImg != null)
            {
                if (ImageSecurity.Imagevalidator(names.IconImg))
                {
                    //names.Icon.DeleteImage("wwwroot/uploads");
                    //names.Icon = names.IconImg.SaveImage(null, "wwwroot/uploads");
                    names.Icon = _imageUploadService.Upload(names.IconImg);
                    ;
                }
                else
                {
                    ModelState.AddModelError("Icon", "لطفا یک فایل درست انتحاب کنید");
                    return View("~/Views/catalog/Property/EditPropertyName.cshtml", names);
                }
            }
            TempData["res"] = "faild";

            if (_propertyService.EditPropertyName(names))
            {
                _logger.CreateAuditScope(new AuditLog<PropertyName>()
                {
                    Entite = names,
                    Action = Command.Update,
                    Modifier = _userId
                });

                _propertyService.RemoveNameCategoryByPropertyNameId(names.Id);
                List<PropertyCategory> propertyCategory = new List<PropertyCategory>();
                foreach (var item in category)
                {
                    propertyCategory.Add(new PropertyCategory
                    {
                        PropertyNameId = names.Id,
                        CategoryId = item
                    });
                }

                bool res = _propertyService.AddNameCategory(propertyCategory);
                TempData["res"] = res ? "success" : "faild";
                return RedirectToAction(nameof(PropertyNameList));
            }
            TempData["res"] =  "faild";
            return RedirectToAction(nameof(PropertyNameList));
        }

        public IActionResult PropertyValueList()
        {
            return View("~/Views/catalog/Property/PropertyValueList.cshtml", _propertyService.GetPropertyValues());
        }

        public IActionResult CreatePropertyValue()
        {
            ViewBag.names = _propertyService.GetPropertyName();
            return View("~/Views/catalog/Property/CreatePropertyValue.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CreatePropertyValue(PropertyValue values)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.names = _propertyService.GetPropertyName();
                return View(values);
            }

            TempData["res"] = "faild";
            if (_propertyService.AddPropertyValue(values) > 0)
            {
                _logger.CreateAuditScope(new AuditLog<PropertyValue>()
                {
                    Entite = values,
                    Action = Command.Create,
                    Modifier = _userId
                });
                TempData["res"] = "success";
            }            

            return RedirectToAction(nameof(PropertyValueList));

        }

        public IActionResult ProductProperty(int id)
        {
            var product = _productService.FindProductById(id);
            ViewBag.ProductName = product.FaTitle;
            ViewBag.ProductImage = product.ImgName;
            long catid = _productService.GetProductCategoryId(id);
            var query = _propertyService.GetProductPropertyForAdd(catid, id);
            ViewBag.productid = id;
            ViewBag.categoryid = catid;
            return View("~/Views/catalog/Property/ProductProperty.cshtml", query);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult ProductProperty(List<int> nameid, List<string> value, long productId, long categoryId, string oldValue)
        {
            if (nameid.Count != value.Count)
            {
                long categoryid = _productService.GetProductCategoryId(productId);
                var query = _propertyService.GetProductPropertyForAdd(categoryid, productId);
                ViewBag.productid = productId;
                ViewBag.categoryid = categoryid;
                return View("~/Views/catalog/Property/ProductProperty.cshtml", query);
            }

            List<PropertyValueForAddViewModel> oldlist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PropertyValueForAddViewModel>>(oldValue);

            List<ProductProperty> addlist = new List<ProductProperty>();
            List<PropertyValue> editlist = new List<PropertyValue>();
            List<ProductProperty> deletelist = new List<ProductProperty>();
            List<PropertyValue> deleteproperty = new List<PropertyValue>();
            for (int i = 0; i < nameid.Count; i++)
            {
                int type = _propertyService.GetPropertyNameType(nameid[i]);
                if (type == 1 || type == 2)
                {
                    if (int.TryParse(value[i], out int t) && int.Parse(value[i]) > 0)
                    {
                        if (_propertyService.CheckValueForName(nameid[i], int.Parse(value[i])) &&
                            _propertyService.CheckNameForCategory(nameid[i], categoryId))
                        {
                            if (!oldlist.Where(o => o.ValueId == int.Parse(value[i])).Any())
                            {
                                addlist.Add(new ProductProperty
                                {
                                    ProductId = productId,
                                    PropertyValueId = int.Parse(value[i])
                                });
                            }
                        }
                    }
                }
                else if (type == 3 || type == 4)
                {

                    PropertyValueForAddViewModel temp = oldlist.Where(o => o.NameId == nameid[i]).FirstOrDefault();
                    if (temp != null)
                    {//TODO Check
                        if (!String.IsNullOrEmpty(value[i]))
                        {
                            editlist.Add(new PropertyValue
                            {
                                Id = temp.ValueId,
                                Value = value[i],
                                PropertyNameId = nameid[i]
                            });
                        }
                        else
                        {
                            deletelist.Add(new ProductProperty
                            {
                                Id = temp.ProductProertyId
                            });
                            deleteproperty.Add(new PropertyValue
                            {
                                Id = temp.ValueId
                            });
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(value[i]))
                        {
                            PropertyValue propertyValue = new PropertyValue
                            {
                                Value = value[i],
                                PropertyNameId = nameid[i]
                            };

                            long newid = _propertyService.AddPropertyValue(propertyValue);
                            if (newid > 0)
                            {
                                addlist.Add(new ProductProperty
                                {
                                    ProductId = productId,
                                    PropertyValueId = newid
                                });
                            }
                        }
                    }
                    oldlist.Remove(temp);
                }
                else if (type == 5)
                {
                    PropertyValueForAddViewModel temp = oldlist.Where(o => o.NameId == nameid[i]).FirstOrDefault();
                    if (temp != null)
                    {
                        if (!String.IsNullOrEmpty(value[i]))
                        {
                            if (value[i] != temp.Value.Replace("\r", "").Replace("\n", ""))
                            {
                                var valueIdIsExist = _propertyService.GetValueIdByValue(nameid[i], value[i]);
                                if (valueIdIsExist != 0)
                                {
                                    addlist.Add(new ProductProperty
                                    {
                                        ProductId = productId,
                                        PropertyValueId = valueIdIsExist
                                    });
                                }
                                else
                                {
                                    PropertyValue propertyValue = new PropertyValue
                                    {
                                        Value = value[i],
                                        PropertyNameId = nameid[i]
                                    };

                                    long newid = _propertyService.AddPropertyValue(propertyValue);
                                    if (newid > 0)
                                    {
                                        addlist.Add(new ProductProperty
                                        {
                                            ProductId = productId,
                                            PropertyValueId = newid
                                        });
                                    }
                                }
                                deletelist.Add(new ProductProperty
                                {
                                    Id = temp.ProductProertyId
                                });
                            }



                        }
                        else
                        {
                            deletelist.Add(new ProductProperty
                            {
                                Id = temp.ProductProertyId
                            });
                            deleteproperty.Add(new PropertyValue
                            {
                                Id = temp.ValueId
                            });
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(value[i]))
                        {
                            var valueIdIsExist = _propertyService.GetValueIdByValue(nameid[i], value[i]);
                            if (valueIdIsExist != 0)
                            {
                                addlist.Add(new ProductProperty
                                {
                                    ProductId = productId,
                                    PropertyValueId = valueIdIsExist
                                });
                            }
                            else
                            {
                                PropertyValue propertyValue = new PropertyValue
                                {
                                    Value = value[i],
                                    PropertyNameId = nameid[i]
                                };

                                long newid = _propertyService.AddPropertyValue(propertyValue);
                                if (newid > 0)
                                {
                                    addlist.Add(new ProductProperty
                                    {
                                        ProductId = productId,
                                        PropertyValueId = newid
                                    });
                                }
                            }
                        }
                    }
                    oldlist.Remove(temp);
                }
            }

            List<long> delValue = value.Select(s => long.TryParse(s, out long n) ? n : (long?)null).Where(n => n.HasValue)
                .Select(n => n.Value).ToList();
            foreach (var item in delValue)
            {
                deletelist.Add(new DataLayer.Entities.Product.ProductProperty
                {
                    Id = oldlist.Where(o => o.ValueId == item).Select(o => o.ProductProertyId).FirstOrDefault()
                });
            }

            if (addlist.Count > 0)
            {
                if (_propertyService.AddProductPropertyList(addlist))
                {
                    _logger.CreateAuditScope(new AuditLog<List<ProductProperty>>()
                    {
                        Entite = addlist,
                        Action = Command.Create,
                        Modifier = _userId
                    });
                }
            }

            if (deletelist.Count > 0)
            {
                if (_propertyService.DeleteProductPropertyList(deletelist))
                {
                    _logger.CreateAuditScope(new AuditLog<List<ProductProperty>>()
                    {
                        Entite = deletelist,
                        Action = Command.Remove,
                        Modifier = _userId
                    });
                }
            }

            if (deleteproperty.Count > 0)
            {
                if (_propertyService.DeletePropertyValueList(deleteproperty))
                {
                    _logger.CreateAuditScope(new AuditLog<List<PropertyValue>>()
                    {
                        Entite = deleteproperty,
                        Action = Command.Remove,
                        Modifier = _userId
                    });
                }                
            }

            if (editlist.Count > 0)
            {
                if (_propertyService.EditPropertyValueList(editlist))
                {
                    _logger.CreateAuditScope(new AuditLog<List<PropertyValue>>()
                    {
                        Entite = editlist,
                        Action = Command.Update,
                        Modifier = _userId
                    });
                }                
            }
            return RedirectToAction(nameof(ProductListContainer));
        }
        #endregion

        #region Variants
        public IActionResult ListVariants()
        {
            return View(_variantService.GetListVariants());
        }
        public IActionResult DeleteVariantPromotion(long id, long variantId)
        {
            if (_variantService.DeleteVariantPromotion(id))
            {
                _logger.CreateAuditScope(new AuditLog<long>()
                {
                    Entite = id,
                    Action = Command.Remove,
                    Modifier = _userId
                });
            }
            return RedirectToAction("VariantPromotions", new { id = variantId });
        }
        public IActionResult VariantPromotions(long id)
        {
            ViewBag.VariantId = id;
            return View(_variantService.GetListVariantPromotions(id));
        }
        public IActionResult ProductVariants(long id)
        {
            var product = _productService.FindProductById(id);
            ViewBag.ProductName = product.FaTitle;
            ViewBag.ProductImage = product.ImgName;
            ViewBag.productid = id;

            return View(_variantService.GetAllVariantsByProductId(id));
        }
        public IActionResult DeleteProductVariant(long id, long productId)
        {
            TempData["res"] = "faild";

            if (_variantService.DeleteVariant(id))
            {
                _logger.CreateAuditScope(new AuditLog<long>()
                {
                    Entite = id,
                    Action = Command.Remove,
                    Modifier = _userId
                });
                TempData["res"] = "success";
            }

            return RedirectToAction("ProductVariants", new { id = productId });
        }
        [AllowAnonymous]
        public IActionResult Test()
        {
            return View();
        }
        public IActionResult CreateVariantPromotion(long id)
        {
            ViewBag.VariantId = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CreateVariantPromotion(VariantPromotionsViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            TempData["res"] = "faild";

            var res = _variantService.AddVariantPromotion(model);
            if (res > 0)
            {
                _logger.CreateAuditScope(new AuditLog<VariantPromotionsViewModel>()
                {
                    Entite = model,
                    Action = Command.Create,
                    Modifier = _userId
                });
                TempData["res"] = "success";

                return RedirectToAction("VariantPromotions", new { id = res });

            }

            ViewBag.VariantId = model.VariantId;
            return View(model);
        }

        public IActionResult CreateProductVariant(long id)
        {
            ViewBag.Guaranteelist = _variantService.GetListGuarantee();
            ViewBag.ProductOptionlist = _variantService.GetListProductOption();

            ViewBag.productid = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CreateProductVariant(Variant variant)
        {
            var isExit =
                _variantService.CheckVariantExist(variant.ProductId, variant.GuaranteeId, variant.ProductOptionId);
            if (isExit)
            {
                TempData["ErrorMessage"] = "شما قبلا برای این محصول با این رنگ موجودی ثبت کرده اید";
                ViewBag.Guaranteelist = _variantService.GetListGuarantee();
                ViewBag.ProductOptionlist = _variantService.GetListProductOption();

                ViewBag.productid = variant.ProductId;
                return View(variant);
            }
            variant.ShopCount = variant.Count;
            variant.VoteCount = 0;
            variant.TotallySatisfied = 0;
            variant.Satisfied = 0;
            variant.Neutral = 0;
            variant.DisSatisfied = 0;
            variant.TotallyDisSatisfied = 0;
            variant.ReserveCount = 0;
            variant.SellerId = _variantService.GetSellerId();
            variant.Id = 0;
            if (!ModelState.IsValid)
                return View(variant);


            TempData["res"] = "faild";
            if (_variantService.AddVariant(variant))
            {
                _logger.CreateAuditScope(new AuditLog<Variant>()
                {
                    Entite = variant,
                    Action = Command.Create,
                    Modifier = _userId
                });
                TempData["res"] = "success";
                return RedirectToAction("ProductVariants", new { id = variant.ProductId });
            }


            ViewBag.Guaranteelist = _variantService.GetListGuarantee();
            ViewBag.ProductOptionlist = _variantService.GetListProductOption();
            ViewBag.productid = variant.ProductId;
            return View(variant);
        }



        public IActionResult EditProductVariant(long id)
        {
            Variant variant = _variantService.GetVariantsId(id);
            if (variant != null)
            {
                ViewBag.Guaranteelist = _variantService.GetListGuarantee();
                ViewBag.ProductOptionlist = _variantService.GetListProductOption();
                ViewBag.productid = variant.ProductId;

                return View(variant);
            }
            TempData["res"] = "faild";
            return RedirectToAction("ProductListContainer");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult EditProductVariant(Variant variant)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Guaranteelist = _variantService.GetListGuarantee();
                ViewBag.ProductOptionlist = _variantService.GetListProductOption();
                ViewBag.productid = variant.ProductId;
                return View(variant);
            }

            TempData["res"] = "faild";
            if (_variantService.EditVariant(variant))
            {
                _logger.CreateAuditScope(new AuditLog<Variant>()
                {
                    Entite = variant,
                    Action = Command.Update,
                    Modifier = _userId
                });
                TempData["res"] = "success";
            }            

            return RedirectToAction("ProductVariants", new { id = variant.ProductId });
        }
        #endregion

        #region Gallery
        public IActionResult GalleryList(long id)
        {
            ViewBag.productid = id;
            var product = _productService.FindProductById(id);
            ViewBag.ProductName = product.FaTitle;
            ViewBag.ProductImage = product.ImgName;
            ViewBag.ProductOptions = _variantService.GetProductProductOptions(product.Id);
            return View(_galleryService.GetProductImagesForAdmin(id));
        }

        public IActionResult CreateProductImage(IFormFile imagname, long productId,long productOptionId)
        {
            string imgname = "";
            long id = productId;
            var productOption = new ProductOption();
            if (imagname != null)
            {
                if (ImageSecurity.Imagevalidator(imagname))
                {
                    //imgname = imagname.SaveImage("", "wwwroot/uploads");
                    //string oldpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", imgname);
                    //string newpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/tumb", imgname);
                    imgname = _imageUploadService.Upload(imagname);
                    ;

                    ProductImage productImage = new ProductImage
                    {
                        ImgName = imgname,
                        ProductId = id,
                    };
                    if (productOptionId != 0)
                    {
                        productOption = _variantService.FindColorById(productOptionId);
                        productImage.ProductOptionId = productOption.Id;
                    }

                    if (_galleryService.AddProductImage(productImage))
                    {
                        _logger.CreateAuditScope(new AuditLog<ProductImage>()
                        {
                            Entite = productImage,
                            Action = Command.Create,
                            Modifier = _userId
                        });
                    }
                    
                }
                else
                {
                    return RedirectToAction(nameof(GalleryList), new { id = id });
                }
            }
            return RedirectToAction(nameof(GalleryList), new { id = id });
        }

        public IActionResult DeleteGalleryImage(long id, long productId)
        {
            ProductImage image = _galleryService.FindImageById(id);
            if (image == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(GalleryList), new { id = productId });
            }

            TempData["res"] = "faild";
            if (_galleryService.DeleteImage(image))
            {
                _logger.CreateAuditScope(new AuditLog<ProductImage>()
                {
                    Entite = image,
                    Action = Command.Remove,
                    Modifier = _userId
                });
                TempData["res"] = "success";
            }            
            return RedirectToAction(nameof(GalleryList), new { id = productId });
        }
        #endregion

        #region Brand
        public IActionResult BrandList()
        {
            return View(_brandService.GetBrandListForAdmin());
        }
        public IActionResult CreateBrand() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CreateBrand(CreateBrandViewModel brand)
        {
            if (!ModelState.IsValid)
                return View(brand);
            string imgname = "";
            if (brand.Image != null)
            {
                if (ImageSecurity.Imagevalidator(brand.Image))
                {
                    //imgname = brand.Image.SaveImage("", "wwwroot/uploads");
                    imgname = _imageUploadService.Upload(brand.Image);
                    ;
                }
                else
                {
                    ModelState.AddModelError("Image", "لطفا یک فایل درست انتحاب کنید");
                    return View(brand);
                }
            }
            Brand brand1 = new Brand
            {
                ImgName = imgname,
                FaTitle = brand.FaTitle,
                EnTitle = brand.EnTitle,
                Descrption = brand.Descrption,
                KeyWord = brand.KeyWord,
                Order = brand.Order
            };

            TempData["res"] = "faild";
            if (_brandService.AddBrand(brand1))
            {
                _logger.CreateAuditScope(new AuditLog<Brand>()
                {
                    Entite = brand1,
                    Action = Command.Create,
                    Modifier = _userId
                });
                TempData["res"] = "success";
            }
            
            return RedirectToAction(nameof(BrandList));

        }

        public IActionResult DeleteBrand(int id)
        {
            var brand = _brandService.GetBrandById(id);

            TempData["res"] = "faild";
            if (_brandService.DeleteBrand(brand))
            {
                _logger.CreateAuditScope(new AuditLog<Brand>()
                {
                    Entite = brand,
                    Action = Command.Remove,
                    Modifier = _userId
                });
                TempData["res"] = "success";
            }            

            return RedirectToAction(nameof(BrandList));

        }
        public IActionResult EditBrand(int id)
        {
            Brand brand = _brandService.GetBrandById(id);
            if (brand != null)
            {
                EditBrandViewModel brandViewModel = new EditBrandViewModel
                {
                    FaTitle = brand.FaTitle,
                    EnTitle = brand.EnTitle,
                    Descrption = brand.Descrption,
                    KeyWord = brand.KeyWord,
                    IsShowInFirstPage = brand.IsShowInFirstPage,
                    Order = brand.Order
                };
                ViewBag.brandid = brand.Id;
                ViewBag.imagname = brand.ImgName;
                return View(brandViewModel);
            }
            return RedirectToAction(nameof(BrandList));

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult EditBrand(EditBrandViewModel brandViewModel, long brandid, string imgname)
        {
            if (!ModelState.IsValid)
                return View(brandViewModel);
            if (brandViewModel.Image != null)
            {
                if (ImageSecurity.Imagevalidator(brandViewModel.Image))
                {
                    try
                    {
                        imgname.DeleteImage("wwwroot/uploads");

                    }
                    catch (Exception e)
                    {
                    }
                    //imgname = brandViewModel.Image.SaveImage(imgname, "wwwroot/uploads");
                    imgname = _imageUploadService.Upload(brandViewModel.Image);
                    ;
                }
                else
                {
                    ModelState.AddModelError("Image", "لطفا یک فایل درست انتحاب کنید");
                    return View(brandViewModel);
                }
            }
            Brand brand = new Brand
            {
                FaTitle = brandViewModel.FaTitle,
                EnTitle = brandViewModel.EnTitle,
                Descrption = brandViewModel.Descrption,
                ImgName = imgname,
                Id = brandid,
                KeyWord = brandViewModel.KeyWord,
                IsShowInFirstPage = brandViewModel.IsShowInFirstPage,
                Order = brandViewModel.Order
            };

            TempData["res"] = "faild";
            if (_brandService.EditBrand(brand))
            {
                _logger.CreateAuditScope(new AuditLog<Brand>()
                {
                    Entite = brand,
                    Action = Command.Update,
                    Modifier = _userId
                });
                TempData["res"] = "success";
            }

            
            return RedirectToAction(nameof(BrandList));
        }
        #endregion

        #region Review
        public IActionResult ProductReview(int id,EnumTypeSystem typeSystem)
        {
            var product = _productService.FindProductById(id);
            ViewBag.ProductName = product.FaTitle;
            ViewBag.ProductImage = product.ImgName;
            long catid = _productService.GetProductCategoryId(id);
            ReviewAdminViewModel review = new ReviewAdminViewModel();
            review.ReviewContent = _reviewService.GetProductReviewForAdmin(id,typeSystem);
            review.RatingValue = _reviewService.GetProductRatingReview(id, typeSystem);
            review.RatingAttribute = _reviewService.GetRatingAttributeByCatId(catid);
            review.TypeSystem = typeSystem;
            ViewBag.productid = id;
            ViewBag.reviewid = review.ReviewContent != null ? review.ReviewContent.ProductReviewId : 0;
            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult ProductReview(
            ReviewAdminViewModel ReviewAdmin,
            List<string> positive,
            List<string> negative,
            List<long> RatingAttributeId,
            List<int> RatingAttributevalue,
            long productId,
            long reviewId,
            string ratingList,
            string ratingValue
            )
        {
            ProductReview review = new ProductReview
            {
                Id = reviewId,
                ProductId = productId,
                Summary = ReviewAdmin.ReviewContent.Summary,
                ShortReview = ReviewAdmin.ReviewContent.ShortReview,
                Review = ReviewAdmin.ReviewContent.Review,
                Positive = string.Join("=", positive),
                Negative = string.Join("=", negative),
                TypeSystem = ReviewAdmin.TypeSystem
            };

            if (_reviewService.EditRview(review))
            {
                _logger.CreateAuditScope(new AuditLog<ProductReview>()
                {
                    Entite = review,
                    Action = Command.Update,
                    Modifier = _userId
                });
            }

            if (review.TypeSystem == EnumTypeSystem.Farnaa)
            {
                return RedirectToAction("ProductListContainerReview");
            }
            else
            {
                return RedirectToAction("ProductListContainerReviewPlus");

            }
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]

        public async Task<JsonResult> ProductReviewAjax([FromBody] ProductReviewContentAjaxViewModel data)
        {

            var review = _reviewService.GetProductReview(data.ProductReviewId);
            review.Summary = data.Summary;
            review.ShortReview = data.ShortReview;
            review.Review = data.Review;
            if (_reviewService.EditRview(review))
            {
                _logger.CreateAuditScope(new AuditLog<ProductReview>()
                {
                    Entite = review,
                    Action = Command.Update,
                    Modifier = _userId
                });
            }

            return Json(JsonConvert.SerializeObject(true));
        }
        #endregion

        #region Attribue
        public IActionResult RatingAttributeList()
        {
            return View(_attributeRatingService.GetRatingAttributes());
        }

        public IActionResult CreateAttribute()
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAdd();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CreateAttribute(RatingAttribute ratingAttribute, List<int> catList)
        {
            if (!ModelState.IsValid && catList.Count <= 0)
            {
                ViewBag.CategoryList = _categoryService.GetCategoriesForAdd();
                return View();
            }

            long id = _attributeRatingService.AddRatingAttribute(ratingAttribute);
            List<CategoryRating> categoryRatings = new List<CategoryRating>();
            if (id > 0)
            {
                foreach (var item in catList)
                {
                    categoryRatings.Add(new CategoryRating
                    {
                        CategoryId = item,
                        RatingAttributeId = id
                    });
                }

                TempData["res"] = "faild";
                if (_attributeRatingService.AddCategoryRating(categoryRatings))
                {
                    _logger.CreateAuditScope(new AuditLog<List<CategoryRating>>()
                    {
                        Entite = categoryRatings,
                        Action = Command.Update,
                        Modifier = _userId
                    });
                    TempData["res"] = "success";
                }

                return RedirectToAction(nameof(RatingAttributeList));
            }
            TempData["res"] = "faild";
            return RedirectToAction(nameof(RatingAttributeList));
        }
        #endregion

        #region RelatedProduct

        public IActionResult RelatedProduct(long id)
        {

            var product = _productService.FindProductById(id);
            ViewBag.ProductId = product.Id;
            ViewBag.ProductName = product.FaTitle;
            ViewBag.ProductImage = product.ImgName;

            var res = _productService.GetRelatedProductForAdmin(id);
            var lstOldRelated = res.Where(c => c.IsRelated);
            var sRelated = "";
            foreach (var related in lstOldRelated)
            {

                sRelated += "," + related.ProductId;
            }

            ViewBag.RelaredProduct = sRelated;

            return View(res);
        }
        public IActionResult AccessoriesProduct(long id)
        {

            var product = _productService.FindProductById(id);
            ViewBag.ProductId = product.Id;
            ViewBag.ProductName = product.FaTitle;
            ViewBag.ProductImage = product.ImgName;

            var res = _productService.GetAccessoriesProductForAdmin(id);
            var lstOldRelated = res.Where(c => c.IsRelated);
            var sRelated = "";
            foreach (var related in lstOldRelated)
            {

                sRelated += "," + related.ProductId;
            }

            ViewBag.RelaredProduct = sRelated;

            return View(res);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult ChangeRelatedProduct(long productId, string relatedProducts)
        {

            var newRelated = relatedProducts.Split(',');
            List<long> newRelatedProducts = new List<long>();
            foreach (var relate in newRelated)
            {
                if (!string.IsNullOrEmpty(relate))
                    newRelatedProducts.Add(Convert.ToInt64(relate));
            }

            if (_productService.ChangeRelatedProduct(productId, newRelatedProducts))
            {
                _logger.CreateAuditScope(new AuditLog<Tuple<long,List<long>>>()
                {
                    Entite = new Tuple<long, List<long>>(productId,newRelatedProducts),
                    Action = Command.Update,
                    Modifier = _userId
                });
            }            


            return RedirectToAction("RelatedProduct", new { id = productId });
        }   [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult ChangeAccessoriesProduct(long productId, string relatedProducts)
        {

            var newRelated = relatedProducts.Split(',');
            List<long> newRelatedProducts = new List<long>();
            foreach (var relate in newRelated)
            {
                if (!string.IsNullOrEmpty(relate))
                    newRelatedProducts.Add(Convert.ToInt64(relate));
            }

            if (_productService.ChangeAccessoriesProduct(productId, newRelatedProducts))
            {
                _logger.CreateAuditScope(new AuditLog<Tuple<long, List<long>>>()
                {
                    Entite = new Tuple<long, List<long>>(productId, newRelatedProducts),
                    Action = Command.Update,
                    Modifier = _userId
                });
            }            

            return RedirectToAction("AccessoriesProduct", new { id = productId });
        }

        #endregion
   
    
    }
}