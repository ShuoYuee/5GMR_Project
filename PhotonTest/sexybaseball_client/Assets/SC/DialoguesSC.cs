
//============================================
//
//    Dialogues来自Dialogues.xlsx文件自动生成脚本
//    110-05-28 上午 10:52:25
//    
//
//============================================
using System;
using System.Collections.Generic;
using ccU3DEngine;


public class DialoguesSC : NBaseSC
{
    public DialoguesSC()
    {
        Create("DialoguesDT");
    }

    public override void f_LoadSCForData(string strData)
    {
        DispSaveData(strData);
    }

    private void DispSaveData(string ppSQL)
    {
        string[] ttt = ppSQL.Split(new string[] { "1#QW" }, System.StringSplitOptions.None);
        DialoguesDT DataDT;
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
                DataDT = new DialoguesDT();
                DataDT.iId = ccMath.atoi(tData[a++]);
                DataDT.szFlowchart = tData[a++];
                DataDT.szCommand = tData[a++];
                DataDT.szMainProperty = tData[a++];
                DataDT.szProperties = tData[a++];
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
