using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{

    public Texture2D cursorArrow;
    //public Texture2D cursorReplacement;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }

    //void OnMouseEnter()
    //{
    //    Cursor.SetCursor(cursorReplacement, Vector2.zero, CursorMode.ForceSoftware);
    //}

    //void OnMouseExit()
    //{
    //    Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    //}
}
