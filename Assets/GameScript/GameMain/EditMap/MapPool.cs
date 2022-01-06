using ccU3DEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapPool : ccBasePool<long>
{
    private string _strMapFile = "AAA";
    private string GetMapFilePath()
    {
        return Application.streamingAssetsPath + "/" + _strMapFile + ".txt";
    }

    public MapPool() : base("MapPoolDT")
    {

    }

    public void f_Reset()
    {
        f_Clear();
    }

    /// <summary>
    /// 加载地图数据
    /// </summary>
    public void f_LoadMap()
    {

        string path = GetMapFilePath();
        bool bNewMap = true;

        MessageBox.DEBUG("f_LoadMap:" + _strMapFile);
        if (!File.Exists(path))
        {
            bNewMap = true;
        }
        else
        {
            StreamReader sr = File.OpenText(path);

            string strData = sr.ReadToEnd();
            sr.Close();

            if (string.IsNullOrEmpty(strData))
            {
                bNewMap = true;
            }
            else
            {
                bNewMap = false;
            }

        }
        if (!bNewMap)
        {
            FileStream tFileStream = File.Open(path, FileMode.OpenOrCreate);
            byte[] aBuf = new byte[tFileStream.Length];
            tFileStream.Read(aBuf, 0, (int)tFileStream.Length);
            tFileStream.Close();

            string strMapData = System.Text.Encoding.UTF8.GetString(aBuf);
            string[] aItem = ccMath.f_String2ArrayString(strMapData, "\n");
            for (int i = 0; i < aItem.Length; i++)
            {
                if (!string.IsNullOrEmpty(aItem[i]))
                {
                    string[] aData = ccMath.f_String2ArrayString(aItem[i], ";");
                    LoadObj(aData);
                }
            }
        }
    }

    #region SaveMap

    /// <summary>
    /// 保存当前编辑的地图信息
    /// </summary>
    public void f_SaveMap()
    {
        string strMapData = "";
        List<BasePoolDT<long>> aData = f_GetAll();
        for (int i = 0; i < aData.Count; i++)
        {
            MapPoolDT tMapPoolDT = (MapPoolDT)aData[i];
            tMapPoolDT.f_UpdateInfor();

            strMapData = strMapData + tMapPoolDT.f_GetInfor() + "\n";
        }

        string path = GetMapFilePath();
       
        FileStream tFileStream = File.Open(path, FileMode.OpenOrCreate);

        byte[] aBuf = System.Text.Encoding.UTF8.GetBytes(strMapData);
        tFileStream.Write(aBuf, 0, aBuf.Length);
        tFileStream.Close();

        MessageBox.DEBUG("f_Save:" + _strMapFile);

    }

    #endregion

    #region AB资源相关
    int iIndex = 0;
    private long CreateKeyId()
    {
        //1138817954
        long iTT = ccMathEx.DateTime2time_t(System.DateTime.Now);
        long iId = iTT * 100 + iIndex;
        iIndex++;
        if (iIndex > 90)
        {
            iIndex = 0;
        }
        return iId;
    }

    public EditObjControll f_AddObj(CharacterDT tCharacterDT)
    {
        long iId = CreateKeyId();
        return AddObj(iId, tCharacterDT);
    }

    /// <summary>
    /// 導入儲存資料
    /// </summary>
    /// <param name="aData">資料</param>
    private void LoadObj(string[] aData)
    {
        long iId = ccMath.atol(aData[0]);
        if (ccMath.atoi(aData[1]) < 1) { return; }
        CharacterDT tCharacterDT = (CharacterDT)glo_Main.GetInstance().m_SC_Pool.m_CharacterSC.f_GetSC(ccMath.atoi(aData[1]));
        EditObjControll tEditObjControll = AddObj(iId, tCharacterDT);
        
        //設定物件的位置、旋轉值、縮放值
        tEditObjControll.gameObject.transform.position = new Vector3(ccMath.atof(aData[2]), ccMath.atof(aData[3]), ccMath.atof(aData[4]));
        tEditObjControll.gameObject.transform.rotation = new Quaternion(ccMath.atof(aData[5]), ccMath.atof(aData[6]), ccMath.atof(aData[7]), ccMath.atof(aData[8]));
        tEditObjControll.gameObject.transform.localScale = new Vector3(ccMath.atof(aData[9]), ccMath.atof(aData[10]), ccMath.atof(aData[11]));

    }

    /// <summary>
    /// 創建物件進地圖
    /// </summary>
    /// <param name="iId"></param>
    /// <param name="tCharacterDT"></param>
    /// <returns></returns>
    private EditObjControll AddObj(long iId, CharacterDT tCharacterDT)
    {           
        GameObject tObj = glo_Main.GetInstance().m_ResourceManager.f_CreateABObj(tCharacterDT.szResName + ".bundle", tCharacterDT.szName);
        
        EditObjControll tEditObjControll = tObj.AddComponent<EditObjControll>();

        MapPoolDT tMapPoolDT = new MapPoolDT();
        tMapPoolDT.f_Set(iId, tObj, tCharacterDT);

        f_Save(tMapPoolDT);
        tEditObjControll.f_Save(tMapPoolDT);

        return tEditObjControll;
    }

    //public bool f_CheckIsDelete(CreateABAction tCreateABAction)
    //{
    //    if (tCreateABAction.m_iRoleId > 0 && string.IsNullOrEmpty(tCreateABAction.m_strBundle) && string.IsNullOrEmpty(tCreateABAction.m_strAB))
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    /// <summary>
    /// 外部调用删除编辑场景里物件操作
    /// </summary>
    /// <param name="iRoleId"></param>
    public void f_DeleteObj(long iRoleId)
    {
        BasePoolDT<long> oData = f_GetForId(iRoleId);
        if (oData == null)
        {
            MessageBox.ASSERT("On Find Obj Data");
        }
        MapPoolDT tMapPoolDT = (MapPoolDT)oData;
        tMapPoolDT.f_Destory();
        f_Delete(iRoleId);

    }

    #endregion



}