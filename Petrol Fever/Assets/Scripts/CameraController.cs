using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float cameraSpeed = 2f;
    [SerializeField]
    private float zoomSpeed = 5f;
    private float zoom = 1f;
    // Camera controller that allows to move camera with mouse and zoom in/out with mouse wheel
    void Update() {
        if (Input.GetMouseButton(0)) {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            transform.position += new Vector3((-x*cameraSpeed - y*cameraSpeed) / zoom , 0, (x*cameraSpeed - y*cameraSpeed) / zoom);
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Math.Round(this.transform.position.y) + -scroll * zoomSpeed <= 38 && Math.Round(this.transform.position.y) + -scroll * zoomSpeed >= 27) {
            if (zoom > 0.75 && scroll < 0 || scroll > 0) {
                zoom += scroll;
            }

            transform.position += new Vector3(scroll * zoomSpeed, -scroll * zoomSpeed, scroll * zoomSpeed);
        }
    }
}
