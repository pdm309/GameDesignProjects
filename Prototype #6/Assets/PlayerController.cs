using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jump;
    private Rigidbody2D rigidbody;
    public bool alive = true;

    private SpriteRenderer spriteR;
    private bool flippedOnce = false;

    private Sprite[] sprites;
    private int frame = 0;
    private int frameOf60 = 0;
    private int bulletcooldown = 0;
    public Text wintext;
    private bool ableToJump = false;
    public int bullets = 0;
    public int level = 1;
    private GameObject[] bulletArray;
    public bool[] shooting = new bool[] { false, false, false };
    private Vector2[] bulletPaths = new Vector2[] { new Vector2( 0, 0 ), new Vector2( 0, 0 ), new Vector2( 0, 0 )};
    private bool charFacingRight = true;
    private bool[] facingRight = new bool[] { true, true, true };
    private int shootingCooldown = 0;
    
    public Player2Controller player2;
    // Use this for initialization
    void Start()
    {
        player2 = FindObjectOfType<Player2Controller>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteR = GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("cat_assault_rifle");
        wintext = GameObject.Find("Text").GetComponent<Text>();
        wintext.text = "";
        bulletArray = GameObject.FindGameObjectsWithTag("Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        frame++;
        frameOf60 = frame % 60;
        if (Input.GetKeyDown(KeyCode.W) && alive) //jumping
        {
            if (ableToJump)
            {
                spriteR.sprite = sprites[87];
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jump);
                ableToJump = false;
            }
        }
        if (Input.GetKey(KeyCode.D) && alive) //moving right
        {
            charFacingRight = true;
            rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
            if (frameOf60 % 8 <= 3)
            {
                if (ableToJump)
                    spriteR.sprite = sprites[50];
            }
            else
            {
                if (ableToJump)
                    spriteR.sprite = sprites[51];
            }
            if (flippedOnce)
            {
                spriteR.flipX = false;
                int a = 0;
                foreach (GameObject bullet in bulletArray)
                {
                    if (shooting[a] == false)
                    {
                        bullet.transform.localPosition = new Vector3(-bullet.transform.localPosition.x, bullet.transform.localPosition.y, bullet.transform.localPosition.z);
                    }
                    a++;
                }
            }
        }
        if (Input.GetKey(KeyCode.A) && alive) //moving left
        {
            charFacingRight = false;
            rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
            if (frameOf60 % 8 <= 3)
            {
                if (ableToJump)
                    spriteR.sprite = sprites[51];
            }
            else
            {
                if (ableToJump)
                    spriteR.sprite = sprites[50];
            }
            spriteR.flipX = true;
            int b = 0;
            foreach (GameObject bullet in bulletArray)
            {
                if (shooting[b] == false)
                {
                    bullet.transform.localPosition = new Vector3(-bullet.transform.localPosition.x, bullet.transform.localPosition.y, bullet.transform.localPosition.z);
                }
                b++;
            }
            flippedOnce = true;
        }

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && alive && ableToJump) //standing still
        {
            spriteR.sprite = sprites[23];
        }


        if (Input.GetKey(KeyCode.S) && bullets > 0 && shootingCooldown == 0) //shoot
        {
            bullets--;
            shooting[bullets] = true;
            bulletPaths[bullets] = new Vector2(rigidbody.transform.position.x, rigidbody.transform.position.y);
            if (charFacingRight == false)
            {
                facingRight[bullets] = false;
            }
            else
            {
                facingRight[bullets] = true;
            }
            shootingCooldown = 30;
        }
        int i = 2;
        foreach(bool shot in shooting)
        {
            if (shot == true)
            {
                bulletArray[i].transform.position = new Vector3(bulletPaths[i].x, bulletPaths[i].y, bulletArray[i].transform.position.z);
                if (facingRight[i] == true)
                {
                    bulletPaths[i] = new Vector2(bulletPaths[i].x + .075f, bulletPaths[i].y);
                    //bulletArray[i].transform.position = new Vector3(bulletPaths[i].x + .075f, bulletPaths[i].y, bulletArray[i].transform.position.z);
                }
                else
                {
                    bulletPaths[i] = new Vector2(bulletPaths[i].x - .075f, bulletPaths[i].y);
                    //bulletArray[i].transform.position = new Vector3(bulletPaths[i].x - .075f, bulletPaths[i].y, bulletArray[i].transform.position.z);
                }
            }
            i--;
        }
        if (shootingCooldown > 0)
        {
            shootingCooldown--;
        }


        if (Input.GetKeyDown(KeyCode.R) && level == 1)
        {
            Application.LoadLevel(0);
        }
        
    }



    void OnCollisionStay2D(Collision2D obj)
    {
        Collider2D collider = obj.collider;
        
        if (obj.gameObject.tag == "ProcGround" || obj.gameObject.tag == "Player 2")
        {
            if (alive && obj.contacts[0].point.y < rigidbody.position.y -.12)
            {
                ableToJump = true;
            }
            
        }
        else if (obj.gameObject.tag == "Elevator")
        {
            ableToJump = true;
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ammo" && bullets == 0)
        {
            bullets = 3;
            foreach (GameObject bullet in bulletArray)
            {
            bullet.GetComponent<Renderer>().enabled = true;
            bullet.GetComponent<CapsuleCollider2D>().enabled = true;
                if (charFacingRight == true)
                {
                    bullet.transform.position = new Vector2(rigidbody.position.x, rigidbody.position.y);
                }
                else
                {
                    bullet.transform.position = new Vector2(rigidbody.position.x, rigidbody.position.y);
                }
                bulletPaths[0] = new Vector2(rigidbody.transform.position.x, rigidbody.transform.position.y);
                bulletPaths[1] = new Vector2(rigidbody.transform.position.x, rigidbody.transform.position.y);
                bulletPaths[2] = new Vector2(rigidbody.transform.position.x, rigidbody.transform.position.y);
                shooting[0] = false;
                shooting[1] = false;
                shooting[2] = false;
                facingRight[0] = false;
                facingRight[1] = false;
                facingRight[2] = false;
            }
        }

        if (other.gameObject.tag == "Bullet 2")
        {
            if (player2.shooting[0] || player2.shooting[1] || player2.shooting[2])
            {
                wintext.text = "Player 1 Wins!\nPress R to restart!";
                alive = false;
                spriteR.sprite = sprites[141];
                //transform.position = new Vector3(rigidbody.position.x, rigidbody.position.y, 0);

            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.tag == "Elevator")
        {
            transform.parent = other.transform;

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Elevator")
        {
            transform.parent = null;

        }
    }
}
