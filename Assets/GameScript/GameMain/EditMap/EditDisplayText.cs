using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ccU3DEngine;

/// <summary>
/// 編輯數值更新
/// </summary>
public class EditDisplayText : MonoBehaviour
{
    #region Public
    /// <summary>編輯模式</summary>
    public EditState _EditState = EditState.Position;
    /// <summary>編輯座標</summary>
    public EditV3 _Coordinate = EditV3.X;
    /// <summary>文本套件</summary>
    public Text _DisplayText = null;
    #endregion

    #region Private
    /// <summary>文字輸入框</summary>
    private InputField _InputField;
    /// <summary>當前編輯三維</summary>
    private Vector3 _DisplayV3 = Vector3.zero;
    /// <summary>當前顯示數值</summary>
    private float _DisplayValue = 0;
    /// <summary>是否正在輸入文字</summary>
    private bool _bInputing = false;
    #endregion

    #region Enum
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
    #endregion

    private void Start()
    {
        //設定文字輸入框事件
        _InputField = GetComponent<InputField>();
        if (_InputField == null)
        {
            _InputField = GetComponentInChildren<InputField>();
        }
        _InputField.onValueChanged.AddListener(f_SetTarget);//當文字內容改變時
        _InputField.onEndEdit.AddListener(f_InputEnd);//當編輯結束時
    }

    private void FixedUpdate()
    {
        if (!EditDisplay.GetInstance().f_GetValueBool() || _bInputing)
        {
            return;
        }

        f_UpdateText();
    }

    /// <summary>更新文本</summary>
    private void f_UpdateText()
    {
        Transform Target = EditDisplay.GetInstance().f_GetTarget();
        if (Target == null)
        {
            _InputField.text = "";
        }
        else
        {
            //確認編輯模式
            if (_EditState == EditState.Position)
            {
                _DisplayV3 = Target.localPosition;
            }
            else if (_EditState == EditState.Rotation)
            {
                _DisplayV3 = Target.rotation.eulerAngles;
            }
            else if (_EditState == EditState.Scale)
            {
                _DisplayV3 = Target.localScale;
            }

            //確認座標
            if (_Coordinate == EditV3.X)
            {
                _DisplayValue = _DisplayV3.x;
            }
            else if (_Coordinate == EditV3.Y)
            {
                _DisplayValue = _DisplayV3.y;
            }
            else if (_Coordinate == EditV3.Z)
            {
                _DisplayValue = _DisplayV3.z;
            }
        }
        _InputField.text = _DisplayValue + "";
    }

    #region 輸入行為
    /// <summary>開始輸入新文本</summary>
    public void f_InputValue()
    {
        //if (EventSystem.current.currentSelectedGameObject != _InputField) { return; }
        //在輸入操作時調用
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
        float fValue = 0;
        try
        {
            fValue = ccMath.atof(_InputField.text);
        }
        catch
        {
            MessageBox.DEBUG("編輯介面輸入項有問題");
            return;
        }

        Transform Target = EditDisplay.GetInstance().f_GetTarget();
        if (Target == null) { return; }
        if (_EditState == EditState.Position)
        {
            Target.localPosition = f_SetCoordinate(Target.localPosition, fValue);
        }
        else if (_EditState == EditState.Rotation)
        {
            Target.eulerAngles = f_SetCoordinate(Target.eulerAngles, fValue);
        }
        else if (_EditState == EditState.Scale)
        {
            Target.localScale = f_SetCoordinate(Target.localScale, fValue);
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
        if (_Coordinate == EditV3.X)
        {
            vValue = new Vector3(fValue, vTarget.y, vTarget.z);
        }
        else if (_Coordinate == EditV3.Y)
        {
            vValue = new Vector3(vTarget.x, fValue, vTarget.z);
        }
        else if (_Coordinate == EditV3.Z)
        {
            vValue = new Vector3(vTarget.x, vTarget.y, fValue);
        }

        return vValue;
    }
    #endregion
}
