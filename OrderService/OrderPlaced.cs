using NServiceBus;

namespace Event
{
    public class OrderPlaced : IEvent
    {
        public string OrderId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string UserName { get; set; }
        public string ImgUrl { get; set; }
    }
}
