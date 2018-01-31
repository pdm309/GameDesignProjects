using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jump;
    private Rigidbody2D rigidbody;
    private bool alive = true;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (alive)
        {
            rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                if (rigidbody.position.y <= -3.7) {
                    rigidbody.velocity = new Vector2(speed, jump);
                }
            }
        }
	}

    void OnCollisionEnter2D (Collision2D obj)
    {
        if (obj.gameObject.tag == "Enemy")
        {
            alive = false;
        }
    }

    
}
