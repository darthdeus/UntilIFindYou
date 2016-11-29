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
    }

    // Update is called once per frame
    void Update()
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

    void ResetRemainingQuestTexts(int activeQuestCounter)
    {
        while (activeQuestCounter < QuestControllers.Count)
        {
            QuestControllers[activeQuestCounter]._quest = null;
            activeQuestCounter++;
        }
    }
}