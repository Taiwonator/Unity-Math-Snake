using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodController : MonoBehaviour {
	public SnakeScriptableObject data;
	public List<GameObject> foodItems;
	public string foodTag;
	public GameObject snakeOne;
	[SerializeField]
	public int foodNumber;
	public GameObject Question;
//	public float ArithmeticAnswer;
//	public GameObject ArithmeticCoin;
//	public GameObject QuadraticCoin;
//	public GameObject CustomCoin;
	public GameObject AdvancedText;

//	public List<int> QuadraticCoefficents;
//	public List<float> QuadraticSolutions;

	public GameObject aAnswer;
	public GameObject bAnswer;
	public GameObject cAnswer;
	public GameObject dAnswer;

	List<QuestionObject> CustomQuestions;
	List<GameObject> AlphabetChoices;

	[SerializeField]
	public List<Vector2> grid;

	public GameObject Free;
	public GameObject Quiz;

	bool start = false;

	public bool basic;
	public int basicNum;

	// Use this for initialization
	void Start () {
//		AlphabetChoices = new List<GameObject> ();
//		AlphabetChoices.Add (aAnswer);
//		AlphabetChoices.Add (bAnswer);
//		AlphabetChoices.Add (cAnswer);
//		AlphabetChoices.Add (dAnswer);

//		QuadraticSolutions = new List<float> ();
//		QuadraticSolutions.Add (0);
//		QuadraticSolutions.Add (0);

		FoodGrid ();

		if (basic) {
			SpawnCoins (basicNum);
		}

//		ArithmeticCoin = data.ArithmeticFood;
//		QuadraticCoin = data.QuadraticFood;
//		CustomCoin = data.CustomFood;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
//			RelocateItem (GameObject.Find("FoodObject(1)"));
		}
	}

	public void TurnOnQuiz(){
		GetComponent<Timer> ().color = new Color32 (94, 255, 202, 255);
		Free.GetComponent<CircleCollider2D> ().enabled = false;
	}

	public void TurnOffQuiz(){
		Free.GetComponent<CircleCollider2D> ().enabled = true;
		GetComponent<Timer> ().color = GetComponent<Timer> ().startColor;
		if (Free != null) {
			GetComponent<ArithmeticQuiz> ().DestroyAll ();
		} else {
			GetComponent<ArithmeticQuiz> ().PlayArithmeticGame (0, foodNumber);
		}
	}


	public void SpawnCoins(int x){
		for(int i = 0; i < x; i++){
			int rand = Random.Range (0, grid.Count);
			//Vector2 pos = new Vector2 (ReturnRounded (Random.Range (-2.5f, 2.5f), data.moveDistance), ReturnRounded (Random.Range (-2.5f, 2f), data.moveDistance));
			Vector2 pos = grid[rand];
			grid.Remove (grid[rand]);
			GameObject temp2 = Instantiate (data.BasicCoin, pos, Quaternion.identity);
			temp2.GetComponent<FoodObject> ().pos = pos;
		}
		if (!start) {
			FillFoodList ();
			start = true;
		}
	}


	public void RelocateItem(GameObject otherGameObject){
		otherGameObject.gameObject.GetComponent<FoodObject> ().prevPos = otherGameObject.gameObject.transform.position;
//		Vector2 pos = new Vector2 (ReturnRounded (Random.Range (-1.5f, 2), data.moveDistance), ReturnRounded (Random.Range (-3, 0.5f), data.moveDistance));
		int rand = Random.Range(0, grid.Count);
		Vector2 pos = grid [rand];
//		while (pos == foodItems [0].GetComponent<FoodObject> ().prevPos) {
		while(Vector2.Distance(pos, otherGameObject.GetComponent<FoodObject> ().prevPos) <= 1f){
//			pos = new Vector2 (ReturnRounded (Random.Range (-1.5f, 2), data.moveDistance), ReturnRounded (Random.Range (-3, 0.5f), data.moveDistance));
			rand = Random.Range(0, grid.Count);
			pos = grid [rand];
		}
//		if (!snakeOne.GetComponent<Snake2> ().isPlayer) {
//			while (Vector2.Distance (pos, new Vector2 (0, 0)) >= 2f) {
//				rand = Random.Range (0, grid.Count);
//				pos = grid [rand];
//			}
//		}

//		for (int i = 0; i < snakeOne.GetComponent<Snake2> ().previousPos.Count; i++) {
//			while (pos == snakeOne.GetComponent<Snake2> ().previousPos [i]) {
////			while(Vector2.Distance(pos, snakeOne.GetComponent<Snake2> ().previousPos [i]) <=3){
//				pos = new Vector2 (ReturnRounded (Random.Range (-1.5f, 2), data.moveDistance), ReturnRounded (Random.Range (-3, 0.5f), data.moveDistance));
//			}
//		}

//		for (int i = 0; i < foodItems.Count; i++) {
//			while (pos == foodItems[i].GetComponent<FoodObject>().pos) {
////			while (Vector2.Distance(pos, foodItems[i].GetComponent<FoodObject>().pos) <= 1.8f) {
//					pos = new Vector2 (ReturnRounded (Random.Range (-1.5f, 2), data.moveDistance), ReturnRounded (Random.Range (-3, 0.5f), data.moveDistance));
//				}
//		}
			
		otherGameObject.gameObject.transform.position = pos;
		otherGameObject.gameObject.GetComponent<FoodObject> ().pos = pos;
		grid.Add (otherGameObject.gameObject.GetComponent<FoodObject> ().prevPos);
		grid.Remove (pos);
	}

	float ReturnRounded(float input, float roundedMultiplier){
		float val = (Mathf.Floor((input / roundedMultiplier)) * roundedMultiplier);
		return val;
	}

	float ReturnRounded2(float input, float roundedMultiplier){
		float val = (Mathf.Round((input / roundedMultiplier)) * roundedMultiplier);
		return val;
	}

	public void FillFoodList(){
		foodItems = new List<GameObject> ();
		for (int i = 0; i < foodNumber; i++) {
			foodItems.Add (GameObject.FindGameObjectsWithTag (foodTag)[i]);
		}
		start = true;
	}

	float DistanceApart(Vector2 a, Vector2 b){
		return Vector2.Distance (a, b);
	}

