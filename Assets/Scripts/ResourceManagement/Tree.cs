using UnityEngine;
using System.Linq;
using System.Collections;

namespace Assets.Scripts.ResourceManagement
{
    public class Tree : MonoBehaviour
    {
        public int capacity = 10;

        public Sprite bottomFullSprite;
        public Sprite bottomEmptySprite;
        public AudioClip chopSound;

        private SpriteRenderer _bottomRenderer;
        private GameObject _player;
        private SpriteRenderer _topRenderer;
        private PlayerInventory _inventory;

        public bool hasWood
        {
            get { return capacity > 0; }
        }

        void Start()
        {
            _player = GameObject.FindWithTag("Player");

            _inventory = _player.GetComponent<PlayerInventory>();
            Debug.Assert(_inventory != null);

            var childRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
            _bottomRenderer = GetComponent<SpriteRenderer>();

            var list = childRenderers.ToList();
            list.Remove(_bottomRenderer);
            childRenderers = list.ToArray();

            if (childRenderers.Length != 1)
            {
                Debug.LogError("Tree script only works when there's one child with a single sprite renderer, got " + childRenderers.Length + " on " +
                               gameObject.name);
            }

            _topRenderer = childRenderers[0];
        }

        void Update()
        {
            if (hasWood)
            {
                _topRenderer.enabled = true;
                _bottomRenderer.sprite = bottomFullSprite;
            }
            else
            {
                _topRenderer.enabled = false;
                _bottomRenderer.sprite = bottomEmptySprite;
            }
        }

        void OnMouseUp()
        {
            if (!hasWood) return;

            if (_inventory.HasTool(PlayerInventory.ToolType.Axe) &&
             _inventory.PickupResource(PlayerInventory.ResourceType.Wood, 1))
            {
                _player.GetComponent<AudioSource>().PlayOneShot(chopSound);
                capacity--;
            }
        }
    }
}