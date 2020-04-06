using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    public float attackTime;
    public float startTimeAttack;

    public Transform attackLocation;
    public float attackRange;
    public LayerMask enemies;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
			if (attackTime == 0)
			{
				if (Input.GetButton("Fire1"))
				{
					animator.SetBool("Is_attacking", true);
					Collider2D[] damage = Physics2D.OverlapCircleAll(attackLocation.position, attackRange, enemies);

					for (int i = 0; i < damage.Length; i++)
					{
							Destroy(damage[i].gameObject);
					}
					attackTime = startTimeAttack;
				}
			}
			if (attackTime > 0)
			{
				attackTime -= Time.deltaTime;
				if (attackTime <= 0)
				{	
					attackTime = 0;
					animator.SetBool("Is_attacking", false);	
				}
			}
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackLocation.position, attackRange);
    }
}