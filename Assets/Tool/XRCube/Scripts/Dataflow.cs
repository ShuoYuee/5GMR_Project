using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
using RenderHeads.Media.AVProVideo;

public class Dataflow :MonoBehaviour
{
	public string Dataflowpath;
	public int DataflowNum;
	public int DataflowProtocol;
	public string dataflowserverip;
	// Start is called before the first frame update
	void Start()
    {
		//UnityEngine.Debug.Log("http://" + dataflowserverip + "/dataflow/"+ DataflowProtocol +"/" + DataflowNum);
		StartCoroutine(GetRequest("http://" + dataflowserverip + "/dataflow/"+ DataflowProtocol +"/"  + DataflowNum));
	}

	// Update is called once per frame
	float time = 0;
	int a = 1;
	int b = 0;
    void Update()
    {
		/*time += Time.deltaTime;
		if(time>1 & a==0)
        {
			time = 0;
			StartCoroutine(GetRequest("http://" + dataflowserverip + "/dataflow/" + DataflowProtocol + "/" + DataflowNum));
		}*/

	}
	static DataFlow_json_get myObject3 = new DataFlow_json_get();
	public class DataFlow_json_get
	{
		public string source_url;
		public bool status;
	}
	public void SendGet(string uri)
	{
		StartCoroutine(GetRequest(uri));
	}
	public IEnumerator GetRequest(string uri)
	{
		using (UnityWebRequest webRequest = UnityWebRequest.Get(uri + "?id=" + Convert.ToString(SystemInfo.deviceUniqueIdentifier)))
		{
			// Request and wait for the desired page.
			yield return webRequest.SendWebRequest();

			string[] pages = uri.Split('/');
			int page = pages.Length - 1;
			if(this.GetComponent<MeshRenderer>())
			this.GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)Resources.Load("Dataflowlost1");
			if (webRequest.isNetworkError)
			{
                UnityEngine.Debug.LogError(pages[page] + ": Error: " + webRequest.error + "_" + uri);

			}
			else
			{
				if (webRequest.isDone)
				{
					string result = webRequest.downloadHandler.text;
                //    UnityEngine.Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);

					myObject3 = JsonUtility.FromJson<DataFlow_json_get>(result);
					if (myObject3.status)
					{
						if (this.GetComponent<MeshRenderer>())
							this.GetComponent<MeshRenderer>().material.mainTexture = null;
						Dataflowpath = myObject3.source_url;
							if (this.GetComponent<MediaPlayer>())
								this.GetComponent<MediaPlayer>().OpenMedia(MediaPathType.AbsolutePathOrURL, Dataflowpath, true);
							if (this.GetComponent<XRCubeUWBPosition>())
								this.GetComponent<XRCubeUWBPosition>().enabled = true;
					//	b++;
					//	UnityEngine.Debug.Log("TimeStamp_"+ System.DateTime.Now + "_Count_" +b+"_DataflowPath_"+ Dataflowpath);
					//	a = 0;
					}
					else
					{
						Dataflowpath = "Null";
						if (this.GetComponent<XRCubeUWBPosition>())
							UnityEngine.Debug.Log("UWB_noData");
						if (this.GetComponent<MediaPlayer>())
							UnityEngine.Debug.Log("MediaPlayer_noData");
					}
				}
			}
		}
	}
}
