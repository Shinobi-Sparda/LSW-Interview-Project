using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCObject : MonoBehaviour, IInteractable
{
    public Quest unfinishedQuest;
    public int questID;
    private Player player;

    private void Start()
    {
        unfinishedQuest = null;
        QuestManager.instance.questCompleted += CompletedQuest;
        player = FindObjectOfType<Player>();
        NpcManager.instance.npcObjects.Add(this);
    }

    public void InteractionAction()
    {
        if (unfinishedQuest != null)
            UiManager.instance.SetupQuestMenu(unfinishedQuest, GiveQuestToPlayer);

        else
        {
            UiManager.instance.MoveialogueBoxToPoint((Vector2)transform.position);
            Debug.Log("No quests today");
        }
    }

    public void GiveQuestToPlayer()
    {
        Debug.Log("accepted quest");
        Destroy(unfinishedQuest.questIndicator);
        UiManager.instance.ActivateQuestMenu(false);

        for (int i = 0; i < 4; i++)
        {
            if (player.interactionsController.playerQuests.Count < questID)
                player.interactionsController.playerQuests.Add(new Quest());

            else
                break;
        }

        player.interactionsController.playerQuests.Insert(questID, unfinishedQuest);
        player.interactionsController.currentQuestID = questID;
    }

    private void CompletedQuest(int id)
    {
        if (id == questID)
            unfinishedQuest = null;
    }

    private void OnDisable()
    {
        QuestManager.instance.questCompleted -= CompletedQuest;
    }
}
