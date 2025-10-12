using CEVerticalShooter.Game.Score;
using TMPro;
using UnityEngine;
using VContainer;

namespace CEVerticalShooter.Game.UI
{
    public class HUDController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private TextMeshProUGUI scoreText;

        private IScoreService _scoreService;

        [Inject]
        private void Construct(IScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        private void Awake()
        {
            _scoreService.OnScoreChange += ScoreService_OnScoreChange;
            ScoreService_OnScoreChange(_scoreService.Score);
        }

        private void OnDestroy()
        {
            _scoreService.OnScoreChange -= ScoreService_OnScoreChange;
        }

        private void ScoreService_OnScoreChange(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}
