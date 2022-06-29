using System.Collections;
using ccU3DEngine;
using UnityEngine;

public class ResManagerState_DispSC : ccMachineStateBase
{
    private bool m_bSaveCatchBuf;

    public ResManagerState_DispSC() : base((int)EM_ResManagerStatic.DispSC) { }

    public override void f_Enter(object Obj)
    {
        byte[] aBytes = (byte[])Obj;
        glo_Main.GetInstance().m_SC_Pool.f_LoadSC(aBytes);
    }

    public override void f_Execute()
    {
        if (glo_Main.GetInstance().m_SC_Pool.f_CheckLoadSuc())
        {
            f_SetComplete((int)EM_ResManagerStatic.Login);
        }
    }
}