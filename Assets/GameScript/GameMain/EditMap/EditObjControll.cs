using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物件編輯控制器
/// </summary>
public class EditObjControll : MonoBehaviour
{
    private MapPoolDT _MapPoolDT;//場景資料(儲存時所用的)

    /// <summary>編輯模式</summary>
    EM_EidtState _EditEM = EM_EidtState.None;
    /// <summary>是否被選中編輯</summary>
    bool _bEdit = false;
    /// <summary>判別值</summary>
    int _iEditValue = 0;

    #region Public Variables
    [Header("Position")]
    public bool isGrabbable = true;
    [Header("Rotation")]
    public bool isRotatable = true;
    public float rotationSpeed = 100f;
    [Header("Scale")]
    public bool isScalable = true;
    public float scaleStep = 0.3f;
    public float minScaleFactor = 0.5f;
    public float maxScaleFactor = 2f;
    #endregion

    #region Private Variables
    private Vector3 _vTargetPos;
    private float _fPosDir = 0;
    Vector3 originSize, minSize, maxSize;

    bool isGrabbing = false;
    bool isGrabbObj = false;
    bool isRotating = false;
    bool isScaling = false;
    #endregion

    #region IInteractable Implementation
    public void OnClicked()
    {
        f_SetGrabState();
    }

    public void OnReleased()
    {
        
    }

    public void OnHovered() { }
    public void OnLeave() { }
    #endregion

    /// <summary>
    /// 儲存地圖資料
    /// </summary>
    /// <param name="tMapPoolDT">地圖資料</param>
    public void f_Save(MapPoolDT tMapPoolDT)
    {
        _MapPoolDT = tMapPoolDT;
        name = _MapPoolDT.iId + "_" + _MapPoolDT.m_CharacterDT.szResName;
        transform.parent = GameMain.GetInstance().f_GetObjParent();
    }
        
    public long f_GetId()
    {
        return _MapPoolDT.iId;
    }

    #region 編輯模式觸發
    /// <summary>開始編輯</summary>
    public void f_StartEdit()
    {
        StopAll();
        _EditEM = GameMain.GetInstance()._EditEM;
        _fPosDir = Vector3.Distance(transform.position, GameMain.GetInstance().m_MainCamera.transform.position);
    }

    /// <summary>進行編輯</summary>
    public void f_EditIng()
    {
        _EditEM = GameMain.GetInstance()._EditEM;
        switch (_EditEM)
        {
            case EM_EidtState.Position:
                f_EditPosition();
                break;
            case EM_EidtState.RotationH:
                f_EditRotation(_iEditValue);
                break;
            case EM_EidtState.RotationV:
                f_EditRotation(_iEditValue);
                break;
            case EM_EidtState.Scale:
                f_EditScale(_iEditValue);
                break;
        }
    }

    /// <summary>結束編輯</summary>
    public void f_EndEdit()
    {
        StopAll();
        _EditEM = EM_EidtState.None;
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (!_bEdit) { return; }
        f_EditIng();
    }

    #region 物件編輯
    /// <summary>
    /// 編輯座標
    /// </summary>
    public void f_EditPosition()
    {
        if (!isGrabbable)
        {
            return;
        }

        if (!isGrabbObj) { return; }

        if (isGrabbing)
        {
            return;
        }

        StartCoroutine(PositionTo(0.2f));
    }

    /// <summary>停止編輯座標</summary>
    public void StopPosition()
    {
        isGrabbing = false;
        StopCoroutine(PositionTo(0));
    }

    /// <summary>
    /// 編輯旋轉值
    /// </summary>
    /// <param name="direction"></param>
    public void f_EditRotation(int direction)
    {
        if (!isRotatable)
        {
            return;
        }

        if (isRotating)
        {
            return;
        }

        StartCoroutine(RotateTo(direction * rotationSpeed, 0.2f));
    }

    /// <summary>停止編輯旋轉值</summary>
    public void StopRotating()
    {
        isRotating = false;
        StopCoroutine(RotateTo(0, 0));
    }

