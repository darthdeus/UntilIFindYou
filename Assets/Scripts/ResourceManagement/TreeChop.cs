using UnityEngine;
using System.Collections;

public class TreeChop : MonoBehaviour {

	private Animator chop;
	// Use this for initialization
	void Start () {
		chop = GetComponent<Animator>();
		chop.enabled = false;
	}

	// Update is called once per frame
	void Update () {

	}

	void OnMouseUp(){
		chop.enabled = true;
	}
}
