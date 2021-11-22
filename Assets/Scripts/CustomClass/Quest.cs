using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string questTitle;
    [TextArea(5,10)]
    public string questDescription;
    public bool isActive;
    public int questReward;
    public GameObject questIndicator;

    public QuestGoal questGoal;
}
