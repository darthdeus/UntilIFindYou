using UnityEngine;
using System.Collections;
using Fungus;

public class BlockCaller : MonoBehaviour
{
    public string sendingMessage;
    Flowchart isRunning; // flowchart carrying the "isRunning" variable.

    // Use this for initialization
    // Gets the required flowchart
    void Start()
    {
        foreach (var flowchart in FindObjectsOfType<Flowchart>())
            if (flowchart.name == "Intro")
                isRunning = flowchart;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Sends message to all flowcharts and flowchart with the matching ReceivingMessage is invoked
    void OnMouseUp()
    {
        if (!isRunning.GetBooleanVariable("isRunning"))
        {
            isRunning.SetBooleanVariable("isRunning", true);
            Flowchart.BroadcastFungusMessage(sendingMessage);
        }
    }
}