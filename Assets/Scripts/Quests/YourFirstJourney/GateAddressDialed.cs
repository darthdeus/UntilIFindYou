using UnityEngine;
using Assets;

public class GateAddressDialed : MonoBehaviour
{
    public Quest AssociatedQuest;
    public Task AssociatedTask;
    public DialingBook DialingBook;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!AssociatedTask.isCompleted && AssociatedQuest.isActive() && DialingBook.CurrentAddress == "defab")
            AssociatedTask.UpdateStatus();
    }
}
