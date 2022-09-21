using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] float maxy,miny;
    Vector3 dragOrigin;

    Vector3 temp2 = Camera.main.transform.position;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if(Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var temp = new Vector3(temp2.x,difference.y,temp2.z);
            // float newY = Mathf.Clamp(Camera.main.transform.position.y,miny,maxy);
            Camera.main.transform.position = ClampCam(Camera.main.transform.position + temp);
        }
    }
    Vector3 ClampCam(Vector3 target)
    {
        // float newX = Mathf.Clamp(target.x,0,0);
        float newY = Mathf.Clamp(target.y,miny,maxy);
        return new Vector3(target.x,newY,target.z);
    }
}
