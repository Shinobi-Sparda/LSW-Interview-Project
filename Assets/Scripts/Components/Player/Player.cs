using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void OnPlayerHealthChanged(int value);
public delegate void OnPlayerMoneyChanged(int value);
public delegate void OnPlayerInventoryChanged(EnumBase.ItemTypes itemType);

public class Player : MonoBehaviour
{
    [HideInInspector]
    public InteractionsController interactionsController;
    [HideInInspector]
    public MovementController movementController;
    public InventoryObject inventory;

    public OnPlayerHealthChanged onHealthChanged;
    public OnPlayerMoneyChanged onMoneyChanged;
    public OnPlayerInventoryChanged onInventoryChanged;

    private Animator animator;
    public ClipSet[] clipSets;

    public BodyPartSet[] bodyParts;

    public int health;
    public int money;

    private void Awake()
    {
        inventory.inventoryType = EnumBase.InventoryTypes.PlayerInventory;
    }

    void Start()
    {
        interactionsController = GetComponent<InteractionsController>();
        movementController = GetComponent<MovementController>();
        animator = GetComponent<Animator>();

        AddMoney(0);
        AddHealth(50);
    }

    public void UpdateAnimation(EnumBase.PlayerAnimStates animState)
    {
        ClipSet clipSet = clipSets.Where(x => x._animState == animState).FirstOrDefault();
        animator.CrossFade(clipSet.clip.name, 0.0001f);
    }

    public SpriteRenderer GetBodyPart(EnumBase.PlayerBodyParts bodyPart)
    {
        BodyPartSet partSet = bodyParts.Where(x => x._bodyPart == bodyPart).FirstOrDefault();
        return partSet.renderer;
    }

    public void AddHealth(int healthAmt)
    {
        health += healthAmt;
        onHealthChanged?.Invoke(health);
    }

    public void AddMoney(int cashAmt)
    {
        money += cashAmt;
        onMoneyChanged?.Invoke(money);
    }

    public void RemoveMoney(int amt)
    {
        money -= amt;
        onMoneyChanged?.Invoke(money);
    }

    [System.Serializable]
    public struct ClipSet
    {
        public EnumBase.PlayerAnimStates _animState;
        public AnimationClip clip;
    }

    [System.Serializable]
    public struct BodyPartSet
    {
        public EnumBase.PlayerBodyParts _bodyPart;
        public SpriteRenderer renderer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.transform.GetComponent<Item>();

        if (item != null)
        {
            switch (item.itemType)
            {
                case EnumBase.ItemTypes.HeadTint:
                    break;
                case EnumBase.ItemTypes.BodyTint:
                    break;
                case EnumBase.ItemTypes.LegTint:
                    break;
                case EnumBase.ItemTypes.Food:
                    break;
                case EnumBase.ItemTypes.Bolts:
                    break;
                case EnumBase.ItemTypes.Coin:
                    AddMoney(item.quantity);
                    break;
                default:
                    break;
            }

            if (item.itemType!= EnumBase.ItemTypes.Coin)
            {
                inventory.AddItem(item.itemType, item.quantity);
                onInventoryChanged?.Invoke(item.itemType);
            }
            
            Destroy(item.gameObject);
        }
    }

    public void UseItem(EnumBase.ItemTypes itemType)
    {
        ItemProps itemProp = FactoryManager.instance.itemFactory.GetItemProps(itemType);
        switch (itemProp.itemClass)
        {
            case EnumBase.ItemClasses.Consumable:
                AddHealth(itemProp.itemAmt);
                break;

            case EnumBase.ItemClasses.Equipment:
                int bodyPartIndex = 0;
                if (itemProp.bodyPart == EnumBase.PlayerBodyParts.Head)
                    bodyPartIndex = 0;
                else if (itemProp.bodyPart == EnumBase.PlayerBodyParts.Body)
                    bodyPartIndex = 1;
                else if (itemProp.bodyPart == EnumBase.PlayerBodyParts.Leg)
                    bodyPartIndex = 2;

                bodyParts[bodyPartIndex].renderer.sprite = itemProp.itemSprite;
                break;

            case EnumBase.ItemClasses.Currency:
                money += itemProp.itemAmt;
                break;

            default:
                break;  
        }

        inventory.RemoveItem(itemType, 1);
        onInventoryChanged?.Invoke(itemType);
    }

    public void BuyItem(EnumBase.ItemTypes itemType, int cost)
    {

        inventory.AddItem(itemType, 1);
        RemoveMoney(cost);
        onInventoryChanged?.Invoke(itemType);
    }

    public void SellItem(EnumBase.ItemTypes itemType)
    {
        inventory.RemoveItem(itemType, 1);
        AddMoney(FactoryManager.instance.itemFactory.GetItemCost(itemType));
        onInventoryChanged?.Invoke(itemType);
    }
}
