using UnityEngine;
using UnityEngine.UI;

public class QuestTextController : MonoBehaviour
{
    public Quest _quest;
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

    // Update is called once per frame
    void Update()
    {
        if (_quest == null)
        {
            questText.text = "";
            taskText.text = "";
        }
        else
        {
            if (!_quest.GetStatus())
            {
                questText.text = _quest.GetTitle();
                Task CurrentTask = GetNextTask();
                if (CurrentTask != null)
                    taskText.text = CurrentTask.GetDescription();
            }
            else
            {
                questText.text = _quest.GetTitle() + " ( Completed )";
                taskText.text = "";
            }
        }
        _tweener.StartTweening(_quest);
        _tweener.StartTweening(GetNextTask());
        _tweener.TweeningUpdate();
    }

    // Checks tasks of active quest and returns the next task or the first unfinished task (in case one task has become unfinished after completion) 
    Task GetNextTask()
    {
        if (_quest != null)
        {
            // Check for task that used to be finished but is no longer finished //
            for (int i = 0; i < _quest.Tasks.Count; i++)
                if (i > taskIndex) break;
                else if (!_quest.Tasks[i].isCompleted && i > taskIndex)
                {
                    taskIndex = i;
                    return _quest.Tasks[taskIndex];
                }
            // Return the next task no matter if it was finished or not // 
            if (_quest.Tasks[taskIndex].isCompleted)
                if (taskIndex + 1 < _quest.Tasks.Count)
                    return _quest.Tasks[++taskIndex];
                else
                    return null;
            else return _quest.Tasks[taskIndex];
        }
        // Return null in case all tasks have been completed or there is no quest active for this instance. //
        return null;
    }
}