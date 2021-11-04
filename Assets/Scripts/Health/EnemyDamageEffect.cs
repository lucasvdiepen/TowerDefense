using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageEffect : MonoBehaviour
{
    public Material enemyMaterial;
    public Material enemyDamageMaterial;
    public float damageEffectTime = 0.1f;

    private Renderer renderer;
    private float materialChangeTime = 0;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public void DamageEffect()
    {
        renderer.material = enemyDamageMaterial;
        materialChangeTime = Time.time;
    }

    private void SetNormalMaterial()
    {
        renderer.material = enemyMaterial;
        materialChangeTime = 0;
    }

    private void Update()
    {
        if(materialChangeTime != 0 && Time.time >= (materialChangeTime + damageEffectTime))
        {
            SetNormalMaterial();
        }
    }
}
