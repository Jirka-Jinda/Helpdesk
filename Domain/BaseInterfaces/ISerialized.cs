namespace Domain.BaseInterfaces
{
    public interface ISerialized<T>
    {
        public string Serialize(T obj);
        public bool TryDeserialize(string str, out T obj);
        public T Deserialize(string str);
    }
}
