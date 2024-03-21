using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proj_Anuj.Models.ViewModels;
using Proj_Anuj.Services.Interface;

namespace Proj_Anuj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAllProduct")]
        public async Task<IEnumerable<ProductVM>> GetAllProducts()
        {
            return await _productService.GetAllProducts();
        }

        [HttpGet("GetProduct")]
        public async Task<ProductVM> GetProduct(int id)
        {
            return await _productService.GetProduct(id);
        }

    }
}
