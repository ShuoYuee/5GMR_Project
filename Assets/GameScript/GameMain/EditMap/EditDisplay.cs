using UnityEngine;

public class EditDisplay : MonoBehaviour
{
    public GameObject _Panel = null;

    private bool _UpdateValue = false;
    private Transform _Target = null;    
    private static EditDisplay _Instance = null;
    public static EditDisplay GetInstance()
    {
        return _Instance;
    }

    private void Start()
    {
        _Instance = this;
    }

    public void f_StartUpdate(Transform Target)
    {
        _Target = Target;
        _UpdateValue = true;
    }

    public void f_StopUpdate()
    {
        _Target = null;
        _UpdateValue = false;
    }

    public bool f_GetValueBool()
    {
        return _UpdateValue;
    }

    public Transform f_GetTarget()
    {
        return _Target;
    }

    public void f_SetPanel(bool bSet)
    {
        if (_Panel == null) { return; }
        _Panel.SetActive(bSet);
    }
}
