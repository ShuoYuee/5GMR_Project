
//============================================
//
//    RPSGame来自RPSGame.xlsx文件自动生成脚本
//    110-06-01 下午 04:01:18
//    
//
//============================================
using System;
using System.Collections.Generic;
using ccU3DEngine;


public class RPSGameSC : NBaseSC
{
    public RPSGameSC()
    {
        Create("RPSGameDT");
    }

    public override void f_LoadSCForData(string strData)
    {
        DispSaveData(strData);
    }

    private void DispSaveData(string ppSQL)
    {
        string[] ttt = ppSQL.Split(new string[] { "1#QW" }, System.StringSplitOptions.None);
        RPSGameDT DataDT;
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
                DataDT = new RPSGameDT();
                DataDT.iId = ccMath.atoi(tData[a++]);
                if (DataDT.iId <= 0)
                {
                    MessageBox.ASSERT("Id错误");
                }
                DataDT.szRPSModel = tData[a++];
                DataDT.fRockRock = ccMath.atof(tData[a++]);
                DataDT.fRockPaper = ccMath.atof(tData[a++]);
                DataDT.fRockScissors = ccMath.atof(tData[a++]);
                DataDT.fPaperRock = ccMath.atof(tData[a++]);
                DataDT.fPaperPaper = ccMath.atof(tData[a++]);
                DataDT.fPaperScissors = ccMath.atof(tData[a++]);
                DataDT.fScissorsRock = ccMath.atof(tData[a++]);
                DataDT.fScissorsPaper = ccMath.atof(tData[a++]);
                DataDT.fScissorsScissors = ccMath.atof(tData[a++]);
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
