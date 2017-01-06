using UnityEngine;
using System.Collections;

public class DeathCollectPaper : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("other.gameObject.tag" + other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            Fungus.Flowchart.BroadcastFungusMessage("PaperFound");
            Destroy(gameObject);
        }
    }
}