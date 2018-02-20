using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

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
        transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
    }
}
