namespace CEVerticalShooter.Core.Save
{
    public interface IDataService<T>
    {
        public T Data { get; }
        public void Save();
    }
}
