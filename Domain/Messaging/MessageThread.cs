using Domain.Abstraction;

namespace Domain.Messaging;

public class MessageThread : DomainObject
{
    public List<Message> Messages { get; set; } = [];

    public MessageThread() { }
}

