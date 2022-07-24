using NServiceBus;

namespace Event
{
    public class CheckAndUpdateStock : IEvent
    {
        public string OrderId { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public bool IsAvailable { get; set; }
    }
}
