using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsController : MonoBehaviour
{
    public LayerMask interactionLayers;
    private float interactionRadius = 1f;
    public List<Quest> playerQuests;
    public int currentQuestID = -1;

    private Player player;

    void Start()
    {
        InputManager.instance.interationInputEvent += Interact;
        player = GetComponent<Player>();
    }

    public void UpdateQuestStatus()
    {
        playerQuests[currentQuestID].questGoal.currentAmt++;


        if (playerQuests[currentQuestID].questGoal.IsGoalReached())
        {
            Debug.Log("completed quest");
            player.AddMoney(playerQuests[currentQuestID].questReward);
            QuestManager.instance.CompletedQuest(currentQuestID);

            playerQuests.Remove(playerQuests[currentQuestID]);
            currentQuestID = -1;
        }

    }

    void Interact()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, interactionRadius, interactionLayers);
        if (collider != null)
        {
            IInteractable interactable = collider.GetComponent<IInteractable>();
            if (interactable != null)
                interactable.InteractionAction();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
