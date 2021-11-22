using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQuestItem
{
    void StartPlayerInteraction();
    void CheckQuestCompleteCondition();
}
