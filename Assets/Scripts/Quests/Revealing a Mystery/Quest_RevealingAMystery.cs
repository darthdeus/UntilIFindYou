using UnityEngine;
using Assets.Scripts;
using System.Collections;
using System;

public class Quest_RevealingAMystery : Quest
{
    void AxeCollectedEvent(object sender, EventArgs e)
    {
        // Task AxeTask = Tasks.Find(x => x is Task_GetAxe);
        // if (!AxeTask.GetStatus() && _inventory.HasTool(PlayerInventory.ToolType.Axe))
        //     AxeTask.UpdateStatus();
    }
    public override void UpdateStatus_DONOTCALL()
    {
        int TotalNumberOfTasks;
        int NumberOfFinishedTasks;
        TotalNumberOfTasks = NumberOfFinishedTasks = 0;
        this.GetNumberOfTasks(out TotalNumberOfTasks, out NumberOfFinishedTasks);
        if (NumberOfFinishedTasks == TotalNumberOfTasks && !this.GetStatus())
        {
            this.MakeCompletable();
            this.FinishQuest();
            Debug.Log("Active: " + this.isActive() + " Completed: " + this.isCompleted() + " Status: " + this.GetStatus() + " Finished Tasks: " + NumberOfFinishedTasks + " Total Tasks: " + TotalNumberOfTasks);
        }
    }

    // Use this for initialization
    void Start()
    {
        OnStarted += AddOnQuestStartedEvents;
        OnFinished += AddOnQuestFinishedEvents;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void AddOnQuestStartedEvents(object sender, EventArgs e)
    {
    }

    void AddOnQuestFinishedEvents(object sender, EventArgs e)
    {
    }
}
