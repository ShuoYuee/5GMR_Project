using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using RenderHeads.Media.AVProVideo;
using System;
using System.Net;
using System.Text;

public class XRCubeMediaPlayerWindow : EditorWindow
{
    private GUISkin CompalGUIskin;
    private string dataflowserverip="192.168.0.101:5001";
    static JSONObject json;
    bool is2D, is3D;
    bool isLocal, isStreamingAssets, isStreaming;
    bool Picgroup, Videogroup, Stereogroup, Distortiongroup;
    private Vector2 scrollViewVector = Vector2.zero;
    public Camera Rcam, Lcam;
    int n, n1, n2, i, PicNum, VideoNum, StereoNum, DataflowNum;
    private string[] PicSelect = { "None", "Default", "Unity Resources", "External Resources" };
    private string[] VideoSelect = { "None", "Default", "Streaming Path", "Unity StreamingAssets", "External Resources", "XRCube Dataflow" };
    private string[] StereoSelect = { "None", "Mono", "Over Under", "Side By Side" };
    private float KeyDelay = 1;
   string Picpath, PicpathL;
    string Videopath, VideopathL;
    string Dataflowpath;
    string VideopathInput;
    string DistortionK1_Input, DistortionK2_Input, DistortionK3_Input;
    string ResolutionH_Input, ResolutionW_Input;
    bool[] compaldataflowbool = new bool[10];
    bool updatedataflow = false;
    public void Awake()
    {
        json = new JSONObject(File.ReadAllText(Application.dataPath + "/XRCube/Editor/Config.json"));
        dataflowserverip = json.GetField("Path").GetField("dataflowserver").str;
        VideopathInput = "https://s3.eu-west-3.amazonaws.com/content.nexplayersdk.com/360test/NYCTimewarp/HLS-360/master.m3u8";
        Videogroup = true; Picgroup = false; Stereogroup = false; Distortiongroup = false;
        is2D = true; is3D = false;
        ResolutionW_Input = "1920"; ResolutionH_Input = "1080";
        DistortionK1_Input = "0"; DistortionK2_Input = "0"; DistortionK3_Input = "0";
        n = 0; n1 = 0; n2 = 0; i = 0; PicNum = 0; VideoNum = 0; StereoNum = 0;
        resetDataflow();
        PicpathL = Application.dataPath + "/Resources/";
        VideopathL = Application.dataPath + "/StreamingAssets/";
        UnityEngine.Debug.Log("Welcom XRCube Media Player.");
        CompalGUIskin = Resources.Load<GUISkin>("XRCubeGUIskin");
    }
    public void OnDestroy()
    {
        UnityEngine.Debug.Log("XRCube Media Player Exit.");
    }
    // Update is called once per frame
    void resetDataflow()
    {
        for (int j = 0; j < compaldataflowbool.Length; j++)
        {
            compaldataflowbool[j] = false;
        }
        Dataflowpath = "Null";
      compaldataflowbool[0] = true;
        DataflowNum = 1;
 
    }
    bool checkdataflowbool(int i)
        {
        for(int j=0;j< compaldataflowbool.Length;j++)
        {
            if(j!=i && compaldataflowbool[j])
            {
                return true;
            }

        }
        return false;
      }
    static DataFlow_json_get myObject3 = new DataFlow_json_get();
    public class DataFlow_json_get
    {
        public string source_url;
        public bool status;
    }

