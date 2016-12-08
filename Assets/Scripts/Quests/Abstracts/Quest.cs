using UnityEngine;
using Fungus;
using System.Collections.Generic;
using System;

public abstract class Quest : MonoBehaviour, IQuest, IStatus
{

    public Flowchart QuestChart;
    // Chart Variables //
    // Status      - Boolean, True - can be finished, False - can't be finished (requirements are not met)
    // Completed   - Boolean, True - has been finished (can't be retaken), False - has not been finished yet (can be taken)
    // Active      - Boolean, True - has been taken (quest is being tracked), False - has not been taken yet
    // Title       - String
    // Description - String
    // End of Variables //
    public List<Task> Tasks;

    public event System.EventHandler OnStarted;
    public event System.EventHandler OnMakeCompletable;
    public event System.EventHandler OnStatusUpdate;
    public event System.EventHandler OnFinished;

    public bool GetStatus()
    {
        return QuestChart.GetBooleanVariable("Status");
    }
    public bool isActive()
    {
        return QuestChart.GetBooleanVariable("Active");
    }
    public bool isCompleted()
    {
        return QuestChart.GetBooleanVariable("Completed");
    }
    public void StartQuest()
    {
        QuestChart.SetBooleanVariable("Active", true);
        if (OnStarted != null)
            OnStarted(this, System.EventArgs.Empty);
    }
    public string GetTitle()
    {
        return QuestChart.GetStringVariable("Title");
    }
    public string GetDescription()
    {
        return QuestChart.GetStringVariable("Description");
    }
    public void FinishQuest()
    {
        if (isActive() && GetStatus() && !isCompleted())
        {
            QuestChart.SetBooleanVariable("Status", false);
            QuestChart.SetBooleanVariable("Active", false);
            QuestChart.SetBooleanVariable("Completed", true);
        }
        if (OnFinished != null)
            OnFinished(this, System.EventArgs.Empty);
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
        foreach (Task _task in Tasks)
        {
            if (!_task.GetStatus())
                TotalTaskNumber++;
            else
            {
                TotalTaskNumber++;
                FinishedTaskNumber++;
            }
        }
    }
    public abstract void UpdateStatus_DONOTCALL();

    public void UpdateStatus()
    {
        UpdateStatus_DONOTCALL();
        if (OnStatusUpdate != null)
            OnStatusUpdate(this, System.EventArgs.Empty);
    }

    public void MakeCompletable()
    {
        QuestChart.SetBooleanVariable("Status", true);
        if (OnMakeCompletable != null)
            OnMakeCompletable(this, System.EventArgs.Empty);
    }
}