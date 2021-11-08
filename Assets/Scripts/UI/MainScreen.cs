using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;

    private void OnEnable()
    {
        playButton.onClick.AddListener(PlayButtonClicked);
        quitButton.onClick.AddListener(QuitButtonClicked);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
    }

    private void PlayButtonClicked()
    {
        FindObjectOfType<GameManager>().LoadNewLevel();
    }

    private void QuitButtonClicked()
    {
        Application.Quit();
    }
}
