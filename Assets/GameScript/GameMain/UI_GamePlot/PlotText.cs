using ccU3DEngine;
using UnityEngine;
using UnityEngine.UI;

public class PlotText
{
    Text _GameText;
    GameObject _Btn_NextArrow;

    private int _iTimeId = -99;
    private int _iTextIndex = 0;
    private string _strDispText = "";

    private string _strText = "";
    private int _iTextSpeed = 0;


    public PlotText(GameObject Btn_NextArrow, Text GameText)
    {
        _Btn_NextArrow = Btn_NextArrow;
        _GameText = GameText;

        _Btn_NextArrow.SetActive(false);
        _GameText.text = "";
    }

    public void f_Play(GamePlotDT tGamePlotDT)
    {
        if (_iTimeId > 0)
        {
            ccTimeEvent.GetInstance().f_UnRegEvent(_iTimeId);
        }

        //1.剧本文字 （参数1剧情文字，参数2显示速度(0-10)，参数3无效，参数4无效）
        _strText = tGamePlotDT.szData1;
        _iTextSpeed = ccMath.atoi(tGamePlotDT.szData2);
        if (_iTextSpeed > 10)
        {
            _iTextSpeed = 10;
        }
        _iTextIndex = 0;
        _strDispText = "";
        
        if (_iTextSpeed <= 0)
        {
            _strDispText = _strText;
            _iTextIndex = _strText.Length;
        }
        else
        {
            _iTimeId = ccTimeEvent.GetInstance().f_RegEvent(1 / _iTextSpeed, true, null, Callback_OnTime);
            Callback_OnTime(null);
        }
        _GameText.gameObject.SetActive(true);


    }

    public void f_FastClick()
    {
        //if (_iTextIndex >= _GamePlotDT.szText.Length)
        //{
        //    if (_GamePlotDT.iNextPlotId > 0)
        //    {
        //        Play(_GamePlotDT.iNextPlotId);
        //    }
        //    else
        //    {
        //        DoExit();
        //    }
        //}
        //else
        //{
        //    FastText();
        //}
    }

    void FastText()
    {
        if (_iTextIndex < _strText.Length)
        {
            ccTimeEvent.GetInstance().f_UnRegEvent(_iTimeId);
            _strDispText = _strText;
            _iTextIndex = _strText.Length;
        }

    }

    void Callback_OnTime(object Obj)
    {
        if (_iTextIndex >= _strText.Length)
        {
            //DoEndSleep();
            _Btn_NextArrow.SetActive(true);
            ccTimeEvent.GetInstance().f_UnRegEvent(_iTimeId);
            //return;
        }
        else
        {
            _iTextIndex++;
            _strDispText = _strText.Substring(0, _iTextIndex);
        }
        _GameText.text = _strDispText;
    }

}
