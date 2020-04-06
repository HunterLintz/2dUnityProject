using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Add fucking comments
 * to ur cs files
 * you absolute madman
 * so i know what i'm looking at 
 * and what you were thinking
 * a lot faster
 * when i'm alone, going through things for funzies
 * or wanting to add things, but unsure of what i can and can't change
 * comments HELP son
 * add them
 * unless it's a solo project that'll be done relatively fast...
 * but on collabs, ALWAYS comment >:C
 * End of rant
 */
public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Camera cam;
    public Animator anim;

    Vector2 movement;
    Vector2 mousePos;

		 public bool isBow = false; 

    // Update is called once per frame
    public void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

				mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        float deltaX = mousePos.x - transform.position.x;
        float deltaY = mousePos.y - transform.position.y;

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
						else
            {
						Debug.Log("faceMouseDir Error");
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
            else
            {
                Debug.Log("faceMouseDir Error");
            }
					}
				}
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

    public void FixedUpdate()
    {
        // Movement normalized across varying framerates
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
				
    }
}