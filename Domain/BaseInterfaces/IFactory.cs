namespace Domain.BaseInterfaces
{
    internal interface IFactory<T>
    {
        public static abstract T Get();
    }
}
