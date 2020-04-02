using System.Collections.Generic;
using BrandedProducts.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace BrandedProducts.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<ProductsResultOutputModel> GetAll()
        {
            var products = new List<ProductOutputModel>
            {
                new ProductOutputModel
                {
                    Name = "ProTech Racket",
                    Description = "The best racket on the market today! Never miss a shot.",
                    Price = 150.00
                },

                new ProductOutputModel
                {
                    Name = "Yellow Spheres",
                    Description = "Performance tennis balls with fantastic durability.",
                    Price = 15.75
                },

                new ProductOutputModel
                {
                    Name = "Go-faster Shorts",
                    Description = "Never be late to the ball again with our patented go-faster fabric technology.",
                    Price = 45.95
                }
            };

            return new ProductsResultOutputModel
            {
                Products = products
            };
        }
    }
}
