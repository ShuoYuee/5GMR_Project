using System;
using System.Collections.Generic;

namespace ccUI_U3DSpace
{
    public class ccUImrManager
    {
        private List<MRUI_LogicBase> _aList = new List<MRUI_LogicBase>();

        private static ccUImrManager _Instance = null;
        public static ccUImrManager GetInstance()
        {
            return _Instance;
        }

        public void f_Init()
        {
            _Instance = this;
        }

        public void f_AddBase(MRUI_LogicBase mRUI_LogicBase)
        {
            _aList.Add(mRUI_LogicBase);
        }

        public void f_SendMsg(string strName, string strMessageType, object oData = null)
        {
            MRUI_LogicBase _LogicBase = f_GetLogicBase(strName);
            if (strMessageType == UIMessageDef.UI_OPEN)
            {
                _LogicBase.f_Open(oData);
            }
            else if (strMessageType == UIMessageDef.UI_CLOSE)
            {
                _LogicBase.f_Close(oData);
            }
        }

        private MRUI_LogicBase f_GetLogicBase(string strName)
        {
            MRUI_LogicBase _LogicBase = _aList.Find(delegate (MRUI_LogicBase p)
              {
                  if (p.name == strName) { return p; }
                  return false;
              });
            return _LogicBase;
        }
    }
}
