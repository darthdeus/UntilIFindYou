public class Task_InteractWithGate : Task
{
    public override string GetDescription()
    {
        return "Interact with the gate.";
    }

    public override string GetProgress()
    {
        if (GetStatus())
            return "You have interacted with the gate.";
        else
            return "Use left mouse button to interact with gate on the southern part of village.";
    }

    public override bool GetStatus()
    {
        return isCompleted;
    }

    public override void UpdateStatus()
    {

    }
}