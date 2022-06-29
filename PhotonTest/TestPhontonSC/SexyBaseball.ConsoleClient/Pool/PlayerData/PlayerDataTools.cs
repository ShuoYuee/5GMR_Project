using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ccU3DEngine;

public class PlayerDataTools
{
    public static void f_InitServerDataToClient(CMsg_GTC_LoginRelt tCMsg_GTC_LoginRelt)
    {
        Data_Pool.GetInstance().m_PlayerData.m_CreateTime = tCMsg_GTC_LoginRelt.m_iServerTime;
        Data_Pool.GetInstance().m_PlayerData.m_iUserId = tCMsg_GTC_LoginRelt.m_PlayerId;
    }
}
