using NServiceBus;

namespace Event
{
    public class StockAvailable : IEvent
    {
        public string OrderId { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public bool IsAvailable { get; set; }
    }
}
