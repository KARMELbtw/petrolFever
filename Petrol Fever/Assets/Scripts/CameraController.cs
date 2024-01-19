using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Camera controller that allows to move camera with mouse and zoom in/out with mouse wheel
    void Update() {
        if (Input.GetMouseButton(0)) {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            transform.position += new Vector3(-x/2 - y/2, 0, x/2 - y/2);
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.position += new Vector3(scroll * 5, -scroll * 5, scroll * 5);
    }
}
