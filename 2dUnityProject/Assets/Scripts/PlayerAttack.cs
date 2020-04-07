using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	private Animator animator;
	public Rigidbody2D rb;
	public float attackTime;
	public float startTimeAttack;
	public bool isBow;
	public Vector2 mousePos;
	public Transform attackLocation;
	public float attackRange;
	public LayerMask enemies;
	private GameObject targetObject;
	private bool targetSpawned = false;
	private float x;
	private float y;
	public GameObject target;
	public Transform firePoint;
	public GameObject arrowPrefab;
	public bool arrowShot = false;
	public float arrowForce = 20f;

	public void Start()
	{
		animator = GetComponent<Animator>();

		rb = GetComponent<Rigidbody2D>();
	}

	public void FixedUpdate()
	{
		isBow = GetComponent<playerMovement>().isBow;
		mousePos = GetComponent<playerMovement>().mousePos;

		if (isBow == false && targetSpawned == true)
		{		
				Cursor.visible = true;
				Destroy(targetObject);
				targetSpawned = false;
		}
		if (isBow == false)
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
		if (isBow == true && targetSpawned == false)
		{
			targetObject = Instantiate(target,mousePos,Quaternion.identity);
			targetSpawned = true;
			Cursor.visible = false;
		}
		if (isBow == true)
		{
				targetObject.transform.position = new Vector2 (mousePos.x,mousePos.y);

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
				if (attackTime <= .33 && arrowShot == false)
				{
					GameObject arrowObject = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
					Rigidbody2D rb = arrowObject.GetComponent<Rigidbody2D>();
					rb.AddForce(firePoint.up * arrowForce, ForceMode2D.Impulse);
					arrowShot = true;
				}
				//sets attacking bool to false once attack timer is back to zero
				if (attackTime <= 0)
				{	
					attackTime = 0;
					animator.SetBool("Is_attacking", false);
					arrowShot = false;	
				}
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		//draws red circle to show attack location and range when selecting player in game window
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(attackLocation.position, attackRange);
	}
}