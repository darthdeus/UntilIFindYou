using UnityEngine;
using System.Collections;
using System.Linq;
using Assets;

public class Rune : MonoBehaviour
{
    private DialingBook _book;
    public string RuneName;
    private AudioSource _sound;
    private bool _alreadyClicked = false;

    void Start()
    {
        _book = DialingBook.Find();
        var sounds = GetComponentsInParent<AudioSource>();

        foreach (var sound in sounds)
        {
            if (sound.clip.name == "GotItem")
            {
                _sound = sound;
            }
        }

    }

    void OnMouseUp()
    {
        if (_book.GetComponent<Renderer>().isVisible)
        {
            if (_book.CurrentAddress.Length < 4)
            {
                _sound.Play();
            }

            if (_alreadyClicked)
            {
                _book.UnsetSlot();
                _alreadyClicked = false;

                GetComponent<SpriteRenderer>().material.color = Color.white;
            }
            else
            {
                _book.SetSlot(RuneName);
                _alreadyClicked = true;

                GetComponent<SpriteRenderer>().material.color = Color.red;
            }
        }
    }
    public void Reset()
    {
        _alreadyClicked = false;
    }
}
