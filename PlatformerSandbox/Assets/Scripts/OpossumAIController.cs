using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpossumAIController : MonoBehaviour {

    public float speed;
    private int direction = -1;
    private Rigidbody2D rb;
    private Vector3 m_Velocity = Vector3.zero;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    private void FixedUpdate()
    {
        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(direction * speed * 10f * Time.fixedDeltaTime, rb.velocity.y);
        // And then smoothing it out and applying it to the character
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            if (!collision.GetComponent<PlayerController>().IsInvinsible())
                collision.GetComponent<PlayerController>().Hurt();
        } else if (collision.transform == transform.parent)
        {
            direction *= -1;
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        }
    }
}
