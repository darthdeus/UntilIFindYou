using UnityEngine;
using Fungus;

public class BlockCaller : MonoBehaviour
{
    public string sendingMessage;
    Flowchart isRunning; // flowchart carrying the "isRunning" variable.
    public Flowchart IntroChart; // flowchart carrying the "isRunning" variable.
    public GameObject others;
    public GameObject chat;

    // Use this for initialization
    // Gets the required flowchart
    void Start()
    {
        if (chat != null)
        {
            chat.SetActive(false);
            chat.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        if (others != null)
        {
            others.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }

        foreach (var flowchart in FindObjectsOfType<Flowchart>())
            if (flowchart.name == "Intro")
                isRunning = flowchart;

    }

    // Update is called once per frame

    // Sends message to all flowcharts and flowchart with the matching ReceivingMessage is invoked
    void OnMouseUp()
    {
        if (GameObject.FindWithTag("Player").GetComponent<PlayerMovementController>().isCloseEnough(gameObject.transform.position))
        {
            if (others != null)
                Destroy(others);
            if (chat != null)
                chat.SetActive(true);

            if (transform.GetComponent<NPCMovement>() != null)
                transform.GetComponent<NPCMovement>().canMove = false;

            if (!IntroChart.GetBooleanVariable("isRunning"))
            {

                isRunning.SetBooleanVariable("isRunning", true);
                IntroChart.SetBooleanVariable("isRunning", true);
                Flowchart.BroadcastFungusMessage(sendingMessage);
            }
        }
    }
}
