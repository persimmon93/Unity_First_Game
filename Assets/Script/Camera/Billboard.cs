using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is set to healthbars of enemies so that it always faces the camera.
/// </summary>
public class Billboard : MonoBehaviour
{
    public Camera cam;

    void Start()
    {
        cam = Camera.main;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.transform.forward);
    }
}
