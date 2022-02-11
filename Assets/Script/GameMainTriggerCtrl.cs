using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Epibyte.ConceptVR;

public class GameMainTriggerCtrl : MonoBehaviour
{
    public Transform _Focus = null;

    //public float _fLookTimeLimt = 2f;
    private EditObjControll _EditObjControll = null;
    private Interactable _Interactable = null;
    private InputField _InputField = null;
    private Button _Button = null;

    private GameObject oCurObj = null;
    private EM_TriggerObj _ObjEm = EM_TriggerObj.None;
    //private float _fLookTime = 0f;
    private bool _bLookTime = false;

    public enum EM_TriggerObj
    {
        None = 0,
        EditObj = 1,    //編輯物
        Button = 2,     //按鈕
        InpuUI = 3,     //輸入文字UI
        ButtonUI = 4,   //按鈕UI
    }

    private void Start()
    {
        //暫預定四個輸入事件
        GameInputCtrl.OnClickCtrlEvent += f_OnClick;
        GameInputCtrl.OnClickBtnOne += f_EditCtrl;
        GameInputCtrl.OnClickBtnTwo += f_EditCtrl;
        GameInputCtrl.OnClickBtnThree += f_EditCtrl;
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

            if (hit.collider.gameObject != oCurObj && oCurObj != null)
            {
                if (_Interactable != null)
                {
                    _Interactable.OnLeave();
                    _Interactable = null;
                }
            }

            //將要偵測的元件一一填入
            if (GameMain.GetInstance().m_EditManager._bEdit && hit.collider.GetComponent<EditObjControll>() != null)//編輯物件
            {
                _ObjEm = EM_TriggerObj.EditObj;
                oCurObj = hit.collider.gameObject;
            }

            else if (!_bLookTime && hit.collider.GetComponent<Interactable>() != null)//按鈕物件
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

        }
        else
        {
            f_SetFocus(hit.point);
        }
        #region
        /*else
        {
            _ObjEm = EM_TriggerObj.None;
            oCurObj = null;

            if (_Interactable != null)
            {
                _Interactable.OnLeave();
                _Interactable = null;
            }

            _fLookTime = 0;
            _bLookTime = false;
            return;
        }

        if (_bLookTime)
        {
                _fLookTime += Time.deltaTime;
            if (_fLookTime >= _fLookTimeLimt)
            {
                _fLookTime = 0;
                _bLookTime = false;

                switch (_ObjEm)
                {
                    case EM_TriggerObj.EditObj:

                        break;
                    case EM_TriggerObj.Button:
                        _Interactable.OnClicked();
                        break;
                }
            }
        }*/
        #endregion
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
                if (!GameMain.GetInstance().m_EditManager._bEdit) { return; }
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
}
