using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Instance;

    private int currentActiveLevel;

    [SerializeField] private ResultView resultView;
    [SerializeField] private Player _player;

    private void Awake()
    {
        Application.targetFrameRate = 90;
        if (Instance == null) Instance = this;
    }

    public void StartLevel(int numberLevel)
    {
        currentActiveLevel = numberLevel;
        LevelData levelData = HandlerDataLevels.Instance.GetDataLevel(numberLevel);
        EventsBusMini.OnStopGame?.Invoke();
        EventsBusMini.OnStartLevel?.Invoke(levelData.countLenght);
        HandlerView.OnScreenOpen?.Invoke(NameScreen.GameUI);
    }

    public void RestartLevel()
    {
        StartLevel(currentActiveLevel);
    }

    public void OpenResult(EventsBusMini.TypeResult typeResult)
    {
        EventsBusMini.OnStopGame?.Invoke();
        int rewardCoin = 0;
        switch (typeResult)
        {
            case EventsBusMini.TypeResult.Win:
                rewardCoin = CoinBalance.Instance.GetCoinResultInLevel();
                HandlerDataLevels.Instance.SuccessLevel(currentActiveLevel);

                CoinBalance.Instance.AddToBalance(rewardCoin);

                HanSoundsGame.OnRunSound?.Invoke(HanSoundsGame.NameSound.Win);
                break;
            case EventsBusMini.TypeResult.Lose:
                rewardCoin = 0;
                HanSoundsGame.OnRunSound?.Invoke(HanSoundsGame.NameSound.Lose);
                break;
        }

        resultView.ShowResult(typeResult, currentActiveLevel, rewardCoin);
    }
}
