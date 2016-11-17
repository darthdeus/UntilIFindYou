using UnityEngine;
using System.Collections;
using System.Linq;
using Assets;

public class Rune : MonoBehaviour {
    private DialingBook _book;
    public string name;
    private AudioSource _sound;
    
	void Start () {
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

    void OnMouseUp() {
        Debug.Log("Rune clicked");

        GetComponent<SpriteRenderer>().material.color = Color.red;

        if (_book.CurrentAddress.Length < 4) {
            _sound.Play();
        }
        
        _book.SetSlot(name);
    }
}
