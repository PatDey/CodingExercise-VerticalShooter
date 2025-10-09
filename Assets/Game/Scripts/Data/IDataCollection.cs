using System;

namespace CEVerticalShooter.Game.Data
{
    public interface IDataCollection<IDData, IDEnum> where IDData : IData<IDEnum> where IDEnum : Enum
    {
        public IDData GetDataWithID(IDEnum id);
    }
}
