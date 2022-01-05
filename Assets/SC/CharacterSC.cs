
//============================================
//
//    Character来自Character.xlsx文件自动生成脚本
//    2018/5/23 20:30:20
//    
//
//============================================
using ccU3DEngine;
using System;
using System.Collections.Generic;



public class CharacterSC : NBaseSC
{
    public CharacterSC()
    {
        Create("CharacterDT", true);
    }

    public override void f_LoadSCForData(string strData)
    {
        DispSaveData(strData);
    }

    private void DispSaveData(string ppSQL)
    {
        string[] ttt = ppSQL.Split(new string[] { "1#QW" }, System.StringSplitOptions.None);
        CharacterDT DataDT;
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
                DataDT = new CharacterDT();
                DataDT.iId = ccMath.atoi(tData[a++]);
                if (DataDT.iId <= 0)
                {
                    MessageBox.ASSERT("Id错误");
                }
                DataDT.szName = tData[a++];
                DataDT.iType = ccMath.atoi(tData[a++]);
                DataDT.szResName = tData[a++];
                DataDT.strReadme = tData[a++];
                DataDT.szLogo = tData[a++];
                DataDT.iAttackPower = ccMath.atoi(tData[a++]);
                DataDT.fAttackSize = ccMath.atof(tData[a++]);
                DataDT.fMoveSpeed = ccMath.atof(tData[a++]);
                DataDT.fViewSize = ccMath.atof(tData[a++]);
                DataDT.szAttackType = tData[a++];
                DataDT.fHeight = ccMath.atof(tData[a++]);
                DataDT.fBodySize = ccMath.atof(tData[a++]);
                DataDT.iNoFind = ccMath.atoi(tData[a++]);
                DataDT.iInvincible = ccMath.atoi(tData[a++]);
                DataDT.iReBirth = ccMath.atoi(tData[a++]);
                DataDT.szAI = tData[a++];
                DataDT.fDisplayScale = ccMath.atof(tData[a++]);
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
