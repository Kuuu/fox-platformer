using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    CharacterController2D character;
    Animator animator;

    public float speed = 40f;
    private float movement = 0f;
    private bool jump = false;
    private bool crouch;

    public float bottom = -10f;


	// Use this for initialization
	void Start () {
        character = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
        GetComponent<CharacterController2D>().OnCrouchEvent.AddListener(OnCrouchEvent);
	}

    void Update()
    {
        movement = Input.GetAxisRaw("Horizontal");


        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetTrigger("jump");
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        if (movement != 0)
        {
            animator.SetBool("running", true);
        } else
        {
            animator.SetBool("running", false);
        }

        if (transform.position.y <= bottom)
        {
            GameController.Instance.RestartLevel();
        }
    }
	
	void FixedUpdate () {
        character.Move(movement * speed * Time.fixedDeltaTime, crouch, jump);
        jump = false;
	}

    void OnCrouchEvent(bool isCrouching)
    {
        if (isCrouching)
        {
            animator.SetBool("crouch", true);
            animator.SetTrigger("startCrouch");
        } else
        {
            animator.SetBool("crouch", false);
        }
    }
}
