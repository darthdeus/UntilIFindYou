public interface IQuest
{
    void StartQuest();
    // Starts the quest.
    void FinishQuest();
    // Finishes the quest.
    bool isActive();
    // Quest has been taken and player is trying to finish it.
    bool isCompleted();
    // Quest has already been turned in and cannot be retaken.
    string GetTitle();
    // The title of the Quest.
    string GetDescription();
    // The description of the quest - more information about the goal in general.
    void GetNumberOfTasks(out int TotalTaskNumber, out int FinishedTaskNumber);
    // Gets number of finished tasks and tasks in total.
    void MakeCompletable();
    // Makes the quest capable of being completed (mostly by setting its status to true)
}