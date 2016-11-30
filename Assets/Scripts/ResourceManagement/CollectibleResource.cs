using UnityEngine;

namespace Assets.Scripts.ResourceManagement {
    [RequireComponent(typeof(Collider2D))]
    public class CollectibleResource : MonoBehaviour {

        public PlayerInventory.ResourceType ResourceType;
        public int Amount = 1;

        public void Start() {
            var collider = GetComponent<Collider2D>();
            if (!collider.isTrigger)
            {
                Debug.LogWarning(string.Format("GameObject with CollectibleTool attached didn't have its collider set to isTrigger ... {0}", gameObject));
                collider.isTrigger = true;
            }
        }


        void OnTriggerEnter2D(Collider2D collider) {
            var inventory = collider.gameObject.GetComponent<PlayerInventory>();
            Debug.Log("Picking up item");
            if (inventory != null) {
                inventory.PickupResource(ResourceType, Amount);
                Destroy(gameObject);
            }
        }
    }
}
