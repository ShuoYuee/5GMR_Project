using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleName
{

    private Text _LeftName;
    private Text _RightName;

    public RoleName(Text LeftName, Text RightName)
    {
        _LeftName = LeftName;
        _RightName = RightName;
        f_Reset();
    }

    public void f_Reset()
    {
        _LeftName.gameObject.SetActive(false);
        _RightName.gameObject.SetActive(false);
    }

    public void SetRoleName(GamePlotDT tGamePlotDT)
    {
        //4.设置角色名（参数1角色名文字，参数2显示左右(0向右 1向左)，参数3无效，参数4无效）
        if (tGamePlotDT.szData2.Contains("0") == true)
        {
            if (tGamePlotDT.szData1.Length == 0)
            {
                _LeftName.text = "";
                _LeftName.gameObject.SetActive(false);
            }
            else
            {
                _LeftName.text = tGamePlotDT.szData1;
                _LeftName.gameObject.SetActive(true);
            }
        }
        else
        {
            if (tGamePlotDT.szData1.Length == 0)
            {
                _RightName.text = "";
                _RightName.gameObject.SetActive(false);
            }
            else
            {
                _RightName.text = tGamePlotDT.szData1;
                _RightName.gameObject.SetActive(true);
            }
        }
    }


}