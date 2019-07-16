using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_api.Models;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public static List<Product> Products { get; set; } = new List<Product>();

        // GET ap
        [HttpGet]
        public ActionResult<List<Product>> Get()
        {
            return Products;
        }

        // GET api/products/1
        [HttpGet("{id}")]
        public ActionResult<Product> GetById([FromRoute] int id)
        {
            int? index = this.GetProductById(id);

            if (!index.HasValue) 
            {
                return NotFound();
            }
            else 
            {
                return Products[index.Value];
            }
        }

        // POST api/products
        [HttpPost]
        public ActionResult<int> Post([FromBody] Product product)
        {
            int maxId = 1;

            if (Products.Count != 0) 
            {
                maxId = Products[Products.Count - 1].Id  +  1;
            }

            product.Id = maxId;

            Products.Add(product);

            return product.Id;
        }

        // PUT api/products/1
        [HttpPut("{id}")]
        public ActionResult<Product> Put([FromRoute] int id, [FromBody] Product product)
        {
            int? index = this.GetProductById(id);

            if (!index.HasValue) 
            {
                return NotFound();
            }
            else 
            {
                Products[index.Value].Name = product.Name;
                Products[index.Value].Description = product.Description;

                return Products[index.Value];
            }
        }

        // DELETE api/products/1
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            int? index = GetProductById(id);

            if (!index.HasValue) 
            {
                return NotFound();
            }
            
            Products.RemoveAt(index.Value);

            return Ok();
        }

        private int? GetProductById(int id) 
        {
            for (int i = 0; i < Products.Count; i++)
            {
                if (Products[i].Id == id)
                {
                    return i;
                }
            }

            return null;
        }
    }
}
