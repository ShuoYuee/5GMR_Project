using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;

public class XRCubeMenuItem : EditorWindow
{
    public Texture btnTexture;
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;
   
    // Add menu named "My Window" to the Window menu
    [MenuItem("XRCube/Media Player")]
    static void Media()
    {
      //  UnityEngine.Debug.Log("132");
        // Get existing open window or if none, make a new one:
        XRCubeMediaPlayerWindow window = (XRCubeMediaPlayerWindow)EditorWindow.GetWindow(typeof(XRCubeMediaPlayerWindow));
        window.title = "XR Cube Media Player";
        window.Show();
    }
    [MenuItem("XRCube/Stereo Mode")]
    static void Stereo()
    {
        // Get existing open window or if none, make a new one:
        XRCubeStereoWindow window = (XRCubeStereoWindow)EditorWindow.GetWindow(typeof(XRCubeStereoWindow));
        window.title = "XR Cube Stereo";
        window.Show();
    }
    [MenuItem("XRCube/Distortion Shader")]
    static void Distortion()
    {
        // Get existing open window or if none, make a new one:
        XRCubeDistortionWindow window = (XRCubeDistortionWindow)EditorWindow.GetWindow(typeof(XRCubeDistortionWindow));
        window.title = "XR Cube Distortion";
        window.Show();
    }
    [MenuItem("XRCube/UWB Position")]
    static void UWB()
    {
        XRCubeUWBWindow window = (XRCubeUWBWindow)EditorWindow.GetWindow(typeof(XRCubeUWBWindow));
        window.title ="XR Cube UWB Position";
        window.Show();
    }
    static string SteamVR_path = "C:/Program Files (x86)/Steam/steamapps/common/SteamVR/";
    
[MenuItem("XRCube/Custom Controller")]
    static void CustomController()
    {
        XRCubeCustomControllerWindow window = (XRCubeCustomControllerWindow)EditorWindow.GetWindow(typeof(XRCubeCustomControllerWindow));
        window.title = "XR Cube Custom Controller";
        window.Show();
    }

    [MenuItem("XRCube/AR Tracking")]
    static void AR()
    {
        XRCubeARWindow window = (XRCubeARWindow)EditorWindow.GetWindow(typeof(XRCubeARWindow));
        window.title = "XR Cube ARtracking";
        window.Show();
    }
    [MenuItem("XRCube/XR Simulator")]
    static void Simulator()
    {
        XRCubeSimulatorWindow window = (XRCubeSimulatorWindow)EditorWindow.GetWindow(typeof(XRCubeSimulatorWindow));
        window.title = "XR Cube Simulator";
        window.Show();
    }
    [MenuItem("XRCube/Help")]
    static void help()
    {

        Application.OpenURL("https://www.apaltec.com/zh-hant/mr%E8%A7%A3%E6%B1%BA%E6%96%B9%E6%A1%88/");
    }
   /* [MenuItem("Example/Overwrite Texture")]
    static void Apply()
    {
        Texture2D texture = Selection.activeObject as Texture2D;
        if (texture == null)
        {
            EditorUtility.DisplayDialog("Select Texture", "You must select a texture first!", "OK");
            return;
        }

        string path = EditorUtility.OpenFilePanel("Overwrite with png", "", "png");
        if (path.Length != 0)
        {
            var fileContent = File.ReadAllBytes(path);
            texture.LoadImage(fileContent);
        }
    }*/
        /* [MenuItem("Examples/Add BoxCollider to Prefab Asset")]
         static void AddBoxColliderToPrefab()
         {
             // Get the Prefab Asset root GameObject and its asset path.
             GameObject assetRoot = Selection.activeObject as GameObject;
             string assetPath = AssetDatabase.GetAssetPath(assetRoot);

             // Load the contents of the Prefab Asset.
             GameObject contentsRoot = PrefabUtility.LoadPrefabContents(assetPath);

             // Modify Prefab contents.
             contentsRoot.AddComponent<BoxCollider>();

             // Save contents back to Prefab Asset and unload contents.
             PrefabUtility.SaveAsPrefabAsset(contentsRoot, assetPath);
             PrefabUtility.UnloadPrefabContents(contentsRoot);
         }
         [MenuItem("Examples/Add BoxCollider to Prefab Asset", true)]
         static bool ValidateAddBoxColliderToPrefab()
         {
             GameObject go = Selection.activeObject as GameObject;
             if (go == null)
                 return false;

             return PrefabUtility.IsPartOfPrefabAsset(go);
         }*/
        GameObject myPrefab;
    void OnGUI()
    {
        /*  EditorGUILayout.LabelField("This is an example of EditorWindow.ShowPopup", EditorStyles.wordWrappedLabel);
         GUILayout.Space(70);
         if (GUILayout.Button("Agree!")) this.Close();
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
         myString = EditorGUILayout.TextField("Text Field", myString);

         groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
         myBool = EditorGUILayout.Toggle("Toggle", myBool);
         myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
         EditorGUILayout.EndToggleGroup();

         if (GUI.Button(new Rect(10, 100, 50, 50), btnTexture))
         {
             Process.Start("O:/XRCube/Jordan_Yang/TrueOpenVR-Settings-master/Build/Project1.exe");
             UnityEngine.Debug.Log("Clicked the button with an image");
         }
         if (GUI.Button(new Rect(100, 100, 50, 50), "123"))
         {
             GameObject preGO;
              GameObject parentGO = Selection.activeObject as GameObject;
             preGO = Instantiate((GameObject)Resources.Load("XRCube/Plane"), Vector3.zero, Quaternion.identity);
             preGO.gameObject.name = "Plane";
             preGO.transform.parent = parentGO.transform;
         }
    */

    }



}