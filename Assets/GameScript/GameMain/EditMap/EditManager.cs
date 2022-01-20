using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Epibyte.ConceptVR;

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
            case "RotationH":
                _EditEM = EM_EditState.RotationH;
                break;
            case "RotationV":
                _EditEM = EM_EditState.RotationV;
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
            glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(MessageDef.UI_MapEditState, 1);//開關提示文字
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
            glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(MessageDef.UI_MapEditState, -1);//開關提示文字
            glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(MessageDef.UI_MapEditState, -6);//開關提示文字
        }
    }

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
            glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(MessageDef.UI_EditObjAnim, null);//關閉動畫提示文字
        }

        _CurEditObjControll.f_AnimPlay(iAddIndex);
    }
}
