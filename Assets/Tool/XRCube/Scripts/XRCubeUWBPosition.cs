using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NativeWebSocket;
using System.IO;

public class XRCubeUWBPosition : MonoBehaviour
{
    WebSocket websocket;
    public String ServerIP= "192.168.0.9";
    public String ServerPort= "8000";
    public String Msg = "";
    public int TagID=0;
    public float timestamp;
    public float roll;
    public float pitch;
    public float yaw;
    public float posX;
    public float posY;
    public float posZ;
    public float quatW;
    public float quatX;
    public float quatY;
    public float quatZ;
    public float smooth;
    public bool showLog=true;
    float time = 0;
    bool isFirst = true;
    int isFirstcount = 0;
    float _smooth;
    static UWB_json_get myObject3 = new UWB_json_get();
    public class UWB_json_get
    {
        public float timestamp;
       // public UWB_json_rot xyz2enu;
        public List<UWB_json_pos> tags;
    }
    [Serializable]
    public class UWB_json_rot
    {


    }
    [Serializable]
    public class UWB_json_pos
    {
        public float id;
        public float posX;
        public float posY;
        public float posZ;
        public float roll;
        public float pitch;
        public float yaw;
        public float quatW;
        public float quatX;
        public float quatY;
        public float quatZ;

    }
    // Start is called before the first frame update
    async void Start()
    {
        isFirst = true;
        isFirstcount = 0;
        //   websocket = new WebSocket("ws://"+ServerIP+":"+ServerPort);
        websocket = new WebSocket("ws://"+this.GetComponent<Dataflow>().Dataflowpath);
        //websocket = new WebSocket("ws://192.168.45.89:8000/TagInfo");
        websocket.OnOpen += () =>
        {
            Debug.Log("UWB Connection open!");
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("UWB Connection Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("UWB Connection closed!");
            Start();
        };

        websocket.OnMessage += (bytes) =>
        {
           // Debug.Log("OnMessage!");
           // Debug.Log(bytes);
            string result = System.Text.Encoding.UTF8.GetString(bytes);
            if(showLog )
            {
              
                Debug.Log(result);
            }
            if (isFirst)
            {
                _smooth = smooth;
                smooth = 0;
                isFirst = false;
            }
            else
            {
                isFirstcount++;
                if (isFirstcount >= 2)
                    smooth = _smooth;
            }
            myObject3 = JsonUtility.FromJson<UWB_json_get>(result);
            timestamp= myObject3.timestamp;
            if (myObject3.tags[TagID].quatW != 0)
                quatW = lowPass(myObject3.tags[TagID].quatW, quatW);
            if (myObject3.tags[TagID].quatX != 0)
                quatX = lowPass(myObject3.tags[TagID].quatX, quatX);
            if (myObject3.tags[TagID].quatY != 0)
                quatY = lowPass(myObject3.tags[TagID].quatY, quatY);
            if (myObject3.tags[TagID].quatZ != 0)
                quatZ = lowPass(myObject3.tags[TagID].quatZ, quatZ);
            //roll = lowPass(myObject3.xyz2enu.roll,roll);
            //pitch = lowPass(myObject3.xyz2enu.pitch, pitch);
            //yaw  = lowPass(myObject3.xyz2enu.yaw, yaw);
            if (myObject3.tags[TagID].posX != 0)
                posX = lowPass(myObject3.tags[TagID].posX, posX);
            if (myObject3.tags[TagID].posY != 0)
                posY = lowPass(myObject3.tags[TagID].posY, posY);
            if (myObject3.tags[TagID].posZ != 0)
                posZ = lowPass(myObject3.tags[TagID].posZ, posZ);


            // getting the message as a string
            // var message = System.Text.Encoding.UTF8.GetString(bytes);
            // Debug.Log("OnMessage! " + message);
        };

        // Keep sending messages at every 0.3s
        InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);

        // waiting for messages
        await websocket.Connect();
        
    }

    void Update()
    {

       
#if !UNITY_WEBGL || UNITY_EDITOR
            websocket.DispatchMessageQueue();
#endif

       
    }

    async void SendWebSocketMessage()
    {
        if (websocket.State == WebSocketState.Open)
        {
            // Sending bytes
            await websocket.Send(new byte[] { 10, 20, 30 });

            // Sending plain text
            await websocket.SendText("plain text message");
        }
    }
    float lowPass(float inx, float outx)
    {
        if (smooth >= 1)
            smooth = 0.999f;
        else if (smooth < 0)
            smooth = 0;
        outx = outx + (1-smooth) * (inx - outx);
        return outx;
    }
    private async void OnApplicationQuit()
    {
              await websocket.Close();
    }

}
