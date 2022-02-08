using System.Collections.Generic;
using UnityEngine;
using ccU3DEngine;
using Epibyte.ConceptVR;

public class GameMain : MonoBehaviour
{
    /// <summary>地圖物件池</summary>
    public MapPool m_MapPool = new MapPool();
    /// <summary>主攝影機</summary>
    public Camera m_MainCamera;
    /// <summary>物件主選單</summary>
    public GameObject m_MainMenu = null;
    /// <summary>素材元件選單</summary>
    public Pagination m_Pagination;
    /// <summary>編輯管理器</summary>
    public EditManager m_EditManager = new EditManager();

    /// <summary>選擇物材質</summary>
    public Material _SelectMaterial;
    /// <summary>主父物件</summary>
    public GameObject m_GameTable;

    private static GameMain _Instance = null;
    public static GameMain GetInstance()
    {
        return _Instance;
    }


    void Awake()
    {
        _Instance = this;
        m_GameTable.SetActive(true);
        ccUIManage.GetInstance().f_SendMsg("UI_GameMain", BaseUIMessageDef.UI_OPEN, null, true);

        ccTimeEvent.GetInstance().f_RegEvent(0.3f, true, null, f_UpdateMenuPos);
    }

    /// <summary>更新菜單位置</summary>
    private void f_UpdateMenuPos(object e)
    {
        if(m_MainMenu != null)
        m_MainMenu.transform.position = m_MainCamera.transform.position + new Vector3(-0.4f, 0.05f, 0);
    }

    #region 地圖功能
    public void f_LoadMap(string strFileName)
    {
        m_MapPool.f_LoadMap(strFileName);
    }

    public void f_SaveMap(string strFileName)
    {
        m_MapPool.f_SaveMap(strFileName);
    }

    public void f_ResetMap()
    {
        int iCount = m_MapPool.f_Count();
        for(int i = 0; i < iCount; i++)
        {
            m_MapPool.f_DeleteObj(m_MapPool.f_GetAll()[0].iId);
        }
    }

    public void f_ExitMap()
    {
        glo_Main.GetInstance().f_Destroy();
    }

    public EditObjControll f_AddObj(CharacterDT tCharacterDT)
    {
        return m_MapPool.f_AddObj(tCharacterDT);
    }

    public void f_DelObj(long iId)
    {
        m_MapPool.f_DeleteObj(iId);
    }

    public Transform f_GetObjParent()
    {
        return m_GameTable.transform;
    }
    #endregion

    #region 編輯功能

    /// <summary>
    /// 點選編輯按鈕
    /// </summary>
    /// <param name="EditTpye">按鈕類型</param>
    public void f_SetEditBtn(string EditTpye)
    {
        m_EditManager.f_SetEditBtn(EditTpye);
        EditDisplay.GetInstance().f_SetPanel(true);
    }

    /// <summary>點選開始編輯按鈕</summary>
    public void f_Edit()
    {
        m_EditManager.f_Edit();
    }

    /// <summary>點選離開編輯按鈕</summary>
    public void f_ExitEdit()
    {
        m_EditManager.f_EditExit();
        EditDisplay.GetInstance().f_SetPanel(false);
    }

    /// <summary>
    /// 點選按鈕
    /// </summary>
    /// <param name="button">按鈕</param>
    public void f_LeaveEdit(TabButton button)
    {
        m_EditManager.f_LeaveEdit(button);
    }

    /// <summary>
    /// 設定軸心模式
    /// </summary>
    /// <param name="strAxis">軸心模式(填軸心名稱)</param>
    public void f_SetEditAxis(string strAxis)
    {
        m_EditManager.f_SetEditAxis(strAxis);
    }

    /// <summary>
    /// 設定座標模式
    /// </summary>
    /// <param name="strPoint">座標模式(填座標名稱)</param>
    public void f_SetEditPoint(string strPoint)
    {
        m_EditManager.f_SetEditPoint(strPoint);
    }

    /// <summary>設定當前點選的編輯按鈕</summary>
    public void f_SetEditBtn(TabButton EditBtn = null)
    {
        m_EditManager.f_SetEditBtn(EditBtn);
    }

    /// <summary>設定Edit按鈕狀態</summary>
    public void f_SetEditBtnOnClick(TabButton EditBtn = null)
    {
        m_EditManager.f_SetEditBtnOnClick(EditBtn);
    }

    /// <summary>
    /// 設定按鈕UI文字
    /// </summary>
    /// <param name="textGroup">按鈕文字群</param>
    public void f_SetOnClickText(Transform textGroup)
    {
        m_EditManager.f_SetOnClickText(textGroup);
    }

    /// <summary>設定當前編輯的物件</summary>
    public void f_SetCurEditObj(EditObjControll Obj)
    {
        m_EditManager.f_SetCurEditObj(Obj);
    }

    /// <summary>取得當前編輯的物件</summary>
    public EditObjControll f_GetCurEditObj()
    {
        return m_EditManager.f_GetCurEditObj();
    }

    /// <summary>
    /// 設定當前軸心模式(確保每一頁面的模式都相同)
    /// </summary>
    /// <param name="TextGroup">按鈕文字群</param>
    public void f_SetCurPoint(Transform TextGroup)
    {
        m_EditManager.f_SetCurPoint(TextGroup);
    }

    /// <summary>
    /// 設定當前座標模式(確保每一頁面的模式都相同)
    /// </summary>
    /// <param name="TextGroup">按鈕文字群</param>
    public void f_SetCurAxis(Transform TextGroup)
    {
        m_EditManager.f_SetCurAxis(TextGroup);
    }

    /// <summary>
    /// 編輯物播放預覽動畫
    /// </summary>
    /// <param name="iAddIndex">增減動畫Index</param>
    public void f_EditObjAnimPlay(int iAddIndex)
    {
        m_EditManager.f_EditObjAnimPlay(iAddIndex);
    }
    #endregion
}
