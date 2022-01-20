﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ccU3DEngine;

/// <summary>
/// 物件編輯控制器
/// </summary>
public class EditObjControll : MonoBehaviour
{
    private Animator _Animator;

    /// <summary>場景資料(儲存時所用的)</summary>
    private MapPoolDT _MapPoolDT;
    /// <summary>預覽動畫組</summary>
    private string[] _strAnimGroup;

    /// <summary>編輯模式</summary>
    EM_EditState _EditEM = EM_EditState.None;
    /// <summary>是否被選中編輯</summary>
    bool _bEdit = false;
    /// <summary>判別值(1為右或前，-1為左或後)</summary>
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
        _EditEM = GameMain.GetInstance().m_EditManager._EditEM;
        _fPosDir = Vector3.Distance(transform.position, GameMain.GetInstance().m_MainCamera.transform.position);
    }

    /// <summary>進行編輯</summary>
    public void f_EditIng()
    {
        _EditEM = GameMain.GetInstance().m_EditManager._EditEM;
        switch (_EditEM)
        {
            case EM_EditState.Position:
                f_EditPosition();
                break;
            case EM_EditState.RotationH:
                f_EditRotation(_iEditValue);
                break;
            case EM_EditState.RotationV:
                f_EditRotation(_iEditValue);
                break;
            case EM_EditState.Scale:
                f_EditScale(_iEditValue);
                break;
        }
    }

    /// <summary>結束編輯</summary>
    public void f_EndEdit()
    {
        StopAll();
        _EditEM = EM_EditState.None;
    }
    #endregion

    private void Start()
    {
        _Animator = GetComponent<Animator>();
        _strAnimGroup = ccMath.f_String2ArrayString(_MapPoolDT.m_CharacterDT.szAnimGroup, ";");
    }

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

    #region 協程變動
    /// <summary>座標協程變動</summary>
    IEnumerator PositionTo(float duration)
    {
        isGrabbing = true;
        float elapsedTime = 0f;

        while (isGrabbing && elapsedTime < duration)
        {
            //物件依照攝影機的方位做移動
            _fPosDir += _iEditValue;
            //Transform _CameraTrans = GameMain.GetInstance().m_MainCamera.transform;
            //Vector3 vNewPos = _CameraTrans.forward + new Vector3(0, 0, _fPosDir);
            //vNewPos = _CameraTrans.TransformPoint(vNewPos);
            transform.position = f_PositionPoint();

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isGrabbing = false;
    }

    private Vector3 f_PositionActive(float fdir)
    {
        switch (GameMain.GetInstance().m_EditManager._EditAxitEM)
        {
            case EM_EditAxis.AxisX:
                return new Vector3(fdir, 0, 0);

            case EM_EditAxis.AxisY:
                return new Vector3(0, fdir, 0);

            case EM_EditAxis.AxisZ:
                return new Vector3(0, 0, fdir);
        }
        return Vector3.zero;
    }

    private Vector3 f_PositionPoint()
    {
        Vector3 vPos = Vector3.zero;
        switch (GameMain.GetInstance().m_EditManager._EditPointEM)
        {
            case EM_EditPoint.WorldPoint:
                vPos = transform.position + f_PositionActive(_fPosDir);
                break;

            case EM_EditPoint.LocalPoint:
                vPos = transform.localPosition + f_PositionActive(_fPosDir);
                break;

            case EM_EditPoint.UserPoint:
                Transform _CameraTrans = GameMain.GetInstance().m_MainCamera.transform;
                vPos = _CameraTrans.forward + f_PositionActive(_fPosDir);
                vPos = _CameraTrans.TransformPoint(vPos);
                break;
        }
        return vPos;
    }

    /// <summary>旋轉值協程變動</summary>
    IEnumerator RotateTo(float toValue, float duration)
    {
        // Coroutine starts running.
        isRotating = true;
        float elapsedTime = 0f;

        while (isRotating && elapsedTime < duration)
        {
            f_RotationPoint(toValue);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Coroutine finishes running.
        isRotating = false;
    }

    private Vector3 f_RotationActive()
    {
        switch (GameMain.GetInstance().m_EditManager._EditAxitEM)
        {
            case EM_EditAxis.AxisX:
                return Vector3.right;

            case EM_EditAxis.AxisY:
                return Vector3.up;

            case EM_EditAxis.AxisZ:
                return Vector3.forward;
        }
        return Vector3.zero;
    }

    private Vector3 f_RotationPoint(float toValue)
    {
        Vector3 vPos = Vector3.zero;
        switch (GameMain.GetInstance().m_EditManager._EditPointEM)
        {
            case EM_EditPoint.WorldPoint:
                transform.Rotate(f_PositionPoint(), Space.World);
                break;

            case EM_EditPoint.LocalPoint:
                transform.Rotate(f_PositionPoint(), Space.Self);
                break;

            case EM_EditPoint.UserPoint:
                transform.RotateAround(transform.position, f_RotationActive(), toValue * Time.deltaTime);
                break;
        }
        return vPos;
    }

    /// <summary>縮放值協程變動</summary>
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

    private Vector3 f_ScaleActive()
    {
        switch (GameMain.GetInstance().m_EditManager._EditAxitEM)
        {
            case EM_EditAxis.AxisX:
                return new Vector3(0, 0, 0);

            case EM_EditAxis.AxisY:
                return new Vector3(0, 0, 0);

            case EM_EditAxis.AxisZ:
                return new Vector3(0, 0, 0);
        }
        return Vector3.zero;
    }

    private Vector3 f_ScalePoint(float toValue)
    {
        Vector3 vPos = Vector3.zero;
        switch (GameMain.GetInstance().m_EditManager._EditPointEM)
        {
            case EM_EditPoint.WorldPoint:
                vPos = transform.position + f_PositionActive(_fPosDir);
                break;

            case EM_EditPoint.LocalPoint:
                //transform.localScale = Vector3.Lerp(from, toValue, (elapsedTime / duration));
                break;

            case EM_EditPoint.UserPoint:
                Transform _CameraTrans = GameMain.GetInstance().m_MainCamera.transform;
                vPos = _CameraTrans.forward + f_PositionActive(_fPosDir);
                vPos = _CameraTrans.TransformPoint(vPos);
                break;
        }
        return vPos;
    }
    #endregion
    #endregion

    #region 預覽動畫
    private int _iAnimIndex = 0;
    /// <summary>
    /// 播放預覽動畫
    /// </summary>
    /// <param name="iAddIndex">增減動畫Index</param>
    public void f_AnimPlay(int iAddIndex)
    {
        if (_Animator == null)
        {
            MessageBox.DEBUG("此物件未設有動畫機");
            return;
        }

        _iAnimIndex += iAddIndex;
        if (_iAnimIndex < 0) { _iAnimIndex = _strAnimGroup.Length; }
        if (_iAnimIndex >= _strAnimGroup.Length) { _iAnimIndex = 0; }

        int iStateId = Animator.StringToHash(_strAnimGroup[_iAnimIndex]);
        bool bHasAction = _Animator.HasState(0, iStateId);

        if (bHasAction)//確認是否擁有該動畫
        {
            _Animator.Play(_strAnimGroup[_iAnimIndex]);
            glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(MessageDef.UI_EditObjAnim, _strAnimGroup[_iAnimIndex]);//開啟動畫提示文字
        }
    }

    /// <summary>停止播放預覽動畫</summary>
    public void f_AnimStop()
    {
        if (_Animator == null)
        {
            MessageBox.DEBUG("此物件未設有動畫機");
            return;
        }

        _Animator.StopRecording();
    }
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
        if(_EditEM != EM_EditState.Position) { return; }
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
            glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(MessageDef.UI_MapEditState, 6);//開關提示文字
        }
        else
        {
            _iEditValue = 0;
            f_EndEdit();
            glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(MessageDef.UI_MapEditState, -6);//開關提示文字
        }
    }
    #endregion
}
