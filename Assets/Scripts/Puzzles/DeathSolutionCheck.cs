using UnityEngine;
using System.Collections;

public class DeathSolutionCheck : MonoBehaviour
{

    public SpriteRenderer Digit1;
    public SpriteRenderer Digit2;
    public SpriteRenderer Digit3;
    public SpriteRenderer Digit4;
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
        if (GetComponent<SpriteRenderer>().color.a != 0f)
        {
            if (Digit1.sprite.name == "bg8" &&
               Digit2.sprite.name == "bg2" &&
               Digit3.sprite.name == "bg3" &&
               Digit4.sprite.name == "bg1")
            {
                Correct();
            }
            else
            {
                Wrong();
            }
        }
    }

    void Correct()
    {
        Fungus.Flowchart.BroadcastFungusMessage("DeathCorrect");
    }

    void Wrong()
    {
        Fungus.Flowchart.BroadcastFungusMessage("DeathWrong");
    }
}
