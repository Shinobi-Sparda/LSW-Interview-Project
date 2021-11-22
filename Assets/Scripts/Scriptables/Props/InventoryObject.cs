using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumBase;

[CreateAssetMenu(menuName = "LSW/" + "Props/" + nameof(InventoryObject))]
public class InventoryObject : ScriptableObject
{
    public Dictionary<ItemTypes, int> itemsList = new Dictionary<ItemTypes, int>();
    public InventoryTypes inventoryType;

    public int GetItemAmt(ItemTypes itemType)
    {
        if (!itemsList.ContainsKey(itemType))
            return 0;

        return itemsList[itemType];
    }

    public void AddItem(ItemTypes itemType, int amt)
    {
        if (!itemsList.ContainsKey(itemType))
        {
            itemsList.Add(itemType, amt);
        }

        else
        {
            itemsList[itemType] += amt;
        }
    }

    public void RemoveItem(ItemTypes itemType, int amt)
    {
        itemsList[itemType] -= amt;

        if (itemsList[itemType] <= 0)
        {
            itemsList.Remove(itemType);
        }
    }
}
