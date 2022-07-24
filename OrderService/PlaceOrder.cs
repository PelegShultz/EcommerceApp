using NServiceBus;

namespace Event
{
    public class PlaceOrder : ICommand
    {
        public string OrderId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public string UserName { get; set; }
        public string ImgUrl { get; set; }
    }
}
