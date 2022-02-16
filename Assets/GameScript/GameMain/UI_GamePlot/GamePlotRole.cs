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
        //21.顯示圖片 （參數1資源名Resources\GamePlot，參數2無效，參數3無效，參數4無效)





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
    /// //25.圖片縮放動畫 （參數1資源名Resources\GamePlot，參數2開始比例(空使用當前大小)，參數3 縮放大小比例(0 - 1)，參數4時間內完成）
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

            //鍵值對兒的形式保存iTween所用到的參數
            Hashtable args = new Hashtable();

            //放大的倍數
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
    /// //24.設置圖片朝向 （參數1資源名Resources\GamePlot，參數2 朝向(0向右 1向左)，參數2無效，參數3無效，參數4無效）
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
    /// //23.設置圖片顯示層次 （參數1資源名Resources\GamePlot，參數2 顯示層次(數越大層次越高(0 - 10))，參數2無效，參數3無效，參數4無效）
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
    /// //22.圖片位置動畫 （參數1資源名Resources\GamePlot，參數2Sx: Sy 開始移動SxSy(空使用當前位置)，參數3Ex: Ey 移動終點Ex:Ey，參數4時間內完成移動)
    /// </summary>
    void ImagePositionMV()
    {
        float[] aData = null;
        if (_GamePlotDT.szData2.Length > 0)
        {
            aData = ccMath.f_String2ArrayFloat(_GamePlotDT.szData2, ":");
            if (aData.Length == 2)
            {
                //參數2Sx: Sy 開始移動SxSy(空使用當前位置)
                transform.localPosition = new Vector3(aData[0], aData[1], 0);
            }
            else if (_GamePlotDT.szData2.Length > 0)
            {
                MessageBox.ASSERT("22.圖片位置動畫 參數2錯誤:" + _GamePlotDT.szData2);
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
                MessageBox.ASSERT("22.圖片位置動畫 參數3錯誤:" + _GamePlotDT.szData3);
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

