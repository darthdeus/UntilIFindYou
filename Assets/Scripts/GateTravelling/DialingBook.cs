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
                
                ResetBook();
                ResetRunes();
            }

            //RaycastHit hit = new RaycastHit();
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Debug.DrawRay(ray.origin, ray.direction);

            //if (Physics.Raycast(ray, out hit))
            //{

            //    if (hit.collider.gameObject == this.gameObject) {       
            //        ResetBook();
            //        ResetRunes();
            //    }
            //}
        }

        public void Activate(GameObject player) {
            _player = player;
            SetVisible(true);
            MoveBook();
        }

        public void SetSlot(string slot) {
            CurrentAddress += slot;
            Debug.Log(String.Format("SETING: {0}", CurrentAddress));

            if (CurrentAddress.Length == 5) {
                Debug.Assert(_player != null, "Dialing wasn't activated, player is missing");

                if (_gateSystem.CheckCorrectAddress(CurrentAddress)) {
                    _success.Play();
                    _readyToGo = true;
                } else {
                    _failure.Play();
                    ResetBook();
                    ResetRunes();
                }
                
            }
        }

        public void UnsetSlot()
        {
            if (CurrentAddress.Length > 0) {
                CurrentAddress = CurrentAddress.Substring(0, CurrentAddress.Length - 1);
            }

            Debug.Log(String.Format("UNSETING: {0}", CurrentAddress));
        }

        public static DialingBook Find() {
            var objects = GameObject.FindGameObjectsWithTag("DialingBook");
            Debug.Assert(objects.Length == 1, "There should be only one DialingBook");

            var manager = objects[0].GetComponent<DialingBook>();
            Debug.Assert(manager != null, "Found a DialingBook without the appropriate component");
            return manager;
        }

        // PRIVATE
        private void SetVisible(bool visible) {
            GetComponent<SpriteRenderer>().enabled = visible;
            foreach (var r in GetComponentsInChildren<SpriteRenderer>()) {
                r.enabled = visible;
            }
        }

        private void ResetRunes()
        {
            foreach (var r in GetComponentsInChildren<SpriteRenderer>())
            {
                r.material.color = Color.white;
            }
        }

        private void ResetBook()
        {
            CurrentAddress = "";
            SetVisible(false);
        }

        private void MoveBook()
        {
            var x = _player.transform.position.x;
            var y = _player.transform.position.y;
            gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
        }
    }
}