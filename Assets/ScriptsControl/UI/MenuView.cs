using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : ViewScreen
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _tutorialBtn;
    [SerializeField] private Button _storeBtn;

    private void Awake()
    {
        _playButton.onClick.AddListener(() =>
        {
            HandlerView.OnScreenOpen?.Invoke(NameScreen.Levels);

            if (PlayerPrefs.GetInt("TutorialShow") == 0)
            {
                PlayerPrefs.SetInt("TutorialShow", 1);
                Tutorials.Instance.RunTutorial();
            }
        });

        _tutorialBtn.onClick.AddListener(() =>
        {
            Tutorials.Instance.RunTutorial();
        });

        _settingsButton.onClick.AddListener(() =>
        {
            HandlerView.OnScreenOpen?.Invoke(NameScreen.Settings);
        });

        _storeBtn.onClick.AddListener(() =>
        {
            HandlerView.OnScreenOpen?.Invoke(NameScreen.Store);
        });
    }
}
