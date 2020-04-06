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
		//checks if attack timer is done and then if the press the attack button set start the timer
		if (attackTime == 0)
		{
			if (Input.GetButton("Fire1"))
			{
				animator.SetBool("Is_attacking", true);
				attackTime = startTimeAttack;
			}
		}
		//timer starts. when attack time is less than .33 destroy any objects in attackLocation collider
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
			//sets attacking bool to false once attack timer is back to zero
			if (attackTime <= 0)
			{	
				attackTime = 0;
				animator.SetBool("Is_attacking", false);	
			}
		}
		//adjusts melee attackLocations position based on movement
		x = Input.GetAxisRaw("Horizontal");
		y = Input.GetAxisRaw("Vertical");
		//right
		if (x > 0)
		{
				attackLocation.position = new Vector2(rb.position.x + (float)0.45, rb.position.y - (float).25);
		}
		//left
		if (x < 0)
		{
				attackLocation.position = new Vector2(rb.position.x -(float)0.45, rb.position.y - (float).25);
		}
		//up
		if (y > 0 && x == 0)
		{
			attackLocation.position = new Vector2(rb.position.x, rb.position.y + (float)0.25);
		}
		//down
		if (y < 0 && x == 0)
		{
			attackLocation.position = new Vector2(rb.position.x, rb.position.y - (float)0.75);
		}
	}

	private void OnDrawGizmosSelected()
	{
		//draws red circle to show attack location and range when selecting player in game window
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(attackLocation.position, attackRange);
	}
}