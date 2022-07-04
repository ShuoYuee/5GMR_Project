using ccU3DEngine;
using GameLogic;
using SexyBaseball.Server;

public class MainState_Idle : ccMachineStateBase
{
    private UI_MainMenu UI_MainMenu;

    public MainState_Idle() : base((int)EM_MainState.Idle)
    {

    }

    public override void f_Enter(object Obj)
    {
        base.f_Enter(Obj);
        MessageBox.DEBUG("進入主介面");
        UI_MainMenu = (UI_MainMenu)Obj;
        glo_Main.GetInstance().m_GameSocket.f_AddListener((int)SocketCommand.GamePlayCheckRelt, new CMsg_CTG_CheckGuessRelt(), On_CMsg_GTC_CheckGameRelt);

        CMsg_CTG_ServerCommand tCMsg_CTG_ServerCommand = new CMsg_CTG_ServerCommand();
        tCMsg_CTG_ServerCommand.m_szAccount = StaticValue.m_strAccount;
        tCMsg_CTG_ServerCommand.m_lPlayerID = StaticValue.m_iUserID;
        tCMsg_CTG_ServerCommand.m_iCommand = (int)EM_GameMod.Guess;
        tCMsg_CTG_ServerCommand.m_iCallState = (int)EM_GuessState.CheckState;
        glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.ServerCommand, tCMsg_CTG_ServerCommand);
    }

    public override void f_Exit()
    {
        base.f_Exit();
        glo_Main.GetInstance().m_GameSocket.f_RemoveListener((int)SocketCommand.GamePlayCheckRelt);
    }

    private void On_CMsg_GTC_CheckGameRelt(object Obj)
    {
        CMsg_CTG_CheckGuessRelt tCMsg_CTG_CheckGuessRelt = (CMsg_CTG_CheckGuessRelt)Obj;
        if (tCMsg_CTG_CheckGuessRelt.m_iResult == (int)EM_GuessState.GameIng)
        {//遊戲已開始
            UI_MainMenu.f_WaitGameEnter();
            MessageBox.DEBUG("遊戲已開始，等待參與下一回合");
        }
        else if(tCMsg_CTG_CheckGuessRelt.m_iResult == (int)EM_GuessState.NotGameIng)
        {//遊戲未開始
            UI_MainMenu.f_MainUICtrl(0);
            MessageBox.DEBUG("遊戲還未開始");
        }
        else if (tCMsg_CTG_CheckGuessRelt.m_iResult == (int)EM_GuessState.CallStart)
        {//開始遊戲
            UI_MainMenu.f_Start();
            MessageBox.DEBUG("遊戲開始");
        }
        else if (tCMsg_CTG_CheckGuessRelt.m_iResult == (int)EM_GuessState.CallRoomMaster)
        {//開始遊戲，成為房主
            UI_MainMenu._bMainGameCtrl = true;
            MessageBox.DEBUG("你已成為房主");
        }
        else if (tCMsg_CTG_CheckGuessRelt.m_iResult == (int)eMsgOperateResult.OR_Error_GameIsStart)
        {//開啟遊戲失敗，遊戲已在運行
            MessageBox.DEBUG("已有房主在進行遊戲");
        }
    }
}
