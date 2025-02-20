namespace Domain.Messaging
{
    public class Message : BaseDomainObject, IComparable<Message>
    {
        public string Text { get; set; }
        public User Author { get; set; }
        public bool Edited => TimeCreated != TimeLastModified;

        public int CompareTo(Message? other)
        {
            if (other == null) 
                throw new ArgumentNullException("Message is null");
            else
                return other.TimeCreated.CompareTo(TimeCreated);
        }
    }
}
