using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{

    public float speed;
    public float jump;
    private Rigidbody2D rigidbody;
    public bool alive = true;
    public PlayerController player;
    GameObject Player1;
    private SpriteRenderer sprite;
    private bool flippedOnce = false;
    private SpriteRenderer spriteR;
    private bool attacking = false;
    private Sprite[] sprites;
    private int frame = 0;
    private int frameOf60 = 0;
    private int frameStartAttack = 0;
    private bool hitboxStarts = false;
    private int hitboxStartupFrames = 12;
    public bool hitboxActive = false;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
        spriteR = GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("cat_sprites");
        Player1 = GameObject.Find("Player 1");
    }

    // Update is called once per frame
    void Update()
    {

        frame++;
        frameOf60 = frame % 60;
        if (alive)
        {
            if (player.transform.position.x > rigidbody.position.x && flippedOnce) //player2 on right side of player1
            {
                spriteR.flipX = false;
            }
            else
            {
                spriteR.flipX = true;
                flippedOnce = true;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && !attacking) //jumping
            {
                if (rigidbody.position.y <= -3.2)
                {
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, jump);
                }
            }
            if (Input.GetKey(KeyCode.RightArrow) && !attacking) //moving right
            {
                rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
                if (frameOf60 % 2 == 0)
                {
                    spriteR.sprite = sprites[3];
                }
                else
                {
                    spriteR.sprite = sprites[4];
                }
            }
            if (Input.GetKey(KeyCode.LeftArrow) && !attacking) //moving left
            {
                rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
                if (frameOf60 % 2 == 0)
                {
                    spriteR.sprite = sprites[4];
                }
                else
                {
                    spriteR.sprite = sprites[3];
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                attacking = true;
                Debug.Log("player2 attack button pressed");
                frameStartAttack = frameOf60;
            }
            if (attacking && !hitboxStarts)
            {
                spriteR.sprite = sprites[7];
                hitboxStartupFrames--;
                if (hitboxStartupFrames == 5)
                {
                    //attack comes out frame 7
                    Debug.Log("player2 hitbox starts");
                    spriteR.sprite = sprites[8];
                    hitboxStarts = true;
                }
            }
            if (hitboxStarts)
            {
                hitboxStartupFrames--;
                if (hitboxStartupFrames < 1 && hitboxStartupFrames > -3)
                {
                    spriteR.sprite = sprites[8];
                    hitboxActive = true;
                    Debug.Log("player2 hitbox active");
                }
                else if (hitboxStartupFrames <= -3 && hitboxStartupFrames > -15)
                {
                    spriteR.sprite = sprites[9];
                    hitboxActive = false;
                    if (hitboxStartupFrames == -3) { Debug.Log("player2 ending lag"); }
                }
                else if (hitboxStartupFrames <= -15)
                {
                    Debug.Log("player2 return to neutral");
                    spriteR.sprite = sprites[6];
                    attacking = false;
                    hitboxStartupFrames = 12;
                    hitboxStarts = false;
                }
            }
        }
    }


    void OnCollisionStay2D(Collision2D obj)
    {
        if (obj.gameObject.tag == "Player 1")
        {
            if (Player1.GetComponent<PlayerController>().hitboxActive)
            {
                Debug.Log("player 1 hit player 2");
                alive = false;
            }
        }
    }


}
