using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistortionTest : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.GetComponent<TextMeshPro>().text = "Distortion K1\n" + this.GetComponent<MeshRenderer>().sharedMaterial.GetFloat("_distortionK1");
    }
}
