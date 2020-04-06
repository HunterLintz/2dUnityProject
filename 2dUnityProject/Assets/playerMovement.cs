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

    // Update is called once per frame
    public void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        //face mouse direction START
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        float deltaX = mousePos.x - transform.position.x;
        float deltaY = mousePos.y - transform.position.y;

        if (!(deltaX == 0f && deltaY == 0f)) //position directly on cursor
        {
            // when mouse is greater than 0 along the x axis , face right
            if (deltaX > 0f && Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
            {
                //right sprite
                anim.SetFloat("Horizontal", movement.x = 1);
            }
            // when mouse is less than 0 along the x axis , face left
            else if (deltaX < 0f && Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
            {
                //left sprite
                anim.SetFloat("Horizontal", movement.x = -1);
            }
            //when mouse is greater than 0 along the y axis , face up
            else if (deltaY > 0f && Mathf.Abs(deltaY) > Mathf.Abs(deltaX))
            {
                //up sprite
                anim.SetFloat("Vertical", movement.y = 1);
            }
            // when mouse is less than 0 along the y axis , face down
            else if (deltaY < 0f && Mathf.Abs(deltaY) > Mathf.Abs(deltaX))
            {
                //down sprite
                anim.SetFloat("Vertical", movement.y = -1);
            }
            else
            {
                Debug.Log("faceMouseDir Error");
            }
        }
        //face mouse direction END

        if (movement.x != 0)
        {
            anim.SetBool("Is_Moving_Side", true);
        }
        else
        {
            anim.SetBool("Is_Moving_Side", false);
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

    public void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
				
    }
}