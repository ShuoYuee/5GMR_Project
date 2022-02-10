using ccU3DEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoolDT: BasePoolDT<long>
{

    public GameObject m_GameObject;

    public CharacterDT m_CharacterDT;

    public float m_CreatePosX; //要產生的位置X 
    public float m_CreatePosY; //要產生的位置Y
    public float m_CreatePosZ; //要產生的位置Z

    public float m_CreateRotX; //要產生的朝向X
    public float m_CreateRotY; //要產生的朝向Y
    public float m_CreateRotZ; //要產生的朝向Z
    public float m_CreateRotW; //要產生的朝向W


    public float m_CreateScaleX; //要產生的朝向X
    public float m_CreateScaleY; //要產生的朝向Y
    public float m_CreateScaleZ; //要產生的朝向Z


    public void f_Set(long iRoleId, GameObject tGameObject, CharacterDT tCharacterDT)
    {
        m_GameObject = tGameObject;
        iId = iRoleId;
        m_CharacterDT = tCharacterDT;

        //替物件裝上碰撞
        if (!tGameObject.TryGetComponent(out Collider tCollider))
        {
            CapsuleCollider collider = tGameObject.AddComponent<CapsuleCollider>();
            collider.radius = tCharacterDT.fBodySize;
            collider.height = tCharacterDT.fHeight;
        }

        //替物件裝上動畫機
        if (!tGameObject.TryGetComponent(out Animator oAnimator))
        {
            Animator tAnimator = tGameObject.AddComponent<Animator>();
            string[] strAnimator = ccMath.f_String2ArrayString(tCharacterDT.szAI, ";");
            if (strAnimator.Length == 2)
            {
                tAnimator.runtimeAnimatorController = glo_Main.GetInstance().m_ResourceManager.f_CreateABAnimator(strAnimator[0], strAnimator[1]);
            }
            else
            {
                MessageBox.DEBUG("動畫機載入出錯：" + tCharacterDT.iId + ", 若沒有設相關動畫機則忽略此警告");
            }
        }
    }

    public void f_UpdateInfor()
    {
        if (m_GameObject == null)
        {
            MessageBox.ASSERT("f_UpdateInfor Fail, m_GameObject == null");
        }
        m_CreatePosX = m_GameObject.transform.position.x; //位置x
        m_CreatePosY = m_GameObject.transform.position.y;
        m_CreatePosZ = m_GameObject.transform.position.z;

        m_CreateRotX = m_GameObject.transform.rotation.x;
        m_CreateRotY = m_GameObject.transform.rotation.y;
        m_CreateRotZ = m_GameObject.transform.rotation.z;
        m_CreateRotW = m_GameObject.transform.rotation.w;

        m_CreateScaleX = m_GameObject.transform.localScale.x;
        m_CreateScaleY = m_GameObject.transform.localScale.y;
        m_CreateScaleZ = m_GameObject.transform.localScale.z;
    }

    public string f_GetInfor()
    {
        return string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11}", iId, m_CharacterDT.iId,
                m_CreatePosX, m_CreatePosY, m_CreatePosZ,
                m_CreateRotX, m_CreateRotY, m_CreateRotZ, m_CreateRotW,
                m_CreateScaleX, m_CreateScaleY, m_CreateScaleZ

            );
    }

    public void f_Destory()
    {
        GameObject.Destroy(m_GameObject);
    }

}
