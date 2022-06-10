using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectURL
{
    private string strURL = "";

    public void fSetURL(string szURL)
    {
        strURL = szURL;
    }

    public void f_ConnectURL()
    {
        if (strURL.Equals("") || strURL == null) { return; }
        Application.OpenURL(strURL);
    }

    public void f_ConnectURL(string szURL)
    {
        if (szURL.Equals("") || szURL == null) { return; }
        Application.OpenURL(szURL);
    }
}
