using ccU3DEngine;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public class UI_GameMain : ccUILogicBase
    {

        private ListPositionCtrl _listPosCtrl;

        //PowerIndicator _PowerIndicator;
        protected override void On_Init()
        {
            MessageBox.DEBUG("启用游戏包中的UI_GameMain脚本");

            _listPosCtrl = f_GetObject("CircularList").GetComponent<ListPositionCtrl>();
            f_RegClickEvent(f_GetObject("BtnSetup"), OnClick_BtnSetup);

            f_RegClickEvent(f_GetObject("Select"), OnClick_Select);
            f_RegClickEvent(f_GetObject("DelObj"), OnClick_DelObj);


            f_RegClickEvent(f_GetObject("LoadMap"), OnClick_LoadMap);
            f_RegClickEvent(f_GetObject("SaveMap"), OnClick_SaveMap);

            //GameObject PowerLine = glo_Main.GetInstance().m_ResourceManager.f_CreateRes("UI/GameMain/PowerLine", false);
            //PowerLine.transform.parent = this.f_GetObject("Power").transform;
            //_PowerIndicator = PowerLine.GetComponent<PowerIndicator>();



        }

        private void CreateTeamItem()
        {
            List<NBaseSCDT> aData = glo_Main.GetInstance().m_SC_Pool.m_CharacterSC.f_GetAll();
            ListPositionCtrlTools.f_Create(_listPosCtrl, aData);
        }

        protected override void On_Open(object e)
        {
            CreateTeamItem();
            _listPosCtrl.Initialize();

            //ccUIManage.GetInstance().f_SendMsg("UIP_GameText", BaseUIMessageDef.UI_OPEN);
            //StaticValue.m_GamePlotControll.f_Play(StaticValue.m_iCurGamePlotId);
            //_PowerIndicator.f_Start();

            InitObjList();
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
            
        }

        protected override void On_UpdateGUI()
        {

        }

        private void OnClick_LoadMap(GameObject go, object obj1, object obj2)
        {
            GameMain.GetInstance().f_LoadMap();
        }

        private void OnClick_SaveMap(GameObject go, object obj1, object obj2)
        {
            GameMain.GetInstance().f_SaveMap();
        }

        private void OnClick_Select(GameObject go, object obj1, object obj2)
        {
            ListItem tListItem = (ListItem)_listPosCtrl.GetCenteredBox();
            CharacterDT tCharacterDT = (CharacterDT)tListItem.m_SCData;
                                    
            GameMain.GetInstance().f_AddObj(tCharacterDT);
        }

        private void OnClick_DelObj(GameObject go, object obj1, object obj2)
        {
            EditObjControll tEditObjControll = null;
            GameMain.GetInstance().f_DelObj(tEditObjControll.f_GetId());
        }
               
        void OnClick_BtnSetup(GameObject go, object obj1, object obj2)
        {
            ccUIHoldPool.GetInstance().f_Hold(this);
            ccUIManage.GetInstance().f_SendMsg("UI_GameSet", BaseUIMessageDef.UI_OPEN, null, true);
        }

        void OnClick_BtnExit(GameObject go, object obj1, object obj2)
        {
            f_Close();
        }    

        private void DoExit()
        {
           
        }

        protected override void On_Destory()
        {

        }

        float GetPower()
        {


            return 0;
        }

       


    }
}