using Domain.Abstraction;

namespace Domain.Messaging;

public class MessageThread : BaseDomainObject
{
    public List<Message> Messages { get; set; } = [];

    public MessageThread() { }

    public void AddMessage(string text)
    {
        Messages.Add(new Message(text));
        Messages.OrderDescending();        
    }
}

