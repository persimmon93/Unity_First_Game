using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;


/// <summary>
/// Is suppose to take picture of objects in game. May not need.
/// </summary>
public class IconGenerator : MonoBehaviour
{
    private static IconGenerator instance;

    private Camera camera;
    private bool takeScreenShotOnNextFrame;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        camera = gameObject.GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeScreenshot_Static(500, 500);
        }
    }
    private void OnPostRender()
    {
        if (takeScreenShotOnNextFrame)
        {
            takeScreenShotOnNextFrame = false;
            RenderTexture renderTexture = camera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/Icons/CameraScreenshot.png", byteArray);
            Debug.Log("Saved CameraScreenshot.png");

            RenderTexture.ReleaseTemporary(renderTexture);
            camera.targetTexture = null;
        }
    }

    private void TakeScreenshot(int width, int height)
    {
        camera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenShotOnNextFrame = true;
    }

    public static void TakeScreenshot_Static(int width, int height)
    {
        instance.TakeScreenshot(width, height);
    }
}
