using System.Collections;
using System.Collections.ObjectModel;

namespace Domain.Messaging;

public class MessageThread : BaseDomainObject, IEnumerable<Message>
{
    private List<Message> _messages = new();
    public ReadOnlyCollection<Message> Messages
    {
        get
        {
            return new ReadOnlyCollection<Message>(_messages);
        }
    }

    public void AddMessage(MessageContent content)
    {
        _messages.Add(new Message(content));
        _messages.OrderDescending();        
    }

#region Enumeration
    public IEnumerator<Message> GetEnumerator()
    {
        return new ThreadEnumerator(Messages);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    class ThreadEnumerator : IEnumerator<Message>
    {
        private int _index = -1;
        public ReadOnlyCollection<Message> Messages;
        public object Current => Messages[_index];

        Message IEnumerator<Message>.Current => Messages[_index];

        public ThreadEnumerator(ReadOnlyCollection<Message> messages)
        {
            Messages = messages;
        }

        public bool MoveNext()
        {
            _index++;
            return _index < Messages.Count();
        }

        public void Reset()
        {
            _index = -1;
        }

        public void Dispose()
        {         
        }
    }
#endregion

}

