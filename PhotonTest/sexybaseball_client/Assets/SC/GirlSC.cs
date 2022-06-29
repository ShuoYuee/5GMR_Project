
//============================================
//
//    Girl来自Girl.xlsx文件自动生成脚本
//    2021/12/14 下午 01:37:57
//    
//
//============================================
using System;
using System.Collections.Generic;
using ccU3DEngine;


public class GirlSC : NBaseSC
{
    public GirlSC()
    {
        Create("GirlDT");
    }

    public override void f_LoadSCForData(string strData)
    {
        DispSaveData(strData);
    }

    private void DispSaveData(string ppSQL)
    {
        string[] ttt = ppSQL.Split(new string[] { "1#QW" }, System.StringSplitOptions.None);
        GirlDT DataDT;
        string[] tData;
        string[] tFoddScData = ttt[1].Split(new string[] { "|" }, System.StringSplitOptions.None);
        for (int i = 0; i < tFoddScData.Length; i++)
        {
            try
            {
                if (tFoddScData[i] == "")
                {
                    MessageBox.DEBUG(m_strRegDTName + "脚本存在空记录, " + i);
                    continue;
                }
                tData = tFoddScData[i].Split(new string[] { "@," }, System.StringSplitOptions.None);
                int a = 0;
                DataDT = new GirlDT();
                DataDT.iId = ccMath.atoi(tData[a++]);
                if (DataDT.iId <= 0)
                {
                    MessageBox.ASSERT("Id错误");
                }
                DataDT.szName = tData[a++];
                DataDT.iStarPlot = ccMath.atoi(tData[a++]);
                DataDT.iEndPlot = ccMath.atoi(tData[a++]);
                DataDT.szLogo = tData[a++];
                DataDT.szLogo1 = tData[a++];
                DataDT.szIcon = tData[a++];
                DataDT.szTachie1 = tData[a++];
                DataDT.szTachie2 = tData[a++];
                DataDT.szTachie3 = tData[a++];
                DataDT.szTachie4 = tData[a++];
                DataDT.szTachie5 = tData[a++];
                DataDT.iLv1Star = ccMath.atoi(tData[a++]);
                DataDT.iLv2Star = ccMath.atoi(tData[a++]);
                DataDT.fWinRateNPC10 = ccMath.atof(tData[a++]);
                DataDT.fWinRatePlayer10 = ccMath.atof(tData[a++]);
                DataDT.fWinRateNPC11 = ccMath.atof(tData[a++]);
                DataDT.fWinRatePlayer11 = ccMath.atof(tData[a++]);
                DataDT.fWinRateNPC12 = ccMath.atof(tData[a++]);
                DataDT.fWinRatePlayer12 = ccMath.atof(tData[a++]);
                DataDT.fWinRateNPC13 = ccMath.atof(tData[a++]);
                DataDT.fWinRatePlayer13 = ccMath.atof(tData[a++]);
                DataDT.fWinRateNPC20 = ccMath.atof(tData[a++]);
                DataDT.fWinRatePlayer20 = ccMath.atof(tData[a++]);
                DataDT.fWinRateNPC21 = ccMath.atof(tData[a++]);
                DataDT.fWinRatePlayer21 = ccMath.atof(tData[a++]);
                DataDT.fWinRateNPC22 = ccMath.atof(tData[a++]);
                DataDT.fWinRatePlayer22 = ccMath.atof(tData[a++]);
                DataDT.fWinRateNPC23 = ccMath.atof(tData[a++]);
                DataDT.fWinRatePlayer23 = ccMath.atof(tData[a++]);
                SaveItem(DataDT);
            }
            catch
            {
                MessageBox.DEBUG(m_strRegDTName + "脚本记录存在错误, " + i);
                continue;
            }
        }
    }

}
