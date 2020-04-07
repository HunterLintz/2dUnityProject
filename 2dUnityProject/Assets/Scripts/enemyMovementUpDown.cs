using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovementUpDown : MonoBehaviour
{
	public float moveSpeed = 5f;
	public Animator anim;
	public Rigidbody2D rb;
	public bool isFront = true;
	public Transform wallCheck;
	public float wallCheckRange;
	Vector2 movement;
	public LayerMask walls;
	// Update is called once per frame
	void Update()
	{		
		movement.x = 0;
		if (isFront == true)
		{	
			movement.y = -1;
			anim.SetBool("isFront",true);
			Collider2D wall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRange, walls);
			if (wall == true)
			{
				isFront = false;
				wallCheck.position = new Vector2(rb.position.x, rb.position.y+ (float)0.5);
			}
		}
		if (isFront == false)
		{
			movement.y = 1;
			anim.SetBool("isFront",false);
			Collider2D wall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRange, walls);
			if (wall == true)
			{
				isFront = true;
				wallCheck.position = new Vector2(rb.position.x, rb.position.y -(float)1);
			}		
		}
	}
	void FixedUpdate()
	{
		rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
	}
	private void OnDrawGizmosSelected()
	{
		//draws red circle to show attack location and range when selecting player in game window
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(wallCheck.position, wallCheckRange);
	}
}
