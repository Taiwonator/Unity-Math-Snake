using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Direction
//Move
//SetParts DONE
//CreateParts DONE
//ObjectsTouched

public class Snake : MonoBehaviour {

	public SnakeScriptableObject data;
	[SerializeField]
	private List<GameObject> snakeBody;  //5 elements

	private bool creationDone = false;
	int sameTwice;

	private int nextDirection = 0;
	private int snakeDirection = 0;
	//0 UP     1 RIGHT
	//2 DOWN   3 LEFT
	private Vector2 nextPos;


	void Awake()
	{
		//Initialise Objects
	}

	void Start()
	{
		//CreateBody
		if (data.moveDistance > data.prefab.transform.localScale.x) {
			data.moveDistance = data.prefab.transform.localScale.x;
		}
	}

	void Update()
	{
		//Movement and detection
		if (!creationDone) {
			CreateBody ();
		}
		Move ();
		Timer ();

//		snakeBody [snakeBody.Count - 1].transform.localScale = new Vector2 (0.5f, 0.5f);
//		for (int i = 0; i <= snakeBody.Count - 2; i++) {
//			snakeBody[i].transform.localScale = new Vector2 (0.25f, 0.25f);
//		}
	}

	void CreateBody()
	{
		snakeBody = new List<GameObject> ();
		InstantiateBody ();
	}

	void InstantiateBody()
	{
		int i = 0;
		while(i < data.snakeLength) {
			GameObject temp = Instantiate (data.prefab, new Vector2(0, 0), Quaternion.identity);
			temp.transform.parent = this.gameObject.transform;
			snakeBody.Add (temp);
			i++;
	    }
		if (i == snakeBody.Count) {
			creationDone = true;
		}
	}

	float timer2;
	void Move()
	{
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			nextDirection = 0;
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			nextDirection = 1;
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			nextDirection = 2;
		} else if(Input.GetKeyDown(KeyCode.LeftArrow)) {
			nextDirection = 3;
		}
		//Debug.Log (nextDirection);
		if (timer >= data.speed) {
			switch (nextDirection) {
			case 0:
				nextPos = new Vector2 (snakeBody [snakeBody.Count - 1].transform.position.x, snakeBody [snakeBody.Count - 1].transform.position.y + data.moveDistance);

				WhatDirection (0);
				break;
			case 1:
				nextPos = new Vector2 (snakeBody [snakeBody.Count - 1].transform.position.x + data.moveDistance, snakeBody [snakeBody.Count - 1].transform.position.y);

				WhatDirection (1);
				break;
			case 2:
				nextPos = new Vector2 (snakeBody [snakeBody.Count - 1].transform.position.x, snakeBody [snakeBody.Count - 1].transform.position.y - data.moveDistance);

				WhatDirection (2);
				break;
			case 3:
				nextPos = new Vector2 (snakeBody [snakeBody.Count - 1].transform.position.x - data.moveDistance, snakeBody [snakeBody.Count - 1].transform.position.y);

				WhatDirection (3);
				break;
			}
			snakeBody [0].transform.position = nextPos;
			GameObject temp = snakeBody [0];
			snakeBody.Remove (snakeBody [0]);
			snakeBody.Insert (snakeBody.Count, temp);
			timer = 0;
		}
	}

	float timer = 0;
	void Timer()
	{
		timer += Time.deltaTime;
	}

	int up; int right; int down; int left;
	void WhatDirection(int di)
	{
		if (di == 0) {
			up += 1;
			right = 0;
			left = 0;
			down = 0;
		} else if (di == 1) {
			right += 1;
			up = 0;
			left = 0;
			down = 0;
		} else if (di == 2) {
			down += 1;
			right = 0;
			left = 0;
			up = 0;
		} else {
			left += 1;
			right = 0;
			up = 0;
			down = 0;
		}
		if (up > 0) {
			//Debug.Log (up);
		}
	}
}
