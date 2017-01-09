using UnityEngine;
using Assets.Scripts;
using System.Collections;
using System;
using Assets;
using Assets.Scripts.Quests.SearchingForClues;

public class Quest_SearchingForClues : Quest
{
    void TravelledBackHomeEvent(object sender, EventArgs e)
    {
        Task TBTask = Tasks.Find(x => x is Task_TravelBack);
        if (!TBTask.GetStatus() && ((DialingBook)sender).CurrentAddress == "iejca")
            TBTask.UpdateStatus();
    }

    void DiscoveredNewPlanetEvent(object sender, EventArgs e)
    {
        Task TBTask = Tasks.Find(x => x is Task_DiscoverPlanet);
        if (!TBTask.GetStatus() && ((DialingBook)sender).CurrentAddress == "ihjoa")
            TBTask.UpdateStatus();
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
        GameObject.Find("DialingBook").GetComponent<DialingBook>().OnTeleportAnim += TravelledBackHomeEvent;
        GameObject.Find("DialingBook").GetComponent<DialingBook>().OnTeleportAnim += DiscoveredNewPlanetEvent;
    }

    void AddOnQuestFinishedEvents(object sender, EventArgs e)
    {
        GameObject.Find("DialingBook").GetComponent<DialingBook>().OnTeleportAnim -= TravelledBackHomeEvent;
        GameObject.Find("DialingBook").GetComponent<DialingBook>().OnTeleportAnim -= DiscoveredNewPlanetEvent;
    }
}
