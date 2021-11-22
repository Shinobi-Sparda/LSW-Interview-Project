using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doggo : MonoBehaviour, IQuestItem, IInteractable
{
    private bool followingPlayer;
    public float followOffset, moveSpeed;
    private Transform ownerPos;
    private Player player;
    public int relatedQuestID;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void CheckQuestCompleteCondition()
    {
        if (Vector3.Distance(transform.position, ownerPos.position) <= 1f)
        {
            player.interactionsController.UpdateQuestStatus();
            followingPlayer = false;
        }
    }

    public void StartPlayerInteraction()
    {
        if (player.interactionsController.currentQuestID== relatedQuestID)
        {
            ownerPos = NpcManager.instance.GetNpcWithQuestID(relatedQuestID).transform;
            followingPlayer = true;
            StartCoroutine(FollowPlayer());
        }

        else
        {
            Debug.Log("Woof");
        }
    }

    private IEnumerator FollowPlayer()
    {
        while (followingPlayer)
        {
            if (Vector2.Distance(transform.position, player.transform.position) > followOffset)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            }

            CheckQuestCompleteCondition();
            yield return null;
        }
    }

    public void InteractionAction()
    {
        StartPlayerInteraction();
    }
}
