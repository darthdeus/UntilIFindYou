using System;
using UnityEngine;

namespace Assets
{
    public class DialingBook : MonoBehaviour
    {
        public string CurrentAddress;
        private GateSystem _gateSystem;
        private GameObject _player;
        private AudioSource _success;
        private AudioSource _failure;
        private bool _readyToGo = false;

        void Start()
        {
            _gateSystem = GateSystem.Find();
            var sounds = GetComponents<AudioSource>();

            foreach (var sound in sounds)
            {
                if (sound.clip.name == "Success")
                {
                    _success = sound;
                }
                else if (sound.clip.name == "Error")
                {
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

                SetVisible(false);
            }
        }

        public void Activate(GameObject player)
        {
            if (!_readyToGo)
            {
                _player = player;
                ResetBook();
                ResetRunes();
                MoveBook();
                SetVisible(true);
            }
        }

        public void SetSlot(string slot)
        {
            if (CurrentAddress.Length < 5)
            {
                CurrentAddress += slot;
                Debug.Log(String.Format("SETTING: {0}", CurrentAddress));

                if (CurrentAddress.Length == 5)
                {
                    Debug.Assert(_player != null, "Dialing wasn't activated, player is missing");

                    if (_gateSystem.CheckCorrectAddress(CurrentAddress))
                    {
                        _success.Play();
                        _readyToGo = true;
                    }
                    else
                    {
                        _failure.Play();
                        SetVisible(false);
                    }
                }
            }
        }

        public void UnsetSlot()
        {
            if (CurrentAddress.Length > 0 && CurrentAddress.Length < 5)
            {
                CurrentAddress = CurrentAddress.Substring(0, CurrentAddress.Length - 1);
            }

            Debug.Log(String.Format("UNSETTING: {0}", CurrentAddress));
        }

        public static DialingBook Find()
        {
            var objects = GameObject.FindGameObjectsWithTag("DialingBook");
            Debug.Assert(objects.Length == 1, "There should be only one DialingBook");

            var manager = objects[0].GetComponent<DialingBook>();
            Debug.Assert(manager != null, "Found a DialingBook without the appropriate component");
            return manager;
        }

        // PRIVATE
        private void SetVisible(bool visible)
        {
            GetComponent<SpriteRenderer>().enabled = visible;
            foreach (var r in GetComponentsInChildren<SpriteRenderer>())
            {
                r.enabled = visible;
            }
        }

        private void ResetRunes()
        {
            foreach (var r in GetComponentsInChildren<SpriteRenderer>())
            {
                r.material.color = Color.white;
            }

            foreach (var rune in GetComponentsInChildren<Rune>())
            {
                rune.Reset();
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