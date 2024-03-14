using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float cameraSpeed = 1f;
    [SerializeField] private float zoomSpeed = 1f;
    private float zoom = 1f;

    [SerializeField] private Vector3 maxCameraPosition;
    [SerializeField] private Vector3 minCameraPosition;

    // Camera controller that allows to move camera with mouse and zoom in/out with mouse wheel
    void FixedUpdate() {
        if (!SceneChanger.IsLookingAtChunk) return;
        if (Input.GetMouseButton(2)) {
            float x = Input.GetAxis("Mouse X") / cameraSpeed;
            float y = Input.GetAxis("Mouse Y") / cameraSpeed;
            transform.position += new Vector3((-x - y) / zoom, 0, (x - y) / zoom);
        }
        
        
        transform.position += new Vector3((Input.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Vertical")) / cameraSpeed / 2, 0, (Input.GetAxisRaw("Horizontal") * -1 + Input.GetAxisRaw("Vertical")) / cameraSpeed / 2);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll == 0) return;
        if (!(Math.Round(this.transform.position.y) + -scroll * zoomSpeed <= maxCameraPosition.y) ||
            !(Math.Round(this.transform.position.y) + -scroll * zoomSpeed >= minCameraPosition.y)) return;
            
        if (zoom > 0.75) {
            zoom += scroll;
        }
        transform.position += new Vector3(scroll * zoomSpeed, -scroll * zoomSpeed, scroll * zoomSpeed);
    }
}