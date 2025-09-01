using ApiCrud.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiCrud.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {        

        private static List<Product> product = new List<Product>();

        [HttpGet]
        public IActionResult Get() => Ok(product);

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = ProductController.product.FirstOrDefault(p => p.Id == id);
            return product == null ? base.NotFound() : base.Ok(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            product.Id = ProductController.product.Count > 0 ? ProductController.product.Max(p => p.Id) + 1 : 1;
            ProductController.product.Add(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product updatedProduct)
        {
            var product = ProductController.product.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = ProductController.product.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            ProductController.product.Remove(product);
            return NoContent();
        }
    }

}

