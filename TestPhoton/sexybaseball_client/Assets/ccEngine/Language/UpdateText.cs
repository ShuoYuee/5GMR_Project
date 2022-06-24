using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ccU3DEngine;


public class UpdateText : MonoBehaviour
{
    private Text _Text = null;
    public string m_strLanuageKey = "";

    void Start()
    {
        _Text = gameObject.GetComponent<Text>();
        if (_Text == null)
        {           
            MessageBox.ASSERT("UpdateText设置的对象未找到Text组件." + gameObject.name);
        }
        else
        {
            f_Update();
        }
    }

    /// <summary>
    /// 更新Text文字显示
    /// </summary>
    public void f_Update()
    {
        if (_Text != null)
        {
            LanguageManager.GetInstance().f_SetText(this, _Text, m_strLanuageKey);
        }
    }

    private void OnDestroy()
    {
        LanguageManager.GetInstance().f_UnRegUpdateText(this);
    }

}
