using System;
using System.Collections;
using System.Threading;
using UnityEngine;

namespace Assets {
    public class DialingManager : MonoBehaviour {
        public string CurrentAddress;
        private GateSystem _gateSystem;
        private GameObject _player;
        private AudioSource _audio;
        private bool _readyToGo = false;

        void Start() {
            _gateSystem = GateSystem.Find();
            _audio = GetComponent<AudioSource>();
            SetVisible(false);
        }

        void Update()
        {
            if (!_audio.isPlaying && _readyToGo && _player != null)
            {
                _gateSystem.DialAndMoveAddress(_player, CurrentAddress);
                _readyToGo = false;
                CurrentAddress = "";
            }
        }

        public void Activate(GameObject player) {
            _player = player;
            SetVisible(true);

            var x = _player.transform.position.x;
            var z = _player.transform.position.z;
            gameObject.transform.position = new Vector3(x, gameObject.transform.position.y, z);
        }

        public void SetSlot(string slot) {
            CurrentAddress += slot;
            Debug.Log("Dialed: " + slot);

            if (CurrentAddress.Length == 5) {
                Debug.Assert(_player != null, "Dialing wasn't activated, player is missing");

                if (_gateSystem.CheckCorrectAddress(CurrentAddress)) {
                    _audio.Play();
                    _readyToGo = true;
                } else {
                    CurrentAddress = "";
                }

                SetVisible(false);
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