using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnShopInventoryChanged(EnumBase.ItemTypes itemType);

public class Shop : MonoBehaviour, IInteractable
{
    public OnShopInventoryChanged onInventoryChanged;

    public InventoryObject inventory;
    public List<EnumBase.ItemTypes> shopItems = new List<EnumBase.ItemTypes>();

    private void Awake()
    {
        inventory.inventoryType = EnumBase.InventoryTypes.ShopInventory;
    }

    private void Start()
    {
        PopulateShop();
    }

    private void PopulateShop()
    {
        foreach (EnumBase.ItemTypes item in shopItems)
            inventory.AddItem(item, Random.Range(10, 20));

        UiManager.instance.shopInventoryUiManager.SetUpShop();
    }

    public void InteractionAction()
    {
        UiManager.instance.shopInventoryUiManager.OpenShopMenu();
    }

    public void RemoveItem(EnumBase.ItemTypes item)
    {
        
    }

    public void BuyItem(EnumBase.ItemTypes itemType)
    {
        inventory.AddItem(itemType, 1);
        onInventoryChanged?.Invoke(itemType);
    }

    public void SellItem(EnumBase.ItemTypes itemType)
    {
        inventory.RemoveItem(itemType, 1);
        onInventoryChanged?.Invoke(itemType);
    }
}
