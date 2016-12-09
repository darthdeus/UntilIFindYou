public class Task_TravelToNewPlanet : Task
{
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        isCheckAllowed = false;
    }
    public override string GetDescription()
    {
        return "Press all the runes in the second row of first page from left to right and then hit the first two runes of the first row";
    }

    public override string GetProgress()
    {
        if (GetStatus())
            return "Well done! You have teleported to a new planet.";
        else
            return "Activate the gate to travel to the new planet.";
    }

    public override bool GetStatus()
    {
        return isCompleted;
    }

    public override void UpdateStatus_DONOTCALL()
    {
        isCompleted = true;
    }
}