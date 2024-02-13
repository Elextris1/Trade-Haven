using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public static ShopUI instance;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private Transform itemContainer;
    [SerializeField] private Transform buttonPrefab;

    private Shop thisShop;

    [SerializeField] private List<SO_ItemInfo> stock = new List<SO_ItemInfo>();
    private void Awake()
    {
        #region Singleton
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        #endregion
    }

    public void OpenShop(Shop shop)
    {
        thisShop = shop;
        LoadShop();
        shopPanel.SetActive(true);
    }
    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }

    private void LoadShop()
    {
        for (int i = itemContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(itemContainer.GetChild(i).gameObject);
        }
        foreach (var item in stock)
        {
            CreateItemButton(item);
        }
    }

    private void CreateItemButton(SO_ItemInfo newItem)
    {
        var newButton = Instantiate(buttonPrefab, itemContainer);
        newButton.name = newItem.name + "_Button";

        //Terrible, but it gets the job done in this case.
        newButton.GetComponentsInChildren<Image>()[1].sprite = newItem.icon;
        newButton.GetComponentInChildren<TextMeshProUGUI>().text = newItem.price.ToString();
        newButton.GetComponent<Button>().onClick.AddListener(() => thisShop.Buy(newItem));
    }
}
