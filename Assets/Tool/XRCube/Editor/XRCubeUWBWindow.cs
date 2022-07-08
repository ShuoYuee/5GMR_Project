using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;

public class XRCubeUWBWindow : EditorWindow
{
    private GUISkin CompalGUIskin;
    static JSONObject json;
    private string dataflowserverip = "192.168.0.101:5001";
    private float KeyDelay = 1;
    // Start is called before the first frame update
    public void Awake()
    {
        json = new JSONObject(File.ReadAllText(Application.dataPath + "/XRCube/Editor/Config.json"));
        dataflowserverip = json.GetField("Path").GetField("dataflowserver").str;
        resetDataflow();
        smooth = 1;
        CompalGUIskin = Resources.Load<GUISkin>("XRCubeGUIskin");
        UnityEngine.Debug.Log("Welcom XRCube UWB Position.");
       // UnityEngine.Debug.Log(dataflowserverip);
    }
    public void OnDestroy()
    {
        UnityEngine.Debug.Log("XRCube UWB Position Exit.");
    }
    // Update is called once per frame
    void Update()
    {
        KeyDelay += Time.deltaTime/100f;
    }
    
    bool[] compaldataflowbool=new bool[10];
    bool updatedataflow = false;
    int DataflowNum = 1;
    float smooth = 0;
    bool pathtracking = false;
    bool testmode = false;
    void OnGUI()
    {
        GUI.skin = CompalGUIskin;
        GUILayout.Label("UWB Tag ID", EditorStyles.boldLabel);
        GUILayout.Space(50);
        GUILayout.Label("Smooth Filter", EditorStyles.boldLabel);
        GUILayout.Label("     "+smooth.ToString());
        GUILayout.Space(30);
        smooth = GUI.HorizontalScrollbar(new Rect(10, 110, 100, 30),smooth, 0f, 0f, 1f);
        compaldataflowbool[0] = GUI.Toggle(new Rect(25, 20, 60, 25), checkdataflowbool(0) ? false : compaldataflowbool[0], " Num 1");
        compaldataflowbool[1] = GUI.Toggle(new Rect(105, 20, 60, 25), checkdataflowbool(1) ? false : compaldataflowbool[1], " Num 2");
        compaldataflowbool[2] = GUI.Toggle(new Rect(25, 40, 60, 25), checkdataflowbool(2) ? false : compaldataflowbool[2], " Num 3");
        compaldataflowbool[3] = GUI.Toggle(new Rect(105, 40, 60, 25), checkdataflowbool(3) ? false : compaldataflowbool[3], " Num 4");
        for (int j = 0; j < compaldataflowbool.Length; j++)
        {
            if (compaldataflowbool[j])
            {
                if (!updatedataflow)
                {
                    updatedataflow = true;
                    DataflowNum = j+1;
                }
            }
        }
        GUILayout.Label("Advenced Options", EditorStyles.boldLabel);
        GUILayout.Space(30);
        pathtracking = GUI.Toggle(new Rect(25, 160, 100, 25), pathtracking, " Path Tracking");
        testmode = GUI.Toggle(new Rect(145, 160, 100, 25), testmode, " Test Mode");
        updatedataflow = false;
        GUILayout.Label("Type Selection", EditorStyles.boldLabel);
        if (GUILayout.Button("UWB Log Printer") && KeyDelay>1)
        {
            KeyDelay = 0;
            GameObject preGO= new GameObject();
            preGO.name = "UWB Log Printer_" + DataflowNum;
            preGO.AddComponent<Dataflow>();
            preGO.GetComponent<Dataflow>().DataflowNum = DataflowNum;
            preGO.GetComponent<Dataflow>().DataflowProtocol = 4;
            preGO.GetComponent<Dataflow>().dataflowserverip = dataflowserverip;
            preGO.AddComponent<XRCubeUWBPosition>();
            preGO.GetComponent<XRCubeUWBPosition>().TagID = 0;
            preGO.GetComponent<XRCubeUWBPosition>().smooth = smooth;
            preGO.GetComponent<XRCubeUWBPosition>().enabled = false;
            if (testmode)
            {
                preGO.AddComponent<XRCubeUWBTestmode>();
                preGO.GetComponent<XRCubeUWBTestmode>().showLog = true;
                preGO.GetComponent<XRCubeUWBTestmode>().enabled = true;
                preGO.GetComponent<XRCubeUWBPosition>().enabled = false;
                preGO.GetComponent<Dataflow>().enabled = false;
            }
        }
        if (GUILayout.Button("UWB Tracking Object") && KeyDelay > 1)
        {
            KeyDelay = 0;
            GameObject preGO = new GameObject();
            preGO.name = "UWB Rig_" + DataflowNum;
            GameObject preGO1 = Instantiate((GameObject)Resources.Load("Prefabs/UWB Tracking Object"));
            preGO1.transform.parent = preGO.transform;
            preGO1.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            preGO1.GetComponent<Dataflow>().DataflowNum = DataflowNum;
            preGO1.GetComponent<Dataflow>().DataflowProtocol = 4;
            preGO1.GetComponent<Dataflow>().dataflowserverip = dataflowserverip;
            preGO1.GetComponent<XRCubeUWBPosition>().TagID = 0;
            preGO1.GetComponent<XRCubeUWBPosition>().smooth = smooth;
            preGO1.GetComponent<XRCubeUWBPosition>().showLog=false;
            preGO1.GetComponent<XRCubeUWBPosition>().enabled = false;
            preGO1.GetComponent<TrailRenderer>().enabled = pathtracking;
            if(testmode)
            {
                preGO1.GetComponent<XRCubeUWBTestmode>().showLog = false;
                preGO1.GetComponent<XRCubeUWBTestmode>().enabled = true;
                preGO1.GetComponent<XRCubeUWBPosition>().enabled = false;
                preGO1.GetComponent<Dataflow>().enabled = false;
                preGO1.GetComponent<XRCubeUWBTracking>().enabled = false;
            }    
                
        }
     }
    bool checkdataflowbool(int i)
    {
        for (int j = 0; j < compaldataflowbool.Length; j++)
        {
            if (j != i && compaldataflowbool[j])
            {
                return true;
            }

        }
        return false;
    }
        void resetDataflow()
        {
            for (int j = 0; j < compaldataflowbool.Length; j++)
            {
                compaldataflowbool[j] = false;
            }
              compaldataflowbool[0] = true;
            DataflowNum = 0;

        }
    }
