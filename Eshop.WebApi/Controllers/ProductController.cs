using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Eshop.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {

        private readonly ILogger<ProductController> _logger;
        IProductService _productService;
        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpPost]
        [Route("GetListProductByFilter")]
        public IActionResult GetListProductByFilter([FromBody] FilterProductApiViewModel filter)
        {

            var res = _productService.GetListProductForApi(filter);
            return Ok(res);
        }
        [HttpGet]
        [Route("GetListProductByBrand/{id}")]
        public IActionResult GetListProductByBrand(long id)
        {
            FilterProductApiViewModel filter = new FilterProductApiViewModel
            {
                BrandId = id
            };
            var res = _productService.GetListProductForApi(filter);
            return Ok(res);
        }
        [HttpGet]
        [Route("GetListProductByCategoryId/{id}")]
        public IActionResult GetListProductByCategoryId(long id)
        {
            FilterProductApiViewModel filter = new FilterProductApiViewModel
            {
                CategoryId = id
            };
            var res = _productService.GetListProductForApi(filter);
            return Ok(res);
        }
        [HttpGet]
        [Route("GetProductById/{id}")]
        public IActionResult GetProductById(long id)
        {
            FilterProductApiViewModel filter = new FilterProductApiViewModel
            {
                ProductId = id
            };
            var res = _productService.GetListProductForApi(filter);
            return Ok(res);
        }
        [HttpGet]
        [Route("GetListProduct")]
        public IActionResult GetListProduct()
        {
            FilterProductApiViewModel filter = new FilterProductApiViewModel();

            var res = _productService.GetListProductForApi(filter);
            return Ok(res);
        }

    }
}
