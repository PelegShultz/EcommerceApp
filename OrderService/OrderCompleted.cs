using NServiceBus;

namespace Event
{
    public class OrderCompleted : IMessage
    {
        public string OrderId { get; set; }
        public string OrderStatus { get; set; }
    }
    public class GetOrderStatus : ICommand
    {
        public string OrderId { get; set; }
    }

    public class OrderStatus : IMessage
    {
        public string OrderId { get; set; }
        public string Status { get; set; }
    }
}
