using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_rest_api_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace my_rest_api_2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private static readonly List<Product> products = new();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id) {
            try
            {
            var existingProduct = products.SingleOrDefault(prd => prd.ProductId == Guid.Parse(id)); 

            if(existingProduct == null) { 
                return NotFound();
            }
            return Ok(existingProduct);           
            }
            catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost] 
        public IActionResult Create(ProductVM productVM) {

            try
            {


                var prd = new Product()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = productVM.ProductName,
                    ProductPrice = productVM.ProductPrice,
                };
                products.Add(prd);
                return Ok(new
                {
                    success = true,
                    Data = prd
                });
            }
            catch(Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error retrieving data from the database");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id,  ProductVM productVM) {
            try
            {
                var prd = products.SingleOrDefault(product  => product.ProductId == Guid.Parse(id));

                if(prd == null)
                {
                    return NotFound();
                }
                if(id != prd.ProductId.ToString()) {
                       return BadRequest(); 
                }
                prd.ProductName = productVM.ProductName;
                prd.ProductPrice = productVM.ProductPrice;
                return Ok();            
            }
            catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var prd = products.SingleOrDefault(product => product.ProductId == Guid.Parse(id));

                if (prd == null)
                {
                    return NotFound();
                }
               products.Remove(prd);    
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }

        }

    }   
}
