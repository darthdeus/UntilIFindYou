using Assets.Scripts;
using UnityEngine;

public class Task_GetWood : Task
{
    public GameObject player;
    PlayerInventory _playerInventory;
    public int TargetWoodAmount;

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
        return "Use your Axe Tool to get " + TargetWoodAmount + " wood.";
    }

    public override string GetProgress()
    {
        if (GetStatus())
            return "Success!";
        else
            return "You have collected " + _playerInventory.ResourceCount(PlayerInventory.ResourceType.Wood) + "/" + TargetWoodAmount + " wood.";
    }

    public override bool GetStatus()
    {
        return isCompleted;
    }

    public override void UpdateStatus()
    {
        if (_playerInventory.ResourceCount(PlayerInventory.ResourceType.Wood) >= TargetWoodAmount)
            isCompleted = true;
        else
            isCompleted = false;
    }
}