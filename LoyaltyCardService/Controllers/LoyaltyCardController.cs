using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoyaltyCardService.Data;
using LoyaltyCardService.Models;

namespace LoyaltyCardService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltyCardController : ControllerBase
    {
        private readonly LoyaltyCardContext _context;

        public LoyaltyCardController(LoyaltyCardContext context)
        {
            _context = context;
        }

        // GET: api/LoyaltyCard
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoyaltyCard>>> GetLoyaltyCards()
        {
            return await _context.LoyaltyCards.ToListAsync();
        }

        // GET: api/LoyaltyCard/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoyaltyCard>> GetLoyaltyCard(long id)
        {
            var loyaltyCard = await _context.LoyaltyCards.FindAsync(id);

            if (loyaltyCard == null)
            {
                return NotFound();
            }

            return loyaltyCard;
        }

        // PUT: api/LoyaltyCard/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoyaltyCard(long id, LoyaltyCard loyaltyCard)
        {
            if (id != loyaltyCard.Id)
            {
                return BadRequest();
            }

            _context.Entry(loyaltyCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoyaltyCardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LoyaltyCard
        [HttpPost]
        public async Task<ActionResult<LoyaltyCard>> PostLoyaltyCard(LoyaltyCard loyaltyCard)
        {
            _context.LoyaltyCards.Add(loyaltyCard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoyaltyCard", new { id = loyaltyCard.Id }, loyaltyCard);
        }

        // DELETE: api/LoyaltyCard/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoyaltyCard(long id)
        {
            var loyaltyCard = await _context.LoyaltyCards.FindAsync(id);
            if (loyaltyCard == null)
            {
                return NotFound();
            }

            _context.LoyaltyCards.Remove(loyaltyCard);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoyaltyCardExists(long id)
        {
            return _context.LoyaltyCards.Any(e => e.Id == id);
        }
    }
}
