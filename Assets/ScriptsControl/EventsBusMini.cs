using System;
using UnityEngine;

public class EventsBusMini : MonoBehaviour
{
    public static Action OnUsedPlatfrom;
    public static Action<int> OnStartLevel;
    public static Action<Transform> OnSetTargetCamera;
    public static Action OnStopGame;
    public static Action<TypeResult> OnResultGame;

    public static Action<Vector2> OnMoveToSavePos;
    public static Action OnRestartSave;
    public static Action OnReloadBonusEcho;

    public enum TypeResult
    {
        Win,
        Lose
    }
}
