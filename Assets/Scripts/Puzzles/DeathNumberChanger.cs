using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeathNumberChanger : MonoBehaviour
{
    public bool isIncrementing;
    public DeathNumberContainer Numbers;
    public SpriteRenderer _renderer;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown()
    {
        if (isIncrementing)
        {
            switch (_renderer.sprite.name)
            {
                case "bg0": _renderer.sprite = Numbers.Number1; return;
                case "bg1": _renderer.sprite = Numbers.Number2; return;
                case "bg2": _renderer.sprite = Numbers.Number3; return;
                case "bg3": _renderer.sprite = Numbers.Number4; return;
                case "bg4": _renderer.sprite = Numbers.Number5; return;
                case "bg5": _renderer.sprite = Numbers.Number6; return;
                case "bg6": _renderer.sprite = Numbers.Number7; return;
                case "bg7": _renderer.sprite = Numbers.Number8; return;
                case "bg8": _renderer.sprite = Numbers.Number9; return;
                case "bg9": _renderer.sprite = Numbers.Number0; return;
                default: return;
            }
        }
        else
        {
            switch (_renderer.sprite.name)
            {
                case "bg0": _renderer.sprite = Numbers.Number9; return;
                case "bg1": _renderer.sprite = Numbers.Number0; return;
                case "bg2": _renderer.sprite = Numbers.Number1; return;
                case "bg3": _renderer.sprite = Numbers.Number2; return;
                case "bg4": _renderer.sprite = Numbers.Number3; return;
                case "bg5": _renderer.sprite = Numbers.Number4; return;
                case "bg6": _renderer.sprite = Numbers.Number5; return;
                case "bg7": _renderer.sprite = Numbers.Number6; return;
                case "bg8": _renderer.sprite = Numbers.Number7; return;
                case "bg9": _renderer.sprite = Numbers.Number8; return;
                default: return;
            }
        }
    }
}
