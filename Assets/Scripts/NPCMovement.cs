using UnityEngine;
using System.Collections;

public class NPCMovement : MonoBehaviour {

	public float moveSpeed= 1 ;
	private Rigidbody2D myRigidBody;
	public bool isWalking;



	public float walkTime = 2,waitTime = 3;
	private float walkCounter, waitCounter;

	private int walkDirection;

	public bool canMove;

	// Use this for initialization
	void Start () 
	{

		myRigidBody = GetComponent<Rigidbody2D>();

		walkCounter = walkTime;
		waitCounter = waitTime;

		ChooseDirection();

		canMove = true;
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!canMove)
		{
			myRigidBody.velocity = Vector2.zero;
			return;
		}

		switch (walkDirection)
		{
		case 0:
			myRigidBody.velocity = new Vector2 (0, moveSpeed);

			break;

		case 1:
			myRigidBody.velocity = new Vector2 (moveSpeed,0);

			break;

		case 2:
			myRigidBody.velocity = new Vector2 (0, -moveSpeed);

			break;

		case 3:
			myRigidBody.velocity = new Vector2 (-moveSpeed,0);
		
			break;
		}


		if (isWalking)
		{

			walkCounter -= Time.deltaTime;
			if (walkCounter < 0) 
			{
				isWalking = false;
				waitCounter = waitTime;
			}
		} 

		else 
		{

			waitCounter -= Time.deltaTime;
			myRigidBody.velocity = Vector2.zero;
			if (waitCounter < 0) 
			{
				ChooseDirection ();
			}
			
		}
	
	}


	void ChooseDirection()
	{
		walkDirection = Random.Range (0, 4);
		isWalking = true;
		walkCounter = walkTime;
	}
}
