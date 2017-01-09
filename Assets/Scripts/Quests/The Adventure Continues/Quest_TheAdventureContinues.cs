using UnityEngine;
using System;
using Assets.Scripts.Quests.TheAdventureContinues;
using Assets;

public class Quest_TheAdventureContinues : Quest
{
    void AddressFiguredEvent(object sender, EventArgs e)
    {
        Task AFTask = Tasks.Find(x => x is Task_FigureAddress);
        if (!AFTask.GetStatus() && ((DialingBook)sender).CurrentAddress == "gbljm")
            AFTask.UpdateStatus();
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
        GameObject.Find("DialingBook").GetComponent<DialingBook>().OnTeleportAnim += AddressFiguredEvent;
    }

    void AddOnQuestFinishedEvents(object sender, EventArgs e)
    {
        GameObject.Find("DialingBook").GetComponent<DialingBook>().OnTeleportAnim -= AddressFiguredEvent;
    }
}
