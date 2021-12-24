using ccU3DEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlotRole : MonoBehaviour
{
    GamePlotDT _GamePlotDT = null;
    private bool _bMoveComplete = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
   
           
    public void f_DoCMD(GamePlotDT tGamePlotDT)
    {
//21.显示图片 （参数1资源名Resources\GamePlot，参数2无效，参数3无效，参数4无效)





        _GamePlotDT = tGamePlotDT;

        if (_GamePlotDT.iStartAction == 21)
        {
            OnMoveComplete();
        }
        else if (_GamePlotDT.iStartAction == 22)
        {
            ImagePositionMV();
        }
        else if (_GamePlotDT.iStartAction == 23)
        {
            ImageLay();
        }
        else if (_GamePlotDT.iStartAction == 24)
        {
            ImageFace();
        }
        else if (_GamePlotDT.iStartAction == 25)
        {
            ImageSize();
        }


        //SetData2(ccMath.f_String2ArrayFloat(_GamePlotDT.szData2, ":"));
        //SetData3(ccMath.f_String2ArrayFloat(_GamePlotDT.szData3, ":"));
        //SetData4(ccMath.f_String2ArrayFloat(_GamePlotDT.szData4, ":"));

        //transform.localPosition = new Vector3(_GamePlotDT.iBirthX, _GamePlotDT.iBirthY, 0);
        //if (_GamePlotDT.iFaceWay == 0)
        //{
        //    transform.localRotation = new Quaternion(0, 0, 0, 0);
        //}
        //else
        //{
        //    transform.localRotation = new Quaternion(0, 180, 0, 0);
        //}
        _bMoveComplete = false;       
    }


    /// <summary>
    /// //25.图片缩放动画 （参数1资源名Resources\GamePlot，参数2开始比例(空使用当前大小)，参数3 缩放大小比例(0 - 1)，参数4时间内完成）
    /// </summary>
    void ImageSize()
    {
        if (_GamePlotDT.szData2.Length > 0)
        {
            float fSize = ccMath.atof(_GamePlotDT.szData2);
            transform.localScale = new Vector3(fSize, fSize, fSize);
        }
        if (_GamePlotDT.szData3.Length > 0)
        {
            float fSize = ccMath.atof(_GamePlotDT.szData3);

            //键值对儿的形式保存iTween所用到的参数
            Hashtable args = new Hashtable();

            //放大的倍数
            args.Add("scale", new Vector3(fSize, fSize, fSize));
           
            args.Add("easeType", iTween.EaseType.linear);
            args.Add("time", ccMath.atof(_GamePlotDT.szData4));
            args.Add("islocal", true);
            args.Add("oncomplete", "OnMoveComplete");

            iTween.ScaleTo(gameObject, args);
        }
        else
        {
            OnMoveComplete();
        }
    }

    /// <summary>
    /// //24.设置图片朝向 （参数1资源名Resources\GamePlot，参数2 朝向(0向右 1向左)，参数2无效，参数3无效，参数4无效）
    /// </summary>
    void ImageFace()
    {
        if (_GamePlotDT.szData2.Length > 0)
        {
            int iFace = ccMath.f_SetMaxMin(ccMath.atoi(_GamePlotDT.szData2), 1, 0);
            if (iFace == 1)
            {
                transform.localRotation = new Quaternion(0, 0, 0, 0);
            }
            else
            {
                transform.localRotation = new Quaternion(0, 180, 0, 0);
            }
        }

        OnMoveComplete();
    }

    /// <summary>
    /// //23.设置图片显示层次 （参数1资源名Resources\GamePlot，参数2 显示层次(数越大层次越高(0 - 10))，参数2无效，参数3无效，参数4无效）
    /// </summary>
    void ImageLay()
    {
        int iLay = ccMath.f_SetMaxMin(ccMath.atoi(_GamePlotDT.szData2), 1, 0);
        //transform.SetAsLastSibling();
        // transform.SetAsFirstSibling();
        transform.SetSiblingIndex(iLay);

        OnMoveComplete();
    }

    /// <summary>
    /// //22.图片位置动画 （参数1资源名Resources\GamePlot，参数2Sx: Sy 开始移动SxSy(空使用当前位置)，参数3Ex: Ey 移动终点Ex:Ey，参数4时间内完成移动)
    /// </summary>
    void ImagePositionMV()
    {
        float[] aData = null;
        if (_GamePlotDT.szData2.Length > 0)
        {
            aData = ccMath.f_String2ArrayFloat(_GamePlotDT.szData2, ":");
            if (aData.Length == 2)
            {
                //参数2Sx: Sy 开始移动SxSy(空使用当前位置)
                transform.localPosition = new Vector3(aData[0], aData[1], 0);
            }
            else if (_GamePlotDT.szData2.Length > 0)
            {
                MessageBox.ASSERT("22.图片位置动画 参数2错误:" + _GamePlotDT.szData2);
            }
        }

        if (_GamePlotDT.szData3.Length > 0)
        {
            aData = ccMath.f_String2ArrayFloat(_GamePlotDT.szData3, ":");
            if (aData.Length == 2)
            {
                //transform.localPosition = new Vector3(aData[0], aData[1], 0);
                Hashtable args = new Hashtable();
                args.Add("position", new Vector3(aData[0], aData[1], 0));
                args.Add("easeType", iTween.EaseType.linear);
                args.Add("time", ccMath.atof(_GamePlotDT.szData4));
                args.Add("islocal", true);
                args.Add("oncomplete", "OnMoveComplete");
                iTween.MoveTo(gameObject, args);
            }
            else if (_GamePlotDT.szData3.Length > 0)
            {
                MessageBox.ASSERT("22.图片位置动画 参数3错误:" + _GamePlotDT.szData3);
            }
        }
        else
        {
            OnMoveComplete();
        }
    }
    
    void OnMoveComplete()
    {
        _bMoveComplete = true;
    }

    bool IsComplete()
    {
        return _bMoveComplete;
    }


    public void f_Destory()
    {
        Destroy(gameObject);
    }

}
