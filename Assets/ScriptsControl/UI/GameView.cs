using UnityEngine;
using UnityEngine.UI;

public class GameView : ViewScreen
{
    [SerializeField] private Button _backButton;

    private void Awake()
    {
        _backButton.onClick.AddListener(() =>
        {
            HandlerView.OnScreenOpen?.Invoke(NameScreen.Levels);

            EventsBusMini.OnStopGame?.Invoke();
        });
    }
}
