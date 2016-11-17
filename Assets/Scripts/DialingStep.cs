using UnityEngine;
using System.Collections;
using System.Linq;
using Assets;

public class DialingStep : MonoBehaviour {
    private DialingManager _manager;
    public string name;
    private AudioSource _sound;
    
	void Start () {
	    _manager = DialingManager.Find();
	    _sound = GetComponentsInParent<AudioSource>()[1];
        
	}

    void OnMouseUp() {
        Debug.Log("Rune clicked");
        _sound.Play();
        _manager.SetSlot(name);
    }
}
