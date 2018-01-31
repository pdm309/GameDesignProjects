using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    public PlayerController player;
    private float distance;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = player.transform.position.x - transform.position.x;
        if (distance >= 10)
        {
            transform.position = new Vector3(player.transform.position.x + 8, transform.position.y, transform.position.z);
        }
    }
}
