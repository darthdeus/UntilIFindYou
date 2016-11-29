using UnityEngine;
using UnityEngine.UI;

public class QuestTextController : MonoBehaviour
{
    public Quest _quest;
    public Text taskText;
    Text questText;
    // Use this for initialization
    void Start()
    {
        questText = gameObject.GetComponent<Text>();
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
                Task CurrentTask = GetFirstUnfinished();
                if (CurrentTask != null)
                    taskText.text = CurrentTask.GetDescription();
            }
            else
            {
                questText.text = _quest.GetTitle() + " ( Completed )";
                taskText.text = "";
            }
        }
    }

    Task GetFirstUnfinished()
    {
        foreach (var task in _quest.Tasks)
            if (!task.isCompleted)
                return task;
        return null;
    }
}