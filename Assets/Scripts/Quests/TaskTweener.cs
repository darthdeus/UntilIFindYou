﻿using UnityEngine;
using UnityEngine.UI;
using System;

public class TaskTweener
{
    QuestTextTweener _parent;
    Task TweenedTask;
    Text TaskText;
    Vector3 TaskTextDefaultScale;
    Vector3 TaskTextTargetScaleIncrease = new Vector3(0.08f, 0.08f, 0);
    Vector3 TaskTextScaleIncreaseSpeed = new Vector3(0.2f, 0.2f, 0);
    GameObject TaskTextDefaultPosition;
    Vector3 taskTargetPositionOffset = new Vector3(6, 0, 0);
    float taskSpeed = 3.0f;
    float taskThreshold = 0.5f;
    public TaskTweener(QuestTextTweener Parent, Text TaskText, GameObject DefaultTaskPosition)
    {
        _parent = Parent;
        this.TaskText = TaskText;

        TweenedTask = null;

        TaskTextDefaultScale = TaskText.transform.localScale;
        TaskTextDefaultPosition = DefaultTaskPosition;
    }
    public bool StartTweening(Task _task)
    {
        if (TweenedTask != null && ReferenceEquals(TweenedTask, _task))
            if (TweenedTask.isCompleted)
                _parent.Tween += TaskSetCompletedText;
            else
                _parent.Tween += TaskSetDescriptionText;

        if (_task != null && TweenedTask == null)
        {
            TweenedTask = _task;

            if (!TweenedTask.isCompleted || TaskText.text == "")
            {
                _parent.Tween += TaskSetDescriptionText;
                _parent.Tween += TaskFadeIn;
            }
            else if (TweenedTask.isCompleted)
                _parent.Tween += TaskSetCompletedText;
            _parent.Tween += TaskScaleUp;

            return true;
        }
        return false;
    }
    void TaskScaleUp(object sender, EventArgs e)
    {
        TaskText.transform.localScale += TaskTextScaleIncreaseSpeed * Time.deltaTime;
        if (TaskText.transform.localScale.magnitude >= TaskTextDefaultScale.magnitude + TaskTextTargetScaleIncrease.magnitude)
        {
            _parent.Tween -= TaskScaleUp;
            TaskText.transform.localScale = TaskTextDefaultScale + TaskTextTargetScaleIncrease;
            _parent.Tween += TaskScaleDown;
        }
    }
    void TaskScaleDown(object sender, EventArgs e)
    {
        TaskText.transform.localScale -= TaskTextScaleIncreaseSpeed * Time.deltaTime;
        if (TaskText.transform.localScale.magnitude <= TaskTextDefaultScale.magnitude)
        {
            _parent.Tween -= TaskScaleDown;
            TaskText.transform.localScale = TaskTextDefaultScale;
            if (TweenedTask.isCompleted)
            {
                _parent.Tween += TaskFadeOut;
                _parent.Tween += TaskMoveRight;
            }
            else
            {
                TweenedTask = null;
            }
        }
    }
    void TaskMoveRight(object sender, EventArgs e)
    {
        Vector3 taskTargetPosition = TaskTextDefaultPosition.transform.position + taskTargetPositionOffset;
        Vector3 direction = taskTargetPosition - TaskText.transform.position;
        if (direction.magnitude > taskThreshold)
        {
            direction.Normalize();
            TaskText.transform.position = TaskText.transform.position + direction * taskSpeed * Time.deltaTime;
        }
        else
        {
            // Without this game object jumps around target and never settles
            TaskText.transform.position = taskTargetPosition;
            _parent.Tween -= TaskMoveRight;
            ResetTaskText();
            TweenedTask = null;
        }
    }
    void TaskFadeOut(object sender, EventArgs e)
    {
        _parent.Tween -= TaskFadeOut;
        TaskText.CrossFadeAlpha(0.00f, 1.00f, true);
    }
    void TaskFadeIn(object sender, EventArgs e)
    {
        _parent.Tween -= TaskFadeIn;
        TaskText.CrossFadeAlpha(0.00f, 0.00f, true);
        TaskText.CrossFadeAlpha(1.00f, 1.00f, true);
    }
    void TaskSetDescriptionText(object sender, EventArgs e)
    {
        _parent.Tween -= TaskSetDescriptionText;
        if (TweenedTask != null)
            TaskText.text = TweenedTask.GetDescription();
    }
    void TaskSetCompletedText(object sender, EventArgs e)
    {
        _parent.Tween -= TaskSetCompletedText;
        TaskText.text = TweenedTask.GetDescription() + " (Completed)";
    }
    void ResetTaskText()
    {
        TaskText.text = "";
        TaskText.transform.position = TaskTextDefaultPosition.transform.position;
        TaskText.CrossFadeAlpha(1.00f, 0.00f, true);
    }
}
