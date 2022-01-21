using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Epibyte.ConceptVR;
using ccU3DEngine;

public class EditManager
{
    /// <summary>是否處於編輯模式</summary>
    public bool _bEdit = false;
    /// <summary>是否已選取編輯物件</summary>
    public bool _bSelectEdit = false;

    /// <summary>編輯類型</summary>
    public EM_EditState _EditEM = EM_EditState.None;
    public EM_EditAxis _EditAxitEM = EM_EditAxis.AxisX;
    public EM_EditPoint _EditPointEM = EM_EditPoint.WorldPoint;

    /// <summary>當前編輯的物件</summary>
    private EditObjControll _CurEditObjControll = null;
    /// <summary>當前點選的編輯按鈕</summary>
    private TabButton _CurEditBtn = null;

    #region 點擊按鈕
    /// <summary>
    /// 點選編輯按鈕
    /// </summary>
    /// <param name="EditTpye">按鈕類型</param>
    public void f_SetEditBtn(string EditTpye)
    {
        switch (EditTpye)
        {
            case "Position":
                _EditEM = EM_EditState.Position;
                break;
            case "Rotation":
                _EditEM = EM_EditState.Rotation;
                break;

            case "Scale":
                _EditEM = EM_EditState.Scale;
                break;
            case "Edit":
                _EditEM = EM_EditState.None;
                f_Edit();
                break;
        }
    }

    /// <summary>點選編輯按鈕</summary>
    public void f_Edit()
    {
        _bEdit = !_bEdit;

        if (_bEdit)
        {
            f_SetEditBtn();
        }
        else
        {
            f_SetEditBtn();
            if (_CurEditObjControll != null)
            {
                _CurEditObjControll.f_SetEditState(false);
            }

            _bSelectEdit = false;
            _CurEditObjControll = null;
        }
    }

    public void f_LeaveEdit(TabButton button)
    {
        button.OnClicked();
    }

    private bool bWait = false;
    public void f_SetEditAxis(string strAxis)
    {
        if (bWait) { return; }
        int iAxis = ccMath.atoi(strAxis);
        if ((int)_EditAxitEM + 1 > iAxis)
        {
            _EditAxitEM = (EM_EditAxis)1;
        }
        else
        {
            _EditAxitEM = (EM_EditAxis)(int)_EditAxitEM + 1;
        }
    }

    public void f_SetEditPoint(string strPoint)
    {
        if (bWait) { return; }
        int iPoint = ccMath.atoi(strPoint);
        if ((int)_EditPointEM + 1 > iPoint)
        {
            _EditPointEM = (EM_EditPoint)1;
        }
        else
        {
            _EditPointEM = (EM_EditPoint)(int)_EditPointEM + 1;
        }  
    }

    private void f_BtnWait(object e)
    {
        bWait = false;
    }
    #endregion

    #region 按鈕反饋
    /// <summary>設定當前點選的編輯按鈕</summary>
    public void f_SetEditBtn(TabButton EditBtn = null)
    {
        if (_CurEditBtn != null && _CurEditBtn != EditBtn)
        {
            _CurEditBtn.DeactivateAllEffects();
            _CurEditBtn.isClicked = false;
        }
        _CurEditBtn = EditBtn;

        if (EditBtn == null) { return; }
        EditBtn.ActivateAllEffects();
        _CurEditBtn.isClicked = true;
    }

    /// <summary>設定Edit按鈕狀態</summary>
    public void f_SetEditBtnOnClick(TabButton EditBtn = null)
    {
        if (_bEdit)
        {
            EditBtn.isClicked = true;
            EditBtn.ActivateAllEffects();
        }
        else
        {
            EditBtn.isClicked = false;
            EditBtn.DeactivateAllEffects();
        }
    }

    public void f_SetOnClickText(Transform textGroup)
    {
        if (bWait) { return; }
        bWait = true;
        ccTimeEvent.GetInstance().f_RegEvent(0.8f, false, null, f_BtnWait);
        for (int i = 0; i < textGroup.childCount; i++)
        {
            if (textGroup.GetChild(i).gameObject.activeSelf)
            {
                textGroup.GetChild(i).gameObject.SetActive(false);
                if (i + 1 >= textGroup.childCount)
                {
                    textGroup.GetChild(0).gameObject.SetActive(true);
                    return;
                }
                else
                {
                    textGroup.GetChild(i + 1).gameObject.SetActive(true);
                    return;
                }
            }
        }
    }

    public void f_SetCurPoint(Transform TextGroup)
    {
        for (int i = 0; i < TextGroup.childCount; i++)
        {
            if (TextGroup.GetChild(i).gameObject.activeSelf)
            {
                GameMain.GetInstance().m_EditManager._EditPointEM = (EM_EditPoint)i + 1;
                return;
            }
        }
    }

    public void f_SetCurAxis(Transform TextGroup)
    {
        for (int i = 0; i < TextGroup.childCount; i++)
        {
            if (TextGroup.GetChild(i).gameObject.activeSelf)
            {
                GameMain.GetInstance().m_EditManager._EditAxitEM = (EM_EditAxis)i + 1;
                return;
            }
        }
    }
    #endregion

    /// <summary>設定當前編輯的物件</summary>
    public void f_SetCurEditObj(EditObjControll Obj)
    {
        _CurEditObjControll = Obj;
    }

    /// <summary>取得當前編輯的物件</summary>
    public EditObjControll f_GetCurEditObj()
    {
        return _CurEditObjControll;
    }

    /// <summary>
    /// 編輯物播放預覽動畫
    /// </summary>
    /// <param name="iAddIndex">增減動畫Index</param>
    public void f_EditObjAnimPlay(int iAddIndex)
    {
        if (!_bEdit || _CurEditObjControll == null) { return; }

        if (iAddIndex == 0)//停止播放預覽動畫
        {
            _CurEditObjControll.f_AnimStop();
        }

        _CurEditObjControll.f_AnimPlay(iAddIndex);
    }
}
