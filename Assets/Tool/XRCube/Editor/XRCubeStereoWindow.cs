using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;


public class XRCubeStereoWindow : EditorWindow
{
    private GUISkin CompalGUIskin;
    public Camera Rcam,Lcam;
    private Vector2 scrollViewVector = Vector2.zero;
    private string[] StereoSelect = { "None", "Mono", "Over Under", "Side By Side" };
      int n2, i, StereoNum;
    // Start is called before the first frame update
    public void Awake()
    {
        UnityEngine.Debug.Log("Welcom XRCube Stereo.");
        CompalGUIskin = Resources.Load<GUISkin>("XRCubeGUIskin");
        n2 = 0;i = 0; StereoNum = 0;

    }
    public void OnDestroy()
    {
        UnityEngine.Debug.Log("XRCube Stereo Exit.");
    }
    // Update is called once per frame
    void Update()
    {
       
    }
    void OnGUI()
    {
        GUI.skin = CompalGUIskin;
        GUILayout.Label("Stereo Settings", EditorStyles.boldLabel);
        GUILayout.Space(10);
        GUILayout.Label("      Type", EditorStyles.label);
        GUILayout.Space(75);
        GUILayout.Label("      Please select the Rendering object in scene.", EditorStyles.boldLabel);
     
        if (GUI.Button(new Rect(175, 27f, 18, 18), ">"))
        {
            if (n2 == 0) n2 = 1;
            else n2 = 0;
        }
        if (n2 == 1)
        {

            scrollViewVector = GUI.BeginScrollView(new Rect(200, 27, 170, 100), scrollViewVector, new Rect(0, 0, 150, 100));
            GUI.Box(new Rect(0, 0, 150, 100), "");
            for (i = 0; i < 4; i++)
            {
                if (GUI.Button(new Rect(0, i * 25, 150, 25), ""))
                {
                    n2 = 0; StereoNum = i;
                }
                GUI.Label(new Rect(5, i * 25, 150, 25), StereoSelect[i]);
            }
            GUI.EndScrollView();
        }
        else
        {
            Rcam = (Camera)EditorGUI.ObjectField(new Rect(22, 70f, 375, 18), "Right Eye (Camera)", Rcam, typeof(Camera));
            Lcam = (Camera)EditorGUI.ObjectField(new Rect(22, 90f, 375, 18), "Left Eye (Camera)", Lcam, typeof(Camera));
            GUI.Label(new Rect(200, 25, 150, 25), StereoSelect[StereoNum], EditorStyles.boldLabel);
        }
        if (GUI.Button(new Rect(110, 160, 200, 25), "Input Stereo Component"))
        {
               GameObject preGO= Selection.activeObject as GameObject;
            if(!preGO.GetComponent<StereoMode>())
             { 
                preGO.AddComponent<StereoMode>();
                preGO.GetComponent<StereoMode>().ChangeStereoModeType(StereoNum);
                preGO.GetComponent<StereoMode>().rightCamera = Rcam;
                preGO.GetComponent<StereoMode>().leftCamera = Lcam;
            }
           

        }
        }
    }
