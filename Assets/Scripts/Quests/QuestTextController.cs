using System;
using UnityEngine;
using UnityEngine.UI;

public class QuestTextController : MonoBehaviour
{
    public Quest _quest
    {
        get
        {
            return quest;
        }
        set
        {
            Task nextTask;
            if (value != null)
            {
                quest = value;
                _tweener.StartTweening(quest);
                nextTask = GetNextTask(quest);
                _tweener.StartTweening(nextTask);
                if (nextTask.isCompleted)
                    OnUpdate += RollTasks;
            }
            else if (value == null && quest != null)
            {
                _tweener.StartTweening(quest);
                nextTask = GetNextTask(quest);
                _tweener.StartTweening(nextTask);
                if (nextTask.isCompleted)
                    OnUpdate += RollTasks;
                quest = value;
            }
        }
    }
    event EventHandler OnUpdate;
    Quest quest;
    Text questText;
    public Text taskText;
    int taskIndex;
    QuestTextTweener _tweener;

    // Use this for initialization
    void Start()
    {
        questText = gameObject.GetComponent<Text>();
        taskIndex = 0;

        questText.text = "";
        taskText.text = "";

        _tweener = new QuestTextTweener(questText, taskText, GameObject.FindWithTag("Player"));
    }

    void RollTasks(object sender, EventArgs e)
    {
        Task nextTask = GetNextTask(quest);
        if (!nextTask.isCompleted)
        {
            if (_tweener.StartTweening(nextTask))
                OnUpdate -= RollTasks;
        }
        else if (!_tweener.StartTweening(nextTask))
            taskIndex--;
    }

    // Update is called once per frame
    void Update()
    {
        if (OnUpdate != null)
            OnUpdate(this, EventArgs.Empty);
        _tweener.TweeningUpdate();
    }

    // Checks tasks of active quest and returns the next task or the first unfinished task (in case one task has become unfinished after completion) 
    Task GetNextTask(Quest quest)
    {
        if (quest != null)
        {
            // Check for task that used to be finished but is no longer finished //
            for (int i = 0; i < quest.Tasks.Count; i++)
                if (i >= taskIndex) break;
                else if (!quest.Tasks[i].isCompleted && i > taskIndex)
                {
                    taskIndex = i;
                    return quest.Tasks[taskIndex];
                }
            // Return the next task no matter if it was finished or not // 
            if (quest.Tasks[taskIndex].isCompleted)
                if (taskIndex + 1 < quest.Tasks.Count)
                    return quest.Tasks[taskIndex++];
                else
                    return null;
            else return quest.Tasks[taskIndex];
        }
        // Return null in case all tasks have been completed or there is no quest active for this instance. //
        return null;
    }
}