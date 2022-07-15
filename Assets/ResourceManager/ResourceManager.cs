using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using ccU3DEngine;
using ccU3DEngine.ccEngine.ThingManager;

/// <summary>
/// 非同步載入資源回檔
/// </summary>
/// <param name="Obj">申請資源對象</param>
/// <param name="asset">非同步載入成功返回後的資源，根據需要通過GameObject.Instantiate創建</param>
public delegate void ResourceCatchDelegate(object Obj, UnityEngine.Object asset);

public class ResourceManager
{

    public ResourceManager()
    {
    }


    public void f_Update()
    {

    }

    #region 創建資源


    public AudioClip f_CreateAudio(string strOther, bool bResetPosition = true)
    {
        AudioClip tAudioClip = f_LoadAudioClip(strOther);
        if (tAudioClip == null)
        {
            MessageBox.ASSERT("f_Create Fail.." + strOther);
        }
        return tAudioClip;
    }

    #region Resource
    public GameObject f_CreateRes(string strRes, bool bResetPosition = true, bool bCreate = true)
    {
        GameObject oOther = CreateResource(strRes, bResetPosition, bCreate);
        if (!bCreate)
        {
            return null;
        }
        if (oOther == null)
        {
            MessageBox.ASSERT("f_Create Fail.." + strRes);
        }
        return oOther;
    }

    public GameObject f_CreateResource(string resPath)
    {
        string ppSQL = resPath;
        if (!ccResourceManager.GetInstance().f_CheckIsHave(ppSQL))
        {
            GameObject oProfab = (GameObject)Resources.Load(ppSQL);
            if (oProfab == null)
            {
                MessageBox.ASSERT("Profab沒找到 " + ppSQL);
                return null;
            }
            GameObject oModel = (GameObject)GameObject.Instantiate(oProfab, Vector3.zero, Quaternion.identity);
            ccResourceManager.GetInstance().f_RegResource(ppSQL, oModel, null, null, true);
        }
        GameObject oBullet = ccResourceManager.GetInstance().f_Instantiate(ppSQL);
        return oBullet;
    }

    /// <summary>
    /// 產生在 Resource資料夾下的資源 (e.g. Model/Bullet/bullet)  )
    /// </summary>
    /// <param name="resPath"> 資源路徑 (範例: Model/Bullet/bullet ) </param>
    GameObject CreateResource(string resPath, bool bResetPosition = true, bool bCreate = true)
    {
        string ppSQL = resPath;
        if (!ccResourceManager.GetInstance().f_CheckIsHave(ppSQL))
        {
            GameObject oProfab = (GameObject)Resources.Load(ppSQL);
            if (oProfab == null)
            {
                MessageBox.ASSERT("Profab沒找到 " + ppSQL);
                return null;
            }
            GameObject oModel = null;
            if (bResetPosition)
            {
                oModel = (GameObject)GameObject.Instantiate(oProfab, Vector3.zero, Quaternion.identity);
            }
            else
            {
                oModel = (GameObject)GameObject.Instantiate(oProfab);
            }
            ccResourceManager.GetInstance().f_RegResource(ppSQL, oModel, null, null, true);
        }
        if (!bCreate)
        {
            return null;
        }

        GameObject oBullet = ccResourceManager.GetInstance().f_Instantiate(ppSQL);
        return oBullet;
    }

    /// <summary>
    /// 創建的GameObject通過此方法進行回收
    /// </summary>
    /// <param name="Obj"></param>
    public void f_DestorySD(GameObject Obj)
    {
        try
        {
            if (!ccResourceManager.GetInstance().f_UnInstantiate(Obj))
            {
                GameObject.Destroy(Obj);
            }
        }
        catch
        {
            GameObject.Destroy(Obj);
        }

    }

    /// <summary>
    /// 獲取音效
    /// </summary>
    /// <param name="ButtleOrBg">按鈕 或者背景音樂 0是按鈕  1為特效 其他為背景音樂</param>
    /// <param name="MusicType">音樂類型</param>
    /// <returns></returns>
    public AudioClip f_LoadAudioClip(string strFile)
    {
        AudioClip tAudioClip = Resources.Load<AudioClip>("Audio/" + strFile) as AudioClip;
        if (tAudioClip == null)
        {
            MessageBox.ASSERT("無此音樂" + strFile);
        }
        return tAudioClip;
    }

    /// <summary>
    /// 獲取材質球
    /// </summary>
    public Material f_LoadMaterial(string strFile)
    {
        Material tMaterial = Resources.Load<Material>("Material/" + strFile) as Material;
        if (tMaterial == null)
        {
            MessageBox.ASSERT("無此材質球" + strFile);
        }
        return tMaterial;
    }
    #endregion

    #region AB資源
    /// <summary>
    /// 載入圖片檔
    /// </summary>
    /// <param name="strAB">AB資源名</param>
    /// <param name="strRes">物件名</param>
    /// <returns></returns>
    public Sprite f_LoadSpriteForAB(string strAB, string strRes)
    {
        Texture2D Img = AssetLoader.LoadAsset(strAB, strRes) as Texture2D;
        return Sprite.Create(Img, new Rect(0, 0, Img.width, Img.height), Vector2.zero);
    }

    /// <summary>
    /// 創建物件
    /// </summary>
    /// <param name="strABBundle">AB資源名</param>
    /// <param name="strAB">物件名</param>
    /// <returns></returns>
    public GameObject f_CreateABObj(string strABBundle, string strAB, ccCallback ccCallback)
    {
        MessageBox.DEBUG("AB 資源名 : " + strABBundle + " 物件名: " + strAB);
        AssetLoader.LoadAssetAsync(strABBundle, strAB, f_CompleteCreateAB, ccCallback);
        /*GameObject oModel = AssetLoader.LoadAsset(strAB + ".bundle", strAB) as GameObject;
        //GameObject oModel = AssetLoader.LoadAsset(strABBundle, strAB) as GameObject;
        //GameObject tObj = GameObject.Instantiate(oModel);

        //座標歸零
        tObj.transform.position = Vector3.zero;
        tObj.transform.rotation = new Quaternion(0, 0, 0, 0);
        tObj.transform.localScale = new Vector3(1, 1, 1);*/
        return null;
    }

    /// <summary>AB資源載入後執行</summary>
    private void f_CompleteCreateAB(string name, UnityEngine.Object obj, object callbackData)
    {
        GameObject tObj = GameObject.Instantiate((GameObject)obj);

        //座標歸零
        tObj.transform.position = Vector3.zero;
        tObj.transform.rotation = new Quaternion(0, 0, 0, 0);
        tObj.transform.localScale = new Vector3(1, 1, 1);

        //載入完成後自訂執行
        ccTimeEvent.GetInstance().f_RegEvent(0f, false, tObj, (ccCallback)callbackData);
    }

    /// <summary>
    /// 載入動畫機
    /// </summary>
    /// <param name="strAB">AB資源名</param>
    /// <param name="strRes">物件名</param>
    /// <returns></returns>
    public RuntimeAnimatorController f_CreateABAnimator(string strAB, string strRes)
    {
        RuntimeAnimatorController tController = AssetLoader.LoadAsset(strAB, strRes) as RuntimeAnimatorController;
        return tController;
    }
    #endregion
    #endregion
}

