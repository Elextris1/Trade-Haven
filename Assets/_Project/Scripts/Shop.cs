using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour, IInteractable
{
    [SerializeField] private SO_ItemInfo gold;

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

        if (interactorInventory.HasSpace)
        {
            if (interactorInventory.RequestItem(gold, item.price))
            {
                interactorInventory.TryAddItem(item, 1);
            }
            else
            {
                Debug.Log("Not enough items in " + interactorInventory);
                return;
            }
        }
        else
        {
            Debug.Log("Not enough space in " + interactorInventory);
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

        if (interactorInventory.TryAddItem(gold, item.price))
        {
            interactorInventory.RequestItem(item, 1);
        }
        else
        {
            Debug.Log("Not enough space for gold");
        }

    }
}
