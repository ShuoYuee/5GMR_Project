using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;

public class XRCubeARWindow : EditorWindow
{
    // Start is called before the first frame update
    public void Awake()
    {
        UnityEngine.Debug.Log("Welcom XRCube ARtracking.");
    }
    public void OnDestroy()
    {
        UnityEngine.Debug.Log("XRCube ARtracking Exit.");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnGUI()
    {
        GUILayout.Space(30);
        EditorGUILayout.LabelField("Coming Soon...", EditorStyles.wordWrappedLabel);
        GUILayout.Space(40);
        if (GUILayout.Button("OK!")) this.Close();
    }
 }
