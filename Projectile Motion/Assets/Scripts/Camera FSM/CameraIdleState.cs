using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraIdleState : CameraBaseState
{
    public override void Update(CameraController camera)
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            camera.TransitionToState(camera.UpState);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            camera.TransitionToState(camera.LeftState);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            camera.TransitionToState(camera.DownState);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            camera.TransitionToState(camera.RightState);
        }
    }
}