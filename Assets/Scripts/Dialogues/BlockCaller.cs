using UnityEngine;
using Fungus;

public class BlockCaller : MonoBehaviour
{
    public string sendingMessage;
    public Flowchart IntroChart; // flowchart carrying the "isRunning" variable.

    // Use this for initialization
    // Gets the required flowchart
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Sends message to all flowcharts and flowchart with the matching ReceivingMessage is invoked
    void OnMouseUp()
    {
        if (!IntroChart.GetBooleanVariable("isRunning"))
        {
            IntroChart.SetBooleanVariable("isRunning", true);
            Flowchart.BroadcastFungusMessage(sendingMessage);
        }
    }
}