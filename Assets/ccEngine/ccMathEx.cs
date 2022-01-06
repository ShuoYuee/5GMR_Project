using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ccMathEx
{
    /// <summary>
    /// 将System.DateTime转换成长整型时间格式
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static long DateTime2time_t(System.DateTime dateTime)
    {
        long time_t;
        System.DateTime dt1 = new System.DateTime(1970, 1, 1, 0, 0, 0);
        System.TimeSpan ts = dateTime - dt1;
        time_t = ts.Ticks / 10000000 - 28800;
        return time_t;
    }


    /// <summary>
    /// 将长整型时间转换成System.DateTime格式
    /// </summary>
    /// <param name="iTime">长整型时间</param>
    /// <returns></returns>
    public static System.DateTime time_t2DateTime(long iTime)
    {
        System.DateTime dt = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Unspecified).AddSeconds(iTime);
        dt = dt.AddHours(8);
        return dt;
    }


    /// <summary>
    /// 检测是否是同一天,跨天重置（延时15秒）
    /// </summary>
    /// <param name="time1">时间1</param>
    /// <param name="time2">时间2</param>
    /// <returns></returns>
    public static bool f_CheckSameDay(long time1, long time2)
    {
        //time1 -= 15;
        //time2 -= 15;
        DateTime dataTime1 = time_t2DateTime(time1);
        DateTime dataTime2 = time_t2DateTime(time2);
        if (dataTime1.Year == dataTime2.Year &&
            dataTime1.Month == dataTime2.Month &&
            dataTime1.Day == dataTime2.Day)
        {
            //Debug.LogError("跨天重置");
            return true;
        }
        return false;
    }

    /// <summary>
    /// 检测是否是同一天,跨天重置（延时15秒）
    /// </summary>
    /// <param name="time1">时间1</param>
    /// <returns></returns>
    public static bool f_CheckSameDayForNow(long time1)
    {
        DateTime dataTime1 = time_t2DateTime(time1);
        DateTime dataTime2 = DateTime.Now;
        if (dataTime1.Year == dataTime2.Year &&
            dataTime1.Month == dataTime2.Month &&
            dataTime1.Day == dataTime2.Day)
        {
            //Debug.LogError("跨天重置");
            return true;
        }
        return false;
    }

    /// <summary>
    /// 保持指定数量的子对象
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="iNewItemNum"></param>
    public static void f_CreateChild(GameObject obj, int iNewChhildNum)
    {
        int iNum = obj.transform.childCount - iNewChhildNum;
        if (iNum > 0)
        {
            for (int i = 0; i < iNum; i++)
            {
                GameObject.Destroy(obj.transform.GetChild(0).gameObject);
            }
        }
        else if (iNum < 0)
        {
            for (int i = iNum; i < 0; i++)
            {
                GameObject tItem = GameObject.Instantiate(obj.transform.GetChild(0).gameObject);
                //tItem.transform.parent = obj.transform;
                tItem.transform.SetParent(obj.transform);
            }
        }

    }

}
