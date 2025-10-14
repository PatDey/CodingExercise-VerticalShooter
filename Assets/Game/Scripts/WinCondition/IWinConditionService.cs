namespace CEVerticalShooter.Game.WinCondition
{
    public interface IWinConditionService
    {
        public bool HasReachedAnyWinCondition {  get; }
        public void ResetWinConditionTracker();
    }
}
