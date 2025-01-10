using System;
using UnityEngine;

public enum NameScreen
{
    MenuUI,
    GameUI,
    Store,
    Levels,
    Settings
}

public class HandlerView : MonoBehaviour
{
    public static Action<NameScreen> OnScreenOpen;

    [SerializeField] private ViewScreen _menu;
    [SerializeField] private ViewScreen _game;
    [SerializeField] private ViewScreen _store;
    [SerializeField] private ViewScreen _levels;
    [SerializeField] private ViewScreen _settings;

    private ViewScreen _screenActive;

    private void Start()
    {
        OnScreenOpen += OpenScreen;

        OpenScreen(NameScreen.MenuUI);
    }

    private void OnDisable()
    {
        OnScreenOpen -= OpenScreen;
    }

    private void OpenScreen(NameScreen nameScreen)
    {
        if (_screenActive != null) _screenActive.Hide();

        switch (nameScreen)
        {
            case NameScreen.MenuUI:
                _screenActive = _menu;
                break;
            case NameScreen.GameUI:
                _screenActive = _game;
                break;
            case NameScreen.Store:
                _screenActive = _store;
                break;
            case NameScreen.Levels:
                _screenActive = _levels;
                break;
            case NameScreen.Settings:
                _screenActive = _settings;
                break;
        }

        _screenActive.Show();
    }
}
