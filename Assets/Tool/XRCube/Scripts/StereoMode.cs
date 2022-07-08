using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using RenderHeads.Media.AVProVideo;

public class StereoMode : MonoBehaviour {
    public enum StereoModeEnum { None, Mono, StereoOverUnder, StereoSideBySide };

    public StereoModeEnum stereoModeType;

    public Camera rightCamera;
    public Camera leftCamera;

    public Material replacementMainMaterial;
    public Material replacementLeftMaterial;
    public Material replacementRightMaterial;
    public Material replacementOverMaterial;
    public Material replacementUnderMaterial;

    public static StereoModeEnum stereoModeTypeFromOtherScene = StereoModeEnum.None;

    private Renderer mainRenderer;
    private RawImage mainRawImage;

    // callback to be called before any camera starts rendering
    public void MyPreRender(Camera cam)
    {        
        if (mainRenderer != null)
        {
            if (stereoModeType == StereoModeEnum.Mono)
            {
                ChangeMaterial(replacementMainMaterial);
            }
            else
            {
                if (cam == rightCamera)
                {
                    if (stereoModeType == StereoModeEnum.StereoOverUnder)
                    {
                        ChangeMaterial(replacementOverMaterial);
                    }
                    else
                    {
                        ChangeMaterial(replacementRightMaterial);
                    }
                }
                else if (cam == leftCamera)
                {
                    if (stereoModeType == StereoModeEnum.StereoOverUnder)
                    {
                        ChangeMaterial(replacementUnderMaterial);
                    }
                    else
                    {
                        ChangeMaterial(replacementLeftMaterial);
                    }
                }
                else
                {
                    // main camera
                    if (stereoModeType == StereoModeEnum.StereoOverUnder)
                    {
                        ChangeMaterial(replacementOverMaterial);
                    }
                    else
                    {
                        ChangeMaterial(replacementRightMaterial);
                    }
                }
            }
        }
        else if (mainRawImage != null)
        {
            if (stereoModeType == StereoModeEnum.Mono)
            {
                mainRawImage.uvRect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
            }
            else
            {
                if (cam == rightCamera)
                {
                    if (stereoModeType == StereoModeEnum.StereoOverUnder)
                    {
                        mainRawImage.uvRect = new Rect(0.0f, 0.5f, 1.0f, 0.5f);
                    }
                    else
                    {
                        mainRawImage.uvRect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
                    }
                }
                else if (cam == leftCamera)
                {
                    if (stereoModeType == StereoModeEnum.StereoOverUnder)
                    {
                        mainRawImage.uvRect = new Rect(0.0f, 0.0f, 1.0f, 0.5f);
                    }
                    else
                    {
                        mainRawImage.uvRect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
                    }
                }
                else
                {
                    // main camera
                    if (stereoModeType == StereoModeEnum.StereoOverUnder)
                    {
                        mainRawImage.uvRect = new Rect(0.0f, 0.5f, 1.0f, 0.5f);
                    }
                    else
                    {
                        mainRawImage.uvRect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
                    }
                }
            }
        }
    }

    private void ChangeMaterial(Material newMaterial)
    {
        this.GetComponent<ApplyToMesh>().enabled = false;
        this.GetComponent<MeshRenderer>().sharedMaterial = newMaterial;
        this.GetComponent<ApplyToMesh>().enabled = true;
    }

    public void ChangeContrast(float f)
    {
        replacementMainMaterial.SetFloat("_Contrast", f);
        replacementLeftMaterial.SetFloat("_Contrast", f);
        replacementOverMaterial.SetFloat("_Contrast", f);
        replacementRightMaterial.SetFloat("_Contrast", f);
        replacementUnderMaterial.SetFloat("_Contrast", f);
    }
    public void ChangeBrightness(float f)
    {
        replacementMainMaterial.SetFloat("_Brightness", f);
        replacementLeftMaterial.SetFloat("_Brightness", f);
        replacementOverMaterial.SetFloat("_Brightness", f);
        replacementRightMaterial.SetFloat("_Brightness", f);
        replacementUnderMaterial.SetFloat("_Brightness", f);
    }
    public void ChangeSaturation(float f)
    {
        replacementMainMaterial.SetFloat("_Saturation", f);
        replacementLeftMaterial.SetFloat("_Saturation", f);
        replacementOverMaterial.SetFloat("_Saturation", f);
        replacementRightMaterial.SetFloat("_Saturation", f);
        replacementUnderMaterial.SetFloat("_Saturation", f);
    }
    public void ChangeReverse(float f)
    {
        replacementMainMaterial.SetFloat("_reverse", f);
    }
    public void ChangeStereoModeType(int f)
    {
        switch (f)
        {
            case 0:
                stereoModeType = StereoModeEnum.Mono;
                break;
            case 1:
                stereoModeType = StereoModeEnum.Mono;
                break;
            case 2:
                stereoModeType = StereoModeEnum.StereoOverUnder;
                break;
            case 3:
                stereoModeType = StereoModeEnum.StereoSideBySide;
                break;
            default:
                stereoModeType = StereoModeEnum.Mono;
                break;
        }
    }

    public void OnEnable()
    {
        if (stereoModeTypeFromOtherScene != StereoModeEnum.None)
            stereoModeType = stereoModeTypeFromOtherScene;

        stereoModeTypeFromOtherScene = StereoModeEnum.None;

        if (stereoModeType == StereoModeEnum.None)
            stereoModeType = StereoModeEnum.Mono;

        mainRenderer = GetComponent<Renderer>();
        mainRawImage = GetComponent<RawImage>();
        Debug.Log("MainRenderer : " + mainRenderer);
        Debug.Log("mainRawImage : " + mainRawImage);

        // register the callback when enabling object
        Camera.onPreRender += MyPreRender;
    }
    public void OnDisable()
    {
        // remove the callback when disabling object
        Camera.onPreRender -= MyPreRender;
    }
}