using ccU3DEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapPool : ccBasePool<long>
{
    //private string _strMapFile = "AAA";

    private string GetMapFilePath(string strFileName)
    {
        //return Application.streamingAssetsPath + "/" + _strMapFile + ".txt";
        return Application.streamingAssetsPath + "/SaveFile/" + strFileName + ".txt";
    }

    private string GetMapSavePath()
    {
        return Application.streamingAssetsPath + "/SaveFile";
    }

    public MapPool() : base("MapPoolDT")
    {

    }

    public void f_Reset()
    {
        f_Clear();
    }

    #region LoadMap
    /// <summary>加載地圖數據</summary>
    public void f_LoadMap(string strFileName)
    {

        string path = GetMapFilePath(strFileName);
        bool bNewMap = true;

        MessageBox.DEBUG("f_LoadMap:" + strFileName);
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

    /// <summary>加載預覽資料</summary>
    public string[] f_LoadPreviewData()
    {
        string[] aData = Directory.GetFileSystemEntries(@GetMapSavePath(), "*.txt");
        List<string> tData = new List<string>();
        for(int i = 0; i < aData.Length; i++)
        {
            tData.Add(Path.GetFileNameWithoutExtension(aData[i]));
        }
        return tData.ToArray();
    }

    /// <summary>確認存檔數量</summary>
    public int f_ChekSaveFileCount()
    {
        DirectoryInfo tDirInfo = new DirectoryInfo(GetMapSavePath());
        return tDirInfo.GetFiles("*.txt").Length;
    }
    #endregion

    #region SaveMap

    /// <summary>
    /// 保存當前編輯的地圖訊息
    /// </summary>
    public void f_SaveMap(string strFileName)
    {
        string strMapData = "";
        List<BasePoolDT<long>> aData = f_GetAll();
        for (int i = 0; i < aData.Count; i++)
        {
            MapPoolDT tMapPoolDT = (MapPoolDT)aData[i];
            tMapPoolDT.f_UpdateInfor();

            strMapData = strMapData + tMapPoolDT.f_GetInfor() + "\n";
        }

        string path = GetMapFilePath(strFileName);
       
        FileStream tFileStream = File.Open(path, FileMode.OpenOrCreate);

        byte[] aBuf = System.Text.Encoding.UTF8.GetBytes(strMapData);
        tFileStream.Write(aBuf, 0, aBuf.Length);
        tFileStream.Close();

        MessageBox.DEBUG("f_Save:" + strFileName);
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
    /// <param name="iId">物件ID</param>
    /// <param name="tCharacterDT">物件資料</param>
    /// <returns></returns>
    private EditObjControll AddObj(long iId, CharacterDT tCharacterDT)
    {           
        GameObject tObj = glo_Main.GetInstance().m_ResourceManager.f_CreateABObj(tCharacterDT.szResName + ".bundle", tCharacterDT.szName);
        EditObjControll tEditObjControll = tObj.AddComponent<EditObjControll>();

        /*//設定物件地圖資料
        MapPoolDT tMapPoolDT = new MapPoolDT();
        tMapPoolDT.f_Set(iId, tObj, tCharacterDT);
        //儲存物件地圖資料
        f_Save(tMapPoolDT);
        tEditObjControll.f_Save(tMapPoolDT);*/
        f_SaveData(iId, tCharacterDT, tEditObjControll);

        return tEditObjControll;
    }

    private void f_SaveData(long iId, CharacterDT tCharacterDT, EditObjControll tEditObj)
    {
        //設定物件地圖資料
        MapPoolDT tMapPoolDT = new MapPoolDT();
        tMapPoolDT.f_Set(iId, tEditObj.gameObject, tCharacterDT);
        //儲存物件地圖資料
        f_Save(tMapPoolDT);
        tEditObj.f_Save(tMapPoolDT);
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
    /// 外部調用刪除編輯場景裡物件操作
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

    #region 增加物件
    public void f_ManualAddObj(GameObject tObj)
    {
        MeshFilter[] tMeshes = tObj.GetComponentsInChildren<MeshFilter>();
        CharacterDT tCharacterDT = null;
        if (tMeshes.Length > 0)
        {
            tCharacterDT = f_FindSC(tMeshes);
        }
        if (tCharacterDT != null)
        {
            GameObject oObj = f_AddObj(tCharacterDT).gameObject;

            oObj.transform.position = tObj.transform.position;
            oObj.transform.localEulerAngles = tObj.transform.localEulerAngles;
            oObj.transform.localScale = tObj.transform.localScale;
            GameObject.Destroy(tObj);
            MessageBox.DEBUG("讀取現場物件 : " + tCharacterDT.iId);
        }
    }

    private CharacterDT f_FindSC(MeshFilter[] tMeshes)
    {
        List<NBaseSCDT> tData = glo_Main.GetInstance().m_SC_Pool.m_CharacterSC.f_GetAll();
        CharacterDT tCharacterDT = null;
        MeshFilter[] aMeshes = null;

        for(int i = 0; i < tData.Count; i++)
        {
            tCharacterDT = (CharacterDT)tData[i];
            GameObject oModel = glo_Main.GetInstance().m_ResourceManager.f_GetABObj(tCharacterDT.szName + ".bundle", tCharacterDT.szResName);
            aMeshes = oModel.GetComponentsInChildren<MeshFilter>();

            bool bCheck = false;
            for(int j = 0; j < tMeshes.Length; j++)
            {
                if (j >= aMeshes.Length) { break; }
                if (tMeshes[j].sharedMesh.name != aMeshes[j].sharedMesh.name) { break; }
                if (j == tMeshes.Length - 1)
                {
                    bCheck = true;
                }
            }

            if (bCheck)
            {
                return (CharacterDT)tData[i];
            }
        }

        return null;
    }
    #endregion
}