using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ccU3DEngine;

public class GameTools
{
    public static string f_GetPlayerName(int iPlayer)
    {        
        return "P" + iPlayer;
    }

    #region 計算動畫是否播放完成
    /// <summary>動畫計算時間</summary>
    private static float _fNormalizedTime = 0;

    /// <summary>
    /// 計算動畫是否播放完成
    /// </summary>
    /// <param name="tAnimator">動畫機</param>
    /// <param name="ccCallback">完成後回調方法</param>
    /// <param name="Obj">回調參數</param>
    /// <returns></returns>
    public static bool OnAnimComplete(Animator tAnimator, ccCallback ccCallback, object Obj = null)
    {
        if (!tAnimator) { return false; }
        AnimatorStateInfo tAnimatorStateInfo;
        tAnimatorStateInfo = tAnimator.GetCurrentAnimatorStateInfo(0);
        if (tAnimatorStateInfo.normalizedTime <= 0.5)
        {
            _fNormalizedTime = 0;
        }
        if (_fNormalizedTime < tAnimatorStateInfo.normalizedTime)
        {
            if (tAnimatorStateInfo.normalizedTime >= 1.0f)
            {
                _fNormalizedTime = 9999;
                ccTimeEvent.GetInstance().f_RegEvent(0f, false, Obj, ccCallback);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 計算動畫是否播放完成
    /// </summary>
    /// <param name="tAnimator">動畫機</param>
    /// <param name="ccCallback">完成後回調方法</param>
    /// <param name="Obj">回調參數</param>
    /// <param name="iCallBackTime">回調延遲時間</param>
    /// <returns></returns>
    public static bool OnAnimComplete(Animator tAnimator, ccCallback ccCallback, object Obj = null, float iCallBackTime = 0)
    {
        if (!tAnimator) { return false; }
        AnimatorStateInfo tAnimatorStateInfo;
        tAnimatorStateInfo = tAnimator.GetCurrentAnimatorStateInfo(0);
        if (tAnimatorStateInfo.normalizedTime <= 0.5)
        {
            _fNormalizedTime = 0;
        }
        if (_fNormalizedTime < tAnimatorStateInfo.normalizedTime)
        {
            if (tAnimatorStateInfo.normalizedTime >= 1.0f)
            {
                _fNormalizedTime = 9999;
                ccTimeEvent.GetInstance().f_RegEvent(iCallBackTime, false, Obj, ccCallback);
                return true;
            }
        }
        return false;
    }

    /// <summary>重整動畫計算</summary>
    public static void f_ReAnimEventTime()
    {
        _fNormalizedTime = 0;
    }
    #endregion

    #region 動畫回調
    private static Animator _AnimatorBack = null;
    private static ccCallback _AnimCallBack = null;
    private static int iAnimEndEvent = 0;
    public static void f_AnimPlayCallBack(Animator tAnimator, string strAnimName, ccCallback ccCallback, object Obj = null)
    {
        _AnimatorBack = tAnimator;
        _AnimCallBack = ccCallback;
        //播放選項面板淡入動畫，並等待動畫播放完畢再做回調方法
        bool bPlay = f_PlayAnimator(_AnimatorBack, strAnimName);
        f_ReAnimEventTime();
        //開啟動畫偵測事件計時器
        if (bPlay)
        {
            iAnimEndEvent = ccTimeEvent.GetInstance().f_RegEvent(0.02f, 1, Obj, f_AnimPlayIng);
        }
        else
        {//如果動畫啟動失敗則直接進行回調
            f_AnimCallBackEnd(Obj);
        }
    }

    private static void f_AnimPlayIng(object Obj)
    {
        OnAnimComplete(_AnimatorBack, f_AnimCallBackEnd, Obj, 0f);
    }

    public static void f_AnimCallBackEnd(object Obj)
    {
        //取消動畫偵測事件計時器
        ccTimeEvent.GetInstance().f_UnRegEvent(iAnimEndEvent);
        _AnimCallBack(Obj);
        _AnimCallBack = null;
        _AnimatorBack = null;
    }
    #endregion

    #region 設定組件

    #region Image
    /// <summary>
    /// 開關圖片組件
    /// </summary>
    /// <param name="tImage">圖片組件</param>
    /// <param name="bSet">開關</param>
    /// <returns></returns>
    public static bool f_SetImage(Image tImage, bool bSet)
    {
        if (tImage)
        {
            tImage.enabled = bSet;
        }
        return tImage;
    }

    public static bool f_SetImageRay(Image tImage, bool bSet)
    {
        if (tImage)
        {
            tImage.raycastTarget = bSet;
        }
        return tImage;
    }

    public static bool f_SetImage(Image tImage, Sprite sprite)
    {
        if (tImage)
        {
            tImage.sprite = sprite;
        }
        return tImage;
    }
    #endregion

    /// <summary>
    /// 設定文字組件
    /// </summary>
    /// <param name="tText">文字組件</param>
    /// <param name="strSet">文字內容</param>
    /// <returns></returns>
    public static bool f_SetText(Text tText, string strSet)
    {
        if (tText)
        {
            tText.text = strSet;
        }
        return tText;
    }

    /// <summary>
    /// 開關GameObject
    /// </summary>
    /// <param name="oObj">遊戲物件</param>
    /// <param name="bSet">開關</param>
    /// <returns></returns>
    public static bool f_SetGameObject(GameObject oObj, bool bSet)
    {
        if (oObj)
        {
            oObj.SetActive(bSet);
        }
        return oObj;
    }

    #endregion

    /// <summary>
    /// 播放指定動畫
    /// </summary>
    /// <param name="tAnimator">動畫機</param>
    /// <param name="strAnim">動畫名</param>
    /// <param name="fTime">淡入時間</param>
    /// <param name="bRePlay">是否為重播動畫</param>
    /// <returns></returns>
    public static bool f_PlayAnimator(Animator tAnimator, string strAnim, float fTime = 0.001f, bool bRePlay = false)
    {
        if (tAnimator)
        {
            if (bRePlay)
            {
                tAnimator.Play(strAnim, 0, 0f);
            }
            else
            {
                tAnimator.CrossFade(strAnim, fTime);
            }
        }
        return tAnimator;
    }

    #region 尋找物件
    /// <summary>
    /// 尋找子物件
    /// </summary>
    /// <param name="name">物件名</param>
    /// <returns></returns>
    public static GameObject f_GetGameObject(List<GameObject> aList, string name)
    {
        for (int i = 0; i < aList.Count; i++)
        {
            if (aList[i].name == name)
            {
                return aList[i];
            }
        }
        return null;
    }

    /// <summary>
    /// 尋找子物件
    /// </summary>
    /// <param name="name">物件名</param>
    /// <returns></returns>
    public static GameObject f_GetGameObject(Transform tParent, string name)
    {
        Transform[] childPos = tParent.GetComponentsInChildren<Transform>();
        for (int i = 0; i < childPos.Length; i++)
        {
            if (childPos[i].name == name)
            {
                return childPos[i].gameObject;
            }
        }
        return null;
    }

    /// <summary>
    /// 尋找父物件
    /// </summary>
    /// <param name="name">物件名</param>
    /// <returns></returns>
    public static GameObject f_GetGameObjectParent(Transform tChild, string name)
    {
        Transform tParent = tChild.parent;
        while (tParent)
        {
            if (tParent.name == name)
            {
                return tParent.gameObject;
            }
            tParent = tParent.parent;
        }
        return null;
    }
    #endregion

}