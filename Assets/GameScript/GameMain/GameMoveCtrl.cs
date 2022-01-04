using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// 視角跟隨滑鼠移動
/// </summary>
public class GameMoveCtrl : MonoBehaviour
{
    public float _fRotationSpeed = 10f;
    float rotationY = 0f;

    private void f_CameraForMouse()
    {
        float fMouseX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * _fRotationSpeed;
        //float fMouseY = transform.localEulerAngles.x + Input.GetAxis("Mouse Y") * _fRotationSpeed;
        rotationY += Input.GetAxis("Mouse Y") * _fRotationSpeed;
        rotationY = Mathf.Clamp(rotationY, -60, 60);

        transform.localEulerAngles = new Vector3(-rotationY, fMouseX, 0);
    }

    // Update is called once per frame
    void Update()
    {
        f_CameraForMouse();
    }
}
