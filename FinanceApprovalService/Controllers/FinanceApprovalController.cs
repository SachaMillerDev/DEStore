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
            // Validate the request
            if (request == null || string.IsNullOrWhiteSpace(request.CustomerName) || request.Amount <= 0)
            {
                return BadRequest("Invalid finance request data.");
            }

            try
            {
                // Simulate a delay that might occur during an HTTP call to an external service
                await Task.Delay(500); // Simulate network delay

                // Simulated response from the finance system
                // In a real-world scenario, you would call the external finance system, e.g., via HTTPClient
                string approvalMessage = $"Finance approved for {request.CustomerName} with amount {request.Amount} over {request.DurationMonths} months.";

                // Return the response
                return Ok(approvalMessage);
            }
            catch (System.Exception ex)
            {
                // Log the exception (logging not implemented here)
                return StatusCode(500, "An error occurred while processing the finance request.");
            }
        }
    }
}
