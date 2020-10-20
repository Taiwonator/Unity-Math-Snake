using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseQuestion : MonoBehaviour {
	int num1;
	int num2;

	public int answer;
	public bool done;

	public Text text;
	public GameObject snakeController;

	public int foodNumber;

	bool first = false;

	// Use this for initialization
	void Start () {
//		snakeController.GetComponent<SnakeController> ().NormalCoins (foodNumber);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	public void RandomQuestion()
//	{
//		num1 = Random.Range (1, 10);
//		num2 = Random.Range (1, 10);
//		answer = num1 + num2;
//
////		text.text = num1 + " + " + num2;
//
////		snakeController.GetComponent<SnakeController> ().SpawnCoins (8, num1+num2);
////		snakeController.GetComponent<SnakeController> ().newSpawnCoins (1, num1+num2);
//		done = true;
//	}
}
