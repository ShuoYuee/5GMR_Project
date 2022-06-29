using System.Collections;
using ccU3DEngine;
using UnityEngine;

public class ResManagerState_Login : ccMachineStateBase
{
    private ccCallback _ccCallback = null;

    public ResManagerState_Login(ccCallback tccCallback)
        : base((int)EM_ResManagerStatic.Login)
    {
        _ccCallback = tccCallback;
    }

    public override void f_Enter(object Obj)
    {
        _ccCallback(Obj);
    }
}