using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Quests.YourFirstJourney
{
    class Task_BringAxeHome : Task
    {
        public GameObject player;
        PlayerInventory _playerInventory;

        private bool _isCompleted = false;

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            _playerInventory = player.GetComponent<PlayerInventory>();
        }
        public override string GetDescription()
        {
            return "Bring axe home.";
        }

        public override string GetProgress()
        {
            if (GetStatus())
                return "Success!";
            else
                return "You have not brought the axe home yet.";
        }

        public override bool GetStatus()
        {
            return _isCompleted;
        }

        public override void UpdateStatus_DONOTCALL()
        {
            _isCompleted = true;
        }
    }
}
