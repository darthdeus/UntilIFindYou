using UnityEngine;
using System.Collections;

public class CursorHolder : MonoBehaviour {

	public Texture2D cursorP;
	public Texture2D cursorY;
	public CursorMode cursormode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;

	// Use this for initialization
	void Start () {
		Cursor.SetCursor(cursorP,hotSpot,cursormode);
	}

	// Update is called once per frame
	void Update () {

	}

	public void setCursorP () {
	Cursor.SetCursor(cursorP,hotSpot,cursormode);
	}

	public void setCursorY () {
	Cursor.SetCursor(cursorY,hotSpot,cursormode);
	}
}
