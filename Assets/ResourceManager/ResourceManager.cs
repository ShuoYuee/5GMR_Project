using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ccU3DEngine;
using ccU3DEngine.ccEngine.ThingManager;

/// <summary>
/// 异步加载资源回调
/// </summary>
/// <param name="Obj">申请资源对象</param>
/// <param name="asset">异步加载成功返回后的资源，根据需要通过GameObject.Instantiate创建</param>
public delegate void ResourceCatchDelegate(object Obj, UnityEngine.Object asset);

public class ResourceManager
{  
    
    public ResourceManager()
    {
    }


    public void f_Update()
    {
           
    }

    #region 创建资源
    

    public AudioClip f_CreateAudio(string strOther, bool bResetPosition = true)
    {
        AudioClip tAudioClip = f_LoadAudioClip(strOther);
        if (tAudioClip == null)
        {
            MessageBox.ASSERT("f_Create Fail.." + strOther);
        }
        return tAudioClip;
    }

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
                MessageBox.ASSERT("Profab没找到 " + ppSQL);
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
            if (oProfab == null){
                MessageBox.ASSERT("Profab没找到 " + ppSQL);
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
    /// 创建的GameObject通过此方法进行回收
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
    /// 获取音效
    /// </summary>
    /// <param name="ButtleOrBg">按钮 或者背景音乐 0是按钮  1为特效 其他为背景音乐</param>
    /// <param name="MusicType">音乐类型</param>
    /// <returns></returns>
    public AudioClip f_LoadAudioClip(string strFile)
    {
        AudioClip tAudioClip = Resources.Load<AudioClip>("Audio/" + strFile) as AudioClip;
        if (tAudioClip == null)
        {
            MessageBox.ASSERT("无此音樂" + strFile);
        }
        return tAudioClip;
    }

    public Sprite f_LoadSpriteForAB(string strAB, string strRes)
    {
        Texture2D Img = AssetLoader.LoadAsset(strAB, strRes) as Texture2D;
        return Sprite.Create(Img, new Rect(0, 0, Img.width, Img.height), Vector2.zero);
    }


    public GameObject f_CreateABObj(string strABBundle, string strAB)
    {
        //GameObject oModel = AssetLoader.LoadAsset(strAB + ".bundle", strAB) as GameObject;
        GameObject oModel = AssetLoader.LoadAsset(strABBundle, strAB) as GameObject;
        GameObject tObj = GameObject.Instantiate(oModel);
        tObj.transform.position = Vector3.zero;

        //Quaternion tQuaternion = Quaternion.Euler(v3Angle.x, v3Angle.y, v3Angle.z);
        tObj.transform.rotation = new Quaternion(0, 0, 0, 0);

        tObj.transform.localScale = new Vector3(1, 1, 1);
        return tObj;
    }

    #endregion



}
