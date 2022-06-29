using ccU3DEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SCLoad
{
    private string glo_strLoadAllSC = "http://123.207.87.187/BicycleWorld/ver/ccData_W.bytes";
    private HttpFile _httpfile;

    public SCLoad()
    {

    }

    public void f_Start()
    {
        _httpfile = new HttpFile(glo_strLoadAllSC, "ccData_W.bytes");
    }

    public bool f_IsComplete()
    {
        if (_httpfile.isDone)
        {
            return true;
        }

        return false;
    }

}