//	float num1;
//	float num2;
//
//	public void SetArithmeticAnswers(string type){
//
//		if (type == "add") {
//			MathsAddition ();
//		} else if (type == "sub") {
//			MathsSubtraction ();
//		} else if (type == "mult") {
//			MathsMultiplication ();
//		} else if (type == "div") {
//			MathsDivision ();
//		}
//
//		int rand = Random.Range (0, foodItems.Count);
//		for(int i = 0; i < foodItems.Count; i++){
//			if (i != rand) {
//				foodItems [i].GetComponent<FoodObject> ().correct = false;
//				float randAns = 0;
//				if (type == "add") {
//					randAns = (Random.Range (1, 20));
//				} else if (type == "sub") {
//					randAns = (Random.Range (-10, 10));
//				} else if (type == "mult") {
//					randAns = (Random.Range (1, 100));
//				} else if (type == "div") {
//					randAns = (Random.Range (0, 50));
//				}
//				while (randAns == ArithmeticAnswer) {
//					if (type == "add") {
//						randAns = (Random.Range (1, 20));
//					} else if (type == "sub") {
//						randAns = (Random.Range (-10, 10));
//					} else if (type == "mult") {
//						randAns = (Random.Range (1, 100));
//					} else if (type == "div") {
//						randAns = (Random.Range (0, 50));
//					}
//				}
//				foodItems [i].GetComponentInChildren<Text> ().text = randAns.ToString ();
//			}
//		}
//		foodItems [rand].GetComponent<FoodObject>().correct = true;
//		if (type == "add") {
//			foodItems [rand].GetComponentInChildren<Text> ().text = (num1 + num2).ToString ();
//		} else if (type == "sub") {
//			foodItems [rand].GetComponentInChildren<Text> ().text = (num1 - num2).ToString ();
//		} else if (type == "mult") {
//			foodItems [rand].GetComponentInChildren<Text> ().text = (num1 * num2).ToString ();
//		} else if (type == "div") {
//			foodItems [rand].GetComponentInChildren<Text> ().text = (num1 / num2).ToString ();
//		}
//	}
//
//	void MathsAddition(){
//		num1 = Random.Range (1, 10);
//		num2 = Random.Range (1, 10);
//		ArithmeticAnswer =  num1 + num2;
//		Question.GetComponent<Text> ().text = num1.ToString() + " + " + num2.ToString();
//	}
//
//	void MathsSubtraction(){
//		num1 = Random.Range (1, 20);
//		num2 = Random.Range (1, 10);
//		ArithmeticAnswer =  num1 - num2;
//		Question.GetComponent<Text> ().text = num1.ToString() + " - " + num2.ToString();
//	}
//
//	void MathsMultiplication(){
//		num1 = Random.Range (1, 10);
//		num2 = Random.Range (1, 10);
//		ArithmeticAnswer =  num1 * num2;
//		Question.GetComponent<Text> ().text = num1.ToString() + " x " + num2.ToString();
//	}	
//
//	void MathsDivision(){
//		num1 = 2*(Random.Range (1, 50));
//		num2 = 2*(Random.Range (1, 3));
//		ArithmeticAnswer =  num1 / num2;
//		Question.GetComponent<Text> ().text = num1.ToString() + " / " + num2.ToString();
//	}	
//
//	string RandomQuestion(){
//		int rand = Random.Range (1, 5);
//		string x;
//		if (rand == 1) {
//			x = "add";
//		} else if(rand == 2){
//			x = "div";
//		} else if(rand == 3){
//			x = "mult";
//		} else{
//			x = "sub";
//		} 
//		return x;
//	}

