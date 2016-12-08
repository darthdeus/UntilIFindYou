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
            // If new quest is assigned, add handlers to it //
            if (value != null)
            {
                quest = value;
                AddEventHandlersToQuest(quest);
            }
            _tweener.StartTweening(quest);      // Animation for the quest text
            nextTask = GetNextTask(quest);      // Prepare next task
            _tweener.StartTweening(nextTask);   // Animation for task text
            if (nextTask != null && nextTask.isCompleted)
                OnUpdate += RollTasks;
            // If quest is removed, remove handlers from it //
            if (value == null && quest != null)
            {
                RemoveEventHandlersFromQuest(quest);
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

    // Adds all basic events to a quest //
    void AddEventHandlersToQuest(Quest quest)
    {
        quest.OnFinished += QuestTweeningEvent;
        quest.OnMakeCompletable += QuestTweeningEvent;
        foreach (Task task in quest.Tasks)
        {
            task.OnStatusUpdate += TaskTweeningEvent;
            task.OnStatusUpdate += CheckQuestStatus;
        }
    }
    // Removes all basic events from a quest //
    void RemoveEventHandlersFromQuest(Quest quest)
    {
        quest.OnFinished -= QuestTweeningEvent;
        quest.OnMakeCompletable -= QuestTweeningEvent;
        foreach (Task task in quest.Tasks)
        {
            task.OnStatusUpdate -= TaskTweeningEvent;
            task.OnStatusUpdate -= CheckQuestStatus;
        }
    }

    // An event that checks the status of a quest (usually after finishing a task) //
    void CheckQuestStatus(object sender, EventArgs e)
    {
        quest.UpdateStatus();
    }
    // An event that attempts to tween the quest text //
    void QuestTweeningEvent(object sender, EventArgs e)
    {
        _tweener.StartTweening((Quest)sender);
        GetNextTask((Quest)sender);
    }
    // An event that attempts to tween the task text //
    void TaskTweeningEvent(object sender, EventArgs e)
    {
        _tweener.StartTweening((Task)sender);
        if (((Task)sender).isCompleted)
        {
            GetNextTask(quest);
            if (taskIndex < quest.Tasks.Count)
            {
                OnUpdate += RollTasks;
            }
        }
    }
    // Keeps tweening tasks and getting new ones until it gets an unfinished task //
    void RollTasks(object sender, EventArgs e)
    {
        Task nextTask = GetNextTask(quest);
        if (nextTask != null)
            if (!nextTask.isCompleted)
            {
                if (_tweener.StartTweening(nextTask))
                {
                    OnUpdate -= RollTasks;
                }
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
                else if (!quest.Tasks[i].isCompleted && i < taskIndex)
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