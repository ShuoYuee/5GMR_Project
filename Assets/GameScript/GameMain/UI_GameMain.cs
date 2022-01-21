using ccU3DEngine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Epibyte.ConceptVR;

namespace GameLogic
{
    public class UI_GameMain : ccUILogicBase
    {
        private ListPositionCtrl _listPosCtrl;
        private Pagination _Pagination;
        private Image _Anchor = null;

        private bool _bAnchor = false;
        private float _fAnchorWait = 0f;

        //PowerIndicator _PowerIndicator;
        protected override void On_Init()
        {
            MessageBox.DEBUG("启用游戏包中的UI_GameMain脚本");

            _listPosCtrl = f_GetObject("CircularList").GetComponent<ListPositionCtrl>();
            _Anchor=f_GetObject("Anchor").GetComponent<Image>();

            f_RegClickEvent(f_GetObject("BtnSetup"), OnClick_BtnSetup);
            f_RegClickEvent(f_GetObject("DelObj"), OnClick_DelObj);


            f_RegClickEvent(f_GetObject("LoadMap"), OnClick_LoadMap);
            f_RegClickEvent(f_GetObject("SaveMap"), OnClick_SaveMap);

            f_InitMessage();
        }

        private void f_InitMessage()
        {
            glo_Main.GetInstance().m_UIMessagePool.f_AddListener(MessageDef.UI_MapObjInit, f_SetMapObjData);    //設定地圖物件資料
        }

        #region 初始化
        /// <summary>初始化地圖物件資料</summary>
        private void f_InitMapObjData()
        {
            List<GameObject> oMapObj = new List<GameObject>();
            List<NBaseSCDT> tData = glo_Main.GetInstance().m_SC_Pool.m_CharacterSC.f_GetAll();
            CharacterDT aData;
            GameObject oData = null;
            for(int i = 0; i < tData.Count; i++)
            {
                aData = (CharacterDT)tData[i];

                //判別資源來源模式
                switch (aData.iDisplayResource)
                {
                    case 1:
                        oData = AssetLoader.LoadAsset(aData.szResName + ".bundle", aData.szDisplayAB) as GameObject;
                        break;
                    default:
                        oData = AssetLoader.LoadAsset(aData.szResName + ".bundle", aData.szName) as GameObject;
                        break;
                }

                if (oData == null)//清除空物件資料
                {
                    tData.RemoveAt(i);
                    i -= 1;
                    continue;
                }
                oMapObj.Add(oData);
            }
            GameMain.GetInstance().m_Pagination.items = oMapObj;//將物件清單傳送給選單腳本
        }

        /// <summary>
        /// 設定物件資料
        /// </summary>
        /// <param name="oMapObj">物件清單</param>
        public void f_SetMapObjData(object oMapObj)
        {
            List<NBaseSCDT> tData = glo_Main.GetInstance().m_SC_Pool.m_CharacterSC.f_GetAll();
            List<GameObject> tMapObj = (List<GameObject>)oMapObj;

            //將資料一一賦予給實例化的物件
            for(int i = 0; i < tMapObj.Count; i++)
            {
                f_CheckIgnore(tMapObj[i]);
                MenuObject tMenuObject = tMapObj[i].AddComponent<MenuObject>();
                tMenuObject.f_InitMenuObj(tData[i]);
            }
        }

        /// <summary>無視物件初始化</summary>
        private void f_CheckIgnore(GameObject e)
        {
            if (e.GetComponent<LoadMapBtn>() != null) { return; }
        }
        #endregion

        private void CreateTeamItem()
        {
            List<NBaseSCDT> aData = glo_Main.GetInstance().m_SC_Pool.m_CharacterSC.f_GetAll();//讀取物件圖示資料
            ListPositionCtrlTools.f_Create(_listPosCtrl, aData);//創建物件圖示
        }

        protected override void On_Open(object e)
        {
            CreateTeamItem();
            _listPosCtrl.Initialize();

            InitObjList();
            f_InitMapObjData();
        }

        void InitObjList()
        {

        }
      
        protected override void On_Close()
        {
           
        }

        protected override void On_Update()
        {
            base.On_Update();
            f_AnchorUIIng();
        }

        private float _fAnchorCurTime = 0f;
        protected override void On_UpdateGUI()
        {
            
        }
        #region 按鈕功能

        /// <summary>讀取場景資料</summary>
        private void OnClick_LoadMap(GameObject go, object obj1, object obj2)
        {
            //GameMain.GetInstance().f_LoadMap();
        }

