﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SwitchMonitor : MonoBehaviour
{

    public string math;

    // Use this for initialization
    void Start()
    {
        ReadTxtFirst();
        //強制解析度



        Screen.SetResolution(1920, 1080, true);
        //硬體螢幕編號切換
        //if (math == "0")
        //{
        //    PlayerPrefs.SetInt("UnitySelectMonitor", 0); // Select monitor 1
        //    Display.displays[0].Activate();
        //    Debug.Log("切換至 第1個螢幕");
        //}
        //else if (math == "1")
        //{

        //    PlayerPrefs.SetInt("UnitySelectMonitor", 1); // Select monitor 2
        //    Display.displays[1].Activate();
        //    Debug.Log("切換至 第2個螢幕");
        //}
        //else if (math == "2")
        //{

        //    PlayerPrefs.SetInt("UnitySelectMonitor", 2); // Select monitor 3
        //    Display.displays[2].Activate();
        //    Debug.Log("切換至 第3個螢幕");
        //}
        //else if (math == "3")
        //{
        //    PlayerPrefs.SetInt("UnitySelectMonitor", 3); // Select monitor 4
        //    Display.displays[3].Activate();
        //    Debug.Log("切換至 第4個螢幕");
        //}
        //else
        //{
        //    Debug.Log("編號錯誤 或 超出第四台螢幕");
        //}

    }


    public void AddTxtTextByFileStream(string txtText)
    {
        string path = Application.streamingAssetsPath + "/Config/Config.txt";
        // 檔流創建一個文字檔
        FileStream file = new FileStream(path, FileMode.Create);
        //得到字串的UTF8 資料流程
        byte[] bts = System.Text.Encoding.UTF8.GetBytes(txtText);
        // 文件寫入資料流程
        file.Write(bts, 0, bts.Length);
        if (file != null)
        {
            //清空緩存
            file.Flush();
            // 關閉流
            file.Close();
            //銷毀資源
            file.Dispose();
        }
    }

    void ReadTxtFirst()
    {
        string path = Application.streamingAssetsPath + "/Config/Config.txt";
        //FileStream fsSource = new FileStream(path, FileMode.Open, FileAccess.Read);

        //文件讀寫流
        StreamReader strr = new StreamReader(path);
        //讀取內容
        string str = strr.ReadToEnd();

        math = str;
    }

}

