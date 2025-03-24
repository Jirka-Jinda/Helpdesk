using Domain.Abstraction;

namespace Domain.Messaging
{
    public class Message : BaseDomainObject, IComparable<Message>
    {
        public Guid ThreadId { get; set; }
        public MessageContent Content { get; set; }
        public bool Edited => TimeCreated != TimeLastModified;

        internal Message(MessageContent content)
        {
            Content = content;
        }

        public int CompareTo(Message? other)
        {
            if (other == null) 
                throw new ArgumentNullException("Message is null");
            else
                return other.TimeCreated.CompareTo(TimeCreated);
        }
    }
}
