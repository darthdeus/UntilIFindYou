using UnityEngine;

namespace Assets.Scripts.ResourceManagement {
    [RequireComponent(typeof(Collider2D))]
    public class CollectibleTool : MonoBehaviour {
        public PlayerInventory.ToolType ToolType;

        public void Start() {
            var collider = GetComponent<Collider2D>();
            if (!collider.isTrigger) {
                Debug.LogWarning(string.Format("GameObject with CollectibleTool attached didn't have its collider set to isTrigger ... {0}", gameObject));
                collider.isTrigger = true;
            }
        }
        
        void OnTriggerEnter2D(Collider2D collider) {
            var inventory = collider.gameObject.GetComponent<PlayerInventory>();
            Debug.Log("Picking up item");
            if (inventory != null) {
                inventory.PickupTool(ToolType);
                Destroy(gameObject);
            }
        }
    }
}
