using System;
using System.Collections;
using System.Threading;
using UnityEngine;

namespace Assets {
    public class DialingBook : MonoBehaviour {
        public string CurrentAddress;
        private GateSystem _gateSystem;
        private GameObject _player;
        private AudioSource _success;
        private AudioSource _failure;
        private bool _readyToGo = false;

        void Start() {
            _gateSystem = GateSystem.Find();
            var sounds = GetComponents<AudioSource>();

            foreach (var sound in sounds) {
                if (sound.clip.name == "Success") {
                    _success = sound;
                } else if (sound.clip.name == "Error") {
                    _failure = sound;
                }
            }

            SetVisible(false);
        }

        void Update()
        {
            // GATE TRAVELING
            if (!_success.isPlaying && _readyToGo && _player != null)
            {
                _gateSystem.DialAndMoveAddress(_player, CurrentAddress);
                _readyToGo = false;
                CurrentAddress = "";
                SetVisible(false);

                foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
                {
                    renderer.material.color = Color.white;
                }
            }
        }

        public void Activate(GameObject player) {
            _player = player;
            SetVisible(true);

            var x = _player.transform.position.x;
            var y = _player.transform.position.y;
            gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
        }

        public void SetSlot(string slot) {
            CurrentAddress += slot;
            Debug.Log("Dialed: " + slot);

            if (CurrentAddress.Length == 5) {
                Debug.Assert(_player != null, "Dialing wasn't activated, player is missing");

                if (_gateSystem.CheckCorrectAddress(CurrentAddress)) {
                    _success.Play();
                    _readyToGo = true;
                    //GetComponent<SpriteRenderer>().material.color = Color.green;
                } else {
                    CurrentAddress = "";
                    _failure.Play();
                }
                SetVisible(false);
            }
        }

        public static DialingBook Find() {
            var objects = GameObject.FindGameObjectsWithTag("DialingBook");
            Debug.Assert(objects.Length == 1, "There should be only one DialingBook");

            var manager = objects[0].GetComponent<DialingBook>();
            Debug.Assert(manager != null, "Found a DialingBook without the appropriate component");
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