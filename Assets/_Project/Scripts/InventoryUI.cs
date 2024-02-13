using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Transform itemContainer;
    [SerializeField] private Transform buttonPrefab;

    [SerializeField] private Inventory playerInventory;

    private bool isOpen;

    private void OnEnable()
    {
        playerInventory.OnItemAdded += UpdateInventory;
        playerInventory.OnItemRemoved += UpdateInventory;
        InputManager.input.Player.Inventory.performed += ToggleInventory;
    }
    private void OnDisable()
    {
        playerInventory.OnItemAdded -= UpdateInventory;
        playerInventory.OnItemRemoved -= UpdateInventory;
        InputManager.input.Player.Inventory.performed -= ToggleInventory;
    }

    private void ToggleInventory(InputAction.CallbackContext context)
    {
        if (!isOpen)
        {
            OpenInventory();
        }
        else
        {
            CloseInventory();
        }
    }

    private void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        isOpen = true;
    }
    private void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        isOpen = false;
    }

    public void UpdateInventory()
    {
        var itemsInInventory = playerInventory.GetItems();
        for (int i = itemContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(itemContainer.GetChild(i).gameObject);
        }

        foreach (var item in itemsInInventory)
        {
            CreateItemButton(item);
        }
    }

    private void CreateItemButton(Inventory.Item newItem)
    {
        var newButton = Instantiate(buttonPrefab, itemContainer);
        newButton.name = newItem.info.name + "_Button";

        //Terrible, but it gets the job done in this case.
        newButton.GetComponentsInChildren<Image>()[1].sprite = newItem.info.icon;
        newButton.GetComponentInChildren<TextMeshProUGUI>().text = newItem.amountStored.ToString();
    }
}
