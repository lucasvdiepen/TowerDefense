using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    public GameObject pauseScreenHolder;

    public Button resumeButton;
    public Button quitButton;

    public bool gameIsEnded = false;

    private void OnEnable()
    {
        resumeButton.onClick.AddListener(ResumeButtonClicked);
        quitButton.onClick.AddListener(QuitButtonClicked);
    }

    private void OnDisable()
    {
        resumeButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) TogglePause();
    }

    private void ResumeButtonClicked()
    {
        HidePause();
    }

    private void QuitButtonClicked()
    {
        SceneManager.LoadScene("MainScreen");
    }

    private void ShowPause()
    {
        if(!gameIsEnded)
        {
            pauseScreenHolder.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void HidePause()
    {
        if(!gameIsEnded)
        {
            pauseScreenHolder.SetActive(false);
            Time.timeScale = 1;
        }
    }

    private void TogglePause()
    {
        if (IsPaused()) HidePause();
        else ShowPause();
    }

    public void GameEnded()
    {
        gameIsEnded = true;
    }

    private bool IsPaused()
    {
        return pauseScreenHolder.activeSelf;
    }
}
