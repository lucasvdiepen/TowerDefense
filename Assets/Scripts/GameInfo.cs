using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    [HideInInspector] private TextAsset jsonMapData;
    [HideInInspector] private int wavesToFinish;

    public static GameInfo gameInfo;

    public void SetMapData(TextAsset mapData)
    {
        jsonMapData = mapData;
    }

    public void SetWavesToFinish(int waves)
    {
        wavesToFinish = waves;
    }

    public int GetWavesToFinish()
    {
        return wavesToFinish;
    }

    public TextAsset GetJsonMapData()
    {
        return jsonMapData;
    }

    private void Awake()
    {
        if(gameInfo != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            gameInfo = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
