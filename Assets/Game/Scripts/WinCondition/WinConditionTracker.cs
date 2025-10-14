namespace CEVerticalShooter
{
    public class WinConditionTracker
    {
        private float _startTime;
        private int _enemiesDefeated;
        public float StartTime => _startTime;
        public int EnemiesDefeated => _enemiesDefeated;

        public WinConditionTracker()
        {
            _startTime = 0;
            _enemiesDefeated = 0;
        }

        public void SetStartTime(float startTime) => _startTime = startTime;
        public void IncreaseEnemiesDefeated() => _enemiesDefeated++;
        public void ResetEnemiesDefeated() => _enemiesDefeated = 0;
    }
}
