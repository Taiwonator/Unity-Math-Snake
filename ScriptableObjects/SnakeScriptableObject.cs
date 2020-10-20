using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SnakeScriptableObject : ScriptableObject {

	public int snakeLength = 5;

	public GameObject prefab;
	public GameObject prefab2;
	public GameObject prefab3;
	public GameObject prefab4;
	public GameObject prefab5;
	public GameObject prefab6;
	public GameObject prefab7;

	public GameObject defaultSkin;

//	public GameObject[] skins;

	public GameObject stone;
	public GameObject coin;
	public Vector2 startingPos;
	public float moveDistance;
	public float speed;
	public float size;
	public int SwipeDirection;

	public Sprite up;
	public Sprite right;
	public Sprite down;
	public Sprite left;

	public Vector2 graphDimensions;
	public Vector2 WorldDimensions;

	public GameObject blue;
	public GameObject red;
	public GameObject yellow;

	public GameObject ArithmeticFood;
	public GameObject ArithmeticQuizFood;
	public GameObject CustomFood;
	public GameObject QuadraticFood;
	public GameObject TrigFood;
	public GameObject BasicCoin;

	public Sprite activeLevel;
	public Sprite inactiveLevel;

}
