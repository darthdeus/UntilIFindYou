using System;
using UnityEngine;

public abstract class Task : MonoBehaviour, IStatus, ITask
{
    public event EventHandler OnStatusUpdate;
    public bool isCompleted = false;
    // true - Allows UpdateStatus_DONOTCALL() method to be called when the quest is started // 
    public bool isCheckAllowed = true;
    public abstract string GetDescription();
    public abstract string GetProgress();
    public abstract bool GetStatus();
    public abstract void UpdateStatus_DONOTCALL();
    public void UpdateStatus()
    {
        UpdateStatus_DONOTCALL();
        if (OnStatusUpdate != null)
            OnStatusUpdate(this, EventArgs.Empty);
    }

    // Checks the status of the quest - same effect as UpdateStatus() but does not Invoke OnStatusUpdate //
    public void CheckStatus(){
        UpdateStatus_DONOTCALL();
    }
}
