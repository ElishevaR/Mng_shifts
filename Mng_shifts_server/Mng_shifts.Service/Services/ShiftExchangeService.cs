using Mng_shifts.Core.Entities;
using Mng_shifts.Core.IRepositories;
using Mng_shifts.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mng_shifts.Service.Services
{

    public class ShiftExchangeService : IShiftExchangeService
    {
        private readonly IShiftExchangeRepository _repo;

        public ShiftExchangeService(IShiftExchangeRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Shift>> GetShiftsByEmployeeIdAsync(int employeeId)
        {
            return await _repo.GetShiftsByEmployeeIdAsync(employeeId);
        }

        public async Task RequestSwapAsync(int shiftId)
        {
            var shift = await _repo.GetShiftByIdAsync(shiftId);
            if (shift == null)
                throw new InvalidOperationException("המשמרת לא קיימת");

            var request = new SwapRequest
            {
                ShiftId = shiftId,
                Status = SwapRequestStatus.Open,
                Comment = "" // ← כאן את מבטיחה שהוא לא NULL

            };

            await _repo.AddSwapRequestAsync(request);

            // שינוי סטטוס המשמרת גם כן
            shift.Status = ShiftStatus.SwapRequested;
            await _repo.UpdateShiftAsync(shift);
        }

        public async Task<List<SwapRequest>> GetOpenSwapRequestsByEmployeeId(int excludingEmployeeId)
        {
            return await _repo.GetOpenSwapRequestsByEmployeeId(excludingEmployeeId);
        }

        public async Task ProposeSwapAsync(int requestId, int proposedShiftId)
        {
            var request = await _repo.GetSwapRequestByIdAsync(requestId);
            if (request == null)
                throw new Exception("בקשת ההחלפה לא נמצאה");

            var shift = await _repo.GetShiftByIdAsync(proposedShiftId);
            if (shift == null)
                throw new Exception("המשמרת המוצעת לא נמצאה");

            var proposal = new SwapProposal
            {
                SwapRequestId = requestId,
                ProposedShiftId = proposedShiftId,
                Status = SwapProposalStatus.WaitingForApproval,
                ProposedAt = DateTime.UtcNow
            };

            await _repo.AddSwapProposalAsync(proposal);
        }

        public async Task<List<SwapRequest>> GetSwapProcessesByEmployeeId(int employeeId)
        {
            return await _repo.GetRequestsByEmployeeAsync(employeeId);
        }
        public async Task<Employee?> GetEmployeeWithShiftsByCredentialsAsync(string username, string password)
        {
            return await _repo.GetEmployeeWithShiftsByCredentialsAsync(username, password);
        }

        public async Task RespondToProposalAsync(int proposalId, string action)
        {
            var proposal = await _repo.GetProposalByIdAsync(proposalId);
            if (proposal == null)
                throw new Exception("Proposal not found");

            proposal.Status = action.ToLower() switch
            {
                "approve" => SwapProposalStatus.Approved,
                "reject" => SwapProposalStatus.Rejected,
                _ => throw new Exception("Invalid action")
            };

            await _repo.UpdateProposalAsync(proposal);
            
            if (proposal.Status == SwapProposalStatus.Approved)
            {
                await _repo.MarkSwapRequestAsCompletedAsync(proposal.SwapRequestId);
                await _repo.SwapShiftsBetweenEmployeesAsync(proposal.SwapRequest.ShiftId, proposal.ProposedShiftId);
                await _repo.RejectOtherProposalsAsync(proposal.SwapRequestId, proposal.Id);
            }
        }



    }

}


