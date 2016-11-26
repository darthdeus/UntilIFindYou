﻿using UnityEngine;
using Fungus;
using System.Collections.Generic;

public abstract class Quest : MonoBehaviour, IQuest, IStatus {

public Flowchart QuestChart;
// Chart Variables //
// Status      - Boolean, True - can be finished, False - can't be finished (requirements are not met)
// Completed   - Boolean, True - has been finished (can't be retaken), False - has not been finished yet (can be taken)
// Active      - Boolean, True - has been taken (quest is being tracked), False - has not been taken yet
// Title       - String
// Description - String
// End of Variables //
public List<Task> Tasks;
    public bool GetStatus() { return QuestChart.GetBooleanVariable("Status"); }
    public bool isActive() { return QuestChart.GetBooleanVariable("Active"); }
    public bool isCompleted() { return QuestChart.GetBooleanVariable("Completed"); }
    public void StartQuest() { QuestChart.SetBooleanVariable("Active", true); }
    public string GetTitle() { return QuestChart.GetStringVariable("Title"); }
    public string GetDescription() { return QuestChart.GetStringVariable("Description"); }
	public void FinishQuest()
    {
		if (isActive() && GetStatus() && !isCompleted())
		{
			QuestChart.SetBooleanVariable("Status", false);
			QuestChart.SetBooleanVariable("Active", false);
			QuestChart.SetBooleanVariable("Completed", true);
    	}
	}
    public string GetProgress()
	{
		int TotalTaskNumber, FinishedTaskNumber;
		GetNumberOfTasks(out TotalTaskNumber, out FinishedTaskNumber);
		if (TotalTaskNumber != FinishedTaskNumber)
			return FinishedTaskNumber + "/" + TotalTaskNumber + " tasks have been completed.";
		else
			return "All " + TotalTaskNumber + " tasks have been completed!";
	}
    public void GetNumberOfTasks(out int TotalTaskNumber, out int FinishedTaskNumber)
    {
        TotalTaskNumber = FinishedTaskNumber = 0;
		foreach (Task _task in Tasks){
			if (!_task.GetStatus()) 
				TotalTaskNumber++;
			else {
				TotalTaskNumber++;
				FinishedTaskNumber++;
			}
		}
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}