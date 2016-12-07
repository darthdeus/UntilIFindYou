using System;
using UnityEngine;
using UnityEngine.UI;

public class QuestTextTweener
{
    // TODO: QuestTextController "GetNextTask()" method
    // TODO: Change Quest Interface font size depending on resolution
    // TODO: Implement Quest and Task events to change texts instead of non-stop refreshing
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

    public void StartTweening(Quest _quest)
    {
        questTweener.StartTweening(_quest);
    }

    public void StartTweening(Task _task)
    {
        taskTweener.StartTweening(_task);
    }
}