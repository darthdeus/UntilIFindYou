﻿using UnityEngine;

namespace Assets.Scripts.Quests.SearchingForClues
{
    class Task_DiscoverPlanet : Task
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
            return "Discover a new planet using the clue.";
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
