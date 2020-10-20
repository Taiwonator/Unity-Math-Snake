using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArithmeticQuiz : MonoBehaviour {
	FoodController foodController;
	public SnakeScriptableObject data;
	public GameObject ArithmeticCoin;
	public float ArithmeticAnswer;

	public GameObject ArithmeticQuizCoin;

	bool start = false;

	// Use this for initialization
	void Start () {
		foodController = GetComponent<FoodController> ();
		ArithmeticCoin = data.ArithmeticFood;
		ArithmeticQuizCoin = data.ArithmeticQuizFood;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void PlayArithmeticGame(int mode, int x) {
		if (mode == 0) {
			if (foodController.foodItems.Count != 0) {
				DestroyAll ();
			} 
			foodController.foodTag = "ArithmeticCoin";
			SpawnArithmeticCoins (x);
		} else {
			if (foodController.foodItems.Count != 0) {
				DestroyAll ();
			}
			foodController.foodTag = "ArithmeticQuizCoin";
			StartArithmeticQuiz (x);
		}
	}

	public void DestroyAll(){
		for (int i = 0; i < foodController.foodItems.Count; i++) {
			Destroy (foodController.foodItems[i]);
		}
	}

	void StartArithmeticQuiz(int x){
		for(int i = 0; i < x; i++){
			int rand = Random.Range (0, foodController.grid.Count);
			//Vector2 pos = new Vector2 (ReturnRounded (Random.Range (-2.5f, 2.5f), data.moveDistance), ReturnRounded (Random.Range (-2.5f, 2f), data.moveDistance));
			Vector2 pos = foodController.grid[rand];
			foodController.grid.Remove (foodController.grid[rand]);
			GameObject temp2 = Instantiate (ArithmeticQuizCoin, pos, Quaternion.identity);
			temp2.GetComponent<FoodObject> ().pos = pos;
		}
		foodController.FillFoodList ();
		SetArithmeticAnswers ("add");
	}

	public void SpawnArithmeticCoins(int x){
		for (int i = 0; i < x; i++) {
			int rand = Random.Range (0, foodController.grid.Count);
			//Vector2 pos = new Vector2 (ReturnRounded (Random.Range (-2.5f, 2.5f), data.moveDistance), ReturnRounded (Random.Range (-2.5f, 2f), data.moveDistance));
			Vector2 pos = foodController.grid [rand];
			foodController.grid.Remove (foodController.grid [rand]);
			GameObject temp2 = Instantiate (ArithmeticCoin, pos, Quaternion.identity);
			temp2.GetComponent<FoodObject> ().pos = pos;
		}
		foodController.FillFoodList ();
		SetArithmeticAnswers ("add");
	}

	float num1;
	float num2;

	public void SetArithmeticAnswers(string type){

		if (type == "add") {
			MathsAddition ();
		} else if (type == "sub") {
			MathsSubtraction ();
		} else if (type == "mult") {
			MathsMultiplication ();
		} else if (type == "div") {
			MathsDivision ();
		}

		int rand = Random.Range (0, foodController.foodItems.Count);
		for(int i = 0; i < foodController.foodItems.Count; i++){
			if (i != rand) {
				foodController.foodItems [i].GetComponent<FoodObject> ().correct = false;
				float randAns = 0;
				if (type == "add") {
					randAns = (Random.Range (1, 20));
				} else if (type == "sub") {
					randAns = (Random.Range (-10, 10));
				} else if (type == "mult") {
					randAns = (Random.Range (1, 100));
				} else if (type == "div") {
					randAns = (Random.Range (0, 50));
				}
				while (randAns == ArithmeticAnswer) {
					if (type == "add") {
						randAns = (Random.Range (1, 20));
					} else if (type == "sub") {
						randAns = (Random.Range (-10, 10));
					} else if (type == "mult") {
						randAns = (Random.Range (1, 100));
					} else if (type == "div") {
						randAns = (Random.Range (0, 50));
					}
				}
				foodController.foodItems [i].GetComponentInChildren<Text> ().text = randAns.ToString ();
			}
		}
		foodController.foodItems [rand].GetComponent<FoodObject>().correct = true;
		if (type == "add") {
			foodController.foodItems [rand].GetComponentInChildren<Text> ().text = (num1 + num2).ToString ();
		} else if (type == "sub") {
			foodController.foodItems [rand].GetComponentInChildren<Text> ().text = (num1 - num2).ToString ();
		} else if (type == "mult") {
			foodController.foodItems [rand].GetComponentInChildren<Text> ().text = (num1 * num2).ToString ();
		} else if (type == "div") {
			foodController.foodItems [rand].GetComponentInChildren<Text> ().text = (num1 / num2).ToString ();
		}
	}

	void MathsAddition(){
		num1 = Random.Range (1, 10);
		num2 = Random.Range (1, 10);
		ArithmeticAnswer =  num1 + num2;
		foodController.Question.GetComponent<Text> ().text = num1.ToString() + " + " + num2.ToString();
//		print (ArithmeticAnswer);
	}

	void MathsSubtraction(){
		num1 = Random.Range (1, 20);
		num2 = Random.Range (1, 10);
		ArithmeticAnswer =  num1 - num2;
		foodController.Question.GetComponent<Text> ().text = num1.ToString() + " - " + num2.ToString();
	}

	void MathsMultiplication(){
		num1 = Random.Range (1, 10);
		num2 = Random.Range (1, 10);
		ArithmeticAnswer =  num1 * num2;
		foodController.Question.GetComponent<Text> ().text = num1.ToString() + " x " + num2.ToString();
	}	

	void MathsDivision(){
		num1 = 2*(Random.Range (1, 50));
		num2 = 2*(Random.Range (1, 3));
		ArithmeticAnswer =  num1 / num2;
		foodController.Question.GetComponent<Text> ().text = num1.ToString() + " / " + num2.ToString();
	}	

	string RandomQuestion(){
		int rand = Random.Range (1, 5);
		string x;
		if (rand == 1) {
			x = "add";
		} else if(rand == 2){
			x = "div";
		} else if(rand == 3){
			x = "mult";
		} else{
			x = "sub";
		} 
		return x;
	}

}
