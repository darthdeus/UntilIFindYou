using UnityEngine;
using System.Collections;
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
        if (AssociatedQuest.isActive() && DialingBook.CurrentAddress == "defab")
            AssociatedTask.isCompleted = true;
    }
}
