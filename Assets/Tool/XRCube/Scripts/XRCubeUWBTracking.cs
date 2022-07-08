using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRCubeUWBTracking : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private Vector3 _pos;
    private Vector3 _rot;
    private Quaternion _Qua;
    void Update()
    {
        if(this.GetComponent<XRCubeUWBPosition>())
        {
            _pos.x = this.GetComponent<XRCubeUWBPosition>().posX;
            _pos.y = this.GetComponent<XRCubeUWBPosition>().posZ;
            _pos.z = this.GetComponent<XRCubeUWBPosition>().posY;
           // _rot.x = this.GetComponent<XRCubeUWBPosition>().roll;
          //  _rot.y = this.GetComponent<XRCubeUWBPosition>().pitch;
           // _rot.z = this.GetComponent<XRCubeUWBPosition>().yaw;
            _Qua.w = this.GetComponent<XRCubeUWBPosition>().quatW;
            _Qua.x = this.GetComponent<XRCubeUWBPosition>().quatX;
            _Qua.y = this.GetComponent<XRCubeUWBPosition>().quatY;
            _Qua.z = this.GetComponent<XRCubeUWBPosition>().quatZ;
            this.transform.localPosition=_pos;
          //  this.transform.localRotation = Quaternion.Euler(_rot);
            this.transform.localRotation = _Qua;

        }

    }
}
