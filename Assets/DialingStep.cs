using UnityEngine;
using System.Collections;
using System.Linq;
using Assets;

public class DialingStep : MonoBehaviour {
    private DialingManager _manager;
    public string name;
    
	void Start () {
	    _manager = DialingManager.Find();
	}

    void OnMouseUp() {
        Debug.Log("Rune clicked");
        _manager.SetSlot(name);
    }
}
