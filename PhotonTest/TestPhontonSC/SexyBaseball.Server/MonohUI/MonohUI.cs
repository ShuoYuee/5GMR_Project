using ccU3DEngine;
using SexyBaseball.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class MonohUI : ccThread
{
    MonohUIForm _MonohUIForm = null;
    private DateTime _dtDelayTime;
    private bool _bInitOK = false;
    private bool _bStop = false;

    public override void run()
    {
        if (_MonohUIForm == null)
        {
            _MonohUIForm = new MonohUIForm();
        }
        _dtDelayTime = System.DateTime.Now;

        Application.Run(_MonohUIForm);
    }
    public void f_Stop()
    {
        _bStop = true;
        _MonohUIForm.Close();
    }

    public void CallBack_Log(string strMsgType, string strMsg)
    {
        if (_bStop)
        {
            return;
        }
        if (!IsInitOK())
        {
           return;
        }
        if (_MonohUIForm != null)
        {
            if (strMsgType == "strMsgType")
            {
                _MonohUIForm.f_Log(MonohUIForm.ELogType.ERROR, strMsg);
            }
            else
            {
                _MonohUIForm.f_Log(MonohUIForm.ELogType.INFO, strMsg);
            }
        }
    }

    public bool IsInitOK()
    {
        if (_bInitOK)
        {
            return _bInitOK;
        }
        System.TimeSpan ts = System.DateTime.Now - _dtDelayTime;
        if (((int)ts.TotalSeconds) > 10)
        {
            _bInitOK = true;
            _MonohUIForm.f_Log(MonohUIForm.ELogType.INFO, "InitOK...");
        }
        return _bInitOK;
    }
}
