using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour, IInteractable
{
    [SerializeField] private SO_ItemInfo gold;
    [SerializeField] private float markup;
    public float Markup
    {
        get { return markup / 100; }
    }

    [field: SerializeField] public List<SO_ItemInfo> sellingItemsList { get; private set; }
    [field: SerializeField] public List<SO_ItemInfo> buyingItemsList { get; private set; }

    public Transform interactor { get; set; }

    public void StartInteracting()
    {
        InputManager.input.Player.Move.Disable();
        InputManager.input.Player.Fire.Disable();
        InputManager.input.UI.Enable();
        ShopUI.instance.OpenShop(this);
    }

    public void StopInteracting()
    {
        InputManager.input.Player.Move.Enable();
        InputManager.input.Player.Fire.Enable();
        InputManager.input.UI.Disable();
        ShopUI.instance.CloseShop();
    }

    public void Buy(SO_ItemInfo item)
    {
        if (!interactor.TryGetComponent<Inventory>(out var interactorInventory))
        {
            Debug.LogError("Inventory not found");
            return;
        }

        var adjustedPrice = (int)(item.price + item.price * Markup);
        if (interactorInventory.RequestItem(gold, adjustedPrice))
        {
            if (!interactorInventory.TryAddItem(item, 1))
            {
                var newItem = Instantiate(item.prefab, transform.position, Quaternion.identity).GetComponent<Item>();
                newItem.amount = 1;
            }
        }
        else
        {
            Debug.Log("Not enough gold in " + interactorInventory);
            return;
        }
    }

    public void Sell(SO_ItemInfo item)
    {
        if (!interactor.TryGetComponent<Inventory>(out var interactorInventory))
        {
            Debug.LogError("Inventory not found");
            return;
        }

        var adjustedPrice = (int)(item.price - item.price * Markup);
        if (interactorInventory.RequestItem(item, 1))
        {
            if (!interactorInventory.TryAddItem(gold, adjustedPrice))
            {
                var newItem = Instantiate(gold.prefab, transform.position, Quaternion.identity).GetComponent<Item>();
                newItem.amount = adjustedPrice;
            }
        }
    }
}
