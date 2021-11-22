using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnumBase;

public class ShopInventoryUiManager : InventoryUI
{
    private Shop shop;

    public Transform shopMenu;
    public Transform shopItemContainer, shopIconStartPos;
    public Transform plyrItemContainer, plyrIconStartPos;

    public override void Start()
    {
        shop = FindObjectOfType<Shop>();
        inventory = shop.inventory;
        base.Start();
        shop.onInventoryChanged += UpdateInventoryItem;
    }

    public void SetUpShop()
    {
        SetupInventory();
    }

    private void SetupInventoryItem(ItemTypes itemType)
    {
        UiInventoryItem uiItem = FactoryManager.instance.itemFactory.GetUiItem(itemType);
        uiItem.transform.SetParent(itemContainer, false);
        uiItem.transform.localPosition = GetUIPosition(itemsList.Count);
        uiItem.Setup(itemType, inventory.GetItemAmt(itemType), inventory.inventoryType);
        itemsList.Add(uiItem);
    }

    public override void UpdateInventoryItem(ItemTypes itemType)
    {
        base.UpdateInventoryItem(itemType);

        if (!updated)
            SetupInventoryItem(itemType);
    }

    public void OpenShopMenu()
    {
        if (UiManager.instance.isShopOpen)
            return;

        UiManager.instance.plyrInventoryUiManager.ShowItemsInshop(plyrItemContainer);
        shopMenu.gameObject.SetActive(true);

        UiManager.instance.isShopOpen = true;
        UiManager.instance.onInventoryMenuOpened?.Invoke();
        Time.timeScale = 0;
    }

    public override void CloseMenu()
    {
        UiManager.instance.isShopOpen = false;
        UiManager.instance.plyrInventoryUiManager.ShowItemsinInventory();

        base.CloseMenu();
    }
}
