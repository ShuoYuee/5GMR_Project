using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ccU3DEngine;
using Epibyte.ConceptVR;

public class XRCubeCtrl : MonoBehaviour
{
    #region ���s�ƥ�
    public delegate void OnClickCtrl(int iInput);
    public static event OnClickCtrl OnClickCtrlEvent;
    public static event OnClickCtrl OnClickBtnOne;
    public static event OnClickCtrl OnClickBtnTwo;
    public static event OnClickCtrl OnClickBtnThree;
    #endregion

    // Start is called before the first frame update
    GameObject GOnow;
    private GameMainTriggerCtrl _GameMainTriggerCtrl;
    public GameObject DM;
    public GameObject Ani;
    public bool showLaser = false;
    int a = 0;
    void Start()
    {
        _GameMainTriggerCtrl = Camera.main.GetComponent<GameMainTriggerCtrl>();
    }
    float time=0;
    // Update is called once per frame
    void Update()
    {
        if (showLaser)
        { 
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity))
            {
                if (hit.transform.tag == "XRCubeCollider")
                {
                    /*if (GOnow != null)
                    {
                        GOnow.transform.GetComponent<MeshRenderer>().enabled = false;
                    }
                    hit.transform.GetComponent<MeshRenderer>().enabled = true;
                    GOnow = hit.transform.gameObject;*/

                    GameObject oCurCollider = hit.collider.gameObject;
                    //�N�n����������@�@��J
                    if (oCurCollider.TryGetComponent(out EditObjControll tEditObjControll))//�s�誫��
                    {
                        _GameMainTriggerCtrl._ObjEm = GameMainTriggerCtrl.EM_TriggerObj.EditObj;
                    }
                    else if (oCurCollider.TryGetComponent(out Interactable interactable))//���s����
                    {
                        _GameMainTriggerCtrl._ObjEm = GameMainTriggerCtrl.EM_TriggerObj.Button;
                    }
                    else if (oCurCollider.TryGetComponent(out InputField inputField))//��J��r�ت���
                    {
                        _GameMainTriggerCtrl._ObjEm = GameMainTriggerCtrl.EM_TriggerObj.InpuUI;
                    }
                    else if (oCurCollider.TryGetComponent(out Button button))//UI���s
                    {
                        _GameMainTriggerCtrl._ObjEm = GameMainTriggerCtrl.EM_TriggerObj.ButtonUI;
                    }
                    else if (oCurCollider.TryGetComponent(out ccUI_U3DSpace.ccInteractable ccInteractable))//MR UI
                    {
                        _GameMainTriggerCtrl._ObjEm = GameMainTriggerCtrl.EM_TriggerObj.MRUI;
                    }

                    _GameMainTriggerCtrl.f_SetCtrl(oCurCollider);
                    _GameMainTriggerCtrl.f_SetFocus(hit.point);
                }
                _GameMainTriggerCtrl.f_SetFocus(hit.point);
            }
            else
            {
                if (GOnow != null)
                {
                    GOnow.transform.GetComponent<MeshRenderer>().enabled = false;
                }
                _GameMainTriggerCtrl.f_LeaveRay();
            }
        }
        else
        {
            if (GOnow != null)
            {
                GOnow.transform.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    public void click()
    {
        if (GOnow != null)
        {
            if (GOnow.transform.name == "XRCube2")
            {
                if(a==0)
                {
                    DM.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load("CompalMR") as Texture2D;
                    a++;
                }
                else
                {
                    DM.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load("CompalVR") as Texture2D;
                    a = 0;
                }

            }
            if (GOnow.transform.name == "XRCube1")
            {
               Ani.GetComponent<Animator>().enabled =!Ani.GetComponent<Animator>().enabled;
            }
        }


    }
   
    public void click(int iClick, int iSet)
    {
        if (!GOnow) { return; }

        if (iClick == 0)
        {//����Ĳ�o
            OnClickCtrlEvent(iSet);
        }
        else if (iClick == 1)
        {//X�b����
            OnClickBtnOne(iSet);
        }
        else if (iClick == 2)
        {//Y�b����
            OnClickBtnTwo(iSet);
        }
        else if (iClick == 3)
        {//Z�b����
            OnClickBtnThree(iSet);
        }
        else if (iClick == 4)
        {//���ܽs��Ҧ�(���ʡB����B�Y��)
            GameMain.GetInstance().m_EditManager.f_SetAddEditPoint(iSet);
        }
        else if (iClick == 5)
        {//���ܪ���y�мҦ�(�@�ɮy�СB���a�y��)
            //GameMain.GetInstance().m_EditManager.f_SetEditPoint(iSet);
            GameMain.GetInstance().m_EditManager.f_SetAddEditPoint(iSet);
        }
    }
}
