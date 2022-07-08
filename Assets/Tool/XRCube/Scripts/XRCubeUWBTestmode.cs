using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class XRCubeUWBTestmode : MonoBehaviour
{
    // Start is called before the first frame update
    public bool showLog = false;
    public TextAsset JsonTest;
    float time = 0;
    StringReader reader;
    public int TagID = 0;
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
    public float smooth=0;
    public float scale=1f;
    public float interval = 20;
    bool isFirst=true;
    int isFirstcount = 0;
    float _smooth;
    private Vector3 _pos;
    private Vector3 _rot;
    private Quaternion _Qua;
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
    void Start()
    {
        isFirst = true;
        isFirstcount = 0;
        JsonTest = Resources.Load("jsontest") as TextAsset;
        reader = new StringReader(JsonTest.text);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time> interval/1000f)
        {
            string line = reader.ReadLine();
            if(!string.IsNullOrEmpty(line))
             {
                if (showLog)
                {
                    Debug.Log(line);
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
                  if (isFirstcount>=2)
                    {
                        smooth = _smooth;
                        _pos.x = posX;
                        _pos.y = posZ;
                        _pos.z = posY;
                        // _rot.x = this.GetComponent<XRCubeUWBPosition>().roll;
                        //  _rot.y = this.GetComponent<XRCubeUWBPosition>().pitch;
                        // _rot.z = this.GetComponent<XRCubeUWBPosition>().yaw;
                        _Qua.w = quatW;
                        _Qua.x = quatX;
                        _Qua.y = quatY;
                        _Qua.z = quatZ;
                        this.transform.localPosition = _pos;
                        //  this.transform.localRotation = Quaternion.Euler(_rot);
                        this.transform.localRotation = _Qua;
                       
                    }
                    
                }
                myObject3 = JsonUtility.FromJson<UWB_json_get>(line);
                timestamp = myObject3.timestamp;
                if(myObject3.tags[TagID].quatW!=0)
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
                    posX = lowPass(myObject3.tags[TagID].posX * scale, posX) ;        
                if (myObject3.tags[TagID].posY != 0)
                    posY = lowPass(myObject3.tags[TagID].posY * scale, posY) ;
                if (myObject3.tags[TagID].posZ != 0)
                    posZ = lowPass(myObject3.tags[TagID].posZ * scale, posZ) ;
            }
            else
            {
                reader = new StringReader(JsonTest.text);
                if (showLog)
                {
                    Debug.Log("UWB Test Mode Loop Flag");
                }
                Debug.Log("UWB Test Mode Loop Flag");
            }
            //  reader.Close();
            time = 0;
        }

        float lowPass(float inx, float outx)
        {
            if (smooth >= 1)
                smooth = 0.999f;
            else if (smooth < 0)
                smooth = 0;
            outx = outx + (1 - smooth) * (inx - outx);
            return outx;
        }
    }
}
