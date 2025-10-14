using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace CEVerticalShooter.Game.Data
{
    [Serializable]
    public class IDDataCollection<Data, ID> : IDataCollection<Data, ID> where Data: class, IData<ID> where ID : Enum
    {
        [SerializeField]
        private List<Data> data;

        public bool TryToGetDataWithID(ID id, out Data dataWithID)
        {
            dataWithID = data.FirstOrDefault(x => EqualityComparer<ID>.Default.Equals(x.ID, id));
            Assert.IsNotNull(dataWithID, $"Data with id {id} not found");
            return dataWithID != null;
        }
    }
}
