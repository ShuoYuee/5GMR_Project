using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ccU3DEngine;
using Epibyte.ConceptVR;

public class XRCubeCtrl : MonoBehaviour
{
    #region 按鈕事件
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
                    //將要偵測的元件一一填入
                    if (oCurCollider.TryGetComponent(out EditObjControll tEditObjControll))//編輯物件
                    {
                        _GameMainTriggerCtrl._ObjEm = GameMainTriggerCtrl.EM_TriggerObj.EditObj;
                    }
                    else if (oCurCollider.TryGetComponent(out Interactable interactable))//按鈕物件
                    {
                        _GameMainTriggerCtrl._ObjEm = GameMainTriggerCtrl.EM_TriggerObj.Button;
                    }
                    else if (oCurCollider.TryGetComponent(out InputField inputField))//輸入文字框物件
                    {
                        _GameMainTriggerCtrl._ObjEm = GameMainTriggerCtrl.EM_TriggerObj.InpuUI;
                    }
                    else if (oCurCollider.TryGetComponent(out Button button))//UI按鈕
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
        {//互動觸發
            OnClickCtrlEvent(iSet);
        }
        else if (iClick == 1)
        {//X軸移動
            OnClickBtnOne(iSet);
        }
        else if (iClick == 2)
        {//Y軸移動
            OnClickBtnTwo(iSet);
        }
        else if (iClick == 3)
        {//Z軸移動
            OnClickBtnThree(iSet);
        }
        else if (iClick == 4)
        {//改變編輯模式(移動、旋轉、縮放)
            GameMain.GetInstance().m_EditManager.f_SetAddEditPoint(iSet);
        }
        else if (iClick == 5)
        {//改變物件座標模式(世界座標、本地座標)
            //GameMain.GetInstance().m_EditManager.f_SetEditPoint(iSet);
            GameMain.GetInstance().m_EditManager.f_SetAddEditPoint(iSet);
        }
    }
}
