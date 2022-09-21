using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Vector3 pos;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            pos = Input.mousePosition;
            float y = Input.GetAxis("Mouse Y");

            // var posY = new Vector3(Camera.main.transform.position.x, )
            if( y <= 0)
                Camera.main.transform.Translate(Vector3.down))
        }
        if(Input.GetMouseButtonUp(0))
        {
            pos = Vector3.zero;
        }
        
    }
}
