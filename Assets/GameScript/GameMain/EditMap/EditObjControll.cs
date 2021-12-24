using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditObjControll : MonoBehaviour
{

    private MapPoolDT _MapPoolDT;

    public void f_Save(MapPoolDT tMapPoolDT)
    {
        _MapPoolDT = tMapPoolDT;
        name = _MapPoolDT.iId + "_" + _MapPoolDT.m_CharacterDT.szResName;
        transform.parent = GameMain.GetInstance().f_GetObjParent();
    }
        
    public long f_GetId()
    {
        return _MapPoolDT.iId;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
