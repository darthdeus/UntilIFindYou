using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerQuestController : MonoBehaviour
{
    public List<Text> QuestTexts;
    List<QuestTextController> QuestControllers;
    public List<Quest> Quests;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        QuestControllers = new List<QuestTextController>();
        foreach (var questText in QuestTexts)
            QuestControllers.Add(questText.GetComponent<QuestTextController>());
        foreach (Quest quest in Quests)
        {
            quest.OnStarted += PassQuestsToControllers;
            quest.OnStarted += TaskAlreadyCompletedCheck;
            quest.OnFinished += PassQuestsToControllers;
        }
    }

    void PassQuestsToControllers(object sender, System.EventArgs e)
    {
        int activeQuestCounter = 0;
        foreach (Quest _quest in Quests)
            if (_quest.isActive())
            {
                // Quest Status Refresher //
                _quest.UpdateStatus();
                // Quest Text Quest-Refresher //
                if (activeQuestCounter < QuestControllers.Count)
                    QuestControllers[activeQuestCounter]._quest = _quest;
                activeQuestCounter++;
            }
        ResetRemainingQuestTexts(activeQuestCounter);
    }

    void TaskAlreadyCompletedCheck(object sender, System.EventArgs e)
    {
        foreach (Quest quest in Quests)
            foreach (Task task in quest.Tasks)
                if (task.isCheckAllowed)
                    task.CheckStatus();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ResetRemainingQuestTexts(int activeQuestCounter)
    {
        while (activeQuestCounter < QuestControllers.Count)
        {
            QuestControllers[activeQuestCounter]._quest = null;
            activeQuestCounter++;
        }
    }
}