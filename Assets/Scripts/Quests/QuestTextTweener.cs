using System;
using UnityEngine;
using UnityEngine.UI;

public class QuestTextTweener
{
    GameObject player;
    event EventHandler Tween;
    public bool TaskIsTweening;
    public bool QuestIsTweening;
    bool TaskIsStarting;
    bool QuestIsStarting;
    public QuestTextTweener(Text QuestText, Text TaskText, GameObject player)
    {
        this.TaskText = TaskText;
        this.QuestText = QuestText;
        this.player = player;

        TaskIsTweening = false;
        QuestIsTweening = false;

        TaskIsStarting = false;
        QuestIsStarting = false;

        TaskTextAbsolutePosition = TaskText.transform.position - player.transform.position;
        QuestTextAbsolutePosition = QuestText.transform.position - player.transform.position;

        TaskTextDefaultScale = TaskText.transform.localScale;
        QuestTextDefaultScale = QuestText.transform.localScale;
    }

    public void TweeningUpdate()
    {
        Tween(this, EventArgs.Empty);
    }

    public void QuestCompletedTweening()
    {
        if (!QuestIsTweening)
        {
            QuestIsTweening = true;
            Debug.Log("Quest Completed Tweening Started.");
            Tween += QuestScaleUp;
        }
    }

    public void TaskCompletedTweening()
    {
        if (!TaskIsTweening)
        {
            TaskIsTweening = true;
            Debug.Log("Task Completed Tweening Started.");
            Tween += TaskScaleUp;
        }
    }

