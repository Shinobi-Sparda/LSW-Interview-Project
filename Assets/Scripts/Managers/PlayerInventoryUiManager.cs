using EnumBase;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryUiManager : InventoryUI
{
    private Player player;
    public Button playerInventoryButton;

    public override void Start()
    {
        player = FindObjectOfType<Player>();
        inventory = player.inventory;
        base.Start();
        playerInventoryButton.onClick.AddListener(delegate { MenuActiveStatus(); });
        player.onInventoryChanged += UpdateInventoryItem;
    }

    private void MenuActiveStatus()
    {
        if (inventoryMenu.gameObject.activeSelf)
        {
            CloseMenu();
            return;
        }

        inventoryMenu.gameObject.SetActive(true);
        UiManager.instance.onInventoryMenuOpened?.Invoke();
        Time.timeScale = 0;
    }

    private void SetupInventoryItem(ItemTypes itemType)
    {
        UiInventoryItem uiItem = FactoryManager.instance.itemFactory.GetUiItem(itemType);
        if (!UiManager.instance.isShopOpen)
            uiItem.transform.SetParent(itemContainer, false);
        else
            uiItem.transform.SetParent(UiManager.instance.shopInventoryUiManager.plyrItemContainer, false);

        uiItem.transform.localPosition = GetUIPosition(itemsList.Count);
        uiItem.Setup(itemType, inventory.GetItemAmt(itemType), inventory.inventoryType);
        itemsList.Add(uiItem);
        UiManager.instance.onInventoryMenuOpened?.Invoke();
    }

    public override void UpdateInventoryItem(ItemTypes itemType)
    {
        base.UpdateInventoryItem(itemType);

        if (!updated)
            SetupInventoryItem(itemType);
    }

    public void ShowItemsInshop(Transform shopContainer)
    {
        foreach (var item in itemsList)
        {
            item.transform.SetParent(shopContainer, false);
        }
    }

    public void ShowItemsinInventory()
    {
        foreach (var item in itemsList)
        {
            item.transform.SetParent(itemContainer, false);
        }
    }
}
