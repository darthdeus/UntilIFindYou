using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ResourceManagement {
    public class ResourceInfoUpdater : MonoBehaviour {
        private PlayerInventory _playerInventory;
        public GameObject txtAvailableTools;
        public GameObject txtNoTools;
        public GameObject axeIcon;
        public GameObject pickaxeIcon;
        public Text txtWoodCount;
        public Text txtStoneCount;

        void Start() {
            _playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        }

        void Update() {
            txtNoTools.SetActive(_playerInventory.Tools.Count == 0);
            txtAvailableTools.SetActive(_playerInventory.Tools.Count > 0);

            axeIcon.GetComponent<SpriteRenderer>().enabled = _playerInventory.HasTool(PlayerInventory.ToolType.Axe);
            pickaxeIcon.GetComponent<SpriteRenderer>().enabled = _playerInventory.HasTool(PlayerInventory.ToolType.Pickaxe);

            txtWoodCount.text = string.Format("Wood: {0}",
                                              _playerInventory.ResourceCount(PlayerInventory.ResourceType.Wood));
            txtStoneCount.text = string.Format("Stone: {0}",
                                               _playerInventory.ResourceCount(PlayerInventory.ResourceType.Stone));
        }
    }

    public static class GameObjectChildFinder {
        public static GameObject FindByNameRecursively(this GameObject gameObject, string name, bool allowNull = true) {
            var transform = gameObject.GetComponent<Transform>();
            foreach (var childTransform in transform.GetComponentsInChildren<Transform>()) {
                if (childTransform.gameObject.name.Equals(name)) {
                    return childTransform.gameObject;
                }

                return childTransform.gameObject.FindByNameRecursively(name);
            }

            if (allowNull) {
                return null;
            } else {
                throw new InvalidOperationException(string.Format(
                                                        "Child with the name {0} not found on gameObject {1}", name,
                                                        gameObject));
            }
        }
    }
}