        /// <summary>儲存場景資料</summary>
        private void OnClick_SaveMap(GameObject go, object obj1, object obj2)
        {
            //GameMain.GetInstance().f_SaveMap();
        }

        /// <summary>選擇物件</summary>
        private void OnClick_Select(GameObject go, object obj1, object obj2)
        {
            ListItem tListItem = (ListItem)_listPosCtrl.GetCenteredBox();
            CharacterDT tCharacterDT = (CharacterDT)tListItem.m_SCData;
                                    
            GameMain.GetInstance().f_AddObj(tCharacterDT);
        }

        /// <summary>刪除物件</summary>
        private void OnClick_DelObj(GameObject go, object obj1, object obj2)
        {
            EditObjControll tEditObjControll = null;
            GameMain.GetInstance().f_DelObj(tEditObjControll.f_GetId());
        }

        /// <summary>開啟設定介面</summary>       
        void OnClick_BtnSetup(GameObject go, object obj1, object obj2)
        {
            ccUIHoldPool.GetInstance().f_Hold(this);
            ccUIManage.GetInstance().f_SendMsg("UI_GameSet", BaseUIMessageDef.UI_OPEN, null, true);
        }

        /// <summary>離開遊戲</summary>
        void OnClick_BtnExit(GameObject go, object obj1, object obj2)
        {
            f_Close();
        }

        #endregion

        private void DoExit()
        {
           
        }

        protected override void On_Destory()
        {
            
        }

        #region 錨點(暫未用到)

        /// <summary>
        /// 開始錨點計時
        /// </summary>
        /// <param name="fWaitTime">等待時長</param>
        public void f_StartAnchorTime(object fWaitTime)
        {
            _fAnchorWait = (float)fWaitTime;
            _bAnchor = true;
        }

        /// <summary>結束錨點計時</summary>
        public void f_EndAnchorTime(object e = null)
        {
            _bAnchor = false;
            _Anchor.fillAmount = 0;
            _fAnchorCurTime = 0;
        }

        /// <summary>錨點計時</summary>
        private void f_AnchorUIIng()
        {
            if (_bAnchor)
            {
                _fAnchorCurTime += Time.deltaTime;
                _Anchor.fillAmount = _fAnchorCurTime / _fAnchorWait;

                if (_fAnchorCurTime >= _fAnchorWait)
                {
                    f_EndAnchorTime();
                }
            }
        }
        #endregion

        #region Debug
        /// <summary>開關提示文字</summary>
        public void f_EditState(object e)
        {
            switch ((int)e)
            {
                case 1:
                    f_GetObject("EditText").SetActive(true);
                    break;
                case -1:
                    f_GetObject("EditText").SetActive(false);
                    break;
                case 2:
                    f_GetObject("DebugText").GetComponent<Text>().text = "LoadMap Success";
                    ccTimeEvent.GetInstance().f_RegEvent(1f, false, null, f_CloseText);
                    break;
                case 3:
                    f_GetObject("DebugText").GetComponent<Text>().text = "SaveMap Success";
                    ccTimeEvent.GetInstance().f_RegEvent(1f, false, null, f_CloseText);
                    break;
                case 4:
                    f_GetObject("DebugText").GetComponent<Text>().text = "Reset Success";
                    f_AnimPlayText(null);
                    ccTimeEvent.GetInstance().f_RegEvent(1f, false, null, f_CloseText);
                    break;
                case 5:
                    f_GetObject("DebugText").GetComponent<Text>().text = "Create Success";
                    ccTimeEvent.GetInstance().f_RegEvent(1f, false, null, f_CloseText);
                    break;
                case 6:
                    f_GetObject("DebugText").GetComponent<Text>().text = "Selecting";
                    break;
                case -6:
                    f_GetObject("DebugText").GetComponent<Text>().text = "Cancel Select";
                    f_AnimPlayText(null);
                    ccTimeEvent.GetInstance().f_RegEvent(1f, false, null, f_CloseText);
                    break;
            }
        }

        private void f_CloseText(object e = null)
        {
            f_GetObject("DebugText").GetComponent<Text>().text = "";
        }

        /// <summary>開關動畫提示文字</summary>
        private void f_AnimPlayText(object e)
        {
            if (e == null)
            {
                f_GetObject("AnimText").GetComponent<Text>().text = "";
                f_GetObject("DebugText").GetComponent<Text>().text = "Selecting";
                return;
            }

            f_GetObject("AnimText").GetComponent<Text>().text = "播放動畫：" + (string)e;
            f_GetObject("DebugText").GetComponent<Text>().text = "";
        }
        #endregion
    }
}