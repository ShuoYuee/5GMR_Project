using ccU3DEngine;

namespace GameLogic
{
    public class UI_GameBattleUI : ccUILogicBase
    {

        protected override void On_Init()
        {
            MessageBox.DEBUG("啟用遊戲包中的UI_GameBattleUI腳本");



        }

        protected override void On_Open(object e)
        {
            //ccUIManage.GetInstance().f_SendMsg("UIP_GameText", BaseUIMessageDef.UI_OPEN);
            //StaticValue.m_GamePlotControll.f_Play(StaticValue.m_iCurGamePlotId);
        }


        protected override void On_Close()
        {

        }

        protected override void On_Update()
        {
            base.On_Update();


            //	//also update the bar to go up and down
            //	float fillSpeed = indicatorFill.fillAmount < 0.2f ? 0.2f : indicatorFill.fillAmount;

            //	if(indicatorFill.fillAmount > 0.95f && delta > 0){
            //		fillSpeed /= powerbarMaxSlowdown;
            //	}

            //	if(indicatorFill.fillAmount > 0.8f && !max)
            //		StartCoroutine(PowerBarMax());

            //	indicatorFill.fillAmount += Time.deltaTime * powerbarSpeed * delta * fillSpeed;

            //	if((delta < 0 && indicatorFill.fillAmount < 0.03f) || (delta > 0 && indicatorFill.fillAmount > 0.99f))				
            //		delta *= -1f;
            //}
            //else{
            //	//disable the bar if it should not be shown
            //	if(indicator.GetBool("Active"))
            //		indicator.SetBool("Active", false);
            //}

        }

        protected override void On_UpdateGUI()
        {

        }

        protected override void On_Destory()
        {

        }


    }
}
