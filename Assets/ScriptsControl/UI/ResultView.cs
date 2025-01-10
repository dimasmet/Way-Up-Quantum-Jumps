using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultView : ViewScreen
{
    [SerializeField] private Button _restartBtn;
    [SerializeField] private Button _levelsBtn;
    [SerializeField] private GameObject _rewardObj;

    [SerializeField] private Text _resultText;
    [SerializeField] private Text _levelText;

    private void Awake()
    {
        _restartBtn.onClick.AddListener(() =>
        {
            Hide();
            Main.Instance.RestartLevel();
        });

        _levelsBtn.onClick.AddListener(() =>
        {
            HandlerView.OnScreenOpen?.Invoke(NameScreen.Levels);
            Hide();
        });
    }

    public void ShowResult(EventsBusMini.TypeResult type,int numberLvl, int coinReward = 0)
    {
        Show();

        _levelText.text = "LEVEL " + (numberLvl + 1);

        switch (type)
        {
            case EventsBusMini.TypeResult.Win:
                _resultText.text = "YOU WIN";
                _rewardObj.SetActive(true);
                _rewardObj.transform.GetChild(0).GetComponent<Text>().text = coinReward.ToString();
                break;
            case EventsBusMini.TypeResult.Lose:
                _resultText.text = "YOU LOSE";
                _rewardObj.SetActive(false);
                break;
        }
    }
}
