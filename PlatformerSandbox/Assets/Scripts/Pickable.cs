using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour {

    public string type;
    private Animator animator;
    private bool isPicked;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isPicked)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("remove"))
            {
                Destroy(gameObject);
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetTrigger("picked");
        isPicked = true;
    }
}
