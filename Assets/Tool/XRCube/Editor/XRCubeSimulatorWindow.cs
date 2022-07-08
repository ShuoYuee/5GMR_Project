using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;


public class XRCubeSimulatorWindow : EditorWindow
{
    private GUISkin CompalGUIskin;
    public Texture btnTexture;


    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;
    string steampath = "C:/Program Files (x86)/Steam";
    string simulatorpath = "C:/Program Files (x86)/XRCubeSimulator";
    private Vector2 scrollViewVector = Vector2.zero;
    private float KeyDelay = 0;
    private string[] HMDselect = { "Default", "GZX5", "GXV3" };
    private string[] Inputselect = { "Disable(no input)", "Emulation(zero input)", "Keyboard" };
    int n, n1, i, HMDnum, InputNum;
    Texture Texture;
    Texture TextureHMD;
    Texture TextureInput;
    string HMD_Resolution = "";
    static JSONObject json;



    int indexNumber;
    bool show = false;
    // Start is called before the first frame update

    public void Awake()
    {
        CompalGUIskin = Resources.Load<GUISkin>("XRCubeGUIskin");
        PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Standalone, ApiCompatibilityLevel.NET_4_6);
        UnityEngine.Debug.Log("Welcom XRCube Simulator.");
        n = 0; n1 = 0; i = 0; HMDnum = 0; InputNum = 0;
        Texture = Resources.Load<Texture>("CloudGame_Logo-02");
        TextureHMD = Resources.Load<Texture>("HMD_Default");
        TextureInput = Resources.Load<Texture>("Disable");
        Json();
        Apply();
    }

    public void OnDestroy()
    {
        UnityEngine.Debug.Log("XRCube Simulator Exit.");
    }
    // Update is called once per frame
    void Update()
    {
        KeyDelay += Time.deltaTime / 100f;
    }
    //  Process.Start("O:/Compal/Jordan_Yang/TrueOpenVR-Settings-master/Build/Project1.exe");
    void Json()
    {
        json = new JSONObject(File.ReadAllText(Application.dataPath + "/XRCube/Editor/Config.json"));
        steampath = json.GetField("Path").GetField("steam").str;
        simulatorpath = json.GetField("Path").GetField("simulator").str;
        HMDnum = (int)json.GetField("Simulator").GetField("HMD").n;
        InputNum = (int)json.GetField("Simulator").GetField("Input").n;
        groupEnabled = json.GetField("Enable").b;
    }
    private static void CreateRegist(string name, string i)
    {
        string[] aimnames;
        RegistryKey hkml = Registry.CurrentUser;
        RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
        RegistryKey aimdir = software.CreateSubKey("TrueOpenVR", true);
        aimdir.SetValue(name, i);
    }
    private static void SetRegist(string name, string i)
    {
        string[] aimnames;
        RegistryKey hkml = Registry.CurrentUser;
        RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
        RegistryKey aimdir = software.OpenSubKey("TrueOpenVR", true);
        aimdir.SetValue(name, i);
    }

    void Apply()
    {
        TOVRreg();
        if (groupEnabled)
        {
            switch (HMDnum)
            {
                case 0:
                    TextureHMD = Resources.Load<Texture>("HMD_Default");
                    HMD_Resolution = Screen.currentResolution.width + "x" + Screen.currentResolution.height;
                    File.Copy(steampath + "/steamapps/common/SteamVR/drivers/tovr/resources/settings/default_old.vrsettings", steampath + "/steamapps/common/SteamVR/drivers/tovr/resources/settings/default.vrsettings", true);
                    break;
                case 1:
                    TextureHMD = Resources.Load<Texture>("HMD_GZX5");
                    HMD_Resolution = "2560x720";
                    File.Copy(steampath + "/steamapps/common/SteamVR/drivers/tovr/resources/settings/GZX5.vrsettings", steampath + "/steamapps/common/SteamVR/drivers/tovr/resources/settings/default.vrsettings", true);
                    break;
                case 2:
                    TextureHMD = Resources.Load<Texture>("HMD_GXV3");
                    HMD_Resolution = "3200x1600";
                    File.Copy(steampath + "/steamapps/common/SteamVR/drivers/tovr/resources/settings/GXV3.vrsettings", steampath + "/steamapps/common/SteamVR/drivers/tovr/resources/settings/default.vrsettings", true);
                    break;
                default:
                    TextureHMD = Resources.Load<Texture>("HMD_Default");
                    HMD_Resolution = Screen.currentResolution.width + "x" + Screen.currentResolution.height;
                    File.Copy(steampath + "/steamapps/common/SteamVR/drivers/tovr/resources/settings/default_old.vrsettings", steampath + "/steamapps/common/SteamVR/drivers/tovr/resources/settings/default.vrsettings", true);
                    break;
            }
            switch (InputNum)
            {
                case 0:
                    TextureInput = Resources.Load<Texture>("Disable");
                    SetRegist("Driver", "Disable.dll");
                    SetRegist("Driver64", "Disable64.dll");
                    break;
                case 1:
                    TextureInput = Resources.Load<Texture>("Emulation");
                    SetRegist("Driver", "Emulation.dll");
                    SetRegist("Driver64", "Emulation64.dll");
                    break;
                case 2:
                    TextureInput = Resources.Load<Texture>("Keyboard");
                    SetRegist("Driver", "Keyboard.dll");
                    SetRegist("Driver64", "Keyboard64.dll");
                    break;
                default:
                    TextureInput = Resources.Load<Texture>("Disable");
                    SetRegist("Driver", "Disable.dll");
                    SetRegist("Driver64", "Disable64.dll");
                    break;
            }
        }
        else
        {
            SetRegist("Driver", "Disable.dll");
            SetRegist("Driver64", "Disable64.dll");
        }
        //  UnityEngine.Debug.Log(json.GetField("Path").GetField("simulator").str);
        json.GetField("Simulator").SetField("HMD", HMDnum);
        json.GetField("Simulator").SetField("Input", InputNum);
        json.SetField("Enable", groupEnabled);
        File.WriteAllText(Application.dataPath + "/XRCube/Editor/Config.json", json.ToString());
        UnityEngine.Debug.Log("XRCube Simulator - Settings applied.");
    }
    void TOVRreg()
    {
        RegistryKey hkml = Registry.CurrentUser;
        RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
        RegistryKey aimdir = software.OpenSubKey("TrueOpenVR", true);
        if (aimdir != null)
        {
            software.DeleteSubKey("TrueOpenVR", true);
        }
        CreateRegist("DistortionProfile", "Sample.ini");
        CreateRegist("Driver", "Disable.dll");
        CreateRegist("Driver64", "Disable64.dll");
        CreateRegist("Drivers", simulatorpath + "/Drivers/");
        CreateRegist("HMDProfiles", simulatorpath + "/HMDProfiles/");
        CreateRegist("Library", simulatorpath + "/TOVR.dll");
        CreateRegist("Library64", simulatorpath + "/TOVR64.dll");
    }

    void OnGUI()
    {
        GUI.skin = CompalGUIskin;
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        if (GUI.Button(new Rect(160, 460f, 120, 25), "reset"))
        {
            groupEnabled = false;
            n = 0;
            n1 = 0;
            HMDnum = 0;
            InputNum = 0;
            Apply();
        }
        groupEnabled = EditorGUILayout.BeginToggleGroup(groupEnabled ? "Simulator Enable" : "Simulator Disable", groupEnabled);
        if (!groupEnabled)
        {
            n = 0;
            n1 = 0;
        }
        GUILayout.Space(3);
        GUILayout.Label("      HMD type", EditorStyles.label);
        if (GUI.Button(new Rect(100, 42f, 20, 20), ">"))
        {
            if (n == 0) n = 1;
            else n = 0;
        }

        if (n == 1)
        {

            scrollViewVector = GUI.BeginScrollView(new Rect(125, 42, 100, 75), scrollViewVector, new Rect(0, 0, 100, 75));
            GUI.Box(new Rect(0, 0, 100, 75), "");
            for (i = 0; i < 3; i++)
            {
                if (GUI.Button(new Rect(0, i * 25, 100, 25), ""))
                {
                    n = 0; HMDnum = i;
                }
                switch (HMDnum)
                {
                    case 0:
                        TextureHMD = Resources.Load<Texture>("HMD_Default");
                        HMD_Resolution = Screen.currentResolution.width + "x" + Screen.currentResolution.height;
                        break;
                    case 1:
                        TextureHMD = Resources.Load<Texture>("HMD_GZX5");
                        HMD_Resolution = "2560x720";
                        break;
                    case 2:
                        TextureHMD = Resources.Load<Texture>("HMD_GXV3");
                        HMD_Resolution = "3200x1600";
                        break;
                    default:
                        TextureHMD = Resources.Load<Texture>("HMD_Default");
                        HMD_Resolution = Screen.currentResolution.width + "x" + Screen.currentResolution.height;
                        break;
                }
                GUI.Label(new Rect(5, i * 25, 100, 25), HMDselect[i]);
            }
            GUI.EndScrollView();
        }
        else
        {
            GUI.Label(new Rect(130, 40, 100, 25), HMDselect[HMDnum], EditorStyles.boldLabel);
        }
        GUILayout.Label("\n      Input type", EditorStyles.label);
        if (GUI.Button(new Rect(100, 70f, 20, 20), ">"))
        {
            if (n1 == 0) n1 = 1;
            else n1 = 0;
        }

        if (n1 == 1)
        {
            scrollViewVector = GUI.BeginScrollView(new Rect(125, 70, 150, 70), scrollViewVector, new Rect(0, 0, 150, 70));
            GUI.Box(new Rect(0, 0, 150, 70), "");
            for (i = 0; i < 3; i++)
            {
                if (GUI.Button(new Rect(0, i * 25, 150, 25), ""))
                {
                    n1 = 0; InputNum = i;
                }
                switch (InputNum)
                {
                    case 0:
                        TextureInput = Resources.Load<Texture>("Disable");
                        break;
                    case 1:
                        TextureInput = Resources.Load<Texture>("Emulation");
                        break;
                    case 2:
                        TextureInput = Resources.Load<Texture>("Keyboard");
                        break;
                    default:
                        TextureInput = Resources.Load<Texture>("Disable");
                        break;
                }
                GUI.Label(new Rect(5, i * 25, 150, 25), Inputselect[i]);
            }
            GUI.EndScrollView();
        }
        else
        {
            if (n != 1)
            {
                GUI.Label(new Rect(130, 68, 150, 25), Inputselect[InputNum], EditorStyles.boldLabel);
            }
        }
        GUI.Label(new Rect(10, 112f, 80, 25), "HMD Preview", EditorStyles.boldLabel);
        GUI.Label(new Rect(10, 412f, 80, 25), "Input Guide", EditorStyles.boldLabel);
        EditorGUILayout.EndToggleGroup();
        if (groupEnabled)
        {
            GUI.DrawTexture(new Rect(10, 140f, 400, 240), TextureHMD);
            GUI.DrawTexture(new Rect(10, 440f, 138, 90), TextureInput);
            GUI.Label(new Rect(10, 380f, 190, 25), "Resolution : " + HMD_Resolution, EditorStyles.label);
        }
        if (GUI.Button(new Rect(285, 460f, 120, 25), "Apply"))
        {
            Apply();

        }
        if (GUI.Button(new Rect(160, 494f, 245, 25), "Launch SteamVR"))
        {
            Apply();
            Process.Start(steampath + "/steamapps/common/SteamVR/bin/win64/vrstartup.exe");
        }
        GUILayout.Space(400);
        GUILayout.Label("\n\n\n\n\nAdvenced Settings", EditorStyles.boldLabel);
        GUILayout.Space(5);
        GUILayout.Label("        Steam Path:", EditorStyles.boldLabel);
        GUILayout.Label("          " + steampath, EditorStyles.textArea);
        if (GUI.Button(new Rect(10, 584f, 15, 15), ""))
        {
            string path = EditorUtility.OpenFolderPanel("Select Steam Folder", steampath, "");
            if (path.Length != 0)
            {
                steampath = path;
            }
            //  UnityEngine.Debug.Log(json.GetField("Path").GetField("simulator").str);
            json.GetField("Path").SetField("steam", steampath);
            File.WriteAllText(Application.dataPath + "/XRCube/Editor/Config.json", json.ToString());
        }
        GUILayout.Space(10);

        GUILayout.Label("        XRCube Simulator Path:", EditorStyles.boldLabel);
        GUILayout.Label("          " + simulatorpath, EditorStyles.textArea);
        if (GUI.Button(new Rect(10, 630f, 15, 15), ""))
        {
            string path = EditorUtility.OpenFolderPanel("Select XRCube Simulator Folder", simulatorpath, "");
            if (path.Length != 0)
            {
                simulatorpath = path;
            }
            //  UnityEngine.Debug.Log(json.GetField("Path").GetField("simulator").str);
            json.GetField("Path").SetField("simulator", simulatorpath);
            File.WriteAllText(Application.dataPath + "/XRCube/Editor/Config.json", json.ToString());
        }
        //test button
        /*  if (GUI.Button(new Rect(10, 243f, 15, 15), ""))
          {
              TOVRreg();

          }*/
        GUILayout.Label("\n  XRCube XR Simulator \n            Settings", EditorStyles.boldLabel);


        if (GUI.Button(new Rect(150, 680f, 40, 40), Texture) && KeyDelay>1)
        {
            KeyDelay = 0;
            Process.Start(simulatorpath + "/XRCubeSimulatorSettings.exe");

        }





    }
}
