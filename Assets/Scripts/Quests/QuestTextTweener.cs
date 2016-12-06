using UnityEngine;
using UnityEngine.UI;

public class QuestTextTweener
{
    GameObject player;
    public bool Task_isTweening;
    public bool Quest_isTweening;

    public QuestTextTweener(Text QuestText, Text TaskText, GameObject player)
    {
        this.TaskText = TaskText;
        this.QuestText = QuestText;
        this.player = player;

        Task_isTweening = false;
        Quest_isTweening = false;

        TaskTextAbsolutePosition = TaskText.transform.position - player.transform.position;
        QuestTextAbsolutePosition = QuestText.transform.position - player.transform.position;
    }

    public void StartQuestTweening()
    {
        if (Quest_isTweening) return;
        Quest_isTweening = true;
    }

    public void StartTaskTweening()
    {
        if (Task_isTweening) return;
        Task_isTweening = true;
    }

    public void Update()
    {
        if (Task_isTweening)
            TaskTweenUpdate();

        if (Quest_isTweening)
            QuestTweenUpdate();

        ResetTaskPosition();
        ResetQuestPosition();
    }

    Text TaskText;
    Vector3 TaskTextAbsolutePosition;
    Vector3 taskTargetPosition = new Vector3(10, 0, 0);
    float taskSpeed = 2.0f;
    float taskThreshold = 0.5f;
    void TaskTweenUpdate()
    {
        Vector3 direction = taskTargetPosition + TaskText.transform.position;
        if (direction.magnitude > taskThreshold)
        {
            direction.Normalize();
            TaskText.transform.position = TaskText.transform.position + direction * taskSpeed * Time.deltaTime;
        }
        else
        {
            // Without this game object jumps around target and never settles
            TaskText.transform.position = taskTargetPosition;
            Task_isTweening = false;
        }
    }

    Text QuestText;
    Vector3 QuestTextAbsolutePosition;
    Vector3 questTargetPosition = new Vector3(2, 0, 0);
    float questSpeed = 2.0f;
    float questThreshold = 0.5f;
    void QuestTweenUpdate()
    {
        Vector3 direction = questTargetPosition + QuestText.transform.position;
        if (direction.magnitude > questThreshold)
        {
            direction.Normalize();
            QuestText.transform.position = QuestText.transform.position + direction * questSpeed * Time.deltaTime;
        }
        else
        {
            // Without this game object jumps around target and never settles
            QuestText.transform.position = questTargetPosition;
            Quest_isTweening = false;
        }
    }
    void ResetTaskPosition()
    {
        if (Task_isTweening) return;
        TaskText.transform.position = player.transform.position + TaskTextAbsolutePosition;
    }

    void ResetQuestPosition()
    {
        if (Quest_isTweening) return;
        QuestText.transform.position = player.transform.position + QuestTextAbsolutePosition;
    }
}