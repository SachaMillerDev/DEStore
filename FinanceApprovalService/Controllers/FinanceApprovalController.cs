using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinanceApprovalService.Models;

namespace FinanceApprovalService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanceApprovalController : ControllerBase
    {
        // Simulate a call to the existing Enabling finance system
        [HttpPost]
        public async Task<ActionResult<string>> ApproveFinance([FromBody] FinanceRequest request)
        {
            // Here you would call the external finance system, e.g., via HTTP
            // For this example, we'll just simulate the response
            await Task.Delay(500); // Simulate network delay

            // Simulated response from the finance system
            return Ok("Finance approved for " + request.CustomerName);
        }
    }
}
