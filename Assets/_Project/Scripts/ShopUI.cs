using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public static ShopUI instance;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private Transform sellingItemContainer;
    [SerializeField] private Transform buyingItemContainer;
    [SerializeField] private Transform buttonPrefab;

    private Shop thisShop;

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
        ClearContainer(sellingItemContainer);
        ClearContainer(buyingItemContainer);

        foreach (var item in thisShop.sellingItemsList)
        {
            CreateButton(item, sellingItemContainer, true);
        }
        foreach (var item in thisShop.buyingItemsList)
        {
            CreateButton(item, buyingItemContainer, false);
        }
    }

    private void ClearContainer(Transform container)
    {
        for (int i = container.childCount -1;  i >= 0; i--)
        {
            Destroy(container.GetChild(i).gameObject);
        }
    }

    private void CreateButton(SO_ItemInfo item, Transform itemContainer, bool isBuyingButton)
    {
        var newButton = Instantiate(buttonPrefab, itemContainer);
        newButton.name = item.name + (isBuyingButton ? " Buy_Button" : " Sell_Button");

        //Terrible, but it gets the job done in this case.
        newButton.GetComponentsInChildren<Image>()[1].sprite = item.icon;

        var textBox = newButton.GetComponentInChildren<TextMeshProUGUI>();
        textBox.gameObject.SetActive(true);

        var adjustedPrice = isBuyingButton ? (int)(item.price + item.price * thisShop.Markup) : (int)(item.price - item.price * thisShop.Markup);
        textBox.text = adjustedPrice.ToString();

        if (isBuyingButton)
        {
            newButton.GetComponent<Button>().onClick.AddListener(() => thisShop.Buy(item));
        }
        else
        {
            newButton.GetComponent<Button>().onClick.AddListener(() => thisShop.Sell(item));
        }
    }
}
