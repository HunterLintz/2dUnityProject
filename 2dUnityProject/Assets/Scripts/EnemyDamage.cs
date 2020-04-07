using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 40;

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        playerMovement player = hitInfo.GetComponent<playerMovement>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
