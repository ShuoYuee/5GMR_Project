using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ccU3DEngine;
using UnityEngine;

/// <summary>
/// 注册状态机
/// </summary>
public class LoginState_Register : ccMachineStateBase
{
    public LoginState_Register() : base((int)EM_LoginState.Register) { }

    public override void f_Enter(object input)
    {
        string url = "http://192.168.0.70/b/iwSp9ik87oxTTFEDB/board"; // TODO
        Application.OpenURL(url);
    }
}