using ccU3DEngine;

namespace GameLogic
{
    public class UI_IntroLogo : ccUILogicBase
    {
        private LogoIntro _logoIntro = null;

        protected override void On_Init()
        {
            _logoIntro = f_GetObject("Panel").GetComponent<LogoIntro>();
            _logoIntro.m_onLogoCompleted.AddListener(UnityAction_OnLogoCompleted);
        }

        protected override void On_Open(object e)
        {
#if UNITY_EDITOR
            // 在編輯器可選擇跳過方便測試。
            if (_logoIntro.m_bSkipIntro)
            {
                UnityAction_OnLogoCompleted();
            }
#endif
        }

        protected override void On_Close()
        {
        }

        protected override void On_Update()
        {
        }

        protected override void On_UpdateGUI()
        {
        }

        protected override void On_Destory()
        {
        }

        private void UnityAction_OnLogoCompleted()
        {
            ccSceneMgr.GetInstance().f_ChangeScene(StrScene.GameMain);
        }
    }
}