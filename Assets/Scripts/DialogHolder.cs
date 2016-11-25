using UnityEngine;
using System.Collections;

public class DialogHolder : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.name == "Player") 
		{

			//Unable to destroy the arrow
			Destroy(GameObject.Find("arrow2"));


			//Add dialog elements

			//Makes NPC stop if dialog box is active code (still not complete)
			if (transform.parent.GetComponent<NPCMovement>() != null) 
			{
				transform.parent.GetComponent<NPCMovement>().canMove = false;
			}
		}
	}
}
