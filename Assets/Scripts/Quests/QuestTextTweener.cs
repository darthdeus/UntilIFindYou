using System;
using UnityEngine;
using UnityEngine.UI;

public class QuestTextTweener
{
    public event EventHandler Tween;
    TaskTweener taskTweener;
    QuestTweener questTweener;
    public QuestTextTweener(Text QuestText, Text TaskText, GameObject player)
    {
        taskTweener = new TaskTweener(this, player, TaskText);
        questTweener = new QuestTweener(this, player, QuestText);
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