using UnityEngine;
using UnityEngine.XR;
using ccU3DEngine;

public class GameInputCtrl : MonoBehaviour
{
    #region 按鈕事件
    public delegate void OnClickCtrl(int iInput);
    public static event OnClickCtrl OnClickCtrlEvent;
    public static event OnClickCtrl OnClickBtnOne;
    public static event OnClickCtrl OnClickBtnTwo;
    public static event OnClickCtrl OnClickBtnThree;

    public static event OnClickCtrl OnClickBtnArrow;
    public static event OnClickCtrl OnClickBtnReset;
    public static event OnClickCtrl OnClickBtnChangeState;
    #endregion

    /// <summary>輸入冷卻</summary>
    bool _bBtnTime = false;
    /// <summary>輸入模式</summary>
    public static ControlState State = ControlState.PC;
    /// <summary>VR輸入裝備</summary>
    private InputDevice[] _Device = new InputDevice[2];

    private void FixedUpdate()
    {
        if (GameMain.GetInstance() == null) { return; }
        f_MouseMoveInput();
        f_EditInput();
        f_InputKey();
    }

    /// <summary>PC移動輸入</summary>
    private void f_MouseMoveInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            OnClickBtnArrow.Invoke(-1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            OnClickBtnArrow.Invoke(1);
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            OnClickBtnReset.Invoke(0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            OnClickBtnArrow.Invoke(-2);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            OnClickBtnArrow.Invoke(2);
        }
    }

    /// <summary>輸入冷卻時間</summary>
    private void f_InputCooling(object e)
    {
        _bBtnTime = false;
    }

    #region 一般輸入
    /// <summary>一般輸入</summary>
    private void f_InputKey()
    {
        //if (_fBtnTime < 0.1f) { return; }
        if (_bBtnTime) { return; }
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
        _bBtnTime = true;
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
        if (Input.GetKeyDown(KeyCode.Space))//選取物件用
        {
            _bBtnTime = true;
            OnClickCtrlEvent(0);
            ccTimeEvent.GetInstance().f_RegEvent(0.1f, false, null, f_InputCooling);
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            _bBtnTime = true;
            OnClickCtrlEvent(1);
            ccTimeEvent.GetInstance().f_RegEvent(0.1f, false, null, f_InputCooling);
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            _bBtnTime = true;
            OnClickCtrlEvent(2);
            ccTimeEvent.GetInstance().f_RegEvent(0.1f, false, null, f_InputCooling);
        }
    }
    #endregion

    #region 編輯模式輸入
    /// <summary>編輯模式下輸入</summary>
    private void f_EditInput()
    {
        if (GameMain.GetInstance().m_EditManager == null) { return; }
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
                OnClickBtnOne(1);
            }
            else if (vPos.x < -0.5f)
            {
                OnClickBtnTwo(-1);
            }
            else
            {
                OnClickBtnOne(0);
            }
        }

        if (_bBtnTime) { return; }
        if (_Device[0].TryGetFeatureValue(CommonUsages.triggerButton, out triggerBtnAction) && triggerBtnAction)//移動座標用
        {
            _bBtnTime = true;
            ccTimeEvent.GetInstance().f_RegEvent(0.2f, false, null, f_InputCooling);
            OnClickCtrlEvent(0);
        }
    }

    /// <summary>PC輸入</summary>
    private void f_PCEditInput()
    {
        //將按鈕功能依序填入
        if (Input.GetKey(KeyCode.Z))//拉前、旋轉、放大用
        {
            _bBtnTime = true;
            OnClickBtnOne(1);
            ccTimeEvent.GetInstance().f_RegEvent(0.1f, false, null, f_InputCooling);
        }
        else if (Input.GetKey(KeyCode.X))
        {
            _bBtnTime = true;
            OnClickBtnOne(-1);
            ccTimeEvent.GetInstance().f_RegEvent(0.1f, false, null, f_InputCooling);
        }
        else if (Input.GetKey(KeyCode.C))
        {
            _bBtnTime = true;
            OnClickBtnTwo(1);
            ccTimeEvent.GetInstance().f_RegEvent(0.1f, false, null, f_InputCooling);
        }
        else if (Input.GetKey(KeyCode.V))
        {
            _bBtnTime = true;
            OnClickBtnTwo(-1);
            ccTimeEvent.GetInstance().f_RegEvent(0.1f, false, null, f_InputCooling);
        }
        else if (Input.GetKey(KeyCode.B))
        {
            _bBtnTime = true;
            OnClickBtnThree(1);
            ccTimeEvent.GetInstance().f_RegEvent(0.1f, false, null, f_InputCooling);
        }
        else if (Input.GetKey(KeyCode.B))
        {
            _bBtnTime = true;
            OnClickBtnThree(-1);
            ccTimeEvent.GetInstance().f_RegEvent(0.1f, false, null, f_InputCooling);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            _bBtnTime = true;
            OnClickCtrlEvent(0);
            ccTimeEvent.GetInstance().f_RegEvent(0.1f, false, null, f_InputCooling);
        }
        else//鬆開按鈕
        {
            OnClickBtnOne(0);
        }
    }
    #endregion
}
