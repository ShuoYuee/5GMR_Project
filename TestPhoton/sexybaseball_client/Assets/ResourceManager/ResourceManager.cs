using System.Collections;
using System.Collections.Generic;
using ccU3DEngine;
using ccU3DEngine.ccEngine.ThingManager;
using UnityEngine;

/// <summary>
/// 异步加载资源回调
/// </summary>
/// <param name="Obj">申请资源对象</param>
/// <param name="asset">异步加载成功返回后的资源，根据需要通过GameObject.Instantiate创建</param>
public delegate void ResourceCatchDelegate(object Obj, UnityEngine.Object asset);

public class ResourceManager
{
    /// <summary>
    /// 异步加载资源回调
    /// </summary>
    /// <param name="Obj">申请资源对象</param>
    /// <param name="asset">异步加载成功返回后的资源，根据需要通过GameObject.Instantiate创建</param>
    public delegate void AsyncLoadCallbackDelegate<ResourceType>(ResourceType loadedResource);
    public delegate void AsyncLoadsCallbackDelegate(Dictionary<string, Object> loadedResources);

    public delegate void AsyncProgressCallbackDelegate(float currentProgress);

    public struct BundleInfo
    {
        public string bundleName { get; private set; }
        public string fileName { get; private set; }

        public BundleInfo(string bundleName, string fileName)
        {
            this.bundleName = bundleName;
            this.fileName = fileName;
        }
    }

    private Coroutine _AsyncResourcesCoroutine;
    private Queue<string> _CurrentLoadingResourcesQueue;

    private Coroutine _AsyncBundlesCoroutine;
    private Queue<string> _CurrentLoadingBundlesQueue;

    private Dictionary<string, System.Action> _OnLoadsCompleted;

    public ResourceManager()
    {
        _AsyncResourcesCoroutine = null;
        _AsyncBundlesCoroutine = null;
        _CurrentLoadingResourcesQueue = new Queue<string>();
        _CurrentLoadingBundlesQueue = new Queue<string>();

        _OnLoadsCompleted = null;
    }

    public void ClearLoadingQueue()
    {
        //InterruptCurrentLoading();
        _CurrentLoadingResourcesQueue.Clear();
        _CurrentLoadingBundlesQueue.Clear();

        _OnLoadsCompleted = null;
    }

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

        if (!f_IsInPool(ppSQL))
            return null;

