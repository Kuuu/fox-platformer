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

    private bool isAlive = true;
    private bool isInvinsible = false;
    private float invinsibleTimeAfterHurt = 1.4f;

    private int lives = 3;


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
            if (!animator.GetBool("crouch"))
            {
                jump = true;
                animator.SetTrigger("jump");
            }
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
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

    public void Hurt()
    {
        if (lives == 1)
        {
            Lose();
        }
        else
        {
            animator.SetTrigger("hurt");
            animator.SetBool("invinsible", true);
            isInvinsible = true;
            Invoke("EndInvinsible", invinsibleTimeAfterHurt);
            lives--;
        }
    }

    public void EndInvinsible()
    {
        animator.SetBool("invinsible", false);
        isInvinsible = false;
    }

    public void Lose()
    {
        isAlive = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        Camera.main.GetComponent<CameraFollow>().target = null;
        animator.SetTrigger("dead");
        character.DeathJump();
    }

    public bool IsInvinsible()
    {
        return isInvinsible;
    }
}
