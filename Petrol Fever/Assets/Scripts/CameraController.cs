using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float cameraSpeed = 5f;
    [SerializeField] private float zoomSpeed = 5f;
    private float zoom = 1f;

    [SerializeField] private float maxX = 16;   
    [SerializeField] private float minX = -40;
    [SerializeField] private float maxZ = 10;
    [SerializeField] private float minZ = -25;
    [SerializeField] private float maxY = 39;
    [SerializeField] private float minY = 27;

    // Camera controller that allows to move camera with mouse and zoom in/out with mouse wheel
    void Update() {
        if (SceneChanger.IsLookingAtChunk) {
            if (Input.GetMouseButton(0)) {
                float x = Input.GetAxis("Mouse X") / cameraSpeed;
                float y = Input.GetAxis("Mouse Y") / cameraSpeed;
                if (Math.Round(this.transform.position.x) + Math.Round(-x - y) <= maxX && 
                    Math.Round(this.transform.position.x) + Math.Round(-x - y) >= minX &&
                    Math.Round(this.transform.position.z) + Math.Round(x - y) <= maxZ &&
                    Math.Round(this.transform.position.z) + Math.Round(x - y) >= minZ) {
                    transform.position += new Vector3((-x - y) / zoom, 0, (x - y) / zoom);
                }

                float scroll = Input.GetAxis("Mouse ScrollWheel");
                if (scroll == 0) return;
                if (!(Math.Round(this.transform.position.y) + -scroll * zoomSpeed <= maxY) ||
                    !(Math.Round(this.transform.position.y) + -scroll * zoomSpeed >= minY)) return;

                if (zoom > 0.75) {
                    zoom += scroll;
                }

                transform.position += new Vector3(scroll * zoomSpeed, -scroll * zoomSpeed, scroll * zoomSpeed);
            }
        }
    }
}