using Domain.Abstraction;

namespace Domain.Messaging
{
    public class Message : BaseDomainObject, IComparable<Message>
    {
        public Guid ThreadId { get; set; }
        public string Text { get; set; }
        public bool Edited => TimeCreated != TimeLastModified;

        private Message() { }        

        public Message(string text)
        {
            Text = text;
        }

        public int CompareTo(Message? other)
        {
            if (other == null) 
                throw new ArgumentNullException(typeof(Message).ToString());
            else
                return other.TimeCreated.CompareTo(TimeCreated);
        }
    }
}
