using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// 視角跟隨滑鼠移動
/// </summary>
public class GameMoveCtrl : MonoBehaviour
{
    public GameObject XR;

    public TestState testState = TestState.PC;
    public enum TestState
    {
        PC = 1,
        XR = 2,
    }

    public float _fRotationSpeed = 10f;
    float rotationY = 0f;

    private void f_CameraForMouse()
    {
        if (testState != TestState.PC) { return; }
        float fMouseX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * _fRotationSpeed;
        //float fMouseY = transform.localEulerAngles.x + Input.GetAxis("Mouse Y") * _fRotationSpeed;
        rotationY += Input.GetAxis("Mouse Y") * _fRotationSpeed;
        rotationY = Mathf.Clamp(rotationY, -60, 60);

        transform.localEulerAngles = new Vector3(-rotationY, fMouseX, 0);
    }

    private void Start()
    {
        if (testState != TestState.PC) { return; }
        if (XR == null) { return; }
        XR.transform.position = Camera.main.transform.position + new Vector3(-5, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        f_CameraForMouse();
    }
}
