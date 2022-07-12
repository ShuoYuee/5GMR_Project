using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ccU3DEngine;
using Epibyte.ConceptVR;
using ccUI_U3DSpace;
using MR_Edit;

/*public class GameMainTriggerCtrl : MonoBehaviour
{
    public Transform _Focus = null;

    private EditObjControll _EditObjControll = null;
    private Interactable _Interactable = null;
    private InputField _InputField = null;
    private Button _Button = null;

    private GameObject oCurObj = null;
    private EM_TriggerObj _ObjEm = EM_TriggerObj.None;
    private bool _bLookTime = false;

    private EM_PlayCtrl _PlayCtrl = EM_PlayCtrl.Positon;

    public enum EM_TriggerObj
    {
        None = 0,
        EditObj = 1,    //編輯物
        Button = 2,     //按鈕
        InpuUI = 3,     //輸入文字UI
        ButtonUI = 4,   //按鈕UI
    }

    /// <summary>輸入控制模式</summary>
    private enum EM_PlayCtrl
    {
        Positon,
        Rotation,
        Scale,
        Height,
    }

    private void Start()
    {
        //暫預定四個輸入事件
        GameInputCtrl.OnClickCtrlEvent += f_OnClick;
        GameInputCtrl.OnClickBtnOne += f_EditCtrl;
        GameInputCtrl.OnClickBtnTwo += f_EditCtrl;
        GameInputCtrl.OnClickBtnThree += f_EditCtrl;

        //控制玩家輸入事件
        GameInputCtrl.OnClickBtnArrow += f_PlayCtrlInput;
        GameInputCtrl.OnClickBtnChangeState += f_ChangePlayCtrlState;
        GameInputCtrl.OnClickBtnReset += f_ResetInitPos;
    }

    // Update is called once per frame
    void Update()
    {
        f_RayTrigger();
    }

    /// <summary>射線碰撞偵測</summary>
    private void f_RayTrigger()
    {
        Ray tRay = new Ray(transform.position, transform.forward * 20);
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * 20, Color.yellow);
        if(Physics.Raycast(tRay, out hit, Mathf.Infinity))
        {
            Debug.DrawLine(tRay.origin, hit.point, Color.red);
            if (_bLookTime) { return; }

            if (hit.collider.gameObject != oCurObj && oCurObj != null)
            {
                if (_Interactable != null)
                {
                    _Interactable.OnLeave();
                    _Interactable = null;
                }
            }

            //將要偵測的元件一一填入
            if (hit.collider.GetComponent<EditObjControll>() != null)//編輯物件
            {
                _ObjEm = EM_TriggerObj.EditObj;
                oCurObj = hit.collider.gameObject;
            }

            else if (hit.collider.GetComponent<Interactable>() != null)//按鈕物件
            {
                _ObjEm = EM_TriggerObj.Button;
                oCurObj = hit.collider.gameObject;
                _Interactable = oCurObj.GetComponent<Interactable>();
                _Interactable.OnHovered();
            }
            else if(hit.collider.GetComponent<InputField>() != null)//輸入文字框物件
            {
                _ObjEm = EM_TriggerObj.InpuUI;
                oCurObj = hit.collider.gameObject;
                _InputField = hit.collider.GetComponent<InputField>();//按鈕UI物件
            }
            else if(hit.collider.GetComponent<Button>() != null)//UI按鈕
            {
                _ObjEm = EM_TriggerObj.ButtonUI;
                oCurObj = hit.collider.gameObject;
                _Button = hit.collider.GetComponent<Button>();
            }

            f_SetFocus(hit.point);
            ccTimeEvent.GetInstance().f_RegEvent(0.1f, false, null, f_InputCooling);
        }
        else
        {
            f_SetFocus(hit.point);
        }
    }

    /// <summary>輸入冷卻時間</summary>
    private void f_InputCooling(object e)
    {
        _bLookTime = false;
    }

    /// <summary>
    /// 設定焦點
    /// </summary>
    /// <param name="vTarget">焦點位置</param>
    private void f_SetFocus(Vector3 vTarget)
    {
        if (_Focus == null) { return; }
        _Focus.position = vTarget;//焦點設置在碰撞物前方

        //保持焦點在視野內的大小
        float distance = Vector3.Distance(transform.position, vTarget);
        float fScale = 0.02f * distance * Mathf.Tan(GetComponent<Camera>().fieldOfView * Mathf.Deg2Rad);
        _Focus.localScale = new Vector3(fScale, fScale, fScale);
    }

    #region 互動輸入事件
    /// <summary>一般輸入事件</summary>
    private void f_OnClick(int e)
    {
        if (oCurObj == null) { return; }

        //將各對象的執行動作一一填入
        switch (_ObjEm)
        {
            case EM_TriggerObj.Button://3D按鈕
                if (_Interactable == null)
                {
                    _ObjEm = EM_TriggerObj.None;
                    return;
                }
                _Interactable.OnClicked();
                break;

            case EM_TriggerObj.EditObj://編輯物
                if (GameMain.GetInstance().m_EditManager._bEdit)
                {
                    if (_EditObjControll != null && _EditObjControll.gameObject != oCurObj)
                    {
                        _EditObjControll.f_SetEditState(false);
                        _EditObjControll = oCurObj.GetComponent<EditObjControll>();
                    }
                    else
                    {
                        _EditObjControll = oCurObj.GetComponent<EditObjControll>();
                    }

                    if (_EditObjControll == null)
                    {
                        _ObjEm = EM_TriggerObj.None;
                        return;
                    }

                    GameMain.GetInstance().m_EditManager._bSelectEdit = true;
                    _EditObjControll.f_SetEditState(true);
                    _EditObjControll.OnClicked();
                }
                else
                {
                    if (oCurObj)
                    {
                        EditObjControll editObjControll = oCurObj.GetComponent<EditObjControll>();
                        if (e == 1)
                        {
                            editObjControll.f_AnimPlay(0);
                        }
                        else if (e == 2)
                        {
                            editObjControll.f_ConnectURL();
                        }
                    }
                }
                break;

            case EM_TriggerObj.InpuUI://UI輸入框
                if (_InputField == null)
                {
                    _ObjEm = EM_TriggerObj.None;
                    return;
                }
                EventSystem.current.SetSelectedGameObject(null);//重新輸入
                _InputField.Select();
                if (GameMain.GetInstance().m_EditManager._bSelectEdit)//若為編輯模式則調用開始輸入文本之功能
                {
                    _InputField.GetComponentInParent<EditDisplayText>().f_InputValue();//調用開始輸入文本之功能
                }
                break;

            case EM_TriggerObj.ButtonUI://UI按鈕
                if (_Button == null)
                {
                    _ObjEm = EM_TriggerObj.None;
                    return;
                }
                _Button.onClick.Invoke();
                break;
        }
    }

    /// <summary>
    /// 編輯模式輸入事件
    /// </summary>
    /// <param name="iInput">判別值(左為-1  右為1)</param>
    private void f_EditCtrl(int iInput)
    {
        if (!GameMain.GetInstance().m_EditManager._bEdit) { return; }
        if (!GameMain.GetInstance().m_EditManager._bSelectEdit) { return; }
        _EditObjControll = GameMain.GetInstance().f_GetCurEditObj();
        if (_EditObjControll == null) { return; }

        _EditObjControll.f_SetInput(iInput);
    }
    #endregion

    #region 控制輸入事件
    private Transform Player;
    /// <summary>改變控制輸入模式</summary>
    private void f_ChangePlayCtrlState(int i = 0)
    {
        int iState = (int)_PlayCtrl;
        iState += 1;
        if (iState > (int)EM_PlayCtrl.Height)
        {
            iState = (int)EM_PlayCtrl.Positon;
        }
        _PlayCtrl = (EM_PlayCtrl)iState;

        if (GameMain.GetInstance()._PlayerCtrlText != null)
        {
            switch (_PlayCtrl)
            {
                case EM_PlayCtrl.Positon:
                    GameMain.GetInstance()._PlayerCtrlText.text = "當前操控模式 : 位移";
                    break;

                case EM_PlayCtrl.Rotation:
                    GameMain.GetInstance()._PlayerCtrlText.text = "當前操控模式 : 旋轉";
                    break;

                case EM_PlayCtrl.Scale:
                    GameMain.GetInstance()._PlayerCtrlText.text = "當前操控模式 : 場景縮放";
                    break;

                case EM_PlayCtrl.Height:
                    GameMain.GetInstance()._PlayerCtrlText.text = "當前操控模式 : 高度";
                    break;
            }
        }
    }

    /// <summary>玩家控制輸入</summary>
    private void f_PlayCtrlInput(int iArrow)
    {
        if (Player == null)
        {
            Player = GameMain.GetInstance().m_Player;
        }

        switch (_PlayCtrl)
        {
            case EM_PlayCtrl.Positon:
                f_CtrlPosition(iArrow);
                break;

            case EM_PlayCtrl.Rotation:
                f_CtrlRotation(iArrow);
                break;

            case EM_PlayCtrl.Scale:
                f_CtrlScale(iArrow);
                break;

            case EM_PlayCtrl.Height:
                f_CtrlHeight(iArrow);
                break;
        }
    }

    #region 玩家控制輸入
    /// <summary>控制玩家位移</summary>
    private void f_CtrlPosition(int iArrow)
    {
        switch (iArrow)
        {
            case 1:
                Player.localPosition += Player.forward * 3f * Time.deltaTime;
                break;

            case -1:
                Player.localPosition -= Player.forward * 3f * Time.deltaTime;
                break;

            case 2:
                Player.localPosition += -Player.right * 3f * Time.deltaTime;
                break;

            case -2:
                Player.localPosition += Player.right * 3f * Time.deltaTime;
                break;
        }
    }

    /// <summary>控制玩家旋轉</summary>
    private void f_CtrlRotation(int iArrow)
    {
        switch (iArrow)
        {
            case 2:
                Player.localEulerAngles += Vector3.up * 20f * Time.deltaTime;
                break;

            case -2:
                Player.localEulerAngles -= Vector3.up * 20f * Time.deltaTime;
                break;
        }
    }

    /// <summary>控制場上物件大小</summary>
    private void f_CtrlScale(int iArrow)
    {
        switch (iArrow)
        {
            case 1:
                GameMain.GetInstance().m_GameTable.transform.localScale += Vector3.one * Time.deltaTime;
                break;

            case -1:
                GameMain.GetInstance().m_GameTable.transform.localScale -= Vector3.one * Time.deltaTime;
                break;
        }
    }

    /// <summary>控制玩家高度</summary>
    private void f_CtrlHeight(int iArrow)
    {
        switch (iArrow)
        {
            case 1:
                Player.localPosition += Player.up * 3f * Time.deltaTime;
                break;
                
            case -1:
                Player.localPosition -= Player.up * 3f * Time.deltaTime;
                break;
        }
    }
    #endregion

    /// <summary>回歸原位</summary>
    private void f_ResetInitPos(int i)
    {
        if (i == 0)
        {
            Player.position = GameMain.GetInstance().m_InitPos.position;
            Player.eulerAngles = GameMain.GetInstance().m_InitPos.eulerAngles;
        }
        else if (i == 1)
        {
            GameMain.GetInstance().m_GameTable.transform.localScale = GameMain.GetInstance().m_InitPos.localScale;
        }
    }
    #endregion
}*/

