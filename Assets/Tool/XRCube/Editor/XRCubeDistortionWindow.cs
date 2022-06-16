using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;


public class XRCubeDistortionWindow : EditorWindow
{
    private GUISkin CompalGUIskin;
    private Vector2 scrollViewVector = Vector2.zero;
    string DistortionK1_Input, DistortionK2_Input, DistortionK3_Input;
    bool is2D, is3D;
    // Start is called before the first frame update
    public void Awake()
    {
        UnityEngine.Debug.Log("Welcom XRCube Distortion.");
        CompalGUIskin = Resources.Load<GUISkin>("XRCubeGUIskin");
        DistortionK1_Input = "0"; DistortionK2_Input = "0"; DistortionK3_Input = "0";
        is2D = true; is3D = false;
    }
    public void OnDestroy()
    {
        UnityEngine.Debug.Log("XRCube Distortion Exit.");
    }
    // Update is called once per frame
    void Update()
    {

    }
    void OnGUI()
    {
        GUI.skin = CompalGUIskin;
        GUILayout.Label("Distortion Settings", EditorStyles.boldLabel);
        is2D = GUI.Toggle(new Rect(21, 30, 50, 25), is3D ? false : is2D, " 2D");
        is3D = GUI.Toggle(new Rect(91, 30, 100, 25), is2D ? false : is3D, " 3D(VR)");
        GUI.Label(new Rect(22, 60, 50, 15), "K1 :", EditorStyles.label);
        GUI.Label(new Rect(22, 80, 50, 15), "K2 :", EditorStyles.label);
        GUI.Label(new Rect(22, 100, 50, 15), "K3 :", EditorStyles.label);
        DistortionK1_Input = GUI.TextField(new Rect(172, 60, 225, 18), DistortionK1_Input, EditorStyles.textArea);
        DistortionK2_Input = GUI.TextField(new Rect(172, 80, 225, 18), DistortionK2_Input, EditorStyles.textArea);
        DistortionK3_Input = GUI.TextField(new Rect(172, 100, 225, 18), DistortionK3_Input, EditorStyles.textArea);
        GUILayout.Space(110);
        GUILayout.Label("      Please select the Rendering object in scene.", EditorStyles.boldLabel);
        if (GUI.Button(new Rect(110, 200, 200, 25), "Replace to Distortion Shader"))
        {
            GameObject preGO = Selection.activeObject as GameObject;
            is2D = is2D ? preGO.GetComponent<MeshRenderer>().sharedMaterial.shader = Shader.Find("XRCube/XRCube_2D") :preGO.GetComponent<MeshRenderer>().sharedMaterial.shader = Shader.Find("XRCube/XRCube_InsideVisible");
            preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK1", float.Parse(DistortionK1_Input));
            preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK2", float.Parse(DistortionK2_Input));
            preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK3", float.Parse(DistortionK3_Input));
        }
        if (GUI.Button(new Rect(110, 250, 200, 25), "Reset to Default Shader"))
        {
            GameObject preGO = Selection.activeObject as GameObject;
            preGO.GetComponent<MeshRenderer>().sharedMaterial.shader = Shader.Find("Standard");
        }
    }
}
