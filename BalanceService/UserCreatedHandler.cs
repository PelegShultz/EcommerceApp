using BalanceService.Data;
using BalanceService.Entities;
using Event;
using NServiceBus;
using NServiceBus.Logging;

namespace BalanceService
{
    public class UserCreatedHandler : IHandleMessages<UserCreated>
    {
        private readonly DataContext _dataContext;
        static readonly ILog log = LogManager.GetLogger<UserCreatedHandler>();

        public UserCreatedHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Handle(UserCreated message, IMessageHandlerContext context)
        {
            log.Info($"Balance Received UserCreated, UserId={message.UserId}, UserName={message.UserName}");
            BalanceEntity userToUpdateBalance = new BalanceEntity()
            {
                UserName = message.UserName,
                Balance = 1000,
                UserId = message.UserId,
            };

            _dataContext.Balance.Add(userToUpdateBalance);
            await _dataContext.SaveChangesAsync();
            
        }
    }
}
