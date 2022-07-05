using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ccU3DEngine;

public class CheerleadControl : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;
    public GameObject[] danceCheerleads;
    public GameObject[] moraCheerleads;
    public bool isMora;

    void Awake()
    {
        glo_Main.GetInstance().m_UIMessagePool.f_AddListener(UIMessageDef.UI_CheerleadMoraGame, ChangeCheerleadState);
    }

    private void Update()
    {
        if(isMora)
            GameTools.OnAnimComplete(moraCheerleads[0].GetComponent<Animator>(), MoraOver, null);
    }

    public void ChangeCheerleadState(object obj)
    {
        if((CheerleadStateClass)obj == CheerleadStateClass.Mora)
        {
            GameTools.f_SetGameObject(danceCheerleads[0], false);
            GameTools.f_SetGameObject(danceCheerleads[1], false);

            GameTools.f_SetGameObject(moraCheerleads[0], true);
            GameTools.f_SetGameObject(moraCheerleads[1], true);
        }
        else
        {
            GameTools.f_SetGameObject(danceCheerleads[0], true);
            GameTools.f_SetGameObject(danceCheerleads[1], true);

            GameTools.f_SetGameObject(moraCheerleads[0], false);
            GameTools.f_SetGameObject(moraCheerleads[1], false);
        }

        isMora = true;
    }

    private void MoraOver(object obj)
    {
        isMora = false;

        ChangeCheerleadState(CheerleadStateClass.Dance);
    }
}
