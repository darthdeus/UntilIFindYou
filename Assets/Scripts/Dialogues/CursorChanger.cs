using UnityEngine;

public class CursorChanger : MonoBehaviour
{

    CursorHolder cursor;
    // Use this for initialization
    void Start()
    {
      cursor = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CursorHolder>();
    }

    /// <summary>
    /// Called when the mouse enters the GUIElement or Collider.
    /// </summary>
    void OnMouseEnter()
    {

      cursor.setCursorY();
      //  if (isInteractable)
    //    {
    //        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    //        Debug.Log("Cursor modified by " + gameObject.name);
    //    }
    }
    /// <summary>
    /// Called when the mouse is not any longer over the GUIElement or Collider.
    /// </summary>
    void OnMouseExit()
    {

        cursor.setCursorP();
      //  if (isInteractable)
    //    {
    //        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    //        Debug.Log("Cursor returned back to normal by " + gameObject.name);
    //    }
    }
}
