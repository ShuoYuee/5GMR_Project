using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 地圖檔案按鈕
/// </summary>
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
