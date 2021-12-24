using ccU3DEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace GameLogic
{
    public class UI_GamePlot : ccUILogicBase
    {
        private Transform _Stage;
        private RoleName _RoleName;
        private PlotText _PlotText;
        private GamePlotDT _GamePlotDT;
        Animator _Animator;

        private GameControllDT _GameControllDT;
        private Text _Title;
        GameObject _LogoRawImage;
        GameObject _LogoFFF = null;
        Image _ImageBg = null;

        
        Dictionary<string, GamePlotRole> _aGamePlotRole = new Dictionary<string, GamePlotRole>();

        protected override void On_Destory()
        {

        }

        protected override void On_Init()
        {
            MessageBox.DEBUG("启用游戏包中的UI_GamePlot脚本");

            f_RegClickEvent(f_GetObject("Btn_NextArrow"), OnClick_Btn_NextArrow);
            f_RegClickEvent(f_GetObject("BtnExit"), OnClick_BtnExit);
            f_RegClickEvent(f_GetObject("BtnBegin"), OnClick_BtnBegin);

            _Title = f_GetObject("Title").GetComponent<Text>();
            _LogoRawImage = f_GetObject("LogoRawImage");
            _Stage = f_GetObject("Stage").transform;

            _ImageBg = f_GetObject("BG").GetComponent<Image>();

            GameObject Panel = f_GetObject("Panel");
            _Animator = Panel.GetComponent<Animator>();

            _PlotText = new PlotText(f_GetObject("Btn_NextArrow"), f_GetObject("GameText").GetComponent<Text>());
            _RoleName = new RoleName(f_GetObject("LeftRoleName").GetComponent<Text>(), f_GetObject("LeftRoleName").GetComponent<Text>());
        }

        private void Reset()
        {
            //_aGameTextDT.Clear();
            if (_LogoFFF != null)
            {
                GameObject.Destroy(_LogoFFF);
            }

        }
        protected override void On_Open(object e)
        {
            _Animator.Play("OpenGameText");

            Reset();

            int iGamePlotId = (int)e;
            Play(iGamePlotId);
        }

        private void Play(int iGamePlotId)
        {
            _GamePlotDT = (GamePlotDT)glo_Main.GetInstance().m_SC_Pool.m_GamePlotSC.f_GetSC(iGamePlotId);

            MessageBox.DEBUG("Play:" + _GamePlotDT.iId);
            /*
            开始执行动作类型：

            1.剧本文字 （参数1剧情文字，参数2显示速度(0-10)，参数3无效，参数4无效）

            4.设置角色名（参数1角色名文字，参数2显示左右(0向右 1向左)，参数3无效，参数4无效）
            5.重置所有（参数1无效，参数2无效，参数3无效，参数4无效）
            6.关闭图片或背景图片（参数1资源名Resources\GamePlot，参数2无效，参数3无效，参数4无效）

            21.显示图片 （参数1资源名Resources\GamePlot，参数2无效，参数3无效，参数4无效)
            22.图片位置动画 （参数1资源名Resources\GamePlot，参数2Sx:Sy  开始移动SxSy(空使用当前位置)，参数3Ex:Ey 移动终点Ex:Ey，参数4时间内完成移动)
            23.设置图片显示层次 （参数1资源名Resources\GamePlot，参数2 显示层次(数越大层次越高(0-10))，参数2无效，参数3无效，参数4无效）
            24.设置图片朝向 （参数1资源名Resources\GamePlot，参数2 朝向(0向右 1向左)，参数2无效，参数3无效，参数4无效）
            25.图片缩放动画 （参数1资源名Resources\GamePlot，参数2开始比例(空使用当前大小)，参数3 缩放大小比例(0-1)，参数4时间内完成）


             */
            if (_GamePlotDT.iStartAction == 1)
            {
                CreateText(_GamePlotDT);
            }            
            else if (_GamePlotDT.iStartAction == 4)
            {
                SetRoleName(_GamePlotDT);
            }
            else if (_GamePlotDT.iStartAction == 5)
            {
                //SetRoleName(_GamePlotDT);
            }
            else if (_GamePlotDT.iStartAction == 6)
            {
                CloseImage(_GamePlotDT);
            }

            else if (_GamePlotDT.iStartAction > 20 && _GamePlotDT.iStartAction < 30)
            {
                ImageCMD(_GamePlotDT);
            }
        
            else
            {
                MessageBox.ASSERT("未知剧情指令：" + _GamePlotDT.iId + ":" + _GamePlotDT.iStartAction);
            }
            PlayNext(_GamePlotDT);

            //if (!string.IsNullOrEmpty(_GamePlotDT.szResurce))
            //{
            //    GameObject Obj = CreateObj(_GamePlotDT.szResurce);
            //    GamePlotRole tGamePlotRole = Obj.GetComponent<GamePlotRole>();
            //    if (tGamePlotRole == null)
            //    {
            //        tGamePlotRole = Obj.AddComponent<GamePlotRole>();
            //    }
            //    tGamePlotRole.f_Play(_GamePlotDT);
            //}
            //if (!string.IsNullOrEmpty(_GamePlotDT.szBG))
            //{
            //    LoadBG();
            //}
            //DoStart();
        }

        private void PlayNext(GamePlotDT tGamePlotDT)
        {
            if (tGamePlotDT.iNeedEnd > 0)
            {
                MessageBox.DEBUG(tGamePlotDT.iNeedEnd + "后结束");
                ccTimeEvent.GetInstance().f_RegEvent(tGamePlotDT.iNeedEnd, false, null, CallBackGoNextPlotDT);
            }
            else
            {
                CallBackGoNextPlotDT(null);
            }
        }

        void CallBackGoNextPlotDT(object Obj)
        {
            if (_GamePlotDT.iEndAction > 0)
            {
                Play(_GamePlotDT.iEndAction);
            }
            else
            {
                glo_Main.GetInstance().m_GameMessagePool.f_Broadcast(MessageDef.UI_GamePlotClose);
            }
        }

        private void CreateText(GamePlotDT tGamePlotDT)
        {
            _PlotText.f_Play(tGamePlotDT);
        }

        private void ImageCMD(GamePlotDT tGamePlotDT)
        {
//21.显示图片 （参数1资源名Resources\GamePlot，参数2无效，参数3无效，参数4无效)
//22.图片位置动画 （参数1资源名Resources\GamePlot，参数2Sx: Sy 开始移动SxSy(空使用当前位置)，参数3Ex: Ey 移动终点Ex:Ey，参数4时间内完成移动)
//23.设置图片显示层次 （参数1资源名Resources\GamePlot，参数2 显示层次(数越大层次越高(0 - 10))，参数2无效，参数3无效，参数4无效）
//24.设置图片朝向 （参数1资源名Resources\GamePlot，参数2 朝向(0向右 1向左)，参数2无效，参数3无效，参数4无效）
//25.图片缩放动画 （参数1资源名Resources\GamePlot，参数2开始比例(空使用当前大小)，参数3 缩放大小比例(0 - 1)，参数4时间内完成）
            GamePlotRole tGamePlotRole = GetGamePlotRole(tGamePlotDT.szData1);
            if (tGamePlotRole == null)
            {
                tGamePlotRole = CreatePlotImage(tGamePlotDT.szData1);
            }
            tGamePlotRole.f_DoCMD(tGamePlotDT);
        }
        
        private void SetRoleName(GamePlotDT tGamePlotDT)
        {
            _RoleName.SetRoleName(tGamePlotDT);
        }

        private GamePlotRole GetGamePlotRole(string strRole)
        {
            GamePlotRole tGamePlotRole = null;
            _aGamePlotRole.TryGetValue(strRole, out tGamePlotRole);
            //if (tGamePlotRole == null)
            //{

            //}
            return tGamePlotRole;
        }

            //private void LoadBG()
        //{
        //    GameObject Obj = CreateObj(_GamePlotDT.szBG);
        //    Obj.transform.parent = f_GetObject("BG").transform;            
        //    Obj.transform.localScale = new Vector3(1, 1, 1);
        //}

        protected override void On_Close()
        {
            if (_LogoFFF != null)
            {
                GameObject.Destroy(_LogoFFF);
            }
            _Animator.Play("GameTextExit");
            glo_Main.GetInstance().m_GameMessagePool.f_Broadcast(MessageDef.LockPlayerControll, false);
        }


        protected override void On_UpdateGUI()
        {

        }
        
        void OnClick_Btn_NextArrow(GameObject go, object obj1, object obj2)
        {
            _PlotText.f_FastClick();
        }

        void OnClick_BtnBegin(GameObject go, object obj1, object obj2)
        {
            DestoryRes();
            Play(1);
        }

        void OnClick_BtnExit(GameObject go, object obj1, object obj2)
        {
            DoExit();
        }
        
        private void DoExit()
        {
            DestoryRes();
            _Animator.Play("GameTextExit");
            ccTimeEvent.GetInstance().f_RegEvent(0.7f, false, null, SleepClose);
        }

        private void SleepClose(object Obj)
        { 
            glo_Main.GetInstance().m_GameMessagePool.f_Broadcast(MessageDef.UI_GamePlotClose);
            f_Close();
        }

        private GamePlotRole CreatePlotImage(string strRes)
        {
            GameObject Obj = glo_Main.GetInstance().m_ResourceManager.f_CreateResource("GamePlot/" + strRes);
            if (Obj == null)
            {
                MessageBox.ASSERT("剧情资源未找到：" + strRes);
                return null;
            }
            Obj.name = strRes;
            Obj.transform.parent = _Stage;          // f_GetObject("Panel").transform;
            GamePlotRole tGamePlotRole = Obj.AddComponent<GamePlotRole>();

            _aGamePlotRole.Add(strRes, tGamePlotRole);
            return tGamePlotRole;
        }

        void CloseImage(GamePlotDT tGamePlotDT)
        {
            GamePlotRole tGamePlotRole = GetGamePlotRole(tGamePlotDT.szData1);
            if (tGamePlotRole != null)
            {
                tGamePlotRole.f_Destory();
            }
            _aGamePlotRole.Remove(tGamePlotDT.szData1);
        }

        private void DestoryRes()
        {            
            foreach (KeyValuePair<string, GamePlotRole> tItem in _aGamePlotRole)
            {                
                tItem.Value.f_Destory();
            }
            _aGamePlotRole.Clear();
        }

    }
}