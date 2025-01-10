using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : ViewScreen
{
    [SerializeField] private Button _backBtn;
    [SerializeField] private Button _soundsBtn;
    [SerializeField] private Button _openPrivacyBtn;
    [SerializeField] private Button _closePrivacyBtn;
    [SerializeField] private GameObject _Privacy;

    [SerializeField] private HanSoundsGame hanSoundsGame;

    private void Awake()
    {
        Application.targetFrameRate = 90;

        _openPrivacyBtn.onClick.AddListener(() =>
        {
            _Privacy.SetActive(true);
        });

        _closePrivacyBtn.onClick.AddListener(() =>
        {
            _Privacy.SetActive(false);
        });

        _backBtn.onClick.AddListener(() =>
        {
            HandlerView.OnScreenOpen?.Invoke(NameScreen.MenuUI);
        });

        _soundsBtn.onClick.AddListener(() =>
        {
            bool state = hanSoundsGame.ChangeSoundsGlobal();
            if (state) _soundsBtn.transform.GetChild(0).GetComponent<Text>().text = "ON";
            else _soundsBtn.transform.GetChild(0).GetComponent<Text>().text = "OFF";
        });
    }
}
