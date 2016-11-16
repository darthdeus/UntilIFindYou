using UnityEngine;
using System.Collections;
using System.Linq;
using Assets;

public class DialingStep : MonoBehaviour {
    private DialingManager _manager;
    public string name;
    

	// Use this for initialization
	void Start () {
	    _manager = GetComponentsInParent<DialingManager>().FirstOrDefault();
	}

    void OnMouseDown() {
        _manager.SetSlot(name);
    }
}