    /// <summary>
    /// 編輯縮放值
    /// </summary>
    /// <param name="direction">1為放大，-1為縮小</param>
    public void f_EditScale(int direction)
    {
        if (!isScalable)
        {
            return;
        }

        if (isScaling)
        {
            return;
        }

        Vector3 toValue;
        if (1 == direction)
        {
            //toValue = transform.localScale + (transform.localScale * scaleStep);
            toValue = transform.localScale + Vector3.one * scaleStep;
        }
        else if (-1 == direction)
        {
            //toValue = transform.localScale - (transform.localScale * scaleStep);
            toValue = transform.localScale - Vector3.one * scaleStep;
        }
        else
        {
            return;
        }

        /*toValue = new Vector3(
            Mathf.Clamp(toValue.x, minSize.x, maxSize.x),
            Mathf.Clamp(toValue.y, minSize.y, maxSize.y),
            Mathf.Clamp(toValue.z, minSize.z, maxSize.z)
        );*/

        StartCoroutine(ScaleTo(toValue, 0.5f));
    }

    /// <summary>停止編輯縮放值</summary>
    public void StopScaling()
    {
        isScaling = false;
        StopCoroutine(ScaleTo(Vector3.zero, 0));
    }

    /// <summary>停止全部編輯</summary>
    public void StopAll()
    {
        StopPosition();
        StopRotating();
        StopScaling();
        //StopAllCoroutines();
    }

    #region 異步變動
    /// <summary>座標異步變動</summary>
    IEnumerator PositionTo(float duration)
    {
        isGrabbing = true;
        float elapsedTime = 0f;

        while (isGrabbing && elapsedTime < duration)
        {
            Transform _CameraTrans = GameMain.GetInstance().m_MainCamera.transform;
            Vector3 vNewPos = _CameraTrans.forward + new Vector3(0, 0, _fPosDir);
            vNewPos = _CameraTrans.TransformPoint(vNewPos);
            transform.position = vNewPos;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isGrabbing = false;
    }

    /// <summary>旋轉值異步變動</summary>
    IEnumerator RotateTo(float toValue, float duration)
    {
        // Coroutine starts running.
        isRotating = true;
        float elapsedTime = 0f;

        while (isRotating && elapsedTime < duration)
        {
            switch (_EditEM)
            {
                case EM_EidtState.RotationH:
                    //transform.Rotate(Vector3.up * toValue * Time.deltaTime);
                    transform.RotateAround(transform.position, Vector3.up, toValue * Time.deltaTime);
                    break;
                case EM_EidtState.RotationV:
                    //transform.Rotate(Vector3.right * toValue * Time.deltaTime);
                    transform.RotateAround(transform.position, Vector3.right, toValue * Time.deltaTime);
                    break;
            }
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Coroutine finishes running.
        isRotating = false;
    }

    /// <summary>縮放值異步變動</summary>
    IEnumerator ScaleTo(Vector3 toValue, float duration)
    {
        // Coroutine starts running.
        isScaling = true;
        Vector3 from = transform.localScale;

        float elapsedTime = 0f;

        while (isScaling && elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(from, toValue, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Coroutine finishes running.
        isScaling = false;
    }
    #endregion
    #endregion

    #region 屬性設定
    /// <summary>
    /// 設定判別值
    /// </summary>
    /// <param name="iSet">判別值</param>
    public void f_SetInput(int iSet)
    {
        _iEditValue = iSet;
    }

    /// <summary>
    /// 設定座標移動狀態
    /// </summary>
    /// <param name="bGrab">是否進行移動</param>
    public void f_SetGrabState(object bGrab = null)
    {
        if(_EditEM != EM_EidtState.Position) { return; }
        if (bGrab == null)
        {
            isGrabbObj = !isGrabbObj;
            return;
        }
        isGrabbObj = (bool)bGrab;
    }

    /// <summary>
    /// 設定該物件編輯狀態
    /// </summary>
    /// <param name="bEdit">是否進入編輯狀態</param>
    public void f_SetEditState(bool bEdit)
    {
        _bEdit = bEdit;
        if (_bEdit)
        {
            GameMain.GetInstance().f_SetCurEditObj(this);
            f_StartEdit();
        }
        else
        {
            _iEditValue = 0;
            f_EndEdit();
        }
    }
    #endregion
}