//	List<float> EquationSolver(int a, int b, int c){
//
//		List<float> solutions = new List<float>();
//
//		float x1 = ( -b+Mathf.Sqrt(Mathf.Pow(b, 2) - (4*a*c))) / 2*a;
//		float x2 = (-b - Mathf.Sqrt (Mathf.Pow (b, 2) - (4 * a * c))) / 2 * a;
//
//		solutions.Add (x1);
//		solutions.Add (x2);
//
//		return solutions;
//	}
//
//	List<int> RandomCoefficents(){
//		int a;
//		int b;
//		int c;
//
//		List<int> coefficents = new List<int> ();
//
//		a = Random.Range (1, 2);
//		b = Random.Range (-25, 26);
//		c = Random.Range (-50, 51);
//
//		while((Mathf.Pow(b, 2) - (4*a*c)) < 0){
//			a = Random.Range (1, 2);
//			b = Random.Range (-25, 26);
//			c = Random.Range (-50, 51);
//		}
//
//		coefficents.Add (a);
//		coefficents.Add (b);
//		coefficents.Add (c);
//
////		print (a + " " + b + " " + c + " " + (Mathf.Pow(b, 2) - (4*a*c)));
//
//		return coefficents;
//	}
//
//	void ChooseQuadratic(){
//		QuadraticCoefficents = RandomCoefficents();
//		List<float> solutions = EquationSolver (QuadraticCoefficents[0], QuadraticCoefficents[1], QuadraticCoefficents[2]);
//		while (solutions [0] != Mathf.RoundToInt (solutions [0])) {
//			QuadraticCoefficents = RandomCoefficents();
//			solutions = EquationSolver (QuadraticCoefficents[0], QuadraticCoefficents[1], QuadraticCoefficents[2]);
//		}
//
//		QuadraticSolutions[0] = (solutions[0]);
//		QuadraticSolutions[1] = (solutions[1]);
//
//		AccessAdvancedUI (QuadraticCoefficents[0], QuadraticCoefficents[1], QuadraticCoefficents[2]);
//
////		print (QuadraticCoefficents[0] + " " + QuadraticCoefficents[1] + " " + QuadraticCoefficents[2] + " " + QuadraticSolutions[0] + " " + QuadraticSolutions[1]);
//	}


//	public void SpawnQuadraticCoins(int x){
//		for(int i = 0; i < x; i++){
//			int rand = Random.Range (0, grid.Count);
//			//Vector2 pos = new Vector2 (ReturnRounded (Random.Range (-2.5f, 2.5f), data.moveDistance), ReturnRounded (Random.Range (-2.5f, 2f), data.moveDistance));
//			Vector2 pos = grid[rand];
//			grid.Remove (grid[rand]);
//			GameObject temp2 = Instantiate (QuadraticCoin, pos, Quaternion.identity);
//			temp2.GetComponent<FoodObject> ().pos = pos;
//		}
//		if (!start) {
//			FillFoodList ();
//			SetQuadraticAnswers ();
//			start = true;
//		}
//	}

//	public void SetQuadraticAnswers(){
//		ChooseQuadratic ();
//		int rand = Random.Range (0, foodItems.Count);
//		for (int i = 0; i < foodItems.Count; i++) {
//			if (i != rand) {
//				foodItems [i].GetComponent<FoodObject> ().correct = false;
//				foodItems [i].GetComponentInChildren<Text> ().text = Random.Range (1, 10).ToString() + ", " + Random.Range (1, 10).ToString();
//			}
//		}
//		foodItems [rand].GetComponent<FoodObject> ().correct = true;
//		foodItems [rand].GetComponentInChildren<Text> ().text = QuadraticSolutions [0].ToString() + ", " + QuadraticSolutions [1].ToString();
//
//	}
//
//	void AccessAdvancedUI(int a, int b, int c){
//		string text = " ";
//		if (b > 0 && c > 0) {
//			text = a + "x<sup>2</sup> + " + b + "x +" + c;
//		} else if (b > 0 && c < 0) {
//			text = a + "x<sup>2</sup> + " + b + "x " + c;
//		} else if (b < 0 && c > 0) {
//			text = a + "x<sup>2</sup> " + b + "x +" + c;
//		} else if (b < 0 && c < 0) {
//			text = a + "x<sup>2</sup> " + b + "x " + c;
//		} else if(b == 0 && c == 0){
//			text = a + "x<sup>2</sup>"; 
//		}  else if(b == 0 && c < 0){
//			text = a + "x<sup>2</sup> " + c; 
//		} else if(b == 0 && c > 0){
//			text = a + "x<sup>2</sup> +" + c; 
//		} else if(b > 0 && c == 0){
//			text = a + "x<sup>2</sup> +" + b + "x"; 
//		} else if(b < 0 && c == 0){
//			text = a + "x<sup>2</sup> " + b + "x"; 
//		}
//		AdvancedText.GetComponent<TextMeshPro> ().text = text;
//	}

//	public void SpawnCustomCoins(int x){
//		for(int i = 0; i < x; i++){
//			int rand = Random.Range (0, grid.Count);
////			Vector2 pos = new Vector2 (ReturnRounded (Random.Range (-2.5f, 2.5f), data.moveDistance), ReturnRounded (Random.Range (-2.5f, 2f), data.moveDistance));
//			Vector2 pos = grid[rand];
//			grid.Remove (grid[rand]);
//			GameObject temp2 = Instantiate (CustomCoin, pos, Quaternion.identity);
//			temp2.GetComponent<FoodObject> ().pos = pos;
//		}
//		if (!start) {
//			FillFoodList ();
//			SetCustomAnswers ();
//			start = true;
//		}
//	}
//
//	public void SetCustomAnswers(){
//		int randQuestionNum = Random.Range (0, GetComponent<CreateCustomQuestions>().CustomQuestions.Count);
//		int rand = Random.Range (0, 4);
//		int rand2 = Random.Range (0, 4);
//		foodItems [rand].GetComponentInChildren<Text>().text = System.Convert.ToChar((rand2 % 4) + 65).ToString();
//		foodItems [(rand + 1) % 4].GetComponentInChildren<Text> ().text = System.Convert.ToChar (((rand2 + 1) % 4) + 65).ToString();;
//		foodItems [(rand + 2) % 4].GetComponentInChildren<Text>().text = System.Convert.ToChar(((rand2 + 2) % 4) + 65).ToString();;
//		foodItems [(rand + 3) % 4].GetComponentInChildren<Text>().text = System.Convert.ToChar(((rand2 + 3) % 4) + 65).ToString();;
////
//		foodItems [rand].GetComponent<FoodObject> ().correct = true;
//		foodItems [(rand + 1) % 4].GetComponent<FoodObject> ().correct = false;
//		foodItems [(rand + 2) % 4].GetComponent<FoodObject> ().correct = false;
//		foodItems [(rand + 3) % 4].GetComponent<FoodObject> ().correct = false;
//
////		if (rand2 == 0) {
////			aAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].correctAns;
////			bAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [0];
////			cAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [1];
////			dAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [2];
////		} else if (rand2 == 1) {
////			bAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].correctAns;
////			aAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [0];
////			cAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [1];
////			dAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [2];
////		} else if (rand2 == 2) {
////			cAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].correctAns;
////			bAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [0];
////			aAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [1];
////			dAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [2];
////		} else if (rand2 == 3) {
////			dAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].correctAns;
////			bAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [0];
////			cAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [1];
////			aAnswer.GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [2];
////		}
//
//		AlphabetChoices[rand2].GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].correctAns;
//		AlphabetChoices [(rand2 + 1) % 4].GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [0];
//		AlphabetChoices[(rand2 + 2) % 4].GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [1];
//		AlphabetChoices[(rand2 + 3) % 4].GetComponentInChildren<Text> ().text = GetComponent<CreateCustomQuestions> ().CustomQuestions [randQuestionNum].incorrectAns [2];
//
//
//
//		AdvancedText.GetComponent<TextMeshPro> ().text = GetComponent<CreateCustomQuestions>().CustomQuestions[randQuestionNum].question;
//	}


	void FoodGrid(){
		grid = new List<Vector2> ();
		for (float i = -2.7f; i < 2.7f; i += data.moveDistance) {
			for (float j = -2.7f; j < 2.25f; j += data.moveDistance) {
				grid.Add (new Vector2(ReturnRounded2(i, data.moveDistance), ReturnRounded2(j, data.moveDistance)));
			}
		}
	}
}