    void OnGUI()
      {
          GUI.skin = CompalGUIskin;
          if (GUI.Button(new Rect(20, 630f, 123, 25), "reset"))
          {
              VideopathInput = "https://s3.eu-west-3.amazonaws.com/content.nexplayersdk.com/360test/NYCTimewarp/HLS-360/master.m3u8";
              Videogroup = true; Picgroup = false; Stereogroup = false; Distortiongroup = false;
              is2D = true; is3D = false;
              ResolutionW_Input = "1920"; ResolutionH_Input = "1080";
              DistortionK1_Input = "0"; DistortionK2_Input = "0"; DistortionK3_Input = "0";
              n = 0; n1 = 0; n2 = 0; i = 0; PicNum = 0; VideoNum = 0; StereoNum = 0;
              Rcam = null; Lcam = null;
            resetDataflow();
          }
          GUILayout.Label("Base Settings", EditorStyles.boldLabel);
          GUILayout.Space(6);
          GUILayout.Label("      Video Type : ", EditorStyles.label);



          is2D = GUI.Toggle(new Rect(180, 25, 50, 25), is3D ? false : is2D, " 2D");
          is3D = GUI.Toggle(new Rect(260, 25, 100, 25), is2D?false: is3D, " 3D(VR)");



          GUILayout.Space(20);

          Picgroup = EditorGUILayout.BeginToggleGroup("Picture", Videogroup ? false : Picgroup);
          GUILayout.Space(40);
          if (GUI.Button(new Rect(175, 88f, 18, 18), ">"))
          {
              if (n == 0) n = 1;
              else n = 0;
          }
          if (!Picgroup)
              n = 0;
          if (n == 1)
          {

              scrollViewVector = GUI.BeginScrollView(new Rect(195, 88, 170, 100), scrollViewVector, new Rect(0, 0, 150,100));
              GUI.Box(new Rect(0, 0, 150, 100), "");
              for (i = 0; i < 4; i++)
              {
                  if (GUI.Button(new Rect(0, i * 25, 150, 25), ""))
                  {
                      n = 0; PicNum = i;
                  }
                  GUI.Label(new Rect(5, i * 25, 150, 25), PicSelect[i]);
              }
              GUI.EndScrollView();
          }
          else
          {
              GUI.Label(new Rect(200, 85, 150, 25), PicSelect[PicNum], EditorStyles.boldLabel);
          }
          if(PicNum>1&&n!=1)
          {
              GUI.Label(new Rect(172, 109, 203, 20),Picpath, EditorStyles.textArea);
              if (GUI.Button(new Rect(380, 110f, 17, 17), ".."))
              {
                  string path = EditorUtility.OpenFilePanel("Select Steam Folder", PicpathL, "BMP,EXR,GIF,HDR,IFF,JPG,PICT,PNG,PSD,TGA,TIFF");
                  if (path.Length != 0)
                  {
                      if (PicNum == 3)
                      {
                          File.Copy(path, Application.dataPath + "/XRCube/Resources/PicTemp/" + Path.GetFileName(path));
                          AssetDatabase.Refresh();
                     }
                      Picpath = Path.GetFileNameWithoutExtension(path);
                  }
              }
          }
          else
          {
              Picpath = "";
          }

          GUILayout.Space(20);
          EditorGUILayout.EndToggleGroup();
          Videogroup = EditorGUILayout.BeginToggleGroup("Video", Picgroup ? false : Videogroup);
          if (!Videogroup)
              n1 = 0;
          if (GUI.Button(new Rect(175, 172f, 18, 18), ">"))
          {
              if (n1 == 0) n1 = 1;
              else n1 = 0;
          }
          if (n1 == 1)
          {

              scrollViewVector = GUI.BeginScrollView(new Rect(195, 172, 170, 120), scrollViewVector, new Rect(0, 0, 150, 150));
              GUI.Box(new Rect(0, 0, 150, 150), "");
              for (i = 0; i < 6; i++)
              {
                  if (GUI.Button(new Rect(0, i * 25, 150, 25), ""))
                  {
                      n1 = 0; VideoNum = i;
                  }
                  GUI.Label(new Rect(5, i * 25, 150, 25), VideoSelect[i]);
              }
              GUI.EndScrollView();
          }
          else
          {
                  GUI.Label(new Rect(200, 170, 150, 25), VideoSelect[VideoNum], EditorStyles.boldLabel);
          }
          if (VideoNum ==2 && n1 != 1)
          {
              VideopathInput= GUI.TextField(new Rect(173, 193, 225, 60), VideopathInput, EditorStyles.textArea);
          }
              if ((VideoNum == 3 | VideoNum == 4) && n1 != 1)
          {
              GUI.Label(new Rect(172, 193, 203, 20), Videopath, EditorStyles.textArea);
              if (GUI.Button(new Rect(380, 195f, 17, 17), ".."))
              {
                  string path = EditorUtility.OpenFilePanel("Select Steam Folder", VideopathL, "asf,avi,dv,m4v,mov,mp4,mpg,mpeg,ogv,vp8,webm,wmv");
                  if (path.Length != 0)
                  {
                      if(VideoNum == 3)
                      {
                          Videopath = Path.GetFileName(path);
                      }
                      else
                      {
                          Videopath = path;
                      }   
                  }
              }
          }
             
         if(VideoNum==5 && n1 != 1)
          {
              compaldataflowbool[0] = GUI.Toggle(new Rect(175, 200, 60, 25), checkdataflowbool(0) ? false : compaldataflowbool[0], " Num 1");
              compaldataflowbool[1] = GUI.Toggle(new Rect(255, 200, 60, 25), checkdataflowbool(1) ? false : compaldataflowbool[1], " Num 2");
              compaldataflowbool[2] = GUI.Toggle(new Rect(335, 200, 60, 25), checkdataflowbool(2) ? false : compaldataflowbool[2], " Num 3");
              compaldataflowbool[3] = GUI.Toggle(new Rect(175, 220, 60, 25), checkdataflowbool(3) ? false : compaldataflowbool[3], " Num 4");
              compaldataflowbool[4] = GUI.Toggle(new Rect(255, 220, 60, 25), checkdataflowbool(4) ? false : compaldataflowbool[4], " Num 5");
              compaldataflowbool[5] = GUI.Toggle(new Rect(335, 220, 60, 25), checkdataflowbool(5) ? false : compaldataflowbool[5], " Num 6");
              compaldataflowbool[6] = GUI.Toggle(new Rect(175, 240, 60, 25), checkdataflowbool(6) ? false : compaldataflowbool[6], " Num 7");
              compaldataflowbool[7] = GUI.Toggle(new Rect(255, 240, 60, 25), checkdataflowbool(7) ? false : compaldataflowbool[7], " Num 8");
            for (int j = 0; j < compaldataflowbool.Length; j++)
            {
             if( compaldataflowbool[j])
                {
                    if(!updatedataflow)
                    {
                        updatedataflow = true;
                        DataflowNum = j + 1;
                        UnityEngine.Debug.Log("http://" + dataflowserverip + "/dataflow/" + DataflowNum);
                        //     UnityEngine.Debug.Log("Dataflowpath_" + Dataflowpath);
                        /*
                             HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://" + dataflowserverip + "/dataflow/" + DataflowNum + "?id=" + Convert.ToString(SystemInfo.deviceUniqueIdentifier));
                             request.Method = "GET";


                             try
                             {
                                 using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                                 {
                                    // UnityEngine.Debug.Log("Publish Response: " + (int)response.StatusCode + ", " + response.StatusDescription);
                                     if ((int)response.StatusCode == 200)
                                     {
                                         using (Stream stream = response.GetResponseStream())
                                         {
                                             StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                                             String responseString = reader.ReadToEnd();


                                         myObject3 = JsonUtility.FromJson<DataFlow_json_get>(responseString);
                                         if (myObject3.status)
                                         {
                                             Dataflowpath = myObject3.source_url;

                                         }
                                         else
                                         {
                                             Dataflowpath = "Null";
                                         }
                                         UnityEngine.Debug.Log("Dataflowpath_" + Dataflowpath);
                                         }
                                     }
                                 }
                             }
                             catch (Exception e)
                             {
                                 UnityEngine.Debug.LogError(e.ToString());
                             }*/
                    }

                }
                    }
                    updatedataflow = false;

        }

          EditorGUILayout.EndToggleGroup();
          //      Screen.height
          GUI.Label(new Rect(22, 295, 150, 15), "Resolution - Width:", EditorStyles.label);
          GUI.Label(new Rect(22, 317, 100, 15), "Height:", EditorStyles.label);
          ResolutionW_Input = GUI.TextField(new Rect(172, 295, 225, 18), ResolutionW_Input, EditorStyles.textArea);
          ResolutionH_Input = GUI.TextField(new Rect(172, 315, 225, 18), ResolutionH_Input, EditorStyles.textArea);
          GUILayout.Space(120);
          GUILayout.Label("\n\n\n\n\nAdvenced Settings", EditorStyles.boldLabel);
          Stereogroup = EditorGUILayout.BeginToggleGroup("Stereo", Stereogroup);
          if (!Stereogroup)
              n2 = 0;
          if(n2==0)
          {
              Rcam = (Camera)EditorGUI.ObjectField(new Rect(22, 430f, 375, 18), "Right Eye (Camera)", Rcam, typeof(Camera));
              Lcam = (Camera)EditorGUI.ObjectField(new Rect(22, 450f, 375, 18), "Left Eye (Camera)", Lcam, typeof(Camera));
          } 
          if (GUI.Button(new Rect(175, 405f, 18, 18), ">"))
          {
              if (n2 == 0) n2 = 1;
              else n2 = 0;
          }
          if (n2 == 1)
          {

              scrollViewVector = GUI.BeginScrollView(new Rect(195, 404, 170, 100), scrollViewVector, new Rect(0, 0, 150, 100));
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

              GUI.Label(new Rect(200, 402, 150, 25), StereoSelect[StereoNum], EditorStyles.boldLabel);
          }

          EditorGUILayout.EndToggleGroup();
          GUILayout.Space(112);
          Distortiongroup = EditorGUILayout.BeginToggleGroup("Distortion", Distortiongroup);
          GUI.Label(new Rect(22, 537, 50, 15), "K1 :", EditorStyles.label);
          GUI.Label(new Rect(22, 557, 50, 15), "K2 :", EditorStyles.label);
          GUI.Label(new Rect(22, 577, 50, 15), "K3 :", EditorStyles.label);
          DistortionK1_Input = GUI.TextField(new Rect(172, 535, 225, 18), DistortionK1_Input, EditorStyles.textArea);
          DistortionK2_Input = GUI.TextField(new Rect(172, 555, 225, 18), DistortionK2_Input, EditorStyles.textArea);
          DistortionK3_Input = GUI.TextField(new Rect(172, 575, 225, 18), DistortionK3_Input, EditorStyles.textArea);

          EditorGUILayout.EndToggleGroup();
          if (GUI.Button(new Rect(150, 630, 245, 25), "Create & Input to the Scene") && KeyDelay>1)
          {
           // UnityEngine.Debug.Log(KeyDelay + "__Key");
             KeyDelay = 0;
              GameObject preGO;
              GameObject parentGO = Selection.activeObject as GameObject;
              if(is2D)
              {
                  if(Picgroup)
                  {
                      preGO = Instantiate((GameObject)Resources.Load("Prefabs/XRCube2DVideoPlayer"));
                      DestroyImmediate(preGO.GetComponent<MediaPlayer>());
                      DestroyImmediate(preGO.GetComponent<ApplyToMesh>());
                      preGO.gameObject.name = "XRCube2DVideoPlayer";
                      preGO.GetComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("XRCube/XRCube_2D"));
                      preGO.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = null;
                      preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK1", 0);
                      preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK2", 0);
                      preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK3", 0);
                      if (PicNum==0)
                      {
                          preGO.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = null;
                      }
                      else if (PicNum == 1)
                      {
                          preGO.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = Resources.Load<Texture>("PicDefault");
                      }
                      else if (PicNum ==2 )
                      {
                          preGO.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = Resources.Load<Texture>(Picpath);
                      }
                      else if (PicNum == 3)
                      {
                          preGO.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = Resources.Load<Texture>("PicTemp/" + Picpath);
                      }
                      preGO.transform.localScale = new Vector3(float.Parse(ResolutionW_Input) / 1000, float.Parse(ResolutionH_Input) / 1000, 0);
                      if (parentGO)
                      {
                          preGO.transform.position = parentGO.transform.position;
                          preGO.transform.parent = parentGO.transform;
                      }
                      if (Stereogroup)
                      {
                          preGO.AddComponent<StereoMode>();
                          preGO.GetComponent<StereoMode>().ChangeStereoModeType(StereoNum);
                          preGO.GetComponent<StereoMode>().rightCamera = Rcam;
                          preGO.GetComponent<StereoMode>().leftCamera = Lcam;
                      }
                      if (Distortiongroup)
                      {
                          preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK1", float.Parse(DistortionK1_Input));
                          preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK2", float.Parse(DistortionK2_Input));
                          preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK3", float.Parse(DistortionK3_Input));
                      }
                  }
                  else if (Videogroup)
                  {
                      preGO = Instantiate((GameObject)Resources.Load("Prefabs/XRCube2DVideoPlayer"));
                      preGO.gameObject.name = "XRCube2DVideoPlayer";
                      preGO.GetComponent<MediaPlayer>().Stop();
                      preGO.GetComponent<MediaPlayer>().Initialise();
                      preGO.GetComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("XRCube/XRCube_2D"));
                      preGO.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = null;
                      preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK1", 0);
                      preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK2", 0);
                      preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK3", 0);
                      if (VideoNum == 0)
                      {
                          preGO.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = null;
                          preGO.GetComponent<MediaPlayer>().OpenMedia(MediaPathType.AbsolutePathOrURL, "", false);
                      }
                      else if (VideoNum == 1)
                      {
                          preGO.GetComponent<MediaPlayer>().OpenMedia(MediaPathType.RelativeToStreamingAssetsFolder, "Cones-2D-1080p60-H264.mp4", true);
                      }
                      else if (VideoNum == 2)
                      {
                          preGO.GetComponent<MediaPlayer>().OpenMedia(MediaPathType.AbsolutePathOrURL, VideopathInput, true);  
                      }
                      else if (VideoNum == 3)
                      {
                          preGO.GetComponent<MediaPlayer>().OpenMedia(MediaPathType.RelativeToStreamingAssetsFolder, Videopath, true);
                      }
                      else if (VideoNum == 4)
                      {
                          preGO.GetComponent<MediaPlayer>().OpenMedia(MediaPathType.RelativeToDataFolder, Videopath, true);
                      }
                      else if (VideoNum == 5)
                      {
                        
                        preGO.AddComponent<Dataflow>();
                        preGO.GetComponent<Dataflow>().DataflowNum = DataflowNum;
                        preGO.GetComponent<Dataflow>().DataflowProtocol = 1;
                        preGO.GetComponent<Dataflow>().dataflowserverip = dataflowserverip;
                      //  preGO.GetComponent<MediaPlayer>().OpenMedia(MediaPathType.AbsolutePathOrURL, Dataflowpath, true);
                        preGO.GetComponent<MediaPlayer>().PlatformOptionsWindows.videoApi = Windows.VideoApi.MediaFoundation;
                        //  DestroyImmediate(preGO.GetComponent<MediaPlayer>());
                        //  DestroyImmediate(preGO.GetComponent<ApplyToMesh>());
                        //   preGO.AddComponent<CompalWebcam>();
                        //   preGO.GetComponent<CompalWebcam>().WebCam = CompalWebcam.wCam.DroidCam;
                    }
                      preGO.transform.localScale = new Vector3(float.Parse(ResolutionW_Input) / 1000, float.Parse(ResolutionH_Input) / 1000, 0);
                      if (parentGO)
                      {
                          preGO.transform.position = parentGO.transform.position;
                          preGO.transform.parent = parentGO.transform;
                      }
                      if (Stereogroup)
                      {
                          preGO.AddComponent<StereoMode>();
                          preGO.GetComponent<StereoMode>().ChangeStereoModeType(StereoNum);
                          preGO.GetComponent<StereoMode>().rightCamera = Rcam;
                          preGO.GetComponent<StereoMode>().leftCamera = Lcam;
                      }
                      if (Distortiongroup)
                      {
                          preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK1", float.Parse(DistortionK1_Input));
                          preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK2", float.Parse(DistortionK2_Input));
                          preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK3", float.Parse(DistortionK3_Input));
                      }

                  }


              }
              else if(is3D)
              {
                  if (Picgroup)
                  {
                      preGO = Instantiate((GameObject)Resources.Load("Prefabs/XRCube3DVideoPlayer"));
                      DestroyImmediate(preGO.GetComponent<MediaPlayer>());
                      DestroyImmediate(preGO.GetComponent<ApplyToMesh>());
                      preGO.gameObject.name = "XRCube3DVideoPlayer";
                      preGO.GetComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("XRCube/XRCube_InsideVisible"));
                      preGO.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = null;
                      preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK1", 0);
                      preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK2", 0);
                      preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK3", 0);
                      if (PicNum == 0)
                      {
                          preGO.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = null;
                      }
                      else if (PicNum == 1)
                      {
                          preGO.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = Resources.Load<Texture>("PicDefault360");
                      }
                      else if (PicNum == 2)
                      {
                          preGO.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = Resources.Load<Texture>(Picpath);
                      }
                      else if (PicNum == 3)
                      {
                          preGO.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = Resources.Load<Texture>("PicTemp/" + Picpath);
                      }
                      if (Stereogroup)
                      {
                          preGO.AddComponent<StereoMode>();
                          preGO.GetComponent<StereoMode>().ChangeStereoModeType(StereoNum);
                          preGO.GetComponent<StereoMode>().rightCamera = Rcam;
                          preGO.GetComponent<StereoMode>().leftCamera = Lcam;
                      }
                      if (Distortiongroup)
                      {
                          preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK1", float.Parse(DistortionK1_Input));
                          preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK2", float.Parse(DistortionK2_Input));
                          preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK3", float.Parse(DistortionK3_Input));
                      }
                  }
                  else if (Videogroup)
                  {
                      preGO = Instantiate((GameObject)Resources.Load("Prefabs/XRCube3DVideoPlayer"));
                      preGO.gameObject.name = "XRCube3DVideoPlayer";
                      preGO.GetComponent<MediaPlayer>().Stop();
                      preGO.GetComponent<MediaPlayer>().Initialise();
                      preGO.GetComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("XRCube/XRCube_InsideVisible"));
                      preGO.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = null;
                      preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK1", 0);
                      preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK2", 0);
                      preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK3", 0);
                      if (VideoNum == 0)
                      {
                          preGO.GetComponent<MediaPlayer>().OpenMedia(MediaPathType.AbsolutePathOrURL, "", false);
                      }
                      else if (VideoNum == 1)
                      {
                          preGO.GetComponent<MediaPlayer>().OpenMedia(MediaPathType.RelativeToStreamingAssetsFolder, "AVProVideoSamples/Cones-360Mono-4K30-H264.mp4", true);
                      }
                      else if (VideoNum == 2)
                      {
                          preGO.GetComponent<MediaPlayer>().OpenMedia(MediaPathType.AbsolutePathOrURL, VideopathInput, true);
                      }
                      else if (VideoNum == 3)
                      {
                          preGO.GetComponent<MediaPlayer>().OpenMedia(MediaPathType.RelativeToStreamingAssetsFolder, Videopath, true);
                      }
                      else if (VideoNum == 4)
                      {
                           preGO.GetComponent<MediaPlayer>().OpenMedia(MediaPathType.RelativeToDataFolder, Videopath, true);
                      }
                      else if (VideoNum == 5)
                      {
                        preGO.AddComponent<Dataflow>();
                        preGO.GetComponent<Dataflow>().DataflowNum = DataflowNum;
                        preGO.GetComponent<Dataflow>().DataflowProtocol = 1;
                        preGO.GetComponent<Dataflow>().dataflowserverip = dataflowserverip;
                        preGO.GetComponent<MediaPlayer>().PlatformOptionsWindows.videoApi = Windows.VideoApi.MediaFoundation;
                      /*  DestroyImmediate(preGO.GetComponent<MediaPlayer>());
                          DestroyImmediate(preGO.GetComponent<ApplyToMesh>());
                          preGO.AddComponent<CompalWebcam>();
                          preGO.GetComponent<CompalWebcam>().WebCam = CompalWebcam.wCam.DroidCam;*/
                      }
                      if (Stereogroup)
                      {
                          preGO.AddComponent<StereoMode>();
                          preGO.GetComponent<StereoMode>().ChangeStereoModeType(StereoNum);
                          preGO.GetComponent<StereoMode>().rightCamera = Rcam;
                          preGO.GetComponent<StereoMode>().leftCamera = Lcam;
                      }
                      if (Distortiongroup)
                      {
                          preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK1", float.Parse(DistortionK1_Input));
                          preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK2", float.Parse(DistortionK2_Input));
                          preGO.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_distortionK3", float.Parse(DistortionK3_Input));
                      }

                  }
              }
              else
              {

              }




          }


          //if (GUILayout.Button("OK!")) this.Close();
      }


    void Update()
    {
        KeyDelay += Time.deltaTime/100f;
       // UnityEngine.Debug.Log(KeyDelay + "__Key");
    }
   
}
