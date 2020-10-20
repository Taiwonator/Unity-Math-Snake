using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrigQuiz : MonoBehaviour {
	FoodController foodController;
	public SnakeScriptableObject data;
	public GameObject TrigCoin;
	string question;
	[SerializeField]
	string answer;
	[SerializeField]
	string prevAnswer = "-1";

	bool start = false;
	int t;

	[SerializeField]
	List<string> possibleAnswers;

	// Use this for initialization
	void Start () {

		foodController = GetComponent<FoodController> ();
		TrigCoin = data.TrigFood;

		possibleAnswers.Add ("1/2");
		possibleAnswers.Add ("√3/2");
		possibleAnswers.Add ("√2/2");
		possibleAnswers.Add ("√3");
		possibleAnswers.Add ("(2√3)/3");
		possibleAnswers.Add ("√2");
		possibleAnswers.Add ("√3/3");
		possibleAnswers.Add ("1");
		possibleAnswers.Add ("2");
		possibleAnswers.Add ("0");
		possibleAnswers.Add ("-1");

		answer = Approximate(SolveTrigFunction (RandomFunction(), RandomNum(5)));
		print (question + "  " + answer);

//		SpawnTrigCoins (3);
//		SetTrigQuestion ();


	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.O)) {
			SetTrigQuestion ();
		}
	}

	float SolveTrigFunction(string function, float diviser){
		float y = 1;
		if(function == "sin"){
		    y = Mathf.Sin (Mathf.PI / diviser);
		} 
		else if(function == "cos"){
			y = Mathf.Cos (Mathf.PI / diviser);
		}
		else if(function == "tan"){
			if (diviser == 2) {
				y = Mathf.Tan (Mathf.PI / 3);
			} else {
				y = Mathf.Tan (Mathf.PI / diviser);
			}
		}
		else if(function == "cosec"){
			if (diviser == 1) {
				y = 1/Mathf.Sin (Mathf.PI / 2);
			} else {
				y = 1/Mathf.Sin (Mathf.PI / diviser);
			}
		}
		else if(function == "sec"){
			if (diviser == 2) {
				y = 1/Mathf.Cos (Mathf.PI / 3);
			} else {
				y = 1/Mathf.Cos (Mathf.PI / diviser);
			}
		}
		else if(function == "cot"){
			if (diviser == 1) {
				y = 1/Mathf.Tan (Mathf.PI / 2);
			} else {
				y = 1/Mathf.Tan (Mathf.PI / diviser);
			}
		}
		question = function + "(" + (180/diviser).ToString() + "°)";
		return y;
	}

	string Approximate(float input){
		string output = " ";
		if (Mathf.Approximately (input, 0.5f)) {
			output = "1/2";
		}
		else if (Mathf.Approximately (input, Mathf.Sqrt (3) / 2)) {
			output = "√3/2";
		}
		else if (Mathf.Approximately (input, Mathf.Sqrt (2) / 2)) {
			output = "√2/2";
		}
		else if (Mathf.Approximately (input, Mathf.Sqrt (3))) {
			output = "√3";
		}
		else if (Mathf.Approximately (input, (2*Mathf.Sqrt (3))/3)) {
			output = "(2√3)/3";
		}
		else if (Mathf.Approximately (input, Mathf.Sqrt (2))) {
			output = "√2";
		}
		else if (Mathf.Approximately (input, Mathf.Sqrt (3) / 3)) {
			output = "√3/3";
		}
		else if (Mathf.Abs(input) < 0.0001f) {
			output = "0";
		}
		else {
			output = input.ToString ();
		}
		return output;
	}


	bool first = false;
	public void SetTrigQuestion(){
		
		prevAnswer = answer;
//		if (!possibleAnswers.Contains (prevAnswer) && prevAnswer.Length != 0) {
//			possibleAnswers.Add (prevAnswer);
//		}
		if (prevAnswer.Length != 0) {
			possibleAnswers.Add (prevAnswer);
		}
		answer = Approximate(SolveTrigFunction (RandomFunction(), RandomNum(t)));
//		foodController.AdvancedText.GetComponent<TextMeshPro> ().text = question;
		foodController.Question.GetComponent<Text>().text = question;
		int rand = Random.Range (0, foodController.foodItems.Count);
		possibleAnswers.Remove(answer);
		if (answer == prevAnswer) {
			possibleAnswers.Remove (answer);
		}

		for (int i = 0; i < foodController.foodItems.Count; i++) {
			if (i != rand) {
				foodController.foodItems [i].GetComponent<FoodObject> ().correct = false;
				int rand2 = Random.Range (0, possibleAnswers.Count);
				foodController.foodItems [i].GetComponentInChildren<Text> ().text = possibleAnswers [rand2];
			}
		}

		foodController.foodItems [rand].GetComponentInChildren<Text> ().text = answer;
		foodController.foodItems [rand].GetComponent<FoodObject> ().correct = true;
	}



	public void SpawnTrigCoins(int x){
		for(int i = 0; i < x; i++){
			int rand = Random.Range (0, foodController.grid.Count);
			//Vector2 pos = new Vector2 (ReturnRounded (Random.Range (-2.5f, 2.5f), data.moveDistance), ReturnRounded (Random.Range (-2.5f, 2f), data.moveDistance));
			Vector2 pos = foodController.grid[rand];
			foodController.grid.Remove (foodController.grid[rand]);
			GameObject temp2 = Instantiate (TrigCoin, pos, Quaternion.identity);
			temp2.GetComponent<FoodObject> ().pos = pos;
		}
		if (!start) {
			foodController.FillFoodList ();
			SetTrigQuestion ();
			start = true;
		}
	}

	string RandomFunction(){
		int rand = Random.Range (1, 7);
		string functionToReturn = " ";
		switch (rand) {
		case 1: 
			functionToReturn = "sin";
			break;
		case 2: 
			functionToReturn = "cos";
			break;
		case 3: 
			functionToReturn = "tan";
			break;
		case 4: 
			functionToReturn = "sec";
			break;
		case 5: 
			functionToReturn = "cosec";
			break;
		case 6: 
			functionToReturn = "cot";
			break;
		}
		return functionToReturn;
	}

	int RandomNum(int x){
		int rand = Random.Range(1, 7);
		if (rand == x || rand == 5) {
			rand++;
		}
		return rand;
	}
}
