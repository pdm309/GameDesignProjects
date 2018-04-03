﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetAssigner : MonoBehaviour {
	[SerializeField]
	Texture2D[] sheetsNormal;
	[SerializeField]
	GameObject RoomObj;
    public GameObject prefab;
    public GameObject preCompass;
    public GameObject preMap;
    public GameObject preGoal;
    public Vector2 roomDimensions = new Vector2(16*17,16*9);
	public Vector2 gutterSize = new Vector2(16*9,16*4);
	public void Assign(Room[,] rooms){
		foreach (Room room in rooms){
			//skip point where there is no room
			if (room == null){
				continue;
			}
			//pick a random index for the array
			int index = Mathf.RoundToInt(Random.value * (sheetsNormal.Length -1));
            //find position to place room
            Vector3 pos = new Vector3(room.gridPos.x * (roomDimensions.x + gutterSize.x - 144), room.gridPos.y * (roomDimensions.y + gutterSize.y - 64), 0);
            RoomInstance myRoom = Instantiate(RoomObj, pos, Quaternion.identity).GetComponent<RoomInstance>();
			myRoom.Setup(sheetsNormal[index], room.gridPos, room.type, room.doorTop, room.doorBot, room.doorLeft, room.doorRight, prefab, preCompass, preMap, preGoal);
		}
	}
}
