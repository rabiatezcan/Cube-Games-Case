using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRightState : CameraBaseState
{
    public override void Update(CameraController camera)
    {
        float movementRate = Constants.amountOfMovement * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            camera.transform.Rotate(new Vector3(0, -1, 0), movementRate);
        }
        else
        {
            camera.transform.position = new Vector3(camera.transform.position.x + movementRate, camera.transform.position.y, camera.transform.position.z);
        }
        
        if (Input.GetKeyUp(KeyCode.D))
        {
            camera.TransitionToState(camera.IdleState);
        }
    }
}