using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
		public Rigidbody2D rb;
    public float attackTime;
    public float startTimeAttack;
		playerMovement move;
    public Transform attackLocation;
    public float attackRange;
    public LayerMask enemies;

		private float x;

		private float y;
    public void Start()
    {
        animator = GetComponent<Animator>();

				rb = GetComponent<Rigidbody2D>();

				move = GetComponent<playerMovement>();
    }

    public void FixedUpdate()
    {
			if (attackTime == 0)
			{
				if (Input.GetButton("Fire1"))
				{
					animator.SetBool("Is_attacking", true);
					attackTime = startTimeAttack;
				}
			}
			if (attackTime > 0)
			{
				attackTime -= Time.deltaTime;
				if (attackTime <= .33)
				{
					Collider2D[] damage = Physics2D.OverlapCircleAll(attackLocation.position, attackRange, enemies);
					for (int i = 0; i < damage.Length; i++)
					{
							Destroy(damage[i].gameObject);
					}
				}
				if (attackTime <= 0)
				{	
					attackTime = 0;
					animator.SetBool("Is_attacking", false);	
				}
			}
			x = Input.GetAxisRaw("Horizontal");
			y = Input.GetAxisRaw("Vertical");
			Debug.Log(x);
			Debug.Log(y);
			if (x > 0)
			{
					attackLocation.position = new Vector2(rb.position.x + (float)0.45, rb.position.y - (float).25);
			}
			if (x < 0)
			{
					attackLocation.position = new Vector2(rb.position.x -(float)0.45, rb.position.y - (float).25);
			}
			if (y > 0 && x == 0)
			{
				attackLocation.position = new Vector2(rb.position.x, rb.position.y + (float)0.25);
			}
			if (y < 0 && x == 0)
			{
				attackLocation.position = new Vector2(rb.position.x, rb.position.y - (float)0.75);
			}
		}


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackLocation.position, attackRange);
    }
}