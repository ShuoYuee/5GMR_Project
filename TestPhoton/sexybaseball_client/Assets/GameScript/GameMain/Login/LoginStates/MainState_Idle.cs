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
        UI_MainMenu = (UI_MainMenu)Obj;
        glo_Main.GetInstance().m_GameSocket.f_AddListener((int)SocketCommand.GamePlayCheckRelt, new CMsg_CTG_CheckGuessRelt(), On_CMsg_GTC_CheckStateRelt);

        CMsg_CTG_GuessCommand tCMsg_CTG_GuessCommand = new CMsg_CTG_GuessCommand();
        tCMsg_CTG_GuessCommand.m_iCheckState = (int)EM_GuessState.CheckState;
        glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.GamePlayCheck, tCMsg_CTG_GuessCommand);
    }

    public override void f_Exit()
    {
        base.f_Exit();
        glo_Main.GetInstance().m_GameSocket.f_RemoveListener((int)SocketCommand.GamePlayCheckRelt);
    }

    private void On_CMsg_GTC_CheckStateRelt(object Obj)
    {
        CMsg_CTG_CheckGuessRelt tCMsg_CTG_CheckGuessRelt = (CMsg_CTG_CheckGuessRelt)Obj;
        if (tCMsg_CTG_CheckGuessRelt.m_iResult == (int)EM_GuessState.GameIng)
        {
            UI_MainMenu.f_Start();
            f_SetComplete((int)EM_MainState.Idle);
        }
    }
}
