using ccU3DEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEvent : MonoBehaviour
{
    private ccCallback _OnStartClose, _OnGetFishComplete;
    public void f_Reg(ccCallback OnStartClose, ccCallback OnGetFishComplete)
    {
        _OnStartClose = OnStartClose;
        _OnGetFishComplete = OnGetFishComplete;
    }

    public void OnStartClose()
    {
        _OnStartClose(null);
    }

    public void OnGetFishComplete()
    {
        if (_OnGetFishComplete != null)
        {
            _OnGetFishComplete(null);
        }
    }



}