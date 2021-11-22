using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumBase;

[CreateAssetMenu(menuName = "LSW/" + "Props/" + nameof(ItemProps))]
public class ItemProps : ScriptableObject
{
    public ItemTypes itemType;
    public ItemClasses itemClass;
    public PlayerBodyParts bodyPart;
    public Sprite itemSprite;
    public int itemAmt, itemCost;
}
