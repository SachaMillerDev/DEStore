using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PriceControlService.Data;
using PriceControlService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceControlService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly PriceControlContext _context;

        public ProductsController(PriceControlContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("updatePrice")]
        public async Task<IActionResult> UpdatePrice([FromBody] ProductUpdateModel model)
        {
            var product = await _context.Products.FindAsync(model.ProductId);
            if (product == null)
            {
                return NotFound($"Product with ID {model.ProductId} not found.");
            }

            // Logic to adjust price based on quantity
            if (model.Quantity < 50) // Example logic: lower price if inventory is low
            {
                product.Price *= 0.9m; // Apply a 10% discount
            }
            else
            {
                product.Price *= 1.1m; // Increase price by 10% if inventory is high
            }

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Price updated based on inventory changes" });
        }
    }

    public class ProductUpdateModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
