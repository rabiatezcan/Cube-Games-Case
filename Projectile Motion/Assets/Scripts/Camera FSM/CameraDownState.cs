using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDownState : CameraBaseState
{
    public override void Update(CameraController camera)
    {
        float movementRate = Constants.amountOfMovement * Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            camera.transform.Rotate(new Vector3(1,0,0), movementRate);
        }
        else
        {
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y - movementRate, camera.transform.position.z);
        }
        
        if (Input.GetKeyUp(KeyCode.S))
        {
            camera.TransitionToState(camera.IdleState);
        }
    }
}
