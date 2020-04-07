using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowScript : MonoBehaviour
{
		public int damage = 40;
    public Rigidbody2D rb;
    public GameObject hitEffect;
		public GameObject enemy;

		void start()
		{
			gameObject.GetComponent<enemy>().TakeDamage(damage);
		}

    void OnCollisionEnter2D(Collision2D collision) 
    {
			GameObject effect =Instantiate(hitEffect, transform.position, Quaternion.identity);
			Destroy(effect,5f);
			Destroy(gameObject);
    }
		void OnTriggerEnter2D(Collider2D hitInfo)
		{
			enemy enemy = hitInfo.GetComponent<enemy>();
			if (enemy != null)
			{
					enemy.TakeDamage(damage);
			}
			GameObject effect =Instantiate(hitEffect, transform.position, Quaternion.identity);
			Destroy(effect,5f);
			Destroy(gameObject);
		}

}
