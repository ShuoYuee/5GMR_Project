using System.Collections;
using ccU3DEngine;
using UnityEngine;

public class ResManagerState_Loop : ccMachineStateBase
{
    public ResManagerState_Loop() : base((int)EM_ResManagerStatic.Loop) { }

    public override void f_Enter(object Obj)
    {
        base.f_Enter(Obj);
    }
}
