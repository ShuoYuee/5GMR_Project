using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ccU3DEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CheerleadSelectBtn : Selectable
{
    public string cheerLeadName;
    public EM_TeamID cheerleadID;
    private float text;

    public override void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("按下");
        glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(UIMessageDef.UI_SelectionCheerlead, cheerleadID);
    }
}
