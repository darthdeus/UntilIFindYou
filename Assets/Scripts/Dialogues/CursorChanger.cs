using UnityEngine;

public class CursorChanger : MonoBehaviour
{

    public bool isInteractable;
    public Texture2D cursorTexture;
    // Use this for initialization

    /// <summary>
    /// Called when the mouse enters the GUIElement or Collider.
    /// </summary>
    void OnMouseEnter()
    {
        if (isInteractable)
        {
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
            Debug.Log("Cursor modified by " + gameObject.name);
        }
    }
    /// <summary>
    /// Called when the mouse is not any longer over the GUIElement or Collider.
    /// </summary>
    void OnMouseExit()
    {
        if (isInteractable)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            Debug.Log("Cursor returned back to normal by " + gameObject.name);
        }
    }
}