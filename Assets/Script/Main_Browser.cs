using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenFulcrum.EmbeddedBrowser;

/// <summary>
/// VR網頁
/// </summary>
public class Main_Browser : MonoBehaviour
{
    public GameObject _MainBrowser;
    private Browser _Browser;

    private static Main_Browser _Instance = null;
    public static Main_Browser GetInstance()
    {
        return _Instance;
    }

    private void Start()
    {
        _Instance = this;
        ccU3DEngine.ccTimeEvent.GetInstance().f_RegEvent(1f, false, null, f_Init);
    }

    private void f_Init(object obj)
    {
        _Browser = GameMain.GetInstance().m_Browser;
    }

    public void f_ConnectURL(string strURL)
    {
        _Browser.Url = strURL;
    }

    public void f_EnableWindow(bool bSet)
    {
        if (_MainBrowser == null) { return; }
        _MainBrowser.SetActive(bSet);
    }
}
