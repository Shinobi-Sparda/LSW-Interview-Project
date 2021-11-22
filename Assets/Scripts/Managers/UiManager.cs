using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void OnInventoryMenuOpened();

public class UiManager : MonoBehaviour
{
    public OnInventoryMenuOpened onInventoryMenuOpened;

    public static UiManager instance;
    public PlayerInventoryUiManager plyrInventoryUiManager;
    public ShopInventoryUiManager shopInventoryUiManager;

    public Text playerHealth, playerMoney;
    [HideInInspector]
    public Player player;
    [HideInInspector]
    public Shop shop;

    public GameObject questUIMenu;
    public Button acceptQuestButton, declineQuestButton;
    public Text questTitle, questDescription, questReward;

    public GameObject dialogueBox;
    private Coroutine dialogueBoxRoutine;

    public float xSpaceBetweenItems, ySpaceBetweenItems;
    public int numberOfColumns, dialogueBoxOffset;

    public bool isShopOpen = false;

    private void Awake()
    {
        if (UiManager.instance == null)
            UiManager.instance = this;

        else
            Destroy(gameObject);
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        player.onHealthChanged += UpdateHealth;
        player.onMoneyChanged += UpdateMoney;
        dialogueBox.gameObject.SetActive(false);

        shop = FindObjectOfType<Shop>();
    }

    public void SetupQuestMenu(Quest quest, Action callback)
    {
        ActivateQuestMenu(true);
        questTitle.text = quest.questTitle;
        questDescription.text = quest.questDescription;
        questReward.text = "Reward : " + quest.questReward.ToString();

        acceptQuestButton.onClick.RemoveAllListeners();
        acceptQuestButton.onClick.AddListener(delegate { callback?.Invoke(); });
        declineQuestButton.onClick.AddListener(delegate { ActivateQuestMenu(false); });
    }

    public void ActivateQuestMenu(bool value)
    {
        questUIMenu.gameObject.SetActive(value);

        if (value)
            Time.timeScale = 0;

        else
            Time.timeScale = 1;
    }

    public void UpdateHealth(int amt)
    {
        playerHealth.text = "Health : " + amt;
    }

    public void UpdateMoney(int amt)
    {
        playerMoney.text = "Money : " + amt;
    }

    public void BuyItem(EnumBase.ItemTypes itemType)
    {
        int cost = FactoryManager.instance.itemFactory.GetItemCost(itemType);
        if (cost <= player.money)
        {
            player.BuyItem(itemType, cost);
            shop.SellItem(itemType);
        }
        else
        {
            Debug.Log("Cannot Afford Item");
        }
    }

    public void MoveialogueBoxToPoint(Vector2 pos)
    {
        if (dialogueBoxRoutine != null)
            StopCoroutine(dialogueBoxRoutine);

        pos = new Vector2(pos.x, pos.y + dialogueBoxOffset);
        dialogueBoxRoutine = StartCoroutine(DialogueBoxRoutine(pos));
    }

    private IEnumerator DialogueBoxRoutine(Vector3 pos)
    {
        dialogueBox.transform.position = pos;
        dialogueBox.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        dialogueBox.gameObject.SetActive(false);
    }

    public void SellItem(EnumBase.ItemTypes itemType)
    {
        player.SellItem(itemType);
        shop.BuyItem(itemType);
    }
}
