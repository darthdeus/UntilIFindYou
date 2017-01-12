using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour
{
    public float movementSpeed = 2f;

    public float minInteractDistance = 1f;
    private Rigidbody2D _rigidbody2D;
    private Animator anim;
    private bool playerMoving;
    private Vector2 lastMove;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public bool isCloseEnough(Vector3 objectPosition)
    {
        float MagnitudeOf3DVectorIn2D = new Vector3((gameObject.transform.position.x - objectPosition.x),
                                        (gameObject.transform.position.y - objectPosition.y), 0f).magnitude;
        if (MagnitudeOf3DVectorIn2D < minInteractDistance)
            return true;
        else
        {
            Debug.Log("Interacted GObject is out of reach:");
            Debug.Log("minInteractDistance: " + minInteractDistance);
            Debug.Log("InteractedObjectDistance: " + MagnitudeOf3DVectorIn2D);
            return false;
        }
    }

    void Update()
    {
        playerMoving = false;
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        _rigidbody2D.velocity = new Vector2(x, y).normalized * movementSpeed;
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            playerMoving = true;

            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        }

        if (Input.GetAxisRaw("Vertical") != 0)
        {
            playerMoving = true;
            lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
        }


        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);

    }


}
