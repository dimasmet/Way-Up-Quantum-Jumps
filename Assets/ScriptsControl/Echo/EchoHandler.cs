using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoHandler : MonoBehaviour
{
    public static EchoHandler Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    [SerializeField] private EchoButton _echoButton;

    [SerializeField] private Player _player;

    public void ActiveBonus()
    {
        _player.SaveSpawn();
        _timeLeft = 0;
        normalizedValue = 0;
        StartCoroutine(StartTimer());
    }

    [SerializeField] private float _timeReset;

    private float _timeLeft = 0f;
    float normalizedValue;

    private IEnumerator StartTimer()
    {
        while (normalizedValue < 1)
        {
            _timeLeft += Time.deltaTime;
            normalizedValue = Mathf.Clamp(_timeLeft / _timeReset, 0.0f, 1.0f);
            _echoButton.ButtonFill(normalizedValue);
            yield return null;
        }

        _echoButton.ChangeState(EchoButton.StateEcho.Open);

        EventsBusMini.OnReloadBonusEcho?.Invoke();
    }
}