public class GameMainTriggerCtrl : MonoBehaviour //責所有輸入事件的回饋 
                                                 //將寫好的事件邏輯Add給XRCubeCtr的Event，當Event被觸發時也會一同被觸發
{
    public Transform _Focus = null;

    private EditObjControll _EditObjControll = null;
    private Interactable _Interactable = null;
    private InputField _InputField = null;
    private Button _Button = null;
    private ccInteractable _ccInteractable = null;

    public EM_TriggerObj _ObjEm = EM_TriggerObj.None;
    private GameObject oCurObj = null;
    private bool _bLookTime = false;

    private EM_PlayCtrl _PlayCtrl = EM_PlayCtrl.Positon;

    public enum EM_TriggerObj
    {
        None = 0,
        EditObj = 1,    //編輯物
        Button = 2,     //按鈕
        InpuUI = 3,     //輸入文字UI
        ButtonUI = 4,   //按鈕UI
        MRUI = 5,       //MR UI
    }

    /// <summary>輸入控制模式</summary>
    private enum EM_PlayCtrl
    {
        Positon,
        Rotation,
        Scale,
        Height,
    }

    private void Start()
    {
        //暫預定四個輸入事件
        //GameInputCtrl.OnClickCtrlEvent += f_OnClick;
        //GameInputCtrl.OnClickBtnOne += f_EditCtrl;
        //GameInputCtrl.OnClickBtnTwo += f_EditCtrl;
        //GameInputCtrl.OnClickBtnThree += f_EditCtrl;
        XRCubeCtrl.OnClickCtrlEvent += f_OnClick;
        XRCubeCtrl.OnClickBtnOne += f_EditCtrl_H;
        XRCubeCtrl.OnClickBtnTwo += f_EditCtrl_V;
        XRCubeCtrl.OnClickBtnThree += f_EditCtrl_D;

        //控制玩家輸入事件
        GameInputCtrl.OnClickBtnArrow += f_PlayCtrlInput;
        //GameInputCtrl.OnClickBtnChangeState += f_ChangePlayCtrlState;
        GameInputCtrl.OnClickBtnReset += f_ResetInitPos;
    }

    // Update is called once per frame
    void Update()
    {
        //f_RayTrigger();
    }

    /*/// <summary>射線碰撞偵測</summary>
    private void f_RayTrigger()
    {
        Ray tRay = new Ray(transform.position, transform.forward * 20);
        RaycastHit hit;
        //Debug.DrawRay(transform.position, transform.forward * 20, Color.yellow);
        if (Physics.Raycast(tRay, out hit, Mathf.Infinity))
        {
            Debug.DrawLine(tRay.origin, hit.point, Color.red);
            if (hit.collider.tag == "XRCubeCollider")
            {
                if (_bLookTime) { return; }

                GameObject oCurCollider = hit.collider.gameObject;
                if (oCurCollider != oCurObj && oCurObj != null)
                {
                    if (_Interactable != null)
                    {
                        _Interactable.OnLeave();
                        _Interactable = null;
                    }
                }

                //將要偵測的元件一一填入
                if (oCurCollider.TryGetComponent(out EditObjControll tEditObjControll))//編輯物件
                {
                    _ObjEm = EM_TriggerObj.EditObj;
                    oCurObj = oCurCollider;
                }
                else if (oCurCollider.TryGetComponent(out Interactable interactable))//按鈕物件
                {
                    _ObjEm = EM_TriggerObj.Button;
                    oCurObj = oCurCollider;
                    _Interactable = interactable;
                    _Interactable.OnHovered();
                }
                else if (oCurCollider.TryGetComponent(out InputField inputField))//輸入文字框物件
                {
                    _ObjEm = EM_TriggerObj.InpuUI;
                    oCurObj = oCurCollider;
                    _InputField = inputField;//按鈕UI物件
                }
                else if (oCurCollider.TryGetComponent(out Button button))//UI按鈕
                {
                    _ObjEm = EM_TriggerObj.ButtonUI;
                    oCurObj = oCurCollider;
                    _Button = button;
                }

                f_SetFocus(hit.point);
                ccTimeEvent.GetInstance().f_RegEvent(0.1f, false, null, f_InputCooling);
            }
            else
            {
                f_SetFocus(hit.point);
            }
        }
    }*/

    /// <summary>輸入冷卻時間</summary>
    private void f_InputCooling(object e)
    {
        _bLookTime = false;
    }

    /// <summary>設定當前觸發物</summary>
    public void f_SetCtrl(GameObject oObj) //如果當前射線碰到觸發物時觸發
    {
        if (oObj != oCurObj && oCurObj != null)
        {
            if (_Interactable != null)
            {
                _Interactable.OnLeave();
                _Interactable = null;
            }
        }

        oCurObj = oObj;
        switch (_ObjEm)
        {
            case EM_TriggerObj.EditObj:
                
                break;

            case EM_TriggerObj.InpuUI:
                _InputField = oCurObj.GetComponent<InputField>();
                break;

            case EM_TriggerObj.Button:
                _Interactable = oCurObj.GetComponent<Interactable>();
                _Interactable.OnHovered();
                break;

            case EM_TriggerObj.ButtonUI:
                _Button = oCurObj.GetComponent<Button>();
                break;

            case EM_TriggerObj.MRUI:
                _ccInteractable = oCurObj.GetComponent<ccInteractable>();
                _ccInteractable.OnHovered();
                break;

            default:
                oCurObj = null;
                break;
        }
    }

    public void f_LeaveRay()
    {
        if (_ccInteractable != null)
        {
            _ccInteractable.UnHovered();
            _ccInteractable = null;
        }
    }

    /// <summary>
    /// 設定焦點
    /// </summary>
    /// <param name="vTarget">焦點位置</param>
    public void f_SetFocus(Vector3 vTarget)
    {
        if (_Focus == null) { return; }
        _Focus.position = vTarget;//焦點設置在碰撞物前方

        //保持焦點在視野內的大小
        float distance = Vector3.Distance(transform.position, vTarget);
        float fScale = 0.02f * distance * Mathf.Tan(GetComponent<Camera>().fieldOfView * Mathf.Deg2Rad);
        _Focus.localScale = new Vector3(fScale, fScale, fScale);
    }

    #region 互動輸入事件
    /// <summary>一般輸入事件</summary>
    private void f_OnClick(int iSet)
    {
        if (oCurObj == null) { return; }

        //將各對象的執行動作一一填入
        switch (_ObjEm)
        {
            case EM_TriggerObj.Button://3D按鈕
                if (_Interactable == null)
                {
                    _ObjEm = EM_TriggerObj.None;
                    return;
                }
                _Interactable.OnClicked();
                break;

            case EM_TriggerObj.EditObj://編輯物
                if (GameMain.GetInstance().m_EditManager._bEdit)
                {
                    if (_EditObjControll != null && _EditObjControll.gameObject != oCurObj)
                    {
                        _EditObjControll.f_SetEditState(false);
                        _EditObjControll = oCurObj.GetComponent<EditObjControll>();
                    }
                    else
                    {
                        _EditObjControll = oCurObj.GetComponent<EditObjControll>();
                    }

                    if (_EditObjControll == null)
                    {
                        _ObjEm = EM_TriggerObj.None;
                        return;
                    }

                    GameMain.GetInstance().m_EditManager._bSelectEdit = true;
                    _EditObjControll.f_SetEditState(true);
                    _EditObjControll.OnClicked();
                }
                else
                {
                    if (oCurObj)
                    {
                        EditObjControll editObjControll = oCurObj.GetComponent<EditObjControll>();
                        if (iSet == 0)
                        {
                            _EditObjControll.f_Interactable();
                        }
                        else if (iSet == 1)
                        {
                            //editObjControll.f_AnimPlay(0);
                            _EditObjControll.f_InteractableEM((int)EM_InterState.Anim);
                        }
                        else if (iSet == 2)
                        {
                            //editObjControll.f_ConnectURL();
                            _EditObjControll.f_InteractableEM((int)EM_InterState.URL);
                        }
                    }
                }
                break;

            case EM_TriggerObj.InpuUI://UI輸入框
                if (_InputField == null)
                {
                    _ObjEm = EM_TriggerObj.None;
                    return;
                }
                EventSystem.current.SetSelectedGameObject(null);//重新輸入
                _InputField.Select();
                if (GameMain.GetInstance().m_EditManager._bSelectEdit)//若為編輯模式則調用開始輸入文本之功能
                {
                    _InputField.GetComponentInParent<EditDisplayText>().f_InputValue();//調用開始輸入文本之功能
                }
                break;

            case EM_TriggerObj.ButtonUI://UI按鈕
                if (_Button == null)
                {
                    _ObjEm = EM_TriggerObj.None;
                    return;
                }
                _Button.onClick.Invoke();
                break;

            case EM_TriggerObj.MRUI:
                if (_ccInteractable == null)
                {
                    _ObjEm = EM_TriggerObj.None;
                    return;
                }
                _ccInteractable.OnClicked(iSet);
                break;
        }
    }

    /// <summary>
    /// 編輯模式輸入事件
    /// </summary>
    /// <param name="iInput">判別值(左為-1  右為1)</param>
    private void f_EditCtrl(int iInput)
    {
        /*if (!GameMain.GetInstance().m_EditManager._bEdit) { return; }
        if (!GameMain.GetInstance().m_EditManager._bSelectEdit) { return; }
        _EditObjControll = GameMain.GetInstance().f_GetCurEditObj();
        if (_EditObjControll != null)
        {
            _EditObjControll.f_SetInput(iInput);
        }*/
    }

    private void f_EditCtrl_V(int iInput)
    {
        _EditObjControll = GameMain.GetInstance().f_GetCurEditObj();
        if (_EditObjControll != null)
        {
            GameMain.GetInstance().f_SetEditAxis((int)EM_EditAxis.AxisY);
            _EditObjControll.f_SetInput(iInput);
        }
    }

    private void f_EditCtrl_H(int iInput)
    {
        _EditObjControll = GameMain.GetInstance().f_GetCurEditObj();
        if (_EditObjControll != null)
        {
            GameMain.GetInstance().f_SetEditAxis((int)EM_EditAxis.AxisX);
            _EditObjControll.f_SetInput(iInput);
        }
    }

    private void f_EditCtrl_D(int iInput)
    {
        _EditObjControll = GameMain.GetInstance().f_GetCurEditObj();
        if (_EditObjControll != null)
        {
            GameMain.GetInstance().f_SetEditAxis((int)EM_EditAxis.AxisZ);
            _EditObjControll.f_SetInput(iInput);
        }
    }
    #endregion

    #region 控制輸入事件
    private Transform Player;
    /*/// <summary>改變控制輸入模式</summary>
    private void f_ChangePlayCtrlState(int i = 0)
    {
        int iState = (int)_PlayCtrl;
        iState += 1;
        if (iState > (int)EM_PlayCtrl.Height)
        {
            iState = (int)EM_PlayCtrl.Positon;
        }
        _PlayCtrl = (EM_PlayCtrl)iState;

        if (GameMain.GetInstance()._PlayerCtrlText != null)
        {
            switch (_PlayCtrl)
            {
                case EM_PlayCtrl.Positon:
                    GameMain.GetInstance()._PlayerCtrlText.text = "當前操控模式 : 位移";
                    break;

                case EM_PlayCtrl.Rotation:
                    GameMain.GetInstance()._PlayerCtrlText.text = "當前操控模式 : 旋轉";
                    break;

                case EM_PlayCtrl.Scale:
                    GameMain.GetInstance()._PlayerCtrlText.text = "當前操控模式 : 場景縮放";
                    break;

                case EM_PlayCtrl.Height:
                    GameMain.GetInstance()._PlayerCtrlText.text = "當前操控模式 : 高度";
                    break;
            }
        }
    }*/

    /// <summary>玩家控制輸入</summary>
    private void f_PlayCtrlInput(int iArrow)
    {
        if (Player == null)
        {
            Player = GameMain.GetInstance().m_Player;
        }

        switch (_PlayCtrl)
        {
            case EM_PlayCtrl.Positon:
                f_CtrlPosition(iArrow);
                break;

            case EM_PlayCtrl.Rotation:
                f_CtrlRotation(iArrow);
                break;

            case EM_PlayCtrl.Scale:
                f_CtrlScale(iArrow);
                break;

            case EM_PlayCtrl.Height:
                f_CtrlHeight(iArrow);
                break;
        }
    }

    #region 玩家控制輸入
    /// <summary>控制玩家位移</summary>
    private void f_CtrlPosition(int iArrow)
    {
        switch (iArrow)
        {
            case 1:
                Player.localPosition += Player.forward * 3f * Time.deltaTime;
                break;

            case -1:
                Player.localPosition -= Player.forward * 3f * Time.deltaTime;
                break;

            case 2:
                Player.localPosition += -Player.right * 3f * Time.deltaTime;
                break;

            case -2:
                Player.localPosition += Player.right * 3f * Time.deltaTime;
                break;
        }
    }

    /// <summary>控制玩家旋轉</summary>
    private void f_CtrlRotation(int iArrow)
    {
        switch (iArrow)
        {
            case 2:
                Player.localEulerAngles += Vector3.up * 20f * Time.deltaTime;
                break;

            case -2:
                Player.localEulerAngles -= Vector3.up * 20f * Time.deltaTime;
                break;
        }
    }

    /// <summary>控制場上物件大小</summary>
    private void f_CtrlScale(int iArrow)
    {
        switch (iArrow)
        {
            case 1:
                GameMain.GetInstance().m_GameTable.transform.localScale += Vector3.one * Time.deltaTime;
                break;

            case -1:
                GameMain.GetInstance().m_GameTable.transform.localScale -= Vector3.one * Time.deltaTime;
                break;
        }
    }

    /// <summary>控制玩家高度</summary>
    private void f_CtrlHeight(int iArrow)
    {
        switch (iArrow)
        {
            case 1:
                Player.localPosition += Player.up * 3f * Time.deltaTime;
                break;

            case -1:
                Player.localPosition -= Player.up * 3f * Time.deltaTime;
                break;
        }
    }
    #endregion

    /// <summary>回歸原位</summary>
    private void f_ResetInitPos(int i)
    {
        if (i == 0)
        {
            Player.position = GameMain.GetInstance().m_InitPos.position;
            Player.eulerAngles = GameMain.GetInstance().m_InitPos.eulerAngles;
        }
        else if (i == 1)
        {
            GameMain.GetInstance().m_GameTable.transform.localScale = GameMain.GetInstance().m_InitPos.localScale;
        }
    }
    #endregion
}

