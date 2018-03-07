using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    private Rigidbody2D rigidbody;
    private int frame = 0;
    private int frameOf60 = 0;
    private bool goingUp = true;
    private int cooldown = 0;

    // Use this for initialization
    void Start ()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        frame++;
        frameOf60 = frame % 60;
        if (cooldown == 0 && goingUp == true)
        {
            rigidbody.transform.position = new Vector3(rigidbody.transform.position.x, rigidbody.transform.position.y + .01f, rigidbody.transform.position.z);
        }
        else if (cooldown == 0 && goingUp == false)
        {
            rigidbody.transform.position = new Vector3(rigidbody.transform.position.x, rigidbody.transform.position.y - .01f, rigidbody.transform.position.z);
        }
        if (cooldown == 0 && rigidbody.transform.position.y <= -3.44)
        {
            cooldown = 120;
            goingUp = true;
        }
        if (cooldown == 0 && rigidbody.transform.position.y >= -1.85)
        {
            cooldown = 120;
            goingUp = false;
        }
        if (cooldown > 0)
        {
            cooldown--;
        }

    }
}
