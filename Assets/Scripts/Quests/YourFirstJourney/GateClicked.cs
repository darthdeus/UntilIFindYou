using UnityEngine;

public class GateClicked : MonoBehaviour
{

    public Quest AssociatedQuest;
    public Task AssociatedTask;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// OnMouseUp is called when the user has released the mouse button.
    /// </summary>
    void OnMouseUp()
    {
        if (GameObject.FindWithTag("Player").GetComponent<PlayerMovementController>().isCloseEnough(gameObject.transform.position))
        {
            if (!AssociatedTask.isCompleted && AssociatedQuest.isActive())
                AssociatedTask.UpdateStatus();
        }
    }

    public void Interact()
    {
        OnMouseUp();
    }
}
