using BalanceService.Data;
using BalanceService.Entities;
using BalanceService.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BalanceService.Controllers
{
    public class BalanceController : BaseController
    {
        private readonly IBalanceRepo _repo;


        public BalanceController(IBalanceRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<BalanceEntity>> GetUserBalance(string username)
        {
            return await _repo.GetUserBalanceAsync(username);
        }
    }
}
