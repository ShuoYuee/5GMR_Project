using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMapBtn : MonoBehaviour
{
    public Text _text;
    public Button _Button;

    // Start is called before the first frame update
    void Start()
    {
        _Button.onClick.AddListener(delegate () { GameMain.GetInstance().m_MapPool.f_LoadMap(_text.text); });
    }
}
