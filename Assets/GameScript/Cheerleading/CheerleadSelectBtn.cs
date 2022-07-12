using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ccU3DEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CheerleadSelectBtn : MonoBehaviour
{
    public string cheerLeadName;
    public EM_TeamID cheerleadID;
    
    public void GetTeamA()
    {
        glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(UIMessageDef.UI_SelectionCheerlead, EM_TeamID.TeamA);
    }

    public void GetTeamB()
    {
        glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(UIMessageDef.UI_SelectionCheerlead, EM_TeamID.TeamB);
    }

    public void StartGame()
    {
        MessageBox.DEBUG("開始遊戲");
        glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(UIMessageDef.StartGame, null);
    }

    public void JointGame()
    {
        glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(UIMessageDef.PlayerJionGame, null);
    }
}
