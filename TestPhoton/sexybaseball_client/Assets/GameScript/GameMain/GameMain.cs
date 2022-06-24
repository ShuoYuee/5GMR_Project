using System.Collections;
using System.Collections.Generic;
using ccU3DEngine;
using ccPhotonSocket;
using SexyBaseball.Server;
using UnityEngine;

public class GameMain : ccSceneBase
{
    public override void UnLoadRes()
    {
        
    }

    protected override void LoadRes()
    {
        
    }

    protected override void StartScene()
    {
        bool isConnected = glo_Main.GetInstance().m_GameSocket.m_bIsConnected;
        if (isConnected)
        {
            ccUIManage.GetInstance().f_SendMsg(StrUI.Login, BaseUIMessageDef.UI_OPEN);
        }
        else
        {
            ccUIManage.GetInstance().f_SendMsg(StrUI.ServerFailure, BaseUIMessageDef.UI_OPEN);
        }
    }


}
