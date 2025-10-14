using CEVerticalShooter.Game.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CEVerticalShooter.Game.WinCondition
{
    public class WinConditionService : IWinConditionService
    {
        private WinConditionTracker _tracker;
        private List<WinConditionChecker> _checkers;
        public bool HasReachedAnyWinCondition => _checkers.Any(x => x.HasBeenReached);
        public WinConditionService(WinConditionDataCollection data, WinConditionTracker tracker) 
        {
            _tracker = tracker;
            _checkers = new List<WinConditionChecker>();
            foreach (WinConditionID id in Enum.GetValues(typeof(WinConditionID)))
            {
                if(data.TryToGetDataWithID(id, out WinConditionData winConditionData))
                {
                    _checkers.Add(new WinConditionChecker(winConditionData, tracker));
                }
            }
        }

        public void ResetWinConditionTracker()
        {
            _tracker.SetStartTime(Time.time);
            _tracker.ResetEnemiesDefeated();
        }
    }
}
