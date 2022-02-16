using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

//協議操作類型
public enum eMsgOperateType
{
    OT_NULL = -99,      //不存在錯誤碼

    OT_CreateAccount = 0, // 創建帳號
    OT_LoginGame = 1, // 登入遊戲
};


//角色屬性枚舉
public enum eChangeRoleDataType
{
    eDefault = 0,
    eAccountId = 1000,      //帳號ID AccountId=UserId

    eGUID = 1001,			//uid
    eLevel = 1002,       //等級	
    eRank = 1003,		//玩家rank
    eLastTime = 1004,	//Last_Login 最後離線時間	
    eCityId = 1005,	//看板娘範本Id

    eExp = 1007,		//經驗

    eGold = 1008,		//金幣
    eToken = 1009,		//魔法石

    eAdvanecPP = 1010,			//AP
    eBp = 1011,			//BP

    eNoobStep = 1015,
    eBan = 1016,

    eActive = 1017,
    eVisitor = 1018,
    eMoney = 1019,

}

