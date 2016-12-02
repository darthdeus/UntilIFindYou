
using UnityEngine;
using System.Collections;
using Fungus;

public class BlockCaller : MonoBehaviour
{
    public string sendingMessage;
    Flowchart isRunning; // flowchart carrying the "isRunning" variable.
    public Flowchart IntroChart; // flowchart carrying the "isRunning" variable.
    public GameObject others;

    // Use this for initialization
    // Gets the required flowchart
    void Start()
    {
        foreach (var flowchart in FindObjectsOfType<Flowchart>())
            if (flowchart.name == "Intro")
                isRunning = flowchart;

    }

    // Update is called once per frame
 
    // Sends message to all flowcharts and flowchart with the matching ReceivingMessage is invoked
    void OnMouseUp()
    {
        
        Destroy(others);
        transform.GetComponent<NPCMovement>() .canMove = false;

        if (!IntroChart.GetBooleanVariable("isRunning"))
        {

            isRunning.SetBooleanVariable("isRunning", true);
            IntroChart.SetBooleanVariable("isRunning", true);
            Flowchart.BroadcastFungusMessage(sendingMessage);
        }

    }
}