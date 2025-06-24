using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mng_shifts.Core.Entities;
using Mng_shifts.Core.IServices;

namespace Mng_shifts.Api.Controllers
{

    namespace Mng_shifts.Api.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class ShiftExchangeController : ControllerBase
        {
            private readonly IShiftExchangeService _service;

            public ShiftExchangeController(IShiftExchangeService service)
            {
                _service = service;
            }

            [HttpGet("employee/{employeeId}/shifts")]
            public async Task<ActionResult<Employee>> GetShiftsByEmployeeId(int employeeId)
            {
                var shifts = await _service.GetShiftsByEmployeeIdAsync(employeeId);
                if (shifts == null)
                    return NotFound();

                return Ok(shifts);
            }
            //אין צורך לשלוף עם משמרות
            [HttpPost("employee/login")]
            public async Task<ActionResult<Employee>> Login([FromBody] LoginRequestDto request)
            {
                var employee = await _service.GetEmployeeWithShiftsByCredentialsAsync(request.Username, request.Password);

                if (employee == null)
                    return Unauthorized("שם משתמש או סיסמה שגויים");

                return Ok(employee);
            }
            // 2. בקשת החלפה
            [HttpPost("request/{shiftId}")]
            public async Task<ActionResult> RequestSwap(int shiftId)
            {
                await _service.RequestSwapAsync(shiftId);
                return Ok();
            }

            // 3. שליפת בקשות פתוחות שלא מהעובד הנוכחי
            [HttpGet("open-requests/{excludingEmployeeId}")]
            public async Task<ActionResult<List<SwapRequest>>> GetOpenSwapRequestsByEmployeeId(int excludingEmployeeId)
            {
                var requests = await _service.GetOpenSwapRequestsByEmployeeId(excludingEmployeeId);
                return Ok(requests);
            }

            [HttpPost("propose")]
            public async Task<ActionResult> ProposeSwap([FromQuery] int requestId, [FromQuery] int proposedShiftId)
            {
                await _service.ProposeSwapAsync(requestId, proposedShiftId);
                return Ok();
            }

            // 6. כל ההחלפות של עובד (כמבקש או כמציע)
            [HttpGet("my-swaps/{employeeId}")]
            public async Task<ActionResult<List<SwapRequest>>> GetSwapProcessesByEmployeeId(int employeeId)
            {
                var swaps = await _service.GetSwapProcessesByEmployeeId(employeeId);
                return Ok(swaps);
            }
             //5. אישור / דחייה של הצעה
            [HttpPut("proposals/{proposalId}/respond-proposal")]
            public async Task<IActionResult> RespondToProposal([FromRoute] int proposalId, [FromQuery] string action)
            {
                if (action != "approve" && action != "reject")
                    return BadRequest("Invalid action");

                try
                {
                    await _service.RespondToProposalAsync(proposalId, action);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

        }

        public class SwapResponseDto
        {
            public int ProposalId { get; set; }

        }
        public class LoginRequestDto
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

    }
}