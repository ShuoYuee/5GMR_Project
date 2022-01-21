using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ccU3DEngine;
using Epibyte.ConceptVR;

public class MapFileManager : MonoBehaviour
{
    //public ListPositionCtrl _ListPositionCtrl;
    public Pagination _Pagination;
    public GameObject _oFileBtn;

    private void Start()
    {
        glo_Main.GetInstance().m_UIMessagePool.f_AddListener(MessageDef.UI_MapObjInit, f_SetLoadMapBtn);
    }

    public void f_Reset()
    {
        /*if (_ListPositionCtrl != null)
        {
            ListPositionCtrlTools.f_Create(_ListPositionCtrl, GameMain.GetInstance().m_MapPool.f_LoadPreviewData());
            _ListPositionCtrl.Initialize();

            ListBoxBase Item = null;
            for(int i = 0; i < _ListPositionCtrl.listBoxes.Length; i++)
            {
                Item = _ListPositionCtrl.listBoxes[i];
                Item.transform.localPosition = new Vector3(0, transform.position.y, 0.1f);
                Item.transform.localEulerAngles = Vector3.zero;
            }
        }*/
        if (_Pagination == null) { return; }
        List<GameObject> oMapObj = new List<GameObject>();
        string[] aData = GameMain.GetInstance().m_MapPool.f_LoadPreviewData();
        for(int i = 0; i < aData.Length; i++)
        {
            oMapObj.Add(_oFileBtn);
        }
        _Pagination.items = oMapObj;
        _Pagination.f_Reset();
    }

    public void f_LoadMap(Text text)
    {
        GameMain.GetInstance().m_MapPool.f_LoadMap(text.text);
    }

    private void f_SetLoadMapBtn(object e)
    {
        List<GameObject> oData = (List<GameObject>)e;
        string[] aData = GameMain.GetInstance().m_MapPool.f_LoadPreviewData();
        for (int i = 0; i < oData.Count; i++)
        {
            if(oData[i].GetComponent<LoadMapBtn>() == null) { return; }
            oData[i].GetComponentInChildren<Text>().text = aData[i];
            oData[i].name = aData[i];
            oData[i].transform.localScale = _oFileBtn.transform.localScale;
        }
    }

    public void f_SaveMap(Text text)
    {
        GameMain.GetInstance().m_MapPool.f_SaveMap(text.text);
    }
}
