using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    public static FactoryManager instance;
    private void Awake()
    {
        FactoryManager.instance = this;
    }

    public ItemFactory itemFactory;
}
