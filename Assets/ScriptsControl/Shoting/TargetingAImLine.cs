using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingAImLine : MonoBehaviour
{
    [SerializeField] private Transform _lineAim;
    [SerializeField] private ShotingPlayer _shoting;

    public void SetDirection(Vector2 direction)
    {
        _lineAim.up = direction;
        _shoting.SetDirection(direction);
    }

    public void StartShoting()
    {
        _lineAim.gameObject.SetActive(true);
        _shoting.StartShoting();
    }

    public void StopShoting()
    {
        _lineAim.gameObject.SetActive(false);
        _shoting.StopShoting();
    }
}
