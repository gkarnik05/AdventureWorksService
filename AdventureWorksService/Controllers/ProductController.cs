using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureWorksService.WebApi.Interfaces;
using AdventureWorksService.WebApi.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace AdventureWorksService.WebApi
{
    [Route("api/product")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(IList<Contract.VProductAndDescription>), StatusCodes.Status200OK)]
        public async Task<IList<VProductAndDescription>> GetAllProducts()
        {
            return await _productService.GetAllProducts();
        }
    }
}
