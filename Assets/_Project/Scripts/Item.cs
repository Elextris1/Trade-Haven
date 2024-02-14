using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private SO_ItemInfo itemInfo;
    [SerializeField] public int amount = 1;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.TryGetComponent<Inventory>(out var otherInventory))
        {
            if (otherInventory.TryAddItem(itemInfo, amount))
            {
                Destroy(gameObject);
            }
        }
    }
}
