using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnumBase
{
    public enum InventoryTypes
    {
        PlayerInventory,
        ShopInventory
    }
    public enum QuestType
    {
        Gathering,
        Escort
    }

    public enum ItemClasses
    {
        Consumable,
        Equipment,
        Currency,
    }

    public enum PlayerAnimStates
    {
        Idle,
        MovingRight,
        MovingLeft,
        MovingUp,
        MovingDown
    }

    public enum ItemTypes
    {
        HeadTint,
        BodyTint,
        LegTint,
        Food,
        Bolts,
        Coin
    }

    public enum PlayerBodyParts
    {
        Head,
        Body,
        Leg
    }
}
