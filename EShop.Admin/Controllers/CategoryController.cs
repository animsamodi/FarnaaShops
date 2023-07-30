using System.Collections.Generic;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EShop.Admin.ViewModels.Products;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels.Category;
using EShop.DataLayer.Entities.Category;
using EShop.DataLayer.Entities.Variety;
using Ganss.XSS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class CategoryController : BaseAdminController
    {
        private ICategoryService _categoryService;
        private IProductService _productService;
        private IBrandService _brandService;
        private IVariantService _variantService;
        public CategoryController(ICategoryService categoryService, IProductService productService,
            IBrandService brandService,IHttpContextAccessor contextAccessor, 
            IVariantService variantService, Logging.AuditLog.IAuditService logger) : base(logger,contextAccessor)
        {
            _categoryService = categoryService;
            _productService = productService;
            _brandService = brandService;
            _variantService = variantService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            var res = _categoryService.GetCategoriesForTree();
            return DataSourceLoader.Load(res, loadOptions);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]

        public IActionResult Post(string values)
        {
            var category = new GetCategoryForTree();
            JsonConvert.PopulateObject(values, category);

            if (_categoryService.IsExistCategoryTitle(0, category.EnTitle))
            {

                return BadRequest("این نام تکراری است");
            }

            if (!TryValidateModel(category))
                return BadRequest("خطا");

            var htmlsanitaizer = new HtmlSanitizer();
            Category cat = new Category
            {
                Descrption = category.Descrption,
                KeyWord = category.KeyWord,
                FAQSchema = category.FAQSchema,
                FaTitle = htmlsanitaizer.Sanitize(category.FaTitle),
                EnTitle = category.EnTitle,
                ImgName = "",
                MetaTitle = category.MetaTitle,
                IsMain = true
            };
            var parentList = new List<long>();
            if (category.ParentId != null && category.ParentId != 0)
                parentList.Add(category.ParentId.Value);

            if (_categoryService.AddCategory(cat, parentList))
            {
                _logger.CreateAuditScope(new Logging.AuditLog.Models.AuditLog<Category>()
                {
                    Modifier = _userId,
                    Action = Logging.AuditLog.Models.Command.Create,
                    Entite = cat,
                });
            }
            
            return Ok();
        }

        [HttpPut]
        [IgnoreAntiforgeryToken]

        public IActionResult Put(int key, string values)
        {

            var cat = _categoryService.GetCategoryByIdForTree(key);

            JsonConvert.PopulateObject(values, cat);
            if (!TryValidateModel(cat))
                return BadRequest("خطا");


            if (_categoryService.IsExistCategoryTitle(cat.Id, cat.EnTitle))
            {

                return BadRequest("این نام تکراری است");
            }

            Category category = new Category()
            {
                Id = cat.Id,
                Descrption = cat.Descrption,
                FaTitle = cat.FaTitle,
                EnTitle = cat.EnTitle,
                ImgName = "",
                KeyWord = cat.KeyWord,
                FAQSchema = cat.FAQSchema,
                MetaTitle = cat.MetaTitle,
                IsMain = true

            };
            var parentList = new List<long>();
            if (cat.ParentId != null && cat.ParentId != 0)
                parentList.Add(cat.ParentId.Value);


            if (_categoryService.UpdateCategory(category, parentList))
            {
                _logger.CreateAuditScope(new Logging.AuditLog.Models.AuditLog<Category>()
                {
                    Modifier = _userId,
                    Action = Logging.AuditLog.Models.Command.Update,
                    Entite = category,
                });
            }

            return Ok();
        }

        [HttpDelete]
        [IgnoreAntiforgeryToken]

        public IActionResult Delete(int key)
        {
            Category category = _categoryService.FindCategoryById(key);
            var countUse = _productService.CountUseCategoryInProducts(key);
            if (countUse > 0)
            {
                return BadRequest($"شما نمیتوانید این دسته بندی را حذف کنید. این دسته بندی در {countUse} محصول استفاده شده است");

            }
            else
            {
                if (_categoryService.DeleteCategory(category))
                {
                    _logger.CreateAuditScope(new Logging.AuditLog.Models.AuditLog<Category>()
                    {
                        Modifier = _userId,
                        Action = Logging.AuditLog.Models.Command.Remove,
                        Entite = category,
                    });
                }

                return Ok();

            }

        }

        [HttpGet]
        public IActionResult UpdatePriceGrouply()
        {
            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePriceGrouply(UpdatePriceGrouplyViewModel model)
        {
            if(model.Percent <= -100 || model.Percent > 100)
                TempData["ErrorMessage"] = "لطفا یک مقدار درصدی صحیح وارد نمایید";
            else if (model.CategoryId == 0 && model.BrandId == 0)
                TempData["ErrorMessage"] = $"یک دسته بندی یا برند انتخاب نمایید.";
            else
            {
                var products = await _productService.GetByCategoryIdAndBrandId(model.CategoryId, model.BrandId);
                foreach (var product in products)
                {
                    foreach (var varient in product.Variants)
                    {
                        varient.Price = model.Percent >= 0 ? ((varient.Price + (varient.Price * model.Percent)) / 100) 
                            : ((varient.Price - (varient.Price * model.Percent)) / 100);

                        _variantService.UpdateRange(product.Variants);
                         _logger.CreateAuditScope(new Logging.AuditLog.Models.AuditLog<List<Variant>>()
                         {
                             Modifier = _userId,
                             Action = Logging.AuditLog.Models.Command.Update,
                             Entite = product.Variants,
                         });
                    }
                }
            }

            ViewBag.CategoryList = _categoryService.GetCategoriesForAddProduct();
            ViewBag.BrandList = _brandService.GetBrandsForAddProduct();

            return View(model);
        }
    }
   
}