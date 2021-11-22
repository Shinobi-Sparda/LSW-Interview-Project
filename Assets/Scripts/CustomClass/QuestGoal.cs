using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public EnumBase.QuestType questType;
    public int requiredAmt;
    public int currentAmt;

    public bool IsGoalReached()
    {
        return (currentAmt >= requiredAmt);
    }
}
