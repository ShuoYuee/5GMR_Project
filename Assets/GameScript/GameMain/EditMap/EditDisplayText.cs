using UnityEngine;
using UnityEngine.UI;
using ccU3DEngine;

public class EditDisplayText : MonoBehaviour
{
    public EditState _EditState = EditState.Position;
    public EditV3 _Coordinate = EditV3.X;
    public Text _DisplayText = null;

    private InputField _InputField;
    private Vector3 _DisplayV3 = Vector3.zero;
    private float _DisplayValue = 0;
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
        _InputField = GetComponent<InputField>();
        if (_InputField == null)
        {
            _InputField = GetComponentInChildren<InputField>();
        }
        _InputField.onValueChanged.AddListener(f_InputValue);
        _InputField.onEndEdit.AddListener(f_InputEnd);
    }

    private void FixedUpdate()
    {
        if (!EditDisplay.GetInstance().f_GetValueBool() || _bInputing)
        {
            return;
        }

        f_UpdateText();
    }

    private void f_UpdateText()
    {
        Transform Target = EditDisplay.GetInstance().f_GetTarget();
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

    private void f_InputValue(string strValue)
    {
        _bInputing = true;
        if (_DisplayValue == ccMath.atof(_InputField.textComponent.text)) { return; }
        f_SetTarget(ccMath.atof(_InputField.textComponent.text));
    }

    private void f_InputEnd(string strValue)
    {
        _bInputing = false;
    }

    private void f_SetTarget(float fValue)
    {
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
