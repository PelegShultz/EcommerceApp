using BalanceService.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BalanceService.Repository
{
    public interface IBalanceRepo
    {

        public  Task<ActionResult<BalanceEntity>> GetUserBalanceAsync(string username);
    }
}
