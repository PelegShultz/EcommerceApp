//using BalanceService.Data;
//using Event;
//using Microsoft.EntityFrameworkCore;
//using NServiceBus;
//using NServiceBus.Logging;


//namespace BalanceService
//{
//    public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
//    {
//        private readonly DataContext _dataContext;
//        static readonly ILog log = LogManager.GetLogger<OrderPlacedHandler>();

//        public OrderPlacedHandler(DataContext dataContext)
//        {
//            _dataContext = dataContext;
//        }

//        public async Task Handle(OrderPlaced message, IMessageHandlerContext context)
//        {
//            log.Info($"Balance has received OrderPlaced, OrderId={message.OrderId} Product name = {message.Name}, Product price = {message.Price}");
//            var user = await _dataContext.Balance.FirstOrDefaultAsync(balance => balance.UserName == message.UserName);


//            if (user != null && (user.Balance - 200) > 0)
//            {
//                var orderBilled = new OrderBilled
//                {
//                    OrderId = message.OrderId,
//                    Name = message.Name
//                };

//                await context.Publish(orderBilled);
//            }

//            log.Info($"Balance Publishing OrderBilled, OrderId={message.OrderId} Product name = {message.Name}");


//        }
//    }
//}
