using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRCubeWebcam : MonoBehaviour
{
    public wCam WebCam = wCam.WeCam;
    public String requestedDeviceName = "";
     UnityEngine.Object myPrefab;
    [Serializable]
    public enum wCam
    {
        WeCam = 0,
        DroidCam = 1,
        GZX5 = 2,
        VCam = 3,
        iVCam = 4
    };
    // Start is called before the first frame update
    void Start()
    {
        if (WebCam == wCam.WeCam)
        {
            requestedDeviceName = "WeCam";

        }
        else if (WebCam == wCam.DroidCam)
        {
            requestedDeviceName = "DroidCam Source 3";
        }
        else if (WebCam == wCam.GZX5)
        {
            requestedDeviceName = "";
        }
        else if (WebCam == wCam.VCam)
        {
            requestedDeviceName = "e2eSoft VCam";
        }
        else if (WebCam == wCam.iVCam)
        {
            requestedDeviceName = "e2eSoft iVCam";
        }
        WebCamTexture webcamTexture = new WebCamTexture();
        WebCamDevice webCamDevice;
        for (int cameraIndex = 0; cameraIndex < WebCamTexture.devices.Length; cameraIndex++)
        {
            if (WebCamTexture.devices[cameraIndex].name == requestedDeviceName)
            {
                webCamDevice = WebCamTexture.devices[cameraIndex];
                webcamTexture = new WebCamTexture(webCamDevice.name);
                print(webcamTexture.requestedHeight + "_" + webcamTexture.requestedWidth);
            }
        }
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
