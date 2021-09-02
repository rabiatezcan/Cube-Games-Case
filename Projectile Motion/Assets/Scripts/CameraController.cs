using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region State Machine Variables

    private CameraBaseState currentState;
    public readonly CameraIdleState IdleState = new CameraIdleState();
    public readonly CameraUpState UpState = new CameraUpState();
    public readonly CameraDownState DownState = new CameraDownState();
    public readonly CameraRightState RightState = new CameraRightState();
    public readonly CameraLeftState LeftState = new CameraLeftState();

    #endregion

    void Start()
    {
        TransitionToState(IdleState);
    }

    void Update()
    {
        currentState.Update(this);
    }

    public void TransitionToState(CameraBaseState state)
    {
        currentState = state;
    }
}