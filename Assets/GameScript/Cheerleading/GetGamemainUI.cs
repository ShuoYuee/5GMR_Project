using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGamemainUI : MonoBehaviour
{
    public Transform parent;

    private void Awake()
    {
        parent = GameObject.Find("CheerleadMRUI").transform;

        this.transform.SetParent(parent, true);
        this.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
    }
}
