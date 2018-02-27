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

    private bool warping = false;
    private Sprite[] sprites;
    private int frame = 0;
    private int frameOf60 = 0;
    private int warpcooldown = 0;
    public Text wintext;
    private bool ableToJump = false;
    GameObject cube;
    public int level = 1;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        spriteR = GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("cat_sprites");
        wintext = GameObject.Find("Text").GetComponent<Text>();
        wintext.text = "";
        Debug.Log("text: " + wintext);
        cube = GameObject.Find("Cube");

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
                spriteR.sprite = sprites[40];
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jump);
                ableToJump = false;
            }
        }
        if (Input.GetKey(KeyCode.D) && alive) //moving right
        {
            rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
            if (frameOf60 % 8 <= 3)
            {
                if (ableToJump)
                    spriteR.sprite = sprites[3];
            }
            else
            {
                if (ableToJump)
                    spriteR.sprite = sprites[4];
            }
            if (flippedOnce)
            {
                spriteR.flipX = false;
            }
        }
        if (Input.GetKey(KeyCode.A) && alive) //moving left
        {
            rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
            if (frameOf60 % 8 <= 3)
            {
                if (ableToJump)
                    spriteR.sprite = sprites[4];
            }
            else
            {
                if (ableToJump)
                    spriteR.sprite = sprites[3];
            }
            spriteR.flipX = true;
            flippedOnce = true;
        }
        
        
        if (Input.GetKeyDown(KeyCode.S) && warpcooldown == 0) //warp
        {
            spriteR.sprite = sprites[40];
            transform.position = new Vector3(rigidbody.position.x, rigidbody.position.y + 1, 0);
            warpcooldown = 60;
            alive = true;
            ableToJump = false;
            wintext.text = "";
            cube.transform.localScale = new Vector3(0.0f, cube.transform.localScale.y, cube.transform.localScale.z);            
        }

        if (warpcooldown > 0)
        {
            cube.transform.localScale+= new Vector3(0.055f, 0.0f, 0.0f);
            warpcooldown--;
        }
        else
        {
            warping = false;
        }

        
        if (!alive && rigidbody.position.x > 2.0 && rigidbody.position.x < 4.0)
        {
            wintext.text = "You know you can doublejump, [R]ight?";
            Debug.Log(wintext.text);
 
        }
        if (!alive && rigidbody.position.x > 4.3 && rigidbody.position.x < 7.5)
        {
            wintext.text = "You touched lava!\nWhat a tragedy, [S]orry...";
            Debug.Log(wintext.text);
        }
        if (Input.GetKeyDown(KeyCode.R) && level == 1)
        {
            Application.LoadLevel(0);
        }
        
    }



    void OnCollisionStay2D(Collision2D obj)
    {
        if (obj.gameObject.tag == "Lava")
        {
            Debug.Log("player 1 died");
            alive = false;
            spriteR.sprite = sprites[32];
            transform.position = new Vector3(rigidbody.position.x, -3.55f, 0);
        }
        else if (obj.gameObject.tag == "ProcGround")
        {
            ableToJump = true;
        }
        if (obj.gameObject.tag == "Goal")
        {
            level++;
            if (level == 2)
            {
                transform.position = new Vector3(9.0f, -3.05f, 0);
            }
            else
            {
                transform.position = new Vector3(15.0f, -3.55f, 0);
                Debug.Log(level);
                Debug.Log(transform.position.x);
            }
            
        }


    }
}
