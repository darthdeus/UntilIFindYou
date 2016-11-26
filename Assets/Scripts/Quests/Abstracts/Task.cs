using UnityEngine;

public abstract class Task : MonoBehaviour, IStatus, ITask {
    public abstract string GetDescription();
    public abstract string GetProgress();
    public abstract bool GetStatus();
    public abstract void UpdateStatus();
}
