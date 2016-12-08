using Assets.Scripts;
using UnityEngine;

public class Task_GetAxe : Task
{
    public GameObject player;
    PlayerInventory _playerInventory;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        _playerInventory = player.GetComponent<PlayerInventory>();
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

    public override void UpdateStatus_DONOTCALL()
    {
        if (_playerInventory.HasTool(PlayerInventory.ToolType.Axe))
            isCompleted = true;
        else
            isCompleted = false;
    }
}