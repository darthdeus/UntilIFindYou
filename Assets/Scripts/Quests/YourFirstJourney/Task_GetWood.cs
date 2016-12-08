using Assets.Scripts;
using UnityEngine;

public class Task_GetWood : Task
{
    public GameObject player;
    PlayerInventory _playerInventory;
    public int TargetWoodAmount;

    private int CurrentWoodAmount
    {
        get { return _playerInventory.ResourceCount(PlayerInventory.ResourceType.Wood); }
    }

    void Start()
    {
        _playerInventory = player.GetComponent<PlayerInventory>();
    }

    public override string GetDescription()
    {
        if (CurrentWoodAmount == 0 || isCompleted)
        {
            return string.Format("Use your Axe Tool to get {0} pieces of wood.", TargetWoodAmount);
        }
        else
        {
            return string.Format("Use your Axe Tool to get {0} more pieces of wood.", TargetWoodAmount - CurrentWoodAmount);
        }
    }

    public override string GetProgress()
    {
        if (GetStatus())
        {
            return "Success!";
        }
        else
        {
            return string.Format("You have collected {0}/{1} pieces of wood.", CurrentWoodAmount, TargetWoodAmount);
        }
    }

    public override bool GetStatus()
    {
        return isCompleted;
    }

    public override void UpdateStatus_DONOTCALL()
    {
        isCompleted = CurrentWoodAmount >= TargetWoodAmount;
    }
}