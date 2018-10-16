using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.GetComponent<PlayerController>().IsInvinsible())
            collision.GetComponent<PlayerController>().Hurt();
    }
}
