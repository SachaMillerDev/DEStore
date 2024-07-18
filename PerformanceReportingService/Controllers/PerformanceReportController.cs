using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerformanceReportingService.Data;
using PerformanceReportingService.Models;

namespace PerformanceReportingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformanceReportController : ControllerBase
    {
        private readonly PerformanceReportContext _context;

        public PerformanceReportController(PerformanceReportContext context)
        {
            _context = context;
        }

        // GET: api/PerformanceReport
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerformanceReport>>> GetPerformanceReports()
        {
            return await _context.PerformanceReports.ToListAsync();
        }

        // GET: api/PerformanceReport/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PerformanceReport>> GetPerformanceReport(long id)
        {
            var performanceReport = await _context.PerformanceReports.FindAsync(id);

            if (performanceReport == null)
            {
                return NotFound();
            }

            return performanceReport;
        }

        // PUT: api/PerformanceReport/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerformanceReport(long id, PerformanceReport performanceReport)
        {
            if (id != performanceReport.Id)
            {
                return BadRequest();
            }

            _context.Entry(performanceReport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerformanceReportExists(id))
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

        // POST: api/PerformanceReport
        [HttpPost]
        public async Task<ActionResult<PerformanceReport>> PostPerformanceReport(PerformanceReport performanceReport)
        {
            _context.PerformanceReports.Add(performanceReport);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerformanceReport", new { id = performanceReport.Id }, performanceReport);
        }

        // DELETE: api/PerformanceReport/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerformanceReport(long id)
        {
            var performanceReport = await _context.PerformanceReports.FindAsync(id);
            if (performanceReport == null)
            {
                return NotFound();
            }

            _context.PerformanceReports.Remove(performanceReport);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PerformanceReportExists(long id)
        {
            return _context.PerformanceReports.Any(e => e.Id == id);
        }
    }
}
