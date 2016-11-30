using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts {
    public class PlayerInventory : MonoBehaviour {
        public readonly HashSet<ToolType> Tools = new HashSet<ToolType>();
        public readonly Dictionary<ResourceType, int> Resources = new Dictionary<ResourceType, int>();
        public Text ResourceInfoText;

        public enum ResourceType {
            Wood,
            Stone
        }

        public enum ToolType {
            Axe,
            Pickaxe
        }

        public int ResourceCount(ResourceType type) {
            if (Resources.ContainsKey(type)) {
                return Resources[type];
            } else {
                return 0;
            }
        }

        public bool HasTool(ToolType type) {
            return Tools.Contains(type);
        }

        void Start() {
            var info = GameObject.Find("ResourceInfo");
            Debug.Assert(info != null);

            ResourceInfoText = info.GetComponent<Text>();
            Debug.Assert(ResourceInfoText != null);
        }

        void Update() {
            var builder = new StringBuilder();

            foreach (var tool in Tools) {
                builder.Append(tool.ToString());
                builder.Append(", ");
            }

            builder.AppendLine();
            foreach (var pair in Resources) {
                builder.Append(string.Format("{0}: {1}, ", pair.Key, pair.Value));
            }

            ResourceInfoText.text = builder.ToString();
        }

        public void PickupTool(ToolType toolType) {
            Tools.Add(toolType);
        }

        public void PickupResource(ResourceType resourceType, int amount) {
            if (Resources.ContainsKey(resourceType)) {
                Resources[resourceType] += amount;
            } else {
                Resources[resourceType] = amount;
            }
        }
    }
}