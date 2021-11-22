using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnumBase;

public abstract class InventoryUI : MonoBehaviour
{
    [HideInInspector]
    public InventoryObject inventory;
    public Transform inventoryMenu, itemContainer, iconStartPos;

    public Button closeButton;
    public List<UiInventoryItem> itemsList = new List<UiInventoryItem>();
    public bool updated = false;

    public virtual void Start()
    {
        closeButton.onClick.AddListener(delegate { CloseMenu(); });
        SetupInventory();
    }

    public void SetupInventory()
    {
        foreach (ItemTypes itemType in inventory.itemsList.Keys)
        {
            UiInventoryItem uiItem = FactoryManager.instance.itemFactory.GetUiItem(itemType);
            uiItem.transform.SetParent(itemContainer, false);
            uiItem.transform.localPosition = GetUIPosition(itemsList.Count);
            uiItem.Setup(itemType, inventory.GetItemAmt(itemType), inventory.inventoryType);
            itemsList.Add(uiItem);
        }
    }

    public virtual void UpdateInventoryItem(ItemTypes itemType)
    {
        updated = false;

        foreach (var uiItem in itemsList)
        {
            if (uiItem._itemType == itemType)
            {
                uiItem.UpdateUiItem(inventory.GetItemAmt(itemType));
                updated = true;
                break;
            }
        }
    }

    public virtual void CloseMenu()
    {
        inventoryMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void ResetItemPositions(UiInventoryItem item)
    {
        itemsList.Remove(item);
        for (int i = 0; i < itemsList.Count; i++)
        {
            itemsList[i].transform.localPosition = GetUIPosition(i);
        }
    }

    public Vector2 GetUIPosition(int index)
    {
        int noOfColumns = UiManager.instance.numberOfColumns;
        float xOffset = UiManager.instance.xSpaceBetweenItems;
        float yOffset = UiManager.instance.ySpaceBetweenItems;

        Vector2 newPos = new Vector2(iconStartPos.localPosition.x + (xOffset * (index % noOfColumns)), iconStartPos.localPosition.y + (-yOffset * (index / noOfColumns)));
        return newPos;
    }
}
