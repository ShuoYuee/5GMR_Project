using System;
using System.Collections.Generic;
using ccU3DEngine;


public class GoodsSC : NBaseSC
{
    public GoodsSC()
    {
        Create("GoodsDT");
    }

    public override void f_LoadSCForData(string strData)
    {
        DispSaveData(strData);
    }

    private void DispSaveData(string ppSQL)
    {
        string[] ttt = ppSQL.Split(new string[] { "1#QW" }, System.StringSplitOptions.None);
        GoodsDT DataDT;
        string[] tData;
        string[] tFoddScData = ttt[1].Split(new string[] { "|" }, System.StringSplitOptions.None);
        for (int i = 0; i < tFoddScData.Length; i++)
        {
            try
            {
                if (tFoddScData[i] == "")
                {
                    MessageBox.DEBUG(m_strRegDTName + "腳本存在空記錄, " + i);
                    continue;
                }
                tData = tFoddScData[i].Split(new string[] { "@," }, System.StringSplitOptions.None);
                int a = 0;
                DataDT = new GoodsDT();
                DataDT.iId = ccMath.atoi(tData[a++]);
                DataDT.szName = tData[a++];
                DataDT.iCatAnime = ccMath.atoi(tData[a++]);
                DataDT.iCatExpA = ccMath.atoi(tData[a++]);
                DataDT.iCatExpB = ccMath.atoi(tData[a++]);
                DataDT.iCatExpC = ccMath.atoi(tData[a++]);
                DataDT.iCatExpD = ccMath.atoi(tData[a++]);
                SaveItem(DataDT);
            }
            catch
            {
                MessageBox.DEBUG(m_strRegDTName + "腳本記錄存在錯誤, " + i);
                continue;
            }
        }
    }

}

