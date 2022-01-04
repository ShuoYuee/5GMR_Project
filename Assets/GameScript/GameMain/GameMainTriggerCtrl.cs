using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Epibyte.ConceptVR;

public class GameMainTriggerCtrl : MonoBehaviour
{
    public float _fLookTimeLimt = 2f;

    private EditObjControll _EditObjControll = null;
    private Interactable _Interactable = null;

    private GameObject oCurObj = null;
    private EM_TriggerObj _ObjEm = EM_TriggerObj.None;
    private float _fLookTime = 0f;
    private bool _bLookTime = false;

    public enum EM_TriggerObj
    {
        None = 0,
        EditObj = 1,
        Button = 2,
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        f_RayTrigger();

        
    }

    private void f_RayTrigger()
    {
        Ray tRay = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(tRay,out hit, Mathf.Infinity))
        {
            Debug.DrawLine(tRay.origin, hit.point, Color.red);
            if (!_bLookTime && hit.collider.GetComponent<EditObjControll>() != null)
            {
                _ObjEm = EM_TriggerObj.EditObj;
                oCurObj = hit.collider.gameObject;
                _EditObjControll = oCurObj.GetComponent<EditObjControll>();
                _bLookTime = true;
            }

            else if (hit.collider.GetComponent<Interactable>() != null)
            {
                _ObjEm = EM_TriggerObj.Button;
                oCurObj = hit.collider.gameObject;
                _Interactable = oCurObj.GetComponent<Interactable>();
                _Interactable.OnHovered();
                _bLookTime = true;
            }

            else if(_bLookTime)
            {
                if (oCurObj.GetComponent<EditObjControll>() == null && oCurObj.GetComponent<Interactable>() == null)
                {
                    _ObjEm = EM_TriggerObj.None;
                    oCurObj = null;

                    if (_Interactable != null)
                    {
                        _Interactable.OnLeave();
                        _Interactable = null;
                    }

                    _fLookTime = 0;
                    _bLookTime = false;
                }
            }         
        }

        if (_bLookTime)
        {
                _fLookTime += Time.deltaTime;
            if (_fLookTime >= _fLookTimeLimt)
            {
                _fLookTime = 0;
                _bLookTime = false;
            }

            switch (_ObjEm)
            {
                case EM_TriggerObj.EditObj:
                    
                    break;
                case EM_TriggerObj.Button:
                    _Interactable.OnClicked();
                    break;
            }
        }
    }
}
