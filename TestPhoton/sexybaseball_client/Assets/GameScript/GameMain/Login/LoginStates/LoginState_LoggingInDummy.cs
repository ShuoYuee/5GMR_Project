using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ccU3DEngine;

/// <summary>
/// 测试用状态机
/// </summary>
public class LoginState_LoggingInDummy : ccMachineStateBase
{
    public LoginState_LoggingInDummy() : base((int)EM_LoginState.LoggingInDummy) { }

    public override void f_Enter(object input)
    {
        ccUIManage.GetInstance().f_SendMsg(StrUI.Start, BaseUIMessageDef.UI_OPEN);
    }
}