using System;
using Assets.Scripts;
using Assets.Scripts.Quests.YourFirstJourney;
using UnityEngine;

public class Quest_YourFirstJourney : Quest
{
    PlayerInventory _inventory;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        OnStarted += AddOnQuestStartedEvents;
        OnFinished += AddOnQuestFinishedEvents;
    }

    void AddOnQuestStartedEvents(object sender, EventArgs e)
    {
        //_inventory.OnResourcePickedUp += WoodCollectedEvent;
        _inventory.OnToolPickedUp += AxeCollectedEvent;
    }

    void AddOnQuestFinishedEvents(object sender, EventArgs e)
    {
        //_inventory.OnResourcePickedUp -= WoodCollectedEvent;
        _inventory.OnToolPickedUp -= AxeCollectedEvent;
    }

    //void WoodCollectedEvent(object sender, EventArgs e)
    //{
    //    Task WoodTask = Tasks.Find(x => x is Task_GetWood);
    //    if (!WoodTask.isCompleted)
    //    {
    //        WoodTask.UpdateStatus();
    //    }
    //}

    void AxeCollectedEvent(object sender, EventArgs e)
    {
        Task AxeTask = Tasks.Find(x => x is Task_GetAxe);
        if (!AxeTask.GetStatus() && _inventory.HasTool(PlayerInventory.ToolType.Axe))
            AxeTask.UpdateStatus();
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
         //   Fungus.Flowchart.BroadcastFungusMessage("YFJCompl");
            Debug.Log("Active: " + this.isActive() + " Completed: " + this.isCompleted() + " Status: " + this.GetStatus() + " Finished Tasks: " + NumberOfFinishedTasks + " Total Tasks: " + TotalNumberOfTasks);
        }
    }
}
