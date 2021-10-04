using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyID : MonoBehaviour
{
    private int id = -1;

    public void Setup(int _id)
    {
        id = _id;
    }

    public int GetID()
    {
        return id;
    }
}
