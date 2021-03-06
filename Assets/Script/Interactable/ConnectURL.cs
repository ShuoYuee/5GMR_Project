using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectURL : Interactable_Component
{
    private string strURL = "";

    public ConnectURL() : base((int)EM_InterState.URL)
    {

    }

    public override void f_Interactable()
    {
        base.f_Interactable();
        f_ConnectURL();
    }

    public void fSetURL(string szURL)
    {
        strURL = szURL;
    }

    public void f_ConnectURL()
    {
        if (strURL.Equals("") || strURL == null) { return; }
        //Application.OpenURL(strURL);
        Main_Browser.GetInstance().f_EnableWindow(true);
        Main_Browser.GetInstance().f_ConnectURL(strURL);
    }

    public void f_ConnectURL(string szURL)
    {
        if (szURL.Equals("") || szURL == null) { return; }
        //Application.OpenURL(szURL);
        Main_Browser.GetInstance().f_EnableWindow(true);
        Main_Browser.GetInstance().f_ConnectURL(strURL);
    }
}
