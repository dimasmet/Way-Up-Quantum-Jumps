using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EchoButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    [Header("View Sprites Button")]
    [SerializeField] private Image _imgBtn;

    public enum StateEcho
    {
        Open,
        Close
    }

    private void Awake()
    {
        _button.onClick.AddListener(() =>
        {
            EchoHandler.Instance.ActiveBonus();
            ChangeState(StateEcho.Close);
        });
    }

    public void ChangeState(StateEcho stateEcho)
    {
        switch (stateEcho)
        {
            case StateEcho.Open:
                _button.interactable = true;
                _imgBtn.fillAmount = 1;
                break;
            case StateEcho.Close:
                _button.interactable = false;
                _imgBtn.fillAmount = 0;
                break;
        }
    }

    public void ButtonFill(float value)
    {
        _imgBtn.fillAmount = value;
    }
}
