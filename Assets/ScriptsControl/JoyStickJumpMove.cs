using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickJumpMove : JoyStickMain
{
    [SerializeField] private Player _movementPlayer;

    public override void Action(float speedPercent)
    {
        base.Action(speedPercent);
        _movementPlayer.SetParamsToMove(Direction, speedPercent);
    }

    public override void DownToJoystick()
    {
        base.DownToJoystick();
        _movementPlayer.StartMove();
    }

    public override void UpToJoystick()
    {
        base.UpToJoystick();
        _movementPlayer.StopMove();
    }
}
