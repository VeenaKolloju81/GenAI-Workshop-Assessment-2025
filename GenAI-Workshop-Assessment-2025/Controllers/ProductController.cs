using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GenAI_Workshop_Assessment_2025.Models;

namespace AspNetCoreProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private static readonly List<Product> _products = new();
        private static int _nextId = 1;

        // GET: api/Product
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // Simulate async operation
                var products = await Task.FromResult(_products.ToList());
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", detail = ex.Message });
            }
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await Task.FromResult(_products.FirstOrDefault(p => p.Id == id));
                if (product == null)
                    return NotFound(new { message = $"Product with ID {id} not found." });

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", detail = ex.Message });
            }
        }

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest(new { message = "Product data is required." });

                if (string.IsNullOrWhiteSpace(product.Name))
                    return BadRequest(new { message = "Product Name is required." });

                if (product.Price <= 0)
                    return BadRequest(new { message = "Product Price must be greater than zero." });

                product.Id = _nextId++;
                await Task.Run(() => _products.Add(product));

                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", detail = ex.Message });
            }
        }

        // PUT: api/Product/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product updatedProduct)
        {
            try
            {
                if (updatedProduct == null)
                    return BadRequest(new { message = "Product data is required." });

                if (string.IsNullOrWhiteSpace(updatedProduct.Name))
                    return BadRequest(new { message = "Product Name is required." });

                if (updatedProduct.Price <= 0)
                    return BadRequest(new { message = "Product Price must be greater than zero." });

                var product = await Task.FromResult(_products.FirstOrDefault(p => p.Id == id));
                if (product == null)
                    return NotFound(new { message = $"Product with ID {id} not found." });

                await Task.Run(() =>
                {
                    product.Name = updatedProduct.Name;
                    product.Description = updatedProduct.Description;
                    product.Price = updatedProduct.Price;
                });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", detail = ex.Message });
            }
        }

        // DELETE: api/Product/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await Task.FromResult(_products.FirstOrDefault(p => p.Id == id));
                if (product == null)
                    return NotFound(new { message = $"Product with ID {id} not found." });

                await Task.Run(() => _products.Remove(product));
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", detail = ex.Message });
            }
        }
    }
}