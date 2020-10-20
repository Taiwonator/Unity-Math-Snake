using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomQuiz : MonoBehaviour {

	public static CustomQuiz custom_controller;

	FoodController foodController;
	public SnakeScriptableObject data;
	public GameObject CustomCoin;
	public GameObject aAnswer;
	public GameObject bAnswer;
	public GameObject cAnswer;
	public GameObject dAnswer;

	[SerializeField]
	public List<QuestionObject> CustomQuestions;
	List<GameObject> AlphabetChoices;

	bool start = false;

	// Use this for initialization
	void Awake () {
		custom_controller = this;
		foodController = GetComponent<FoodController> ();
		AlphabetChoices = new List<GameObject> ();
		AlphabetChoices.Add (aAnswer);
		AlphabetChoices.Add (bAnswer);
		AlphabetChoices.Add (cAnswer);
		AlphabetChoices.Add (dAnswer);
		CustomCoin = data.CustomFood;
//		CustomQuestions = new List<QuestionObject> (GetComponent<CreateCustomQuestions> ().CustomQuestions);
//		print(DataManager.control.questions.questions);
		if (DataManager.control == null) {
			CustomQuestions = new List<QuestionObject> (GetComponent<CreateCustomQuestions> ().CustomQuestions);
			print ("why:  " + DataManager.control);
		} else {
			if (DataManager.control.questions.questions != null) {
				if (DataManager.control.questions.questions.Count == 0 || DataManager.control.questions.questions.Count == 1) {
					CustomQuestions = new List<QuestionObject> (GetComponent<CreateCustomQuestions> ().CustomQuestions);
				} else {
					for (int i = 0; i < DataManager.control.questions.questions.Count; i++) {
						QuestionObject temp = new QuestionObject ();
						temp.Restore (DataManager.control.questions.questions [i]);
						CustomQuestions.Add (temp);
					}
				}
			} else {
				CustomQuestions = new List<QuestionObject> (GetComponent<CreateCustomQuestions> ().CustomQuestions);
			}

//			print (DataManager.control);
		}

	}

	// Update is called once per frame
	void Update () {

	}

	public void SpawnCustomCoins(int x){
		for(int i = 0; i < x; i++){
			int rand = Random.Range (0, foodController.grid.Count);
			//			Vector2 pos = new Vector2 (ReturnRounded (Random.Range (-2.5f, 2.5f), data.moveDistance), ReturnRounded (Random.Range (-2.5f, 2f), data.moveDistance));
			Vector2 pos = foodController.grid[rand];
			foodController.grid.Remove (foodController.grid[rand]);
			GameObject temp2 = Instantiate (CustomCoin, pos, Quaternion.identity);
			temp2.GetComponent<FoodObject> ().pos = pos;
		}
		if (!start) {
			foodController.FillFoodList ();
			SetCustomAnswers ();
			start = true;
		}
	}

	public void SetCustomAnswers(){
		int randQuestionNum = Random.Range (0, CustomQuestions.Count);
		int rand = Random.Range (0, 4);
		int rand2 = Random.Range (0, 4);
		foodController.foodItems [rand].GetComponentInChildren<Text>().text = System.Convert.ToChar((rand2 % 4) + 65).ToString();
		foodController.foodItems [(rand + 1) % 4].GetComponentInChildren<Text> ().text = System.Convert.ToChar (((rand2 + 1) % 4) + 65).ToString();;
		foodController.foodItems [(rand + 2) % 4].GetComponentInChildren<Text>().text = System.Convert.ToChar(((rand2 + 2) % 4) + 65).ToString();;
		foodController.foodItems [(rand + 3) % 4].GetComponentInChildren<Text>().text = System.Convert.ToChar(((rand2 + 3) % 4) + 65).ToString();;
		//
		foodController.foodItems [rand].GetComponent<FoodObject> ().correct = true;
		foodController.foodItems [(rand + 1) % 4].GetComponent<FoodObject> ().correct = false;
		foodController.foodItems [(rand + 2) % 4].GetComponent<FoodObject> ().correct = false;
		foodController.foodItems [(rand + 3) % 4].GetComponent<FoodObject> ().correct = false;

		//		if (rand2 == 0) {
		//			aAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].correctAns;
		//			bAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [0];
		//			cAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [1];
		//			dAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [2];
		//		} else if (rand2 == 1) {
		//			bAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].correctAns;
		//			aAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [0];
		//			cAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [1];
		//			dAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [2];
		//		} else if (rand2 == 2) {
		//			cAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].correctAns;
		//			bAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [0];
		//			aAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [1];
		//			dAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [2];
		//		} else if (rand2 == 3) {
		//			dAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].correctAns;
		//			bAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [0];
		//			cAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [1];
		//			aAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [2];
		//		}

		AlphabetChoices[rand2].GetComponentInChildren<Text> ().text = CustomQuestions [randQuestionNum].correctAns;
		AlphabetChoices [(rand2 + 1) % 4].GetComponentInChildren<Text> ().text = CustomQuestions [randQuestionNum].incorrectAns [0];
		AlphabetChoices[(rand2 + 2) % 4].GetComponentInChildren<Text> ().text = CustomQuestions [randQuestionNum].incorrectAns [1];
		AlphabetChoices[(rand2 + 3) % 4].GetComponentInChildren<Text> ().text = CustomQuestions [randQuestionNum].incorrectAns [2];



		foodController.AdvancedText.GetComponent<TextMeshPro> ().text =CustomQuestions[randQuestionNum].question;
	}
}