    Text TaskText;
    Vector3 TaskTextDefaultScale;
    Vector3 TaskTextTargetScaleIncrease = new Vector3(0.08f, 0.08f, 0);
    Vector3 TaskTextScaleIncreaseSpeed = new Vector3(0.2f, 0.2f, 0);
    void TaskScaleUp(object sender, EventArgs e)
    {
        TaskText.transform.localScale += TaskTextScaleIncreaseSpeed * Time.deltaTime;
        if (TaskText.transform.localScale.magnitude >= TaskTextDefaultScale.magnitude + TaskTextTargetScaleIncrease.magnitude)
        {
            Tween -= TaskScaleUp;
            TaskText.transform.localScale = TaskTextDefaultScale + TaskTextTargetScaleIncrease;
            Tween += TaskScaleDown;
        }
    }
    void TaskScaleDown(object sender, EventArgs e)
    {
        TaskText.transform.localScale -= TaskTextScaleIncreaseSpeed * Time.deltaTime;
        if (TaskText.transform.localScale.magnitude <= TaskTextDefaultScale.magnitude)
        {
            Tween -= TaskScaleDown;
            TaskText.transform.localScale = TaskTextDefaultScale;
            if (!TaskIsStarting)
            {
                Tween += TaskFadeOut;
                Tween += TaskMoveRight;
            }
            else
            {
                TaskIsStarting = false;
                TaskIsTweening = false;
            }
        }
    }
    Vector3 TaskTextAbsolutePosition;
    Vector3 taskTargetPositionOffset = new Vector3(6, 0, 0);
    float taskSpeed = 3.0f;
    float taskThreshold = 0.5f;
    // TODO: New Task/Quest Appear event
    // TODO: QuestTextController "GetNextTask()" method
    // TODO: Change Quest Interface font size depending on resolution
    // TODO: Implement Quest and Task events to change texts instead of non-stop refreshing
    void TaskMoveRight(object sender, EventArgs e)
    {
        Vector3 taskTargetPosition = player.transform.position + TaskTextAbsolutePosition + taskTargetPositionOffset;
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
            Tween -= TaskMoveRight;
            ResetTaskPosition();
            TaskIsTweening = false;
        }
    }

    void TaskFadeOut(object sender, EventArgs e)
    {
        Tween -= TaskFadeOut;
        TaskText.CrossFadeAlpha(0.00f, 1.00f, true);
    }
    void TaskFadeIn(object sender, EventArgs e)
    {
        Tween -= TaskFadeIn;
        TaskText.CrossFadeAlpha(0.00f, 0.00f, true);
        TaskText.CrossFadeAlpha(1.00f, 1.00f, true);
    }

    Text QuestText;
    Vector3 QuestTextDefaultScale;
    Vector3 QuestTextTargetScaleIncrease = new Vector3(0.08f, 0.08f, 0);
    Vector3 QuestTextScaleIncreaseSpeed = new Vector3(0.2f, 0.2f, 0);
    void QuestScaleUp(object sender, EventArgs e)
    {
        QuestText.transform.localScale += QuestTextScaleIncreaseSpeed * Time.deltaTime;
        if (QuestText.transform.localScale.magnitude >= QuestTextDefaultScale.magnitude + QuestTextTargetScaleIncrease.magnitude)
        {
            QuestText.transform.localScale = QuestTextDefaultScale + QuestTextTargetScaleIncrease;
            Tween -= QuestScaleUp;
            Tween += QuestScaleDown;
        }
    }
    void QuestScaleDown(object sender, EventArgs e)
    {
        QuestText.transform.localScale -= QuestTextScaleIncreaseSpeed * Time.deltaTime;
        if (QuestText.transform.localScale.magnitude <= QuestTextDefaultScale.magnitude)
        {
            QuestText.transform.localScale = QuestTextDefaultScale;
            Tween -= QuestScaleDown;
            if (!QuestIsStarting)
            {
                Tween += QuestFadeOut;
                Tween += QuestMoveRight;
            }
            else
            {
                QuestIsStarting = false;
                QuestIsTweening = false;
            }
        }
    }

    void QuestFadeOut(object sender, EventArgs e)
    {
        Tween -= QuestFadeOut;
        QuestText.CrossFadeAlpha(0.00f, 1.00f, true);
    }
    void QuestFadeIn(object sender, EventArgs e)
    {
        Tween -= QuestFadeIn;
        QuestText.CrossFadeAlpha(0.00f, 0.00f, true);
        QuestText.CrossFadeAlpha(1.00f, 1.00f, true);
    }
    Vector3 QuestTextAbsolutePosition;
    Vector3 questTargetPositionOffset = new Vector3(6, 0, 0);
    float questSpeed = 3.0f;
    float questThreshold = 0.5f;
    void QuestMoveRight(object sender, EventArgs e)
    {
        Vector3 questTargetPosition = player.transform.position + QuestTextAbsolutePosition + questTargetPositionOffset;
        Vector3 direction = questTargetPosition - QuestText.transform.position;
        if (direction.magnitude > questThreshold)
        {
            direction.Normalize();
            QuestText.transform.position = QuestText.transform.position + direction * questSpeed * Time.deltaTime;
        }
        else
        {
            // Without this game object jumps around target and never settles
            QuestText.transform.position = questTargetPosition;
            Tween -= QuestMoveRight;
            ResetQuestPosition();
            QuestIsTweening = false;
        }
    }
    public void QuestStartedTweening()
    {
        if (!QuestIsTweening)
        {
            QuestIsTweening = true;
            QuestIsStarting = true;
            Debug.Log("Quest Started Tweening Started.");
            Tween += QuestFadeIn;
            Tween += QuestScaleUp;
        }
    }

    public void TaskStartedTweening()
    {
        if (!TaskIsTweening)
        {
            TaskIsStarting = true;
            TaskIsTweening = true;
            Debug.Log("Task Started Tweening Started.");
            Tween += TaskFadeIn;
            Tween += TaskScaleUp;
        }
    }

    void ResetTaskPosition()
    {
        TaskText.transform.position = player.transform.position + TaskTextAbsolutePosition;
        TaskText.CrossFadeAlpha(1.00f, 0.00f, true);
    }

    void ResetQuestPosition()
    {
        QuestText.transform.position = player.transform.position + QuestTextAbsolutePosition;
        QuestText.CrossFadeAlpha(1.00f, 0.00f, true);
    }
}