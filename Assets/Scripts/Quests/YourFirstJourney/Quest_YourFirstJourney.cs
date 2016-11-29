using UnityEngine;

public class Quest_YourFirstJourney : Quest
{
    public override void UpdateStatus()
    {
        foreach (Task _task in Tasks)
            _task.UpdateStatus();

        int TotalNumberOfTasks;
        int NumberOfFinishedTasks;
        TotalNumberOfTasks = NumberOfFinishedTasks = 0;
        this.GetNumberOfTasks(out TotalNumberOfTasks, out NumberOfFinishedTasks);
        if (NumberOfFinishedTasks == TotalNumberOfTasks && !this.GetStatus())
        {
            this.MakeCompletable();
            this.FinishQuest();
            Fungus.Flowchart.BroadcastFungusMessage("YFJCompl");
            Debug.Log("Active: " + this.isActive() + " Completed: " + this.isCompleted() + " Status: " + this.GetStatus() + " Finished Tasks: " + NumberOfFinishedTasks + " Total Tasks: " + TotalNumberOfTasks);
        }
    }
}
