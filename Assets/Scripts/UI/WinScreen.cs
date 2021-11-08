using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    public GameObject winScreenHolder;
    public Button nextButton;
    public Button quitButton;

    private void OnEnable()
    {
        nextButton.onClick.AddListener(NextButtonClicked);
        quitButton.onClick.AddListener(QuitButtonClicked);
    }

    private void OnDisable()
    {
        nextButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
    }

    public void ShowWinScreen()
    {
        FindObjectOfType<PauseScreen>().GameEnded();
        FindObjectOfType<EnemySpawner>().StopSpawning();

        winScreenHolder.SetActive(true);

    }

    public void NextButtonClicked()
    {
        FindObjectOfType<GameManager>().LoadNewLevel();
    }

    public void QuitButtonClicked()
    {
        SceneManager.LoadScene("MainScreen");
    }
}
