using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class playerMovement : MonoBehaviour
{
	public float moveSpeed = 5f;
	public Rigidbody2D rb;
	public Camera cam;
	public Animator anim;
	Vector2 movement;
	public Vector2 mousePos;
	public bool isBow = false; 
	private Material matWhite;
	private Material matDefault;
	SpriteRenderer sr;
	public int health = 100;

	public int lives = 3;

	public void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
		matDefault = sr.material;

		sr.material = matDefault;
	}
	// Update is called once per frame
	public void Update()
	{
		// Input
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");

		movement = movement.normalized;

		mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

		float deltaX = mousePos.x - transform.position.x;
		float deltaY = mousePos.y - transform.position.y;

		//if statement that starts "bow combat" movement
		if (isBow == true)
		{
			if (Input.GetButtonDown("Fire2") && isBow == true)
			{
				isBow = false;
				anim.SetBool("is_Bow",false);
				anim.SetFloat("Horizontal", 0);
				anim.SetFloat("Vertical", 0);
				anim.SetFloat("Speed", 0);
				anim.SetFloat("HorizontalIdle", 0);
				anim.SetFloat("VerticalIdle", 0);
			}
			if (!(deltaX == 0f && deltaY == 0f)) //position directly on cursor
			{
				if (movement != Vector2.zero)
				{
					anim.SetFloat("Speed", movement.sqrMagnitude);
				}
				else
				{
					anim.SetFloat("Speed", 0);	
				}
				if (deltaX > 0f && Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
				{
					//right sprite
					anim.SetFloat("Horizontal", 1);
					anim.SetFloat("Vertical", 0);
					anim.SetFloat("HorizontalIdle", 1);
					anim.SetFloat("VerticalIdle", 0);
				}
				else if (deltaX < 0f && Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
				{
					//left sprite
					anim.SetFloat("Horizontal", -1);
					anim.SetFloat("Vertical", 0);								
					anim.SetFloat("HorizontalIdle",-1);
					anim.SetFloat("VerticalIdle", 0);
				}
				if (deltaY > 0f && Mathf.Abs(deltaY) > Mathf.Abs(deltaX))
				{
					//up sprite
					anim.SetFloat("Horizontal", 0);
					anim.SetFloat("Vertical", 1);								
					anim.SetFloat("VerticalIdle", 1);
					anim.SetFloat("HorizontalIdle", 0);
				}
				else if (deltaY < 0f && Mathf.Abs(deltaY) > Mathf.Abs(deltaX))
				{
						//down sprite
					anim.SetFloat("Horizontal", 0);
					anim.SetFloat("Vertical", -1);								
					anim.SetFloat("VerticalIdle", -1);
					anim.SetFloat("HorizontalIdle", 0);
				}
			}
		}
		//other if statement that is for melee combat
		else if (isBow == false)
		{
			// face mouse direction END
			if (Input.GetButtonDown("Fire2") && isBow == false)
			{
				isBow = true;
				anim.SetBool("is_Bow",true);						
				anim.SetFloat("Horizontal", 0);
				anim.SetFloat("Vertical", 0);
				anim.SetFloat("Speed", 0);
				anim.SetFloat("HorizontalIdle", 0);
				anim.SetFloat("VerticalIdle", 0);
			}
			if (movement != Vector2.zero)
			{
					anim.SetFloat("Horizontal", movement.x);
					anim.SetFloat("Vertical", movement.y);
					anim.SetFloat("Speed", movement.sqrMagnitude);
					anim.SetFloat("HorizontalIdle", movement.x);
					anim.SetFloat("VerticalIdle", movement.y);
			}
			else
			{
					anim.SetFloat("Horizontal", 0);
					anim.SetFloat("Vertical", 0);
					anim.SetFloat("Speed", 0);
			} 					
		}  
	}
	public void TakeDamage(int damage)
    {
        sr.material = matWhite;
        health -= damage;
        if (health <= 0)
        {
            playerDie();
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
    public void playerDie()
    {
        lives -= 1;
        if (lives <= 0)
        {

            gameOver();
        }
        else
        {
            health = 100;
            rb.position = new Vector2(0, 0);
            Invoke("ResetMaterial", .1f);
        }

    }
    public void gameOver()
    {
        gameObject.SetActive(false);
        Invoke("ResetMaterial", .1f);
    }
	public void FixedUpdate()
	{
			// Movement normalized across varying framerates
			rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
			
	}
}