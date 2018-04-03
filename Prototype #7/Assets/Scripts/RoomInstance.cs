using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInstance : MonoBehaviour {
	public Texture2D tex;
	[HideInInspector]
	public Vector2 gridPos;
	public int type; // 0: normal, 1: enter
	[HideInInspector]
	public bool doorTop, doorBot, doorLeft, doorRight;
	[SerializeField]
	GameObject doorU, doorD, doorL, doorR, doorWall;
	[SerializeField]
	ColorToGameObject[] mappings;
	float tileSize = 16;
    public GameObject prefab;
    public GameObject preCompass;
    public GameObject preMap;
    public GameObject preGoal;
    Vector2 roomSizeInTiles = new Vector2(9,17);
    //Sprite[] sprites = Resources.LoadAll<Sprite>("walls1");
	public void Setup(Texture2D _tex, Vector2 _gridPos, int _type, bool _doorTop, bool _doorBot, bool _doorLeft, bool _doorRight, GameObject _prefab, GameObject _preCompass, GameObject _preMap, GameObject _preGoal){
        tex = _tex;
        gridPos = _gridPos;
        type = _type;
        doorTop = _doorTop;
        doorBot = _doorBot;
        doorLeft = _doorLeft;
        doorRight = _doorRight;
        prefab = _prefab;
        preCompass = _preCompass;
        preMap = _preMap;
        preGoal = _preGoal;
        MakeDoors();
        GenerateRoomTiles();
    }
    void MakeDoors(){
		//top door, get position then spawn
		Vector3 spawnPos = transform.position + Vector3.up*(roomSizeInTiles.y/4 * tileSize) - Vector3.up*(tileSize/4);
		PlaceDoor(spawnPos, doorTop, doorU);
		//bottom door
		spawnPos = transform.position + Vector3.down*(roomSizeInTiles.y/4 * tileSize) - Vector3.down*(tileSize/4);
		PlaceDoor(spawnPos, doorBot, doorD);
		//right door
		spawnPos = transform.position + Vector3.right*(roomSizeInTiles.x * tileSize) - Vector3.right*(tileSize);
		PlaceDoor(spawnPos, doorRight, doorR);
		//left door
		spawnPos = transform.position + Vector3.left*(roomSizeInTiles.x * tileSize) - Vector3.left*(tileSize);
		PlaceDoor(spawnPos, doorLeft, doorL);
	}
	void PlaceDoor(Vector3 spawnPos, bool door, GameObject doorSpawn){
		// check whether its a door or wall, then spawn
		if (door){
            //Instantiate(doorSpawn, spawnPos, Quaternion.identity).transform.parent = transform;
            if (Random.Range(0, 5) == 3)
            {
                //Debug.Log("Lucky!");
                if (GameObject.Find("Compass(Clone)") == null)
                {
                    //Debug.Log("Dropping Compass");
                    Instantiate(preCompass, spawnPos, Quaternion.identity);
                }
                else if (GameObject.Find("Map(Clone)") == null)
                {
                    //Debug.Log("Dropping Map");
                    Instantiate(preMap, spawnPos, Quaternion.identity);
                }
                else if (GameObject.Find("Goal(Clone)") == null)
                {
                    //Debug.Log("Dropping Goal");
                    Instantiate(preGoal, spawnPos, Quaternion.identity);
                }
            }
        }
        else{
            Instantiate(prefab, spawnPos, Quaternion.identity);//.transform.parent = transform;
            //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //cube.transform.position = new Vector3(spawnPos.x, spawnPos.y, 1);
            //cube.transform.localScale += new Vector3(15.0f, 15.0f, 0.0f);
            //Destroy(cube.GetComponent("BoxCollider"));
            //Rigidbody2D rigid = cube.AddComponent<Rigidbody2D>();
            //BoxCollider2D bx = cube.AddComponent<BoxCollider2D>();

            //GameObject go = new GameObject("New Sprite");
            //SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
            //renderer.size = new Vector2(7.0f, 12.0f);
            //renderer.sprite = sprites[num];
            //GameObject go = Instantiate(Resources.Load("walls1" + num)) as GameObject;
        }
	}
	void GenerateRoomTiles(){
		//loop through every pixel of the texture
		for(int x = 0; x < tex.width; x++){
			for (int y = 0; y < tex.height; y++){
				GenerateTile(x,y);
			}
		}
	}
	void GenerateTile(int x, int y){
		Color pixelColor = tex.GetPixel(x,y);
		//skip clear spaces in texture
		if (pixelColor.a == 0){
			return;
		}
		//find the color to math the pixel
		foreach (ColorToGameObject mapping in mappings){
			if (mapping.color.Equals(pixelColor)){
                Vector3 spawnPos = positionFromTileGrid(x,y);
                Instantiate(prefab, spawnPos, Quaternion.identity);//.transform.parent = this.transform;
                //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //cube.transform.position = new Vector3(spawnPos.x, spawnPos.y, 1);
                //cube.transform.localScale += new Vector3(15.0f, 15.0f, 0.0f);
                //Destroy(cube.GetComponent("BoxCollider"));
                //Rigidbody2D rigid = cube.AddComponent<Rigidbody2D>();
                //BoxCollider2D bx = cube.AddComponent<BoxCollider2D>();

                //GameObject go = new GameObject("New Sprite");
                //SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
                //renderer.size = new Vector2(7.0f, 12.0f);
                //renderer.sprite = sprites[num];

                //GameObject go = Instantiate(Resources.Load("walls1" + num)) as GameObject;
            }
		}
	}
	Vector3 positionFromTileGrid(int x, int y){
		Vector3 ret;
		//find difference between the corner of the texture and the center of this object
		Vector3 offset = new Vector3((-roomSizeInTiles.x + 1)*tileSize, (roomSizeInTiles.y/4)*tileSize - (tileSize/4), 0);
		//find scaled up position at the offset
		ret = new Vector3(tileSize * (float) x, -tileSize * (float) y, 0) + offset + transform.position;
		return ret;
	}
}
