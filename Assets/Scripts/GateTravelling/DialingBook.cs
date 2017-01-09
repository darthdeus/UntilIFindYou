using System;
using UnityEngine;

namespace Assets
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class DialingBook : MonoBehaviour
    {
        public string CurrentAddress;
        private GateSystem _gateSystem;
        private GameObject _player;
        private AudioSource _success;
        private AudioSource _failure;
        private bool _readyToGo = false;
        private SpriteRenderer _spriteRenderer;
        private Animator _lightAnimator;
        event EventHandler OnTeleportAnim;

        public bool _isVisible
        {
            get { return _spriteRenderer.isVisible; }
        }

        void Start()
        {
            _gateSystem = GateSystem.Find();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _lightAnimator = GameObject.FindGameObjectWithTag("Light").GetComponent<Animator>();

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

            OnTeleportAnim += CallAnimationFlowchartBlock;
        }

        void Update()
        {
            // Gate Travelling Animation //
            if (OnTeleportAnim != null && _success.isPlaying)
            {
                OnTeleportAnim(this, EventArgs.Empty);
                OnTeleportAnim -= CallAnimationFlowchartBlock;
            }
            // GATE TRAVELLING
            if (!_success.isPlaying && _readyToGo && _player != null)
            {
                OnTeleportAnim += CallAnimationFlowchartBlock;
                _gateSystem.DialAndMoveAddress(_player, CurrentAddress);
                _readyToGo = false;
                _lightAnimator.SetBool("isDialing", false);
                SetVisible(false);
            }

            // Mouse click raycast to close
            if (Input.GetMouseButtonUp(0) && _isVisible)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                var hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

                if (hit)
                {
                    var go = hit.collider.gameObject;
                    var book = go.GetComponent<DialingBook>();
                    var rune = go.GetComponent<Rune>();
                    var gate = go.GetComponent<GateController>();

                    if (book != null || rune != null || gate != null)
                    {
                        Debug.Log("Book clicked");
                    }
                    else
                    {
                        SetVisible(false);
                        Debug.Log("Hiding book");
                    }
                }
                else if (_isVisible)
                {
                    SetVisible(false);
                    Debug.Log("Clicked outside the book, hiding");
                }
            }
        }

        public void Activate(GameObject player, GameObject gate)
        {
            if (!_readyToGo)
            {
                _player = player;
                ResetBook();
                ResetRunes();
                MoveBook(gate);
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
                        _lightAnimator.SetBool("isDialing", true);
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
            if (!visible) transform.position += new Vector3(2f, 2f, 2f);
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

        private void MoveBook(GameObject gate)
        {
            var x = gate.transform.position.x + 1;
            var y = gate.transform.position.y;
            gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
        }

        void CallAnimationFlowchartBlock(object sender, EventArgs e)
        {
            Fungus.Flowchart.BroadcastFungusMessage("EarthQuakeFade");
        }
    }
}