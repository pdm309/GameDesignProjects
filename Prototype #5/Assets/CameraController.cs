using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public PlayerController player;
    private int level;
    private float distance;
    private float distancey;
    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        level = player.GetComponent<PlayerController>().level;
        if (level >= 2)
        {
            distancey = player.transform.position.y - transform.position.y;
        }
        else
        {
            distancey = 0;
        }
        distance = player.transform.position.x - transform.position.x;

        transform.position = new Vector3(transform.position.x + distance, transform.position.y + distancey, transform.position.z);
    }
}
