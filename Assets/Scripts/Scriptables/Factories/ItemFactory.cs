using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EnumBase;

[CreateAssetMenu(menuName = "LSW/" + "Factories/" + nameof(ItemFactory))]
public class ItemFactory : ScriptableObject
{
    public UiInventoryItem UiItemPrefab;
    private Queue<UiInventoryItem> uiItemPool = new Queue<UiInventoryItem>();
    public List<UIItemSet> itemSets;

    public UiInventoryItem GetUiItem(ItemTypes itemType)
    {
        UiInventoryItem ins;
        if (uiItemPool.Count > 0)
        {
            ins = uiItemPool.Dequeue();
            ins.transform.position = Vector3.zero;
            ins.gameObject.SetActive(true);
            return ins;
        }

        ins = Instantiate(UiItemPrefab);
        return ins;
    }

    public Sprite GetItemImage(ItemTypes item)
    {
        if (itemSets == null || itemSets.Count <= 0) return null;
        Dictionary<ItemTypes, UIItemSet> set = itemSets.ToDictionary(p => p.itemType, p => p);
        return set[item].prop.itemSprite;
    }

    public int GetItemCost(ItemTypes item)
    {
        if (itemSets == null || itemSets.Count <= 0) return 0;
        Dictionary<ItemTypes, UIItemSet> set = itemSets.ToDictionary(p => p.itemType, p => p);
        return set[item].prop.itemCost;
    }

    public ItemProps GetItemProps(ItemTypes item)
    {
        if (itemSets == null || itemSets.Count <= 0) return null;
        Dictionary<ItemTypes, UIItemSet> set = itemSets.ToDictionary(p => p.itemType, p => p);
        return set[item].prop;
    }

    public void Recycle(GameObject item)
    {
        item.gameObject.SetActive(false);
        uiItemPool.Enqueue(item.GetComponent<UiInventoryItem>());
    }

    [System.Serializable]
    public struct UIItemSet
    {
        public ItemTypes itemType;
        public ItemProps prop;
    }
}
