using System;
using UnityEngine;
using UnityEngine.UI;

public class QuestTextTweener
{
    public event EventHandler Tween;
    TaskTweener taskTweener;
    QuestTweener questTweener;
    public QuestTextTweener(Text QuestText, Text TaskText, GameObject player, GameObject DefaultQuestPosition, GameObject DefaultTaskPosition)
    {
        taskTweener = new TaskTweener(this, TaskText, DefaultTaskPosition);
        questTweener = new QuestTweener(this, QuestText, DefaultQuestPosition);
    }

    public void TweeningUpdate()
    {
        if (Tween != null)
            Tween(this, EventArgs.Empty);
    }

    public bool StartTweening(Quest _quest)
    {
        return questTweener.StartTweening(_quest);
    }

    public bool StartTweening(Task _task)
    {
        return taskTweener.StartTweening(_task);
    }
}