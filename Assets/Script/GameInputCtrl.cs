using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Events;
using ccU3DEngine;

public class GameInputCtrl : MonoBehaviour
{
    public delegate void OnClickCtrl(int iInput);
    public static event OnClickCtrl OnClickCtrlEvent;
    public static event OnClickCtrl OnClickBtnOne;
    public static event OnClickCtrl OnClickBtnTwo;
    public static event OnClickCtrl OnClickBtnThree;

    public Transform Player;
    float _fBtnTime = 0f;

    public static ControlState State = ControlState.PC;
    private InputDevice[] _Device = new InputDevice[2];
    
    private void Update()
    {
        _fBtnTime += Time.deltaTime;//按鈕間隔時間
    }

    private void FixedUpdate()
    {
        f_EditInput();
        f_InputKey();
    }

    /// <summary>PC移動輸入</summary>
    private void f_MouseMoveInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Player.localPosition += transform.forward * 3f * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Player.localPosition += -transform.forward * 3f * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Player.localPosition += -transform.right * 3f * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Player.localPosition += transform.right * 3f * Time.deltaTime;
        }
    }

    #region 一般輸入
    /// <summary>一般輸入</summary>
    private void f_InputKey()
    {
        if (_fBtnTime < 0.1f) { return; }
        switch (State)
        {
            case ControlState.VR:
                f_VRInputKey();
                break;

            case ControlState.PC:
                f_PCInputKey();
                break;
        }
    }

    /// <summary>VR輸入</summary>
    private void f_VRInputKey()
    {
        _fBtnTime = 0;
        if (!_Device[0].isValid || !_Device[1].isValid)
        {
            _Device[0] = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            _Device[1] = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        }

        bool triggerBtnAction = false;
        if (_Device[0].TryGetFeatureValue(CommonUsages.triggerButton, out triggerBtnAction) && triggerBtnAction)
        {
            OnClickCtrlEvent(0);
        }
    }

    /// <summary>PC輸入</summary>
    private void f_PCInputKey()
    {
        if (GameMain.GetInstance().m_EditManager._bEdit) { return; }
        if (Input.GetKey(KeyCode.Space))//選取物件用
        {
            OnClickCtrlEvent(0);
            _fBtnTime = 0;
        }
    }
    #endregion

    #region 編輯模式輸入
    /// <summary>編輯模式下輸入</summary>
    private void f_EditInput()
    {
        if (!GameMain.GetInstance().m_EditManager._bEdit) { return; }
        switch (State)
        {
            case ControlState.VR:
                f_VREditInput();
                break;

            case ControlState.PC:
                f_PCEditInput();
                break;
        }
    }

    /// <summary>VR輸入</summary>
    private void f_VREditInput()
    {
        if (!_Device[0].isValid || !_Device[1].isValid)
        {
            _Device[0] = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            _Device[1] = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        }

        if (!GameMain.GetInstance().m_EditManager._bEdit) { return; }
        bool triggerBtnAction = false;
        if (_Device[1].TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 vPos))
        {
            if (vPos.x > 0.5f)
            {
                //_EditObjControll.f_SetInput(1);
                OnClickBtnOne(1);
            }
            else if (vPos.x < -0.5f)
            {
                //_EditObjControll.f_SetInput(-1);
                OnClickBtnTwo(-1);
            }
            else
            {
                //_EditObjControll.f_SetInput(0);
                OnClickBtnOne(0);
            }
        }

        if (_fBtnTime < 0.2f) { return; }
        if (_Device[0].TryGetFeatureValue(CommonUsages.triggerButton, out triggerBtnAction) && triggerBtnAction)//移動座標用
        {
            _fBtnTime = 0;
            //_EditObjControll.OnClicked();
            OnClickCtrlEvent(0);
        }
    }

    /// <summary>PC輸入</summary>
    private void f_PCEditInput()
    {
        if (Input.GetKey(KeyCode.RightArrow))//拉前、旋轉、放大用
        {
            //_EditObjControll.f_SetInput(1);
            OnClickBtnOne(1);
            _fBtnTime = 0;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))//退後、旋轉、縮小用
        {
            //_EditObjControll.f_SetInput(-1);
            OnClickBtnTwo(-1);
            _fBtnTime = 0;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            //_EditObjControll.f_SetInput(0);
            OnClickBtnOne(0);
        }

        _fBtnTime += Time.deltaTime;//按鈕間隔時間
        if (_fBtnTime < 0.01f) { return; }
        if (Input.GetKeyUp(KeyCode.Space))//移動座標用
        {
            OnClickCtrlEvent(0);
            _fBtnTime = 0;
        }
    }
    #endregion
}
