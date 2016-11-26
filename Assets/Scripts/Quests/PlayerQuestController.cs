using UnityEngine;
using System.Collections.Generic;

public class PlayerQuestController : MonoBehaviour {

public List<Quest> Quests;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Quest _quest in Quests)
			_quest.UpdateStatus();
	}
}
