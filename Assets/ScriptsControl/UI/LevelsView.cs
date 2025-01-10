using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsView : ViewScreen
{
    [SerializeField] private ButtonLevel[] buttonLevels;
    [SerializeField] private Button _backButton;

    private void Awake()
    {
        _backButton.onClick.AddListener(() =>
        {
            HandlerView.OnScreenOpen?.Invoke(NameScreen.MenuUI);
        });
    }

    public void InitLevelsButtons(WrapperLevels wrapperLevels)
    {
        for (int i = 0; i < buttonLevels.Length; i++)
        {
            buttonLevels[i].InitLevelButton(wrapperLevels.levelDatas[i]);
        }
    }

    public void UpdateView()
    {
        for (int i = 0; i < buttonLevels.Length; i++)
        {
            buttonLevels[i].UpdateStateViewLevel();
        }
    }

    public override void Show()
    {
        base.Show();
        UpdateView();
    }
}
