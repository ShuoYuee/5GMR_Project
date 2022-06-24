using ccU3DEngine;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    public void OnGameInitialized()
    {
        // 登陆游戏成功，初始游戏数据准备进入游戏
        ccUIManage.GetInstance().f_SendMsg(StrUI.IntroLogo, BaseUIMessageDef.UI_OPEN);
    }
}