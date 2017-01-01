using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneReset : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void ResetScene()
    {
        Fungus.Flowchart.BroadcastFungusMessage("EarthQuakeFade");
        // TODO HOW TO RESET SCENE? OR ANY OTHER GAME ENDING SHOULD GO HERE
        //SceneManager.LoadScene("Main");
        Debug.Log("Game ended");
    }
}
