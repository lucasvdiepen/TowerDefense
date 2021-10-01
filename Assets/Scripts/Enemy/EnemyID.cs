using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyID : MonoBehaviour
{
    private int id;

    public void Setup(int _id)
    {
        id = _id;
    }

    public float GetID()
    {
        return id;
    }
}
