using Mng_shifts.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mng_shifts.Core.IServices
{
    public interface IShiftExchangeService
    {
        Task<List<Shift>> GetShiftsByEmployeeIdAsync(int employeeId);
        Task<Employee?> GetEmployeeWithShiftsByCredentialsAsync(string username, string password);
        Task RequestSwapAsync(int shiftId);
        Task<List<SwapRequest>> GetOpenSwapRequestsByEmployeeId(int excludingEmployeeId);
        Task ProposeSwapAsync(int requestId, int proposedShiftId);
        Task<List<SwapRequest>> GetSwapProcessesByEmployeeId(int employeeId);
        Task RespondToProposalAsync(int proposalId, string action);

    }
}
