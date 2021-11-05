using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{
    public GameObject loseScreenHolder;
    public Button retryButton;
    public Button quitButton;

    private void OnEnable()
    {
        retryButton.onClick.AddListener(RetryButtonClicked);
        quitButton.onClick.AddListener(QuitButtonClicked);
    }

    private void OnDisable()
    {
        retryButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
    }

    public void ShowLoseScreen()
    {
        FindObjectOfType<PauseScreen>().GameEnded();
        FindObjectOfType<EnemySpawner>().StopSpawning();

        loseScreenHolder.SetActive(true);

    }

    public void RetryButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitButtonClicked()
    {
        SceneManager.LoadScene("MainScreen");
    }
}
