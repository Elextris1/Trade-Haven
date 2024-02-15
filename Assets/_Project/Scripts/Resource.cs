using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour, IInteractable
{
    [SerializeField] private SO_ItemInfo item;
    [SerializeField] private int yield;
    [SerializeField] private bool destroyOnInteract;

    public Transform interactor { get; set; }

    public void StartInteracting()
    {
        if (interactor.TryGetComponent<Inventory>(out var interactorInventory))
        {
            if (interactorInventory.TryAddItem(item, yield))
            {
                if (destroyOnInteract)
                {
                    Destroy(gameObject);
                }
                return;
            }
        }
        Instantiate(item.prefab);
    }

    public void StopInteracting()
    {

    }
}
