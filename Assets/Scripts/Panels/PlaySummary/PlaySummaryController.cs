using UnityEngine;

public class PlaySummaryController : PanelController
{
    [SerializeField] private ResultView resultView;
    [SerializeField] DanceSession danceSessionConfig;
    [SerializeField] private BackgroundView _backgroundView;

    protected override void OnAwake()
    {
    }

    protected override void SubscribeToEvents(bool isSubscribed)
    {
    }

    private void Start()
    {
        Wallet.Instance.AddCoins(danceSessionConfig.coins);
        _activeCharacter.UpdateExperience(danceSessionConfig.rounds, danceSessionConfig.points);
        _backgroundView.Show(_activeCharacter.Config
                                             .reelBackground[0]);
        resultView.Show(danceSessionConfig.rounds.ToString(),
                        (danceSessionConfig.rounds * danceSessionConfig.points).ToString("F0"),
                        danceSessionConfig.coins.ToString());
    }
}