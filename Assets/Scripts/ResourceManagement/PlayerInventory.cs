using Fungus;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Quests.TheAdventureContinues;

namespace Assets.Scripts
{
    public class PlayerInventory : MonoBehaviour
    {
        public readonly HashSet<ToolType> Tools = new HashSet<ToolType>();
        public readonly Dictionary<ResourceType, int> Resources = new Dictionary<ResourceType, int>();
        public readonly Dictionary<ResourceType, int> VillageResources = new Dictionary<ResourceType, int>();
        public event System.EventHandler OnToolPickedUp;
        public event System.EventHandler OnResourcePickedUp;
        private GameObject _banditIcon;
        public Flowchart BanditChart;

        public int DefenseWoodAmount = 20;

        public enum ResourceType
        {
            Wood,
            Stone
        }

        public enum ToolType
        {
            Axe,
            Pickaxe
        }

        public Dictionary<ResourceType, ToolType> RequiredTools = new Dictionary<ResourceType, ToolType>() {
            {ResourceType.Wood, ToolType.Axe},
            {ResourceType.Stone, ToolType.Pickaxe}
        };

        public int ResourceCount(ResourceType type)
        {
            if (Resources.ContainsKey(type))
            {
                return Resources[type];
            }
            else
            {
                return 0;
            }
        }

        public bool HasTool(ToolType type)
        {
            return Tools.Contains(type);
        }

        public void PickupTool(ToolType toolType)
        {
            Tools.Add(toolType);
            if (OnToolPickedUp != null)
                OnToolPickedUp(this, EventArgs.Empty);
        }

        public bool PickupResource(ResourceType resourceType, int amount)
        {
            var requiredTool = RequiredTools[resourceType];
            if (HasTool(requiredTool))
            {
                if (Resources.ContainsKey(resourceType))
                {
                    Resources[resourceType] += amount;
                }
                else
                {
                    Resources[resourceType] = amount;
                }
                if (OnResourcePickedUp != null)
                    OnResourcePickedUp(this, EventArgs.Empty);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void VillageAttacked()
        {
            _banditIcon.GetComponent<SpriteRenderer>().enabled = true;
            VillageResources[ResourceType.Wood] = 0;
        }

        public void TransferResourcesForDefense()
        {
            TransferResourcesToVillage(ResourceType.Wood, ResourceCount(ResourceType.Wood));
        }

        public void TransferResourcesToVillage(ResourceType type, int count)
        {
            if (!VillageResources.ContainsKey(type))
            {
                VillageResources.Add(type, 0);
            }

            if (!Resources.ContainsKey(type))
            {
                Resources.Add(type, 0);
            }

            if (Resources[type] < count)
            {
                Debug.Log("Trying to transfer more resources than the player has.");
            }
            else
            {
                VillageResources[type] += count;
                Resources[type] -= count;
            }
        }

        public bool IsVillageDefendable()
        {
            return VillageResources.ContainsKey(ResourceType.Wood) &&
                   VillageResources[ResourceType.Wood] >= DefenseWoodAmount;
        }

        public void Start()
        {
            _banditIcon = GameObject.FindWithTag("BanditIcon");
        }

        public void Update()
        {
            if (IsVillageDefendable())
            {
                _banditIcon.GetComponent<SpriteRenderer>().enabled = false;
                BanditChart.SetBooleanVariable("isDefendable", true);
                GameObject.Find("Task_GiveWood").GetComponent<Task_GiveWood>().UpdateStatus();
            }
            else
            {
                _banditIcon.GetComponent<SpriteRenderer>().enabled = true;
                BanditChart.SetBooleanVariable("isDefendable", false);
            }
        }
    }
}