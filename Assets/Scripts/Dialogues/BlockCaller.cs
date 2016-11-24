using UnityEngine;
using System.Collections;
using Fungus;

public class BlockCaller : MonoBehaviour
{

    public string sendingMessage;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseUp()
    {
        Flowchart.BroadcastFungusMessage(sendingMessage);
    }
}