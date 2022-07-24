using NServiceBus;

namespace Event
{
    public class ReturnToStock : IEvent
    {
        public string ProductName { get; set; }
    }
}
