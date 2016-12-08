using System;
using UnityEngine;

public abstract class Task : MonoBehaviour, IStatus, ITask
{
    public event EventHandler OnStatusUpdate;
    public bool isCompleted = false;
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
}
