using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jump;
    private Rigidbody2D rigidbody;
    private bool alive = true;
    public Player2Controller player2;
    private SpriteRenderer spriteR;
    private bool flippedOnce = false;

    private bool attacking = false;
    private Sprite[] sprites;
    private int frame = 0;
    private int frameOf60 = 0;
    private int frameStartAttack = 0;
    private bool hitboxStarts = false;
    private int hitboxStartupFrames = 12;
    public bool hitboxActive = false;
    GameObject Player2;
    //public Text wintext;

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        player2 = FindObjectOfType<Player2Controller>();
        spriteR = GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("cat_sprites");
        Player2 = GameObject.Find("Player 2");
        //wintext.text = "";
    }
	
	// Update is called once per frame
	void Update () {
        frame++;
        frameOf60 = frame % 60;
        if (alive)
        {
            
            if (player2.transform.position.x > rigidbody.position.x && flippedOnce) //player2 on right side of player1
            {
                spriteR.flipX = false;
            }
            else
            {
                spriteR.flipX = true;
                flippedOnce = true;
            }
            if (Input.GetKeyDown(KeyCode.W) && !attacking) //jumping
            {
                if (rigidbody.position.y <= -3.2) {
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, jump);
                }
            }
            if (Input.GetKey(KeyCode.D) && !attacking) //moving right
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
            if (Input.GetKey(KeyCode.A) && !attacking) //moving left
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
            if (Input.GetKeyDown(KeyCode.S) && !attacking) //attacking
            {
                attacking = true;
                Debug.Log("player1 attack button pressed");
                frameStartAttack = frameOf60;
            }
            if (attacking && !hitboxStarts)
            {
                spriteR.sprite = sprites[7];
                hitboxStartupFrames--;
                if (hitboxStartupFrames == 5)
                {
                    //attack comes out frame 7
                    Debug.Log("player1 hitbox starts");
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
                    Debug.Log("player1 hitbox active");
                }
                else if (hitboxStartupFrames <= -3 && hitboxStartupFrames > -15)
                {
                    spriteR.sprite = sprites[9];
                    hitboxActive = false;
                    if (hitboxStartupFrames == -3) { Debug.Log("player1 ending lag"); }
                }
                else if (hitboxStartupFrames <= -15)
                {
                    Debug.Log("player1 return to neutral");
                    spriteR.sprite = sprites[6];
                    attacking = false;
                    hitboxStartupFrames = 12;
                    hitboxStarts = false;
                }
            }
        }
        //else
        //{
            //wintext.text = "Player 2 Wins!";
        //}
	}


    void OnCollisionStay2D(Collision2D obj)
    {
        if (obj.gameObject.tag == "Player 2")
        {
            if (Player2.GetComponent<Player2Controller>().hitboxActive)
            {
                Debug.Log("player 2 hit player 1");
                alive = false;
            }
        }
    }


}
