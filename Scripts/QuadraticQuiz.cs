using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuadraticQuiz : MonoBehaviour {
	FoodController foodController;
	public SnakeScriptableObject data;
	public GameObject QuadraticCoin;
	public List<int> QuadraticCoefficents;
	public List<float> QuadraticSolutions;

	bool start = false;

	int count;

	// Use this for initialization
	void Start () {
		foodController = GetComponent<FoodController> ();
		QuadraticSolutions = new List<float> ();
		QuadraticSolutions.Add (0);
		QuadraticSolutions.Add (0);
		QuadraticCoin = data.QuadraticFood;

//		ChooseQuadratic ();
//		print (count);
//		ChooseQuadratic ();
//		print (count);
//		ChooseQuadratic ();
//		print (count);
//		ChooseQuadratic ();
//		print (count);
//		ChooseQuadratic ();
//		print (count);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnQuadraticCoins(int x){
		for(int i = 0; i < x; i++){
			int rand = Random.Range (0, foodController.grid.Count);
			//Vector2 pos = new Vector2 (ReturnRounded (Random.Range (-2.5f, 2.5f), data.moveDistance), ReturnRounded (Random.Range (-2.5f, 2f), data.moveDistance));
			Vector2 pos = foodController.grid[rand];
			foodController.grid.Remove (foodController.grid[rand]);
			GameObject temp2 = Instantiate (QuadraticCoin, pos, Quaternion.identity);
			temp2.GetComponent<FoodObject> ().pos = pos;
		}
		if (!start) {
			foodController.FillFoodList ();
			SetQuadraticAnswers ();
			start = true;
		}
	}

	List<float> EquationSolver(int a, int b, int c){

		List<float> solutions = new List<float>();

		float x1 = ( -b+Mathf.Sqrt(Mathf.Pow(b, 2) - (4*a*c))) / 2*a;
		float x2 = (-b - Mathf.Sqrt (Mathf.Pow (b, 2) - (4 * a * c))) / 2 * a;

		solutions.Add (x1);
		solutions.Add (x2);

		return solutions;
	}

	List<int> RandomCoefficents(){
		int a;
		int b;
		int c;
		List<int> coefficents = new List<int> ();

		a = Random.Range (1, 2);
		b = Random.Range (-25, 26);
		c = Random.Range (-50, 51);

		while((Mathf.Pow(b, 2) - (4*a*c)) < 0){
			count++;
			a = Random.Range (1, 2);
			b = Random.Range (-25, 26);
			c = Random.Range (-50, 51);
		}

		coefficents.Add (a);
		coefficents.Add (b);
		coefficents.Add (c);

		//		print (a + " " + b + " " + c + " " + (Mathf.Pow(b, 2) - (4*a*c)));

		return coefficents;
	}

	void ChooseQuadratic(){
		QuadraticCoefficents = RandomCoefficents();
		List<float> solutions = EquationSolver (QuadraticCoefficents[0], QuadraticCoefficents[1], QuadraticCoefficents[2]);
		count = 0;
		while (solutions [0] != Mathf.RoundToInt (solutions [0])) {
			count++;
			QuadraticCoefficents = RandomCoefficents();
			solutions = EquationSolver (QuadraticCoefficents[0], QuadraticCoefficents[1], QuadraticCoefficents[2]);
		}
		QuadraticSolutions[0] = (solutions[0]);
		QuadraticSolutions[1] = (solutions[1]);

		AccessAdvancedUI (QuadraticCoefficents[0], QuadraticCoefficents[1], QuadraticCoefficents[2]);

		//		print (QuadraticCoefficents[0] + " " + QuadraticCoefficents[1] + " " + QuadraticCoefficents[2] + " " + QuadraticSolutions[0] + " " + QuadraticSolutions[1]);
	}

	public void SetQuadraticAnswers(){
		ChooseQuadratic ();
		int rand = Random.Range (0, foodController.foodItems.Count);
		for (int i = 0; i < foodController.foodItems.Count; i++) {
			if (i != rand) {
				foodController.foodItems [i].GetComponent<FoodObject> ().correct = false;
				foodController.foodItems [i].GetComponentInChildren<Text> ().text = Random.Range (-10, 20).ToString() + ", " + Random.Range (-10, 20).ToString();
			}
		}
		foodController.foodItems [rand].GetComponent<FoodObject> ().correct = true;
		foodController.foodItems [rand].GetComponentInChildren<Text> ().text = QuadraticSolutions [0].ToString() + ", " + QuadraticSolutions [1].ToString();

	}

	void AccessAdvancedUI(int a, int b, int c){
		string text = " ";
		if (b > 0 && c > 0) {
			text = a + "x<sup>2</sup> + " + b + "x +" + c;
		} else if (b > 0 && c < 0) {
			text = a + "x<sup>2</sup> + " + b + "x " + c;
		} else if (b < 0 && c > 0) {
			text = a + "x<sup>2</sup> " + b + "x +" + c;
		} else if (b < 0 && c < 0) {
			text = a + "x<sup>2</sup> " + b + "x " + c;
		} else if(b == 0 && c == 0){
			text = a + "x<sup>2</sup>"; 
		}  else if(b == 0 && c < 0){
			text = a + "x<sup>2</sup> " + c; 
		} else if(b == 0 && c > 0){
			text = a + "x<sup>2</sup> +" + c; 
		} else if(b > 0 && c == 0){
			text = a + "x<sup>2</sup> +" + b + "x"; 
		} else if(b < 0 && c == 0){
			text = a + "x<sup>2</sup> " + b + "x"; 
		}
		foodController.AdvancedText.GetComponent<TextMeshPro> ().text = text;
	}

}
