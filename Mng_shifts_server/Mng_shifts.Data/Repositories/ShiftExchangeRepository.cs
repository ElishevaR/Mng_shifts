using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mng_shifts.Data.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Mng_shifts.Core.Entities;
    using Mng_shifts.Core.IRepositories;

    public class ShiftExchangeRepository : IShiftExchangeRepository
    {


        private readonly DataContext _context;

        public ShiftExchangeRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Employee?> GetEmployeeWithShiftsByCredentialsAsync(string username, string password)
        {
            return await _context.Employees
                .Include(e => e.Shifts)
                .FirstOrDefaultAsync(e => e.Username == username && e.PasswordHash == password);
        }
        public async Task<List<Shift>> GetShiftsByEmployeeIdAsync(int employeeId)
        {
            return await _context.Shifts
                .Where(s => s.EmployeeId == employeeId)
                .ToListAsync();
        }
        public async Task AddSwapRequestAsync(SwapRequest request)
        {
            _context.SwapRequests.Add(request);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SwapRequest>> GetOpenSwapRequestsByEmployeeId(int excludingEmployeeId)
        {
            return await _context.SwapRequests
                .Include(r => r.Shift)
                    .ThenInclude(s => s.Employee)
                .Include(r => r.SwapProposals)
                .Where(r =>
                    r.Status == SwapRequestStatus.Open &&
                    r.Shift.EmployeeId != excludingEmployeeId)
                .ToListAsync();
        }
        public async Task AddSwapProposalAsync(SwapProposal proposal)
        {
            _context.SwapProposals.Add(proposal);
            await _context.SaveChangesAsync();
        }
        public async Task<List<SwapRequest>> GetRequestsByEmployeeAsync(int employeeId)
        {
            return await _context.SwapRequests
                .Include(r => r.Shift)
                    .ThenInclude(s => s.Employee) // ⬅️ מוסיף את העובד של המשמרת
                .Include(r => r.SwapProposals)
                    .ThenInclude(p => p.ProposedShift)
                        .ThenInclude(s => s.Employee) // ⬅️ גם להצעות נוסיף את העובד של המשמרת המוצעת
                .Where(r =>
                    r.Shift.EmployeeId == employeeId ||
                    r.SwapProposals.Any(p => p.ProposedShift.EmployeeId == employeeId))
                .ToListAsync();
        }
        public async Task UpdateShiftAsync(Shift shift)
        {
            _context.Shifts.Update(shift);
            await _context.SaveChangesAsync();
        }
        public async Task<Shift?> GetShiftByIdAsync(int id)
        {
            return await _context.Shifts.FindAsync(id);
        }

        public async Task<SwapRequest?> GetSwapRequestByIdAsync(int id)
        {
            return await _context.SwapRequests
                .Include(r => r.Shift)
                .FirstOrDefaultAsync(r => r.Id == id);
        }






        public async Task<SwapProposal?> GetProposalByIdAsync(int proposalId)
        {
            return await _context.SwapProposals
                .Include(p => p.SwapRequest)
                .Include(p => p.ProposedShift)
                .FirstOrDefaultAsync(p => p.Id == proposalId);
        }

        public async Task UpdateProposalAsync(SwapProposal proposal)
        {
            _context.SwapProposals.Update(proposal);
            await _context.SaveChangesAsync();
        }

        public async Task MarkSwapRequestAsCompletedAsync(int swapRequestId)
        {
            var request = await _context.SwapRequests.FindAsync(swapRequestId);
            if (request != null)
            {
                request.Status = SwapRequestStatus.Completed;
                await _context.SaveChangesAsync();
            }
        }

        public async Task SwapShiftsBetweenEmployeesAsync(int requestShiftId, int proposedShiftId)
        {   
            var requestShift = await _context.Shifts.FindAsync(requestShiftId);
            var proposedShift = await _context.Shifts.FindAsync(proposedShiftId);

            if (requestShift == null)
                throw new Exception($" not found requestShiftId: {requestShiftId}");
            if(proposedShift == null)
                throw new Exception($" not found proposedShift: {proposedShift}");

            // החלפת הבעלות
            var tempEmployeeId = requestShift.EmployeeId;
            requestShift.EmployeeId = proposedShift.EmployeeId;
            proposedShift.EmployeeId = tempEmployeeId;

            requestShift.Status = ShiftStatus.Swapped;
            proposedShift.Status = ShiftStatus.Swapped;

            await _context.SaveChangesAsync();
        }

        public async Task RejectOtherProposalsAsync(int swapRequestId, int acceptedProposalId)
        {
            var others = await _context.SwapProposals
                .Where(p => p.SwapRequestId == swapRequestId && p.Id != acceptedProposalId)
                .ToListAsync();

            foreach (var p in others)
                p.Status = SwapProposalStatus.Rejected;

            await _context.SaveChangesAsync();
        }
    }
}








