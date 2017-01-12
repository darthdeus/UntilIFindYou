using UnityEngine;
using System.Collections;

public class SpaceBarInteraction : MonoBehaviour
{

    public GameObject _interactableObject = null;

    private Assets.Scripts.ResourceManagement.Tree _treeScript;
    private GateController _gateControllerScript;
    private GateClicked _gateClickedScript;
    private BlockCaller _blockCallerScript;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_interactableObject != null && Input.GetKeyUp(KeyCode.Space))
        {
            if (_treeScript != null) _treeScript.Interact();
            if (_gateClickedScript != null) _gateClickedScript.Interact();
            if (_gateControllerScript != null) _gateControllerScript.Interact();
            if (_blockCallerScript != null) _blockCallerScript.Interact();
        }
    }

    /// <summary>
	/// Sent when an incoming collider makes contact with this object's
	/// collider (2D physics only).
	/// </summary>
	/// <param name="other">The Collision2D data associated with this collision.</param>
	void OnCollisionEnter2D(Collision2D other)
    {
        if (_interactableObject != other.gameObject)
        {
            _interactableObject = other.gameObject;

            _treeScript = _interactableObject.GetComponent<Assets.Scripts.ResourceManagement.Tree>();
            _gateControllerScript = _interactableObject.GetComponent<GateController>();
            _gateClickedScript = _interactableObject.GetComponent<GateClicked>();
            _blockCallerScript = _interactableObject.GetComponent<BlockCaller>();
        }
    }

    /// <summary>
    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionExit2D(Collision2D other)
    {
        if (_interactableObject == other.gameObject)
            _interactableObject = null;
    }

}
