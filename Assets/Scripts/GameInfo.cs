using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    [HideInInspector] public TextAsset jsonMapData;
    [HideInInspector] public int wavesToFinish = 15;

    public static GameInfo gameInfo;

    public void SetMapData(TextAsset mapData)
    {
        jsonMapData = mapData;
    }

    public void SetWavesToFinish(int waves)
    {
        wavesToFinish = waves;
    }

    private void Awake()
    {
        if(gameInfo != null)
        {
            Destroy(gameObject);
        }
        else
        {
            gameInfo = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
