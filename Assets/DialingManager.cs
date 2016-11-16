using System;
using System.Collections;
using UnityEngine;

namespace Assets {

    public class DialingManager : MonoBehaviour {
        private string _currentAddress;
        private GateSystem _gateSystem;
        private GameObject _player;

        void Start() {
            _gateSystem = GateSystem.FindGateSystem();      
        }

        public void Activate(GameObject player) {
            _player = player;
            Debug.Log("Activated");
        }

        public void SetSlot(string slot) {
            _currentAddress += slot;
            Debug.Log("Dialed: " + slot);

            if (_currentAddress.Length == 5) {
                Debug.Assert(_player != null, "Dialing wasn't activated, player is missing");
                _gateSystem.DialAndMoveAddress(_player, _currentAddress);
                Debug.Log("FINAL ADDRESS: " + _currentAddress);
                _currentAddress = "";
            } 
        }
    }
}