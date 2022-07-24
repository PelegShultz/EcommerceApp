using NServiceBus;

namespace Event
{
    public class UserCreated : ICommand
    {
        public string UserName { get; set; }
        public int UserId { get; set; }
    }
}
