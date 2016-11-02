using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

    public GameObject Player;

	void Start () {
	}
	
	void Update () {
	    transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);
	}
}
