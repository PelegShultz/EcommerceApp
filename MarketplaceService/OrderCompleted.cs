using NServiceBus;

namespace Event
{
    public class OrderCompleted : IMessage
    {
        public string OrderId { get; set; }
        public string OrderStatus { get; set; }
    }
}
