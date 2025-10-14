using System;

namespace CEVerticalShooter.Game.Data
{
    public interface IDataCollection<IDData, IDEnum> where IDData : IData<IDEnum> where IDEnum : Enum
    {
        public bool TryToGetDataWithID(IDEnum id, out IDData data);
    }
}
