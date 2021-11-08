using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextAsset[] jsonMapData;
    public int wavesToWinIncrease = 3;

    public static GameManager gameManager;

    private void Awake()
    {
        if (gameManager != null)
        {
            Destroy(gameObject);
        }
        else
        {
            gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadNewLevel()
    {
        FindObjectOfType<GameInfo>().SetMapData(jsonMapData[Random.Range(0, jsonMapData.Length)]);
        FindObjectOfType<GameInfo>().SetWavesToFinish(GetWavesToWin() + wavesToWinIncrease);

        SceneManager.LoadScene("GameScene");
    }

    public int GetWavesToWin()
    {
        return PlayerPrefs.GetInt("wavesToWin", 3);
    }
}
