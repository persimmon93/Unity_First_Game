using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Target to follow.
    public Transform target;
    //Camera offset from target.
    public Vector3 offset;
    private float currentZoom = 10f;
    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;

    public float yawSpeed = 100f;
    private float currentYaw = 0f;

    public float pitch = 2f;

    void Update()
    {
        if (!GameManager.gameIsPaused)
        {
            //-= Is regular. += is inverted.
            currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            //currentzoom will be between minzoom and maxzoom.
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

            //A and D Later change it to ctrl + right mouse button.
            currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
        }
    }
    void LateUpdate()
    {
        if (!GameManager.gameIsPaused)
        {
            transform.position = target.position - offset * currentZoom;
            transform.LookAt(target.position + Vector3.up * pitch);

            transform.RotateAround(target.position, Vector3.up, currentYaw);
        }
    }
}
