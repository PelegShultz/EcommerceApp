using NServiceBus;

namespace Event
{
    public class BillOrder : IEvent
    {
        public string OrderId { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
    }
}
