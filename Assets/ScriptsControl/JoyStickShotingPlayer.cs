using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickShotingPlayer : JoyStickMain
{
    [SerializeField] private TargetingAImLine _targetingAImLine;

    public override void Action(float percentSpeed)
    {
        base.Action(percentSpeed);
        _targetingAImLine.SetDirection(Direction);
    }

    public override void DownToJoystick()
    {
        base.DownToJoystick();
        _targetingAImLine.StartShoting();
    }

    public override void UpToJoystick()
    {
        base.UpToJoystick();
        _targetingAImLine.StopShoting();
    }
}
