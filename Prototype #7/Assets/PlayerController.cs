using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jump;
    public Rigidbody2D rigidbody;
    public bool alive = true;

    private SpriteRenderer spriteR;
    private bool flippedOnce = false;

    private Sprite[] sprites;
    private Sprite[] chestSprites;
    private int frame = 0;
    private int frameOf60 = 0;

    private bool won = false;
    private bool lost = false;
    private bool haveMap = false;
    private bool haveCompass = false;
    private bool failed = false;
    public Text wintext;
    public int level = 1;
    public GameObject quad;
    public GameObject arrow;
    int timeLeft = 60;
    GameObject point;

    GameObject[] mapSprites;
    Vector3 startPos;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteR = GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("Custom Edited - Super Smash Bros Customs - Captain Falcon");
        chestSprites = Resources.LoadAll<Sprite>("chest2");
        wintext = GameObject.Find("Text").GetComponent<Text>();
        wintext.text = "Time Left: 60";
        point = GameObject.Find("Map(Clone)");
        startPos = rigidbody.transform.position;
        mapSprites = GameObject.FindGameObjectsWithTag("MapSprite");
        //Debug.Log(mapSprites);
    }

    // Update is called once per frame
    void Update()
    {
        frame++;
        frameOf60 = frame % 60;
        if (frameOf60 == 1 && !won)
        {
            timeLeft--;
            wintext.text = "Time Left: " + timeLeft;
        }
        if (timeLeft <= 0)
        {
            wintext.text = "Time!";
            if (!won)
            {
                lost = true;
                GameObject.Find("MainCamera").GetComponent<AudioSource>().Stop();
                if (!failed)
                {
                    GameObject.Find("failed").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("failed").GetComponent<AudioSource>().clip, 0.7f);
                    failed = true;
                }

            }
        }

        if (rigidbody.transform.position.x < startPos.x - 128)
        {
            //left of starting/recent room
            foreach (GameObject mapSprite in mapSprites)
            {
                if (mapSprite.GetComponent<SpriteRenderer>().color.r < 1f && mapSprite.GetComponent<SpriteRenderer>().color.g < 1f && mapSprite.GetComponent<SpriteRenderer>().color.b < 1f)
                {
                    foreach (GameObject mapSprit in mapSprites)
                    {
                        
                        if (mapSprit.transform.position.x == mapSprite.transform.position.x - 16 && mapSprit.transform.position.y == mapSprite.transform.position.y)
                        {
                            mapSprit.GetComponent<SpriteRenderer>().color = mapSprite.GetComponent<SpriteRenderer>().color;
                            mapSprite.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
                            startPos = new Vector3(startPos.x - 256, startPos.y, startPos.z);
                        }
                    }
                }

            }
        }
        if (rigidbody.transform.position.x > startPos.x + 128)
        {
            //right of starting/recent room
            foreach (GameObject mapSprite in mapSprites)
            {
                if (mapSprite.GetComponent<SpriteRenderer>().color.r < 1f && mapSprite.GetComponent<SpriteRenderer>().color.g < 1f && mapSprite.GetComponent<SpriteRenderer>().color.b < 1f)
                {
                    foreach (GameObject mapSprit in mapSprites)
                    {

                        if (mapSprit.transform.position.x == mapSprite.transform.position.x + 16 && mapSprit.transform.position.y == mapSprite.transform.position.y)
                        {
                            mapSprit.GetComponent<SpriteRenderer>().color = mapSprite.GetComponent<SpriteRenderer>().color;
                            mapSprite.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
                            startPos = new Vector3(startPos.x + 256, startPos.y, startPos.z);
                        }
                    }
                }
            }
        }
        if (rigidbody.transform.position.y > startPos.y + 64)
        {
            //up of starting/recent room
            foreach (GameObject mapSprite in mapSprites)
            {
                if (mapSprite.GetComponent<SpriteRenderer>().color.r < 1f && mapSprite.GetComponent<SpriteRenderer>().color.g < 1f && mapSprite.GetComponent<SpriteRenderer>().color.b < 1f)
                {
                    foreach (GameObject mapSprit in mapSprites)
                    {

                        if (mapSprit.transform.position.x == mapSprite.transform.position.x && mapSprit.transform.position.y == mapSprite.transform.position.y + 8)
                        {
                            mapSprit.GetComponent<SpriteRenderer>().color = mapSprite.GetComponent<SpriteRenderer>().color;
                            mapSprite.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
                            startPos = new Vector3(startPos.x, startPos.y + 128, startPos.z);
                            
                        }
                    }
                }
            }
        }
        if (rigidbody.transform.position.y < startPos.y - 64)
        {
            //down of starting/recent room
            foreach (GameObject mapSprite in mapSprites)
            {
                if (mapSprite.GetComponent<SpriteRenderer>().color.r < 1f && mapSprite.GetComponent<SpriteRenderer>().color.g < 1f && mapSprite.GetComponent<SpriteRenderer>().color.b < 1f)
                {
                    foreach (GameObject mapSprit in mapSprites)
                    {

                        if (mapSprit.transform.position.x == mapSprite.transform.position.x && mapSprit.transform.position.y == mapSprite.transform.position.y - 8)
                        {
                            mapSprit.GetComponent<SpriteRenderer>().color = mapSprite.GetComponent<SpriteRenderer>().color;
                            mapSprite.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
                            startPos = new Vector3(startPos.x, startPos.y - 128, startPos.z);
                        }
                    }
                }

            }

        }

        if (Input.GetKey(KeyCode.D) && !won && !lost) //moving right
        {
            rigidbody.AddForce(Vector2.right * speed);
            //rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
            if (frameOf60 % 30 <= 6)
            {
                spriteR.sprite = sprites[3];
            }
            else if(frameOf60 % 30 > 6 && frameOf60 % 30 < 12)
            {
                spriteR.sprite = sprites[4];
            }
            else if (frameOf60 % 30 > 11 && frameOf60 % 30 < 18)
            {
                spriteR.sprite = sprites[5];
            }
            else if (frameOf60 % 30 > 17 && frameOf60 % 30 < 24)
            {
                spriteR.sprite = sprites[6];
            }
            else if (frameOf60 % 30 > 23)
            {
                spriteR.sprite = sprites[7];
            }

            if (flippedOnce)
            {
                spriteR.flipX = false;
            }
        }
        if (Input.GetKey(KeyCode.A) && !won && !lost) //moving left
        {
            rigidbody.AddForce(-Vector2.right * speed);
            //rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
            if (frameOf60 % 30 <= 6)
            {
                spriteR.sprite = sprites[7];
            }
            else if (frameOf60 % 30 > 6 && frameOf60 % 30 < 12)
            {
                spriteR.sprite = sprites[6];
            }
            else if (frameOf60 % 30 > 11 && frameOf60 % 30 < 18)
            {
                spriteR.sprite = sprites[5];
            }
            else if (frameOf60 % 30 > 17 && frameOf60 % 30 < 24)
            {
                spriteR.sprite = sprites[4];
            }
            else if (frameOf60 % 30 > 23)
            {
                spriteR.sprite = sprites[3];
            }
            spriteR.flipX = true;
            flippedOnce = true;
        }
        if (Input.GetKey(KeyCode.S) && !won && !lost) //moving down
        {
            rigidbody.AddForce(-Vector2.up * speed);
            //rigidbody.velocity = new Vector2(rigidbody.velocity.x, -speed);
        }
        if (Input.GetKey(KeyCode.W) && !won && !lost) //moving up
        {
            rigidbody.AddForce(Vector2.up * speed);
            //rigidbody.velocity = new Vector2(rigidbody.velocity.x, speed);
        }

        float f = AngleBetweenVector2(rigidbody.transform.position, point.transform.position);
        //Debug.Log(f);
        //Debug.Log(rigidbody.transform.position);
        //Debug.Log(point.transform.position);
        arrow.GetComponent<Rigidbody2D>().rotation = f;


        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(0);
        }

    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        return Mathf.Atan2(vec2.y - vec1.y, vec2.x - vec1.x) * 180 / Mathf.PI + 90;
    }

    void OnCollisionEnter2D(Collision2D obj)
    {
        Collider2D collider = obj.collider;

        if (obj.gameObject.tag == "Ammo")
        {
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Goal")
        {
            spriteR.sprite = sprites[1];
            won = true;
            wintext.text = "You win!";
            GameObject.Find("MainCamera").GetComponent<AudioSource>().Stop();
            spriteR.flipX = false;
            rigidbody.transform.position = new Vector3(rigidbody.transform.position.x, rigidbody.transform.position.y, rigidbody.transform.position.z - 3);
            rigidbody.transform.localScale = new Vector3(rigidbody.transform.localScale.x * 5, rigidbody.transform.localScale.y * 5, rigidbody.transform.localScale.z);
            GetComponent<AudioSource>().Play();
        }
        if (other.gameObject.tag == "Map")
        {
            Debug.Log("MAP GET");
            quad.SetActive(true);
            if (!haveMap)
            {
                quad.GetComponent<AudioSource>().Play();
                haveMap = true;
            }
            point = GameObject.Find("Goal(Clone)");
            GameObject.Find("Map(Clone)").GetComponent<SpriteRenderer>().sprite = chestSprites[1];

        }
        if (other.gameObject.tag == "Compass")
        {
            Debug.Log("COMPASS GET");
            arrow.SetActive(true);
            GameObject.Find("Compass(Clone)").GetComponent<SpriteRenderer>().sprite = chestSprites[1];
            if (!haveCompass)
            {
                arrow.GetComponent<AudioSource>().Play();
                haveCompass = true;
            }
        }
    }
}
    