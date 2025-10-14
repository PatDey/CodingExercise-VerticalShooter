using CEVerticalShooter.Game.Data;
using UnityEngine;

namespace CEVerticalShooter.Game.WinCondition
{
    public class WinConditionChecker
    {
        private WinConditionData _data;
        private WinConditionTracker _tracker;
        public WinConditionID ID => _data.ID;
        public float WinConditionValue => _data.WinConditionValue;
        public bool HasBeenReached => CheckWinCondition();

        public WinConditionChecker(WinConditionData data, WinConditionTracker tracker)
        {
            _data = data;
            _tracker = tracker;
        }

        private bool CheckWinCondition()
        {
            return ID switch
            {
                WinConditionID.EnemiesDefeated => _tracker.EnemiesDefeated >= WinConditionValue,
                WinConditionID.TimeSurvived => _tracker.StartTime + WinConditionValue <= Time.time,
                _ => false,
            };
        }
    }
}
