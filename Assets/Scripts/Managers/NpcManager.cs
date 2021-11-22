using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    public static NpcManager instance;
    public List<NPCObject> npcObjects = new List<NPCObject>();

    private void Awake()
    {
        if (NpcManager.instance == null)
            NpcManager.instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Invoke(nameof(Init), .1f);
    }

    public void Init()
    {
        QuestManager.instance.AssignNPCQuest(npcObjects);
    }

    public NPCObject GetNpcWithQuestID(int id)
    {
        foreach (var item in npcObjects)
        {
            if (item.questID == id)
                return item;
        }

        return null;
    }

}
