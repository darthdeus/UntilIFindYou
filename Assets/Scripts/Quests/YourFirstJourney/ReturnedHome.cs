using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Quests.YourFirstJourney
{
    class ReturnedHome : MonoBehaviour
    {
        public Quest AssociatedQuest;
        public Task AssociatedTask;
        public DialingBook DialingBook;
        public GameObject player;
        PlayerInventory _playerInventory;

        // Use this for initialization
        void Start()
        {
            _playerInventory = player.GetComponent<PlayerInventory>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!AssociatedTask.isCompleted && AssociatedQuest.isActive() && DialingBook.CurrentAddress == "abcde" && _playerInventory.HasTool(PlayerInventory.ToolType.Axe))
            {
                AssociatedTask.UpdateStatus();
            }
        }
    }
}
