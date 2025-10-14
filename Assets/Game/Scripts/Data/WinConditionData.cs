using CEVerticalShooter.Game.WinCondition;
using System;
using UnityEngine;

namespace CEVerticalShooter.Game.Data
{
    [Serializable]
    public class WinConditionData : IData<WinConditionID>
    {
        [SerializeField]
        private WinConditionID id;

        [SerializeField]
        private float winConditionValue;

        public WinConditionID ID => id;
        public float WinConditionValue => winConditionValue;

    }
}