        return ccResourceManager.GetInstance().f_Instantiate(ppSQL);
    }


    private bool f_IsInPool(string ppSQL)
    {
        if (!ccResourceManager.GetInstance().f_CheckIsHave(ppSQL))
        {
            GameObject oProfab = (GameObject)Resources.Load(ppSQL);
            if (oProfab == null)
            {
                MessageBox.ASSERT("Profab没找到 " + ppSQL);
                return false;
            }
            GameObject oModel = GameObject.Instantiate(oProfab, Vector3.zero, Quaternion.identity);
            ccResourceManager.GetInstance().f_RegResource(ppSQL, oModel, null, null, true);
        }
        return true;
    }

    /// <summary>
    /// 產生在 Resource資料夾下的資源 (e.g. Model/Bullet/bullet)  )
    /// </summary>
    /// <param name="resPath"> 資源路徑 (範例: Model/Bullet/bullet ) </param>
    private GameObject CreateResource(string resPath, bool bResetPosition = true, bool bCreate = true)
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

    #region Loading
    /*
    #region Sync
    private ResourceType[] LoadResources<ResourceType>(string[] strPaths) where ResourceType : Object
    {
        if(strPaths == null || strPaths.Length <= 0)
        {
            Debug.LogError("Target paths was 'Null'. Type: " + typeof(ResourceType));
            return null;
        }

        int pathCount = strPaths.Length;
        ResourceType[] loadedResources = new ResourceType[pathCount];

        for (int i = 0; i < pathCount; i++)
        {
            loadedResources[i] = LoadResource<ResourceType>(strPaths[i]);
        }

        return loadedResources;
    }

    private ResourceType LoadResource<ResourceType>(string strPath) where ResourceType : Object
    {
        if(string.IsNullOrWhiteSpace(strPath))
        {
            Debug.LogError("Target resource path was invalid. Type: " + typeof(ResourceType));
            return null;
        }

        ResourceType loadedResource = Resources.Load<ResourceType>(strPath);

        if(loadedResource == null)
        {
            Debug.LogError("Target resource '" + strPath + "' does not exist. Type: " + typeof(ResourceType));
            return null;
        }

        return loadedResource;
    }

    private ResourceType[] LoadBundleAsset<ResourceType>(BundleInfo[] bundleInfos) where ResourceType : Object
    {
        if (bundleInfos == null || bundleInfos.Length <= 0)
        {
            Debug.LogError("Target paths was 'Null'. Type: " + typeof(ResourceType));
            return null;
        }

        int bundleCount = bundleInfos.Length;
        ResourceType[] loadedResources = new ResourceType[bundleCount];

        for (int i = 0; i < bundleCount; i++)
        {
            loadedResources[i] = LoadBundleAsset<ResourceType>(bundleInfos[i]);
        }

        return loadedResources;
    }

    private ResourceType LoadBundleAsset<ResourceType>(BundleInfo bundleInfo) where ResourceType : Object
    {
        return LoadBundleAsset<ResourceType>(bundleInfo.bundleName, bundleInfo.fileName);
    }

    private ResourceType LoadBundleAsset<ResourceType>(string bundleName, string fileName) where ResourceType : Object
    {
        if (string.IsNullOrWhiteSpace(bundleName))
        {
            Debug.LogError("Target bundle name was invalid. Type: " + typeof(ResourceType));
            return null;
        }
        else if (string.IsNullOrWhiteSpace(fileName))
        {
            Debug.LogError("Target resource path was invalid. Type: " + typeof(ResourceType));
            return null;
        }

        ResourceType loadedResource = AssetLoader.LoadAsset(bundleName, fileName) as ResourceType;

        if (loadedResource == null)
        {
            Debug.LogError("Target resource '" + fileName + "' is not exist in target bundle '" + bundleName + ", or target bundle is not found. '. Type: " + typeof(ResourceType));
            return null;
        }

        return loadedResource;
    }
    #endregion

    #region Async
    public void InterruptCurrentLoading()
    {
        if(_AsyncResourcesCoroutine != null)
        {
            ccResourceManager.GetInstance().StopCoroutine(_AsyncResourcesCoroutine);
            _AsyncResourcesCoroutine = null;
            Debug.Log("Interrupt current loading resources coroutine.");
        }
        
        if(_AsyncBundlesCoroutine != null)
        {
            ccResourceManager.GetInstance().StopCoroutine(_AsyncResourcesCoroutine);
            _AsyncBundlesCoroutine = null;
            Debug.Log("Interrupt current loading bundle coroutine.");
        }
    }

    private void LoadResourcesAsync<ResourceType>(string[] strPaths, AsyncLoadCallbackDelegate<> onLoadCompleted = null) where ResourceType : Object
    {
        if (strPaths == null || strPaths.Length <= 0)
        {
            Debug.LogError("Target paths was 'Null'. Type: " + typeof(ResourceType));
            return;
        }

        int pathCount = strPaths.Length;

        for (int i = 0; i < pathCount; i++)
        {
            if (string.IsNullOrWhiteSpace(strPaths[i]))
            {
                Debug.LogError("Target resource path was invalid. Type: " + typeof(ResourceType));
                continue;
            }

        }
    }

    private void LoadResourcesAsync<ResourceType>(string strPath, AsyncLoadCallbackDelegate<ResourceType> onLoadCompleted = null) where ResourceType : Object
    {
        if (string.IsNullOrWhiteSpace(strPath))
        {
            Debug.LogError("Target resource path was invalid. Type: " + typeof(ResourceType));
            return;
        }

        if(onLoadCompleted != null)
        {
            _OnLoadCompleted += () => onLoadCompleted
        }

        _AsyncResourcesCoroutine = ccResourceManager.GetInstance().StartCoroutine(AsyncLoading());
    }

    private ResourceType[] LoadBundleAsset<ResourceType>(BundleInfo[] bundleInfos) where ResourceType : Object
    {
        if (bundleInfos == null || bundleInfos.Length <= 0)
        {
            Debug.LogError("Target paths was 'Null'. Type: " + typeof(ResourceType));
            return null;
        }

        int bundleCount = bundleInfos.Length;
        ResourceType[] loadedResources = new ResourceType[bundleCount];

        for (int i = 0; i < bundleCount; i++)
        {
            loadedResources[i] = LoadBundleAsset<ResourceType>(bundleInfos[i]);
        }

        return loadedResources;
    }

    private ResourceType LoadBundleAsset<ResourceType>(BundleInfo bundleInfo) where ResourceType : Object
    {
        return LoadBundleAsset<ResourceType>(bundleInfo.bundleName, bundleInfo.fileName);
    }

    private ResourceType LoadBundleAsset<ResourceType>(string bundleName, string fileName) where ResourceType : Object
    {
        if (string.IsNullOrWhiteSpace(bundleName))
        {
            Debug.LogError("Target bundle name was invalid. Type: " + typeof(ResourceType));
            return null;
        }
        else if (string.IsNullOrWhiteSpace(fileName))
        {
            Debug.LogError("Target resource path was invalid. Type: " + typeof(ResourceType));
            return null;
        }

        ResourceType loadedResource = AssetLoader.LoadAsset(bundleName, fileName) as ResourceType;

        if (loadedResource == null)
        {
            Debug.LogError("Target resource '" + fileName + "' is not exist in target bundle '" + bundleName + ", or target bundle is not found. '. Type: " + typeof(ResourceType));
            return null;
        }

        return loadedResource;
    }
    
    private IEnumerator AsyncLoading()
    {

        yield break;
    }
    #endregion
    */
    public Sprite f_LoadSpriteForAB(string strAB, string strRes)
    {
        Texture2D Img = AssetLoader.LoadAsset(strAB, strRes) as Texture2D;
        return Sprite.Create(Img, new Rect(0, 0, Img.width, Img.height), Vector2.zero);
    }
    #endregion
}