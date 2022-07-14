
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
                    MessageBox.DEBUG(m_strRegDTName + "腳本存在空記錄, " + i);
                    continue;
                }
                tData = tFoddScData[i].Split(new string[] { "@," }, System.StringSplitOptions.None);
                int a = 0;
                DataDT = new CharacterDT();
                DataDT.iId = ccMath.atoi(tData[a++]);
                if (DataDT.iId <= 0)
                {
                    MessageBox.ASSERT("Id錯誤");
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
                DataDT.iDisplayResource = ccMath.atoi(tData[a++]);
                DataDT.szDisplayAB = tData[a++];
                DataDT.fDisplayScale = ccMath.atof(tData[a++]);
                DataDT.szAnimGroup = tData[a++];
                DataDT.szAudioSource = tData[a++];
                DataDT.szURL = tData[a++];
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

