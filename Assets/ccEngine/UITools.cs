using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITools
{

    public static Vector3 f_ChangeUIPos2ScenePos(GameObject oUIObj, Camera UICamera, Camera WorldCamera)
    {
        RectTransform tRectTransform = (RectTransform)oUIObj.transform;

        Vector3[] fourCorners = new Vector3[4];
        tRectTransform.GetWorldCorners(fourCorners);

        Vector2 v2WinPosS = UICamera.WorldToScreenPoint(fourCorners[0]);

        float ScreenScale_Width = WorldCamera.pixelWidth / tRectTransform.rect.width;
        float ScreenScale_Height = WorldCamera.pixelHeight / tRectTransform.rect.height;
        float fMapX = (oUIObj.transform.position.x - v2WinPosS.x) * ScreenScale_Width;
        float fMapY = (oUIObj.transform.position.y - v2WinPosS.y) * ScreenScale_Height;

        Vector3 tempVector = new Vector3(fMapX, fMapY, 0);
        tempVector = WorldCamera.ScreenToWorldPoint(tempVector);

        return tempVector;
    }



    /// <summary>
    /// 計算content相對父級的bounds
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public static Bounds calculateRelativeBounds(RectTransform content)
    {
        return calculateRelativeBounds(content.parent, content);
    }


    /// <summary>
    /// 計算相對於relativeTo的bounds
    /// </summary>
    /// <param name="relativeTo"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public static Bounds calculateRelativeBounds(Transform relativeTo, RectTransform content)
    {
        Bounds bounds = calculateWorldBounds(content);

        if (relativeTo != null)
        {
            Vector3 temp;

            temp = relativeTo.InverseTransformPoint(bounds.size);
            temp.z = 0;
            bounds.size = temp;
            temp = relativeTo.InverseTransformPoint(bounds.center);
            temp.z = 0;
            bounds.center = temp;

            bounds.extents = bounds.size / 2f;
            bounds.min = bounds.center - bounds.extents;
            bounds.max = bounds.center + bounds.extents;

        }

        return bounds;
    }

    /// <summary>
    /// 計算content相對世界座標的bounds
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public static Bounds calculateWorldBounds(RectTransform content)
    {
        Bounds bounds = new Bounds();
        Vector2 min = new Vector2(float.MaxValue, float.MaxValue);
        Vector2 max = new Vector2(float.MinValue, float.MinValue);


        getRect(content, ref min, ref max);

        bounds.size = max - min;
        bounds.center = (max + min) / 2f;
        bounds.extents = bounds.size / 2f;
        bounds.min = bounds.center - bounds.extents;
        bounds.max = bounds.center + bounds.extents;

        return bounds;
    }

    /// <summary>
    /// 計算content的最大邊框
    /// </summary>
    /// <param name="content"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    public static void getRect(RectTransform content, ref Vector2 min, ref Vector2 max)
    {
        Vector3[] corners = new Vector3[4];
        content.GetWorldCorners(corners);
        min.x = Mathf.Min(new float[] { min.x, corners[0].x, corners[2].x });
        min.y = Mathf.Min(new float[] { min.y, corners[0].y, corners[2].y });
        max.x = Mathf.Max(new float[] { max.x, corners[0].x, corners[2].x });
        max.y = Mathf.Max(new float[] { max.y, corners[0].y, corners[2].y });

        for (int i = 0, imax = content.childCount; i < imax; i++)
        {
            getRect(content.GetChild(i) as RectTransform, ref min, ref max);
        }

    }


}
