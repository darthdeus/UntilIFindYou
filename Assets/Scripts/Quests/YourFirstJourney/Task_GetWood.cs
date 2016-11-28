using UnityEngine;

public class Task_GetWood : Task
{
    public GameObject player;
    ResourceManager ResourceManager;
    public int TargetWoodAmount;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        ResourceManager = player.GetComponent<ResourceManager>();
    }
    public override string GetDescription()
    {
        return "Use your Axe Tool to get " + TargetWoodAmount + " wood.";
    }

    public override string GetProgress()
    {
        if (GetStatus())
            return "Success!";
        else
            return "You have collected " + ResourceManager.ResourceCount(ResourceManager.ResourceType.Wood) + "/" + TargetWoodAmount + " wood.";
    }

    public override bool GetStatus()
    {
        return isCompleted;
    }

    public override void UpdateStatus()
    {
        if (ResourceManager.ResourceCount(ResourceManager.ResourceType.Wood) >= TargetWoodAmount)
            isCompleted = true;
        else
            isCompleted = false;
    }
}