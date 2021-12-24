using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListItem : ListBoxBase
{
    public Text m_Text;
    public Image m_Image;
    public Image m_Highlight;
    public Text m_Num;


    public NBaseSCDT m_SCData;
    public string m_strName;
    public Sprite m_Logo;
    public string m_strNum = "";

    protected override void UpdateDisplayContent(object content)
    {
        //var team = (TeamData)content;

        // image.sprite = level.girl.sprite;
        m_Text.text = m_strName;
        m_Image.sprite = m_Logo;
        m_Num.text = m_strNum;
    }
}