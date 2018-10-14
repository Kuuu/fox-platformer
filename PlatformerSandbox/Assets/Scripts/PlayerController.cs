﻿using System.Collections;
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

    private bool isAlive = true;


	// Use this for initialization
	void Start () {
        character = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
        GetComponent<CharacterController2D>().OnCrouchEvent.AddListener(OnCrouchEvent);
	}

    void Update()
    {
        if (transform.position.y <= bottom)
        {
            GameController.Instance.RestartLevel();
        }

        if (!isAlive) return;

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
    }
	
	void FixedUpdate () {
        if (!isAlive) return;

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

    public void Lose()
    {
        isAlive = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        Camera.main.GetComponent<CameraFollow>().target = null;
        animator.SetTrigger("hurt");
        character.DeathJump();
    }
}
