using NServiceBus;

namespace Event
{
    public class OrderBilled : IEvent
    {
        public string OrderId { get; set; }
        public string Name { get; set; }
        public string ProductName { get; set; }
        public bool IsAvailable { get; set; }
    }
}
