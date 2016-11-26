using UnityEngine;
using System.Collections;
using System;

public abstract class Task : MonoBehaviour, IStatus, ITask {
    public abstract string GetDescription();
    public abstract string GetProgress();
    public abstract bool GetStatus();

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
