using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnQuestCompleted(int questId);

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public OnQuestCompleted questCompleted;
    public List<Quest> quests;
    public GameObject questIndicator;

    private void Awake()
    {
        if (QuestManager.instance == null)
            QuestManager.instance = this;

        else
            Destroy(gameObject);
    }

    public void AssignNPCQuest(List<NPCObject> nPC)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            nPC[i].unfinishedQuest = quests[i];
            nPC[i].questID = i;
            Vector2 pos = new Vector2(nPC[i].transform.position.x, nPC[i].transform.position.y + 1.2f);
            nPC[i].unfinishedQuest.questIndicator = Instantiate(questIndicator, pos, Quaternion.identity, nPC[i].transform);
        }
    }

    public void CompletedQuest(int questID)
    {
        quests[questID].isActive = false;
        quests[questID].questGoal.currentAmt = quests[questID].questGoal.requiredAmt;

        questCompleted?.Invoke(questID);
        quests.Remove(quests[questID]);
    }

}
