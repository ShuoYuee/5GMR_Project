using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ccU3DEngine;

/// <summary>
/// 編輯數值更新
/// </summary>
public class EditDisplayText : MonoBehaviour
{
    /// <summary>編輯模式</summary>
    public EditState _EditState = EditState.Position;
    /// <summary>編輯座標</summary>
    public EditV3 _Coordinate = EditV3.X;
    /// <summary>文本套件</summary>
    public Text _DisplayText = null;

    /// <summary>文字輸入框</summary>
    private InputField _InputField;
    /// <summary>當前編輯三維</summary>
    private Vector3 _DisplayV3 = Vector3.zero;
    /// <summary>當前顯示數值</summary>
    private float _DisplayValue = 0;
    /// <summary>是否正在輸入文字</summary>
    private bool _bInputing = false;

    public enum EditState
    {
        Position,
        Rotation,
        Scale,
    }

    public enum EditV3
    {
        X,
        Y,
        Z,
    }

    private void Start()
    {
        //設定文字輸入框事件
        _InputField = GetComponent<InputField>();
        if (_InputField == null)
        {
            _InputField = GetComponentInChildren<InputField>();
        }
        _InputField.onValueChanged.AddListener(f_SetTarget);
        _InputField.onEndEdit.AddListener(f_InputEnd);
    }

    private void FixedUpdate()
    {
        if (!EditDisplay.GetInstance().f_GetValueBool() || _bInputing)
        {
            return;
        }

        f_InputValue();
        f_UpdateText();
    }

    /// <summary>更新文本</summary>
    private void f_UpdateText()
    {
        Transform Target = EditDisplay.GetInstance().f_GetTarget();
        //確認編輯模式
        switch (_EditState)
        {
            case EditState.Position:
                _DisplayV3 = Target.localPosition;
                break;

            case EditState.Rotation:
                _DisplayV3 = Target.localEulerAngles;
                break;

            case EditState.Scale:
                _DisplayV3 = Target.localScale;
                break;
        }

        //確認座標
        switch (_Coordinate)
        {
            case EditV3.X:
                _DisplayValue = _DisplayV3.x;
                break;

            case EditV3.Y:
                _DisplayValue = _DisplayV3.y;
                break;

            case EditV3.Z:
                _DisplayValue = _DisplayV3.z;
                break;
        }

        _InputField.text = _DisplayValue + "";
    }

    /// <summary>開始輸入新文本</summary>
    private void f_InputValue()
    {
        if (EventSystem.current.currentSelectedGameObject != _InputField) { return; }
        _bInputing = true;
    }

    /// <summary>結束輸入新文本</summary>
    private void f_InputEnd(string strValue)
    {
        _bInputing = false;
    }

    /// <summary>依文本設定目標物</summary>
    private void f_SetTarget(string strTarget)
    {
        float fValue = ccMath.atof(_InputField.text);
        Transform Target = EditDisplay.GetInstance().f_GetTarget();
        switch (_EditState)
        {
            case EditState.Position:
                Target.localPosition = f_SetCoordinate(Target.localPosition, fValue);
                break;

            case EditState.Rotation:
                Target.localEulerAngles = f_SetCoordinate(Target.localEulerAngles, fValue);
                break;

            case EditState.Scale:
                Target.localScale = f_SetCoordinate(Target.localScale, fValue);
                break;
        }
    }

    /// <summary>
    /// 依三維設定座標
    /// </summary>
    /// <param name="vTarget">目標物三維</param>
    /// <param name="fValue">顯示數值</param>
    /// <returns></returns>
    private Vector3 f_SetCoordinate(Vector3 vTarget, float fValue)
    {
        Vector3 vValue = Vector3.zero;
        switch (_Coordinate)
        {
            case EditV3.X:
                vValue = new Vector3(fValue, vTarget.y, vTarget.z);
                break;

            case EditV3.Y:
                vValue = new Vector3(vTarget.x, fValue, vTarget.z);
                break;

            case EditV3.Z:
                vValue = new Vector3(vTarget.x, vTarget.y, fValue);
                break;
        }

        return vValue;
    }
}
