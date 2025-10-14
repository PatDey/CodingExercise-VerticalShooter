using System;

namespace CEVerticalShooter
{
    public interface IData<T> where T : Enum
    {
        public T ID { get; }
    }
}
