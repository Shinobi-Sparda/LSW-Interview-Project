using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiInventoryItem : MonoBehaviour
{
    public Button button, popUpbutton;
    public Text quantityText, popUpButtonText;

    public Image image;
    private EnumBase.InventoryTypes currentInventory;
    public EnumBase.ItemTypes _itemType;

    public void Setup(EnumBase.ItemTypes itemType, int amt, EnumBase.InventoryTypes inventory)
    {
        currentInventory = inventory;
        image.sprite = FactoryManager.instance.itemFactory.GetItemImage(itemType);
        _itemType = itemType;
        quantityText.text = amt.ToString();
        popUpbutton.gameObject.SetActive(false);
        UiManager.instance.onInventoryMenuOpened += OnItemCLicked;

        //button.onClick.RemoveAllListeners();
        //button.onClick.AddListener(delegate { OnItemCLicked(); });
    }

    private void OnItemCLicked()
    {
        if (UiManager.instance.isShopOpen)
        {
            if (currentInventory == EnumBase.InventoryTypes.PlayerInventory)
            {
                popUpButtonText.text = "sell";
                popUpbutton.onClick.RemoveAllListeners();
                popUpbutton.gameObject.SetActive(true);
                popUpbutton.onClick.AddListener(delegate { UiManager.instance.SellItem(_itemType); });
            }

            else if (currentInventory == EnumBase.InventoryTypes.ShopInventory)
            {
                popUpButtonText.text = "buy";
                popUpbutton.onClick.RemoveAllListeners();
                popUpbutton.gameObject.SetActive(true);
                popUpbutton.onClick.AddListener(delegate { UiManager.instance.BuyItem(_itemType); });
            }
        }

        else
        {
            popUpButtonText.text = "use";
            popUpbutton.onClick.RemoveAllListeners();
            popUpbutton.gameObject.SetActive(true);
            popUpbutton.onClick.AddListener(delegate { UiManager.instance.player.UseItem(_itemType); });
        }
    }

    public void UpdateUiItem(int itemAmt)
    {
        quantityText.text = itemAmt.ToString();
        if (itemAmt == 0)
        {
            if (currentInventory == EnumBase.InventoryTypes.PlayerInventory)
                UiManager.instance.plyrInventoryUiManager.ResetItemPositions(this);

            else if (currentInventory == EnumBase.InventoryTypes.ShopInventory)
                UiManager.instance.shopInventoryUiManager.ResetItemPositions(this);

            FactoryManager.instance.itemFactory.Recycle(this.gameObject);
        }
    }
}
