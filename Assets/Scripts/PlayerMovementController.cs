using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour {

    public float movementSpeed = 2f;

    private Rigidbody2D _rigidbody2D;

    void Start () {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update () {
	    float x = Input.GetAxisRaw("Horizontal");
	    float y = Input.GetAxisRaw("Vertical");

        _rigidbody2D.velocity = new Vector2(x, y).normalized * movementSpeed;
	}

    void OnCollisionEnter2D(Collision2D other) {
        var gate = other.collider.GetComponent<GateController>();

        // We've collided with a gate
        if (gate != null) {
            Debug.Assert(gate.connectedGate != null);

            // TODO - replace this with a scene-specified position
            var exitOffset = new Vector3(0, -2, 0);
            transform.position = gate.connectedGate.transform.position + exitOffset;
        }
    }
}
