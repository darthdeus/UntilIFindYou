﻿using UnityEngine;

namespace Assets.Scripts.Quests.FallingIntoTrap
{
    class Task_AnswerQuestion : Task
    {
        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            isCheckAllowed = false;
        }

        public override string GetDescription()
        {
            return "Find an answer to skull's question.";
        }

        public override string GetProgress()
        {
            if (GetStatus())
                return "Success!";
            else
                return "Not Finished Yet.";
        }

        public override bool GetStatus()
        {
            return isCompleted;
        }

        public override void UpdateStatus_DONOTCALL()
        {
            isCompleted = true;
        }
    }
}
