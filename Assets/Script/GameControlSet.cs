using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ControlState
{
    PC,
    VR,
}

public class GameControlSet : MonoBehaviour
{
    public ControlState State = ControlState.VR;

    // Start is called before the first frame update
    void Start()
    {
        GameInputCtrl.State = State;
    }
}
