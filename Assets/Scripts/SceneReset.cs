using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneReset : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void ResetScene()
    {
        Fungus.Flowchart.BroadcastFungusMessage("EarthQuakeFade");
        StartCoroutine(WaitFor(3.6f));
    }
    IEnumerator WaitFor(float Seconds)
    {
        yield return new WaitForSeconds(Seconds);
        Application.Quit();         // Won't Work in Unity debug mode - works in build //
        Debug.Log("Game ended");
    }
}
