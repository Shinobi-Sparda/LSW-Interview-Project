using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public Button startButton;

    private void Start()
    {
        startButton.onClick.AddListener(delegate { LoadGame(); });
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
