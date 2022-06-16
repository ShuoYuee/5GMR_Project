using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;

public class XRCubeCustomControllerWindow : EditorWindow
{
    private GUISkin CompalGUIskin;
    private float KeyDelay = 1;
    private bool autohide;
    private string autohide_time;
    // Start is called before the first frame update
    public void Awake()
    {
       
        CompalGUIskin = Resources.Load<GUISkin>("XRCubeGUIskin");
        UnityEngine.Debug.Log("Welcom XRCube Custom Controller.");
        autohide = true;
       autohide_time = "12";

}
    public void OnDestroy()
    {
        UnityEngine.Debug.Log(" XRCube Custom Controller Exit.");
    }
    // Update is called once per frame
    void Update()
    {
        KeyDelay += Time.deltaTime / 100f;
    }
    void OnGUI()
    {
        GUI.skin = CompalGUIskin;
        GUILayout.Label("Settings", EditorStyles.boldLabel);
        autohide = GUI.Toggle(new Rect(25, 20, 200, 25), autohide ? false : true, "Auto Hide Controller");
        GUI.Label(new Rect(22, 50, 100, 15), "Auto hide after", EditorStyles.label);
        GUI.Label(new Rect(182, 50, 100, 15), "seconds.", EditorStyles.label);
        autohide_time = GUI.TextField(new Rect(122, 50, 50, 18), autohide_time, EditorStyles.textArea);
       
        GUILayout.Space(60);
        if (GUILayout.Button("Create & Input Custom Controller") && KeyDelay > 1)
        {
            KeyDelay = 0;
            GameObject preGO;
            GameObject preGO1;
            if (!GameObject.Find("XRCubeControllerSystem"))
            {
                preGO = Instantiate((GameObject)Resources.Load("Prefabs/XRCubeControllerSystem"));
                preGO.name = "XRCubeControllerSystem";
            }
            else
            {
                preGO = GameObject.Find("XRCubeControllerSystem");
            }
            if (!GameObject.Find("XRCubeController"))
            {

                preGO1 = Instantiate((GameObject)Resources.Load("Prefabs/XRCubeController"));
                preGO1.name = "XRCubeController";
            }
            else
            {
                preGO1 = GameObject.Find("XRCubeController");
            }
            preGO.GetComponent<XRCubeCustomController>().XRCubeControllerGO = preGO1.gameObject;
            preGO.GetComponent<XRCubeCustomController>().CtrlLaser = preGO1.transform.GetChild(0).gameObject;
            preGO.GetComponent<XRCubeCustomController>().autohide = autohide;
            preGO.GetComponent<XRCubeCustomController>().autohide_time = float.Parse(autohide_time);
            

        }
        if (GUILayout.Button("Create & Input XRCube Collider") && KeyDelay > 1)
        {
            KeyDelay = 0;
            GameObject preGO;
            preGO = Instantiate((GameObject)Resources.Load("Prefabs/XRCubeCollider"));
            preGO.name = "XRCubeCollider";
        }
    }
  

}
