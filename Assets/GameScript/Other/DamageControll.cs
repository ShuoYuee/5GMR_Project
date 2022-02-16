using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageControll : MonoBehaviour
{
    public MeshFilter m_Num1;
    public MeshFilter m_Num2;
    public MeshFilter m_Num3;
    /// <summary>
    /// 千位
    /// </summary>
    public MeshFilter m_Num4;

    public Mesh[] m_aNumMesh = new Mesh[10];

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void f_ShowNum(int iNum)
    {
        m_Num1.gameObject.SetActive(true);
        m_Num2.gameObject.SetActive(true);
        m_Num3.gameObject.SetActive(true);
        if (m_Num4 != null)
        {
            m_Num4.gameObject.SetActive(true);
        }
        m_Num1.mesh = m_aNumMesh[0];
        m_Num2.mesh = m_aNumMesh[0];
        m_Num3.mesh = m_aNumMesh[0];
        if (m_Num4 != null)
        {
            m_Num4.mesh = m_aNumMesh[0];
        }
        if (iNum < 10)
        {
            m_Num1.mesh = m_aNumMesh[iNum];
        }
        else if (10 <= iNum && iNum < 100)
        {
            int iNum1 = iNum % 10;
            int iNum2 = iNum / 10;
            m_Num1.mesh = m_aNumMesh[iNum1];
            m_Num2.mesh = m_aNumMesh[iNum2];
        }
        else if (100 <= iNum && iNum < 1000)
        {
            int iNum1 = iNum % 10;
            int iNum2 = iNum / 10;
            iNum2 = iNum2 % 10;
            int iNum3 = iNum / 100;
            m_Num1.mesh = m_aNumMesh[iNum1];
            m_Num2.mesh = m_aNumMesh[iNum2];
            m_Num3.mesh = m_aNumMesh[iNum3];

        }
        else if (1000 <= iNum && iNum < 10000)
        {
            if (m_Num4 == null)
            {
                MessageBox.ASSERT("顯示第4位元數位為空 " + iNum);
            }
            int iNum1 = iNum % 10;
            iNum = iNum / 10;
            int iNum2 = iNum % 10;
            iNum = iNum / 10;
            int iNum3 = iNum % 10;
            iNum = iNum / 10;
            int iNum4 = iNum % 10;
            m_Num1.mesh = m_aNumMesh[iNum1];
            m_Num2.mesh = m_aNumMesh[iNum2];
            m_Num3.mesh = m_aNumMesh[iNum3];
            m_Num4.mesh = m_aNumMesh[iNum4];

            m_Num1.gameObject.SetActive(true);
            m_Num2.gameObject.SetActive(true);
            m_Num3.gameObject.SetActive(true);
            m_Num4.gameObject.SetActive(true);
        }
        else
        {
            MessageBox.ASSERT("DamageControll > 1000");
        }

    }


}

