using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 地圖檔案按鈕
/// </summary>
public class LoadMapBtn : MonoBehaviour
{
    [Tooltip("按鈕文字(文字內容將改為存檔檔名")]
    public Text _text;
    [Tooltip("按鈕套件")]
    public Button _Button;

    // Start is called before the first frame update
    void Start()
    {
        //_Button.onClick.AddListener(delegate () { GameMain.GetInstance().m_MapPool.f_LoadMap(_text.text); });
        _Button.onClick.AddListener(delegate () { f_OnClickLoadMapBtn(); });
    }

    /// <summary>按鈕事件</summary>
    private void f_OnClickLoadMapBtn() //當按下按鈕回傳資料給 MapFileManager 的 f_OnClickFile
    {
        glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(MessageDef.UI_LoadBtn, _text.text);
    }
}
