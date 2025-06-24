using Mng_shifts.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mng_shifts.Core.IRepositories
{

    public interface IShiftExchangeRepository
    {
        Task<Employee?> GetEmployeeWithShiftsByCredentialsAsync(string username, string password);
        Task<List<Shift>> GetShiftsByEmployeeIdAsync(int employeeId);
        Task AddSwapRequestAsync(SwapRequest request);
        Task<List<SwapRequest>> GetOpenSwapRequestsByEmployeeId(int excludingEmployeeId);
        Task<List<SwapRequest>> GetRequestsByEmployeeAsync(int employeeId);
        Task AddSwapProposalAsync(SwapProposal proposal);
        Task<Shift?> GetShiftByIdAsync(int id);
        Task UpdateShiftAsync(Shift shift);
        Task<SwapRequest?> GetSwapRequestByIdAsync(int id);





        Task<SwapProposal?> GetProposalByIdAsync(int proposalId);
        Task UpdateProposalAsync(SwapProposal proposal);
        Task MarkSwapRequestAsCompletedAsync(int swapRequestId);
        Task SwapShiftsBetweenEmployeesAsync(int requestShiftId, int proposedShiftId);
        Task RejectOtherProposalsAsync(int swapRequestId, int acceptedProposalId);
    }

}
