using System;
using System.Collections;
using UnityEngine;

namespace Assets {
    public class DialingManager : MonoBehaviour {
        public string CurrentAddress;
        private GateSystem _gateSystem;
        private GameObject _player;

        void Start() {
            _gateSystem = GateSystem.Find();
            SetVisible(false);
        }

        public void Activate(GameObject player) {
            _player = player;
            SetVisible(true);
            Debug.Log("Activated");
        }

        public void SetSlot(string slot) {
            CurrentAddress += slot;
            Debug.Log("Dialed: " + slot);

            if (CurrentAddress.Length == 5) {
                Debug.Assert(_player != null, "Dialing wasn't activated, player is missing");
                if (_gateSystem.DialAndMoveAddress(_player, CurrentAddress)) {
                    Debug.Log("Address dialed successfully " + CurrentAddress);
                } else {
                    Debug.Log("Invalid address " + CurrentAddress);
                }
                CurrentAddress = "";
            }
        }

        public static DialingManager Find() {
            var objects = GameObject.FindGameObjectsWithTag("DialingManager");
            Debug.Assert(objects.Length == 1, "There should be only one DialingManager");

            var manager = objects[0].GetComponent<DialingManager>();
            Debug.Assert(manager != null, "Found a DialingManager without the appropriate component");
            return manager;
        }

        public void SetVisible(bool visible) {
            GetComponent<SpriteRenderer>().enabled = visible;
            foreach (var renderer in GetComponentsInChildren<SpriteRenderer>()) {
                renderer.enabled = visible;
            }
        }
    }
}