using UnityEngine;

/// <summary>
/// 編輯數值顯示
/// </summary>
public class EditDisplay : MonoBehaviour
{
    /// <summary>UI群組</summary>
    public GameObject _Panel = null;

    /// <summary>是否允許更新文字</summary>
    private bool _UpdateValue = false;
    /// <summary>目標編輯物</summary>
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

    /// <summary>
    /// 開始更新文字
    /// </summary>
    /// <param name="Target">目標編輯物</param>
    public void f_StartUpdate(Transform Target)
    {
        _Target = Target;
        _UpdateValue = true;
    }

    /// <summary>結束更新文字</summary>
    public void f_StopUpdate()
    {
        _Target = null;
        _UpdateValue = false;
    }

    /// <summary>是否允許更新</summary>
    public bool f_GetValueBool()
    {
        return _UpdateValue;
    }

    /// <summary>獲取目標編輯物</summary>
    public Transform f_GetTarget()
    {
        return _Target;
    }

    public void f_ClearTarget()
    {
        _Target = null;
    }

    /// <summary>
    /// 設定UI顯示
    /// </summary>
    /// <param name="bSet">是否顯示</param>
    public void f_SetPanel(bool bSet)
    {
        GameTools.f_SetGameObject(_Panel, bSet);
    }
}
