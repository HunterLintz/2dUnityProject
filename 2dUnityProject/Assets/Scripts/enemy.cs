using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private Material matWhite;
    private Material matDefault;
    SpriteRenderer sr;

    public int health = 100;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = sr.material;
    }
    public void TakeDamage (int damage)
    {

        sr.material = matWhite;
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            Invoke("ResetMaterial", .1f);
        }
    }
    void ResetMaterial()
    {
        sr.material = matDefault;
    }
    void Die()
    {
        Destroy(gameObject);
    }

}
