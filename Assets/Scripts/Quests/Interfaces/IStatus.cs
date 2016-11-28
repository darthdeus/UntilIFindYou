public interface IStatus
{
    bool GetStatus();
    // True = Finished - can be turned in, False = requirements to turn in quest are not met.
    string GetProgress();
    // Returns a string giving player the information about progress. 
    // ("3/5 Wood Collected", "Seek help in village", "Seek help in village (Completed)")
    void UpdateStatus();
}