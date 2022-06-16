using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenFulcrum.EmbeddedBrowser;

public class Main_Browser : MonoBehaviour
{
    public GameObject _MainBrowser;
    private Browser _Browser;

    private static Main_Browser _Instance = null;
    public static Main_Browser GetInstance()
    {
        return _Instance;
    }

    private void Awake()
    {
        _Instance = this;
        _Browser = GameMain.GetInstance().m_Browser;
        f_EnableWindow(false);
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
