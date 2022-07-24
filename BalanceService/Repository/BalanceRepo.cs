using BalanceService.Data;
using BalanceService.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BalanceService.Repository
{
    public class BalanceRepo : IBalanceRepo
    {
        private readonly DataContext _context;

        public BalanceRepo(DataContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<BalanceEntity>> GetUserBalanceAsync(string username)
        {

            return await _context.Balance.SingleOrDefaultAsync(x => x.UserName == username);
        }
    }
}
