using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryControlService.Data;
using InventoryControlService.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace InventoryControlService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryItemsController : ControllerBase
    {
        private readonly InventoryControlContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public InventoryItemsController(InventoryControlContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItem>>> GetInventoryItems()
        {
            return await _context.InventoryItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryItem>> GetInventoryItem(int id)
        {
            var inventoryItem = await _context.InventoryItems.FindAsync(id);
            if (inventoryItem == null)
            {
                return NotFound();
            }
            return inventoryItem;
        }

        [HttpPost]
        public async Task<ActionResult<InventoryItem>> PostInventoryItem(InventoryItem inventoryItem)
        {
            _context.InventoryItems.Add(inventoryItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetInventoryItem), new { id = inventoryItem.Id }, inventoryItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventoryItem(int id, InventoryItem inventoryItem)
        {
            if (id != inventoryItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(inventoryItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Notify the PriceControlService
            using (var client = _httpClientFactory.CreateClient())
            {
                client.BaseAddress = new Uri("http://localhost:5001"); // Ensure this is the correct address for PriceControlService
                var content = new StringContent(JsonConvert.SerializeObject(new { ProductId = id, Quantity = inventoryItem.Quantity }), Encoding.UTF8, "application/json");
                var result = await client.PostAsync("/api/products/updatePrice", content);

                if (!result.IsSuccessStatusCode)
                {
                    // Log and handle error (you can expand this to handle specific status codes, etc.)
                    return StatusCode((int)result.StatusCode, "Error notifying PriceControlService.");
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryItem(int id)
        {
            var inventoryItem = await _context.InventoryItems.FindAsync(id);
            if (inventoryItem == null)
            {
                return NotFound();
            }
            _context.InventoryItems.Remove(inventoryItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
