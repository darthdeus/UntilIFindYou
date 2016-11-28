using UnityEngine;

public class Task_GetAxe : Task
{
    public GameObject player;
    ResourceManager ResourceManager;

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
        return "Get an Axe Tool.";
    }

    public override string GetProgress()
    {
        if (GetStatus())
            return "You are now capable of harvesting wood.";
        else
            return "You have not found an axe yet.";
    }

    public override bool GetStatus()
    {
        return isCompleted;
    }

    public override void UpdateStatus()
    {
        if (ResourceManager.HasTool(ResourceManager.ToolType.Axe))
            isCompleted = true;
        else
            isCompleted = false;
    }
}