using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class SO_ItemInfo : ScriptableObject
{
    [Header("---Item Info---")]
    public Sprite icon;
    public bool isStackable;

    [Header("---Value---")]
    public int price;
}
