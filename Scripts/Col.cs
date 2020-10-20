using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Col : MonoBehaviour {
	public string tag;
	public string snakeBodyTag;
	public GameObject controller;
	public Snake2 this_Snake;

	public int score = 0;

	public SnakeScriptableObject data;

//	List<Vector2> squareGrid;
	List<GridPosition> squareGrid2;

	// Use this for initialization
	void Start () {
		this_Snake = transform.parent.gameObject.GetComponent<Snake2>();
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetKeyDown (KeyCode.P)) {
//			this_Snake.AddSnakeTail ();
//			if (this_Snake.speed >= 0.2f) {
//				this_Snake.speed -= 0.01f;
//				////////debug.Log (this_Snake.speed);
//			}			
//			snake.GetComponent<SnakeController> ().DestroyCoins ();
//			GameObject.Find ("Text").GetComponent<ChooseQuestion> ().RandomQuestion ();
//			GameObject.Find("Floor").GetComponent<Timer>().timer = 10;
//			GameObject.Find ("Score").GetComponent<Score> ().score += 1;
//			GameObject.Find ("GoodSound").GetComponent<AudioSource> ().Play ();
//		}
		if(Input.GetKeyDown(KeyCode.Q)){
			
		}
	}

	int ArithmeticQuizCount = 0;
	GameObject temp;

	void OnTriggerEnter2D(Collider2D other)
	{

		if (this_Snake.previousPos.Count >= 4 && !controller.GetComponent<SnakeController> ().turnedOn) {
			controller.GetComponent<SnakeController> ().TurnOnOverlap ();
		}

		if (other.gameObject.tag == "basicCoin") {
//			other.gameObject.transform.position = new Vector2 (ReturnRounded (Random.Range (-1.5f, 2), data.moveDistance), ReturnRounded (Random.Range (-3, 0.5f), data.moveDistance));
			controller.GetComponent<FoodController> ().RelocateItem (other.gameObject);
			this_Snake.AddSnakeTail ();
			controller.GetComponent<Timer> ().ResetTimer();
			score += 10;
			GameObject.Find ("Score").GetComponent<Score> ().IncrementScore (1, 10);
			GameObject.Find ("GoodSound").GetComponent<AudioSource> ().Play ();
		}

		if (other.gameObject.tag == "ArithmeticCoin") {

			if (other.gameObject.GetComponent<FoodObject> ().correct == true) {
				controller.GetComponent<FoodController> ().RelocateItem (other.gameObject);
				controller.GetComponent<ArithmeticQuiz> ().SetArithmeticAnswers (RandomQuestion());
				this_Snake.AddSnakeTail ();
				if (this_Snake.speed >= 0.2f) {
					this_Snake.speed -= 0.01f;
					//////debug.Log (snake.GetComponent<SnakeController> ().snake.speed);
				}	
				controller.GetComponent<Timer> ().ResetTimer();
				score += 10;
				GameObject.Find ("Score").GetComponent<Score> ().IncrementScore (1, 10);
				GameObject.Find ("GoodSound").GetComponent<AudioSource> ().Play ();
			} else {
				//StartCoroutine (Death ());
//				this_Snake.Death ();
				StopSnake ();
			}

		}

		if (other.gameObject.tag == "QuadraticCoin") {
			if (other.gameObject.GetComponent<FoodObject> ().correct == true) {
				controller.GetComponent<FoodController> ().RelocateItem (other.gameObject);
				controller.GetComponent<QuadraticQuiz> ().SetQuadraticAnswers ();
				this_Snake.AddSnakeTail ();
				if (this_Snake.speed >= 0.2f) {
					this_Snake.speed -= 0.01f;
					//////debug.Log (this_Snake.speed);
				}	
				controller.GetComponent<Timer> ().ResetTimer();
				score += 10;
				GameObject.Find ("Score").GetComponent<Score> ().IncrementScore(1, 10);
				GameObject.Find ("GoodSound").GetComponent<AudioSource> ().Play ();
			} else{
				//StartCoroutine (Death ());
				//this_Snake.Death ();
				StopSnake ();
			}
		}

		if (other.gameObject.tag == "CustomCoin") {
			if (other.gameObject.GetComponent<FoodObject> ().correct == true) {
				controller.GetComponent<FoodController> ().RelocateItem (other.gameObject);
				controller.GetComponent<CustomQuiz> ().SetCustomAnswers ();
				this_Snake.AddSnakeTail ();
				if (this_Snake.speed >= 0.2f) {
					this_Snake.speed -= 0.01f;
					//////debug.Log (this_Snake.speed);
				}	
				controller.GetComponent<Timer> ().ResetTimer();
				score += 10;
				GameObject.Find ("Score").GetComponent<Score> ().IncrementScore(1, 10);
				GameObject.Find ("GoodSound").GetComponent<AudioSource> ().Play ();
			} else{
				//StartCoroutine (Death ());
				//this_Snake.Death ();
				StopSnake ();
			}
				
		}

		if (other.gameObject.tag == "TrigCoin") {
			if (other.gameObject.GetComponent<FoodObject> ().correct == true) {
				controller.GetComponent<FoodController> ().RelocateItem (other.gameObject);
				controller.GetComponent<TrigQuiz> ().SetTrigQuestion ();
				this_Snake.AddSnakeTail ();
				if (this_Snake.speed >= 0.2f) {
					this_Snake.speed -= 0.01f;
					//////debug.Log (this_Snake.speed);
				}	
				controller.GetComponent<Timer> ().ResetTimer ();
				score += 10;
				GameObject.Find ("Score").GetComponent<Score> ().IncrementScore(1, 10);
				GameObject.Find ("GoodSound").GetComponent<AudioSource> ().Play ();
			} else {
				//StartCoroutine (Death ());
				//this_Snake.Death ();
				StopSnake ();
			}
		}

		if (other.gameObject.tag == "StartArithmetic") {
			controller.GetComponent<ArithmeticQuiz> ().PlayArithmeticGame (0, controller.GetComponent<FoodController> ().foodNumber);
			GameObject.Find ("GoodSound").GetComponent<AudioSource> ().Play ();
			controller.GetComponent<Timer> ().ResetTimer();
			GameObject.Find ("Score").GetComponent<Score> ().score = 0;
			Destroy (other.gameObject);
		}

		if (other.gameObject.tag == "StartQuadratic") {
			controller.GetComponent<QuadraticQuiz> ().SpawnQuadraticCoins (controller.GetComponent<FoodController> ().foodNumber);
			GameObject.Find ("GoodSound").GetComponent<AudioSource> ().Play ();
			controller.GetComponent<Timer> ().ResetTimer();
			Destroy (other.gameObject);
		}

		if (other.gameObject.tag == "StartCustom") {
			controller.GetComponent<CustomQuiz> ().SpawnCustomCoins (4);
			GameObject.Find ("GoodSound").GetComponent<AudioSource> ().Play ();
			controller.GetComponent<Timer> ().ResetTimer();
			Destroy (other.gameObject);
		} 

		if (other.gameObject.tag == "StartGraph") {
			controller.GetComponent<GraphQuiz> ().Reset ();
			controller.GetComponent<GraphQuiz> ().CreateLines ();
			controller.GetComponent<GraphQuiz> ().run = true;
			controller.GetComponent<Timer> ().ResetTimer();
        	GameObject.Find ("GoodSound").GetComponent<AudioSource> ().Play ();
			Destroy (other.gameObject);
		}

		if (other.gameObject.tag == "StartTrig") {
			controller.GetComponent<TrigQuiz> ().SpawnTrigCoins (3);
			controller.GetComponent<Timer> ().ResetTimer();
			GameObject.Find ("GoodSound").GetComponent<AudioSource> ().Play ();
			Destroy (other.gameObject);
		}

		if (other.gameObject.tag == "StartArithmeticQuiz") {
			GameObject.Find ("GoodSound").GetComponent<AudioSource> ().Play ();
			controller.GetComponent<Timer> ().ResetTimer();
			controller.GetComponent<ArithmeticQuiz> ().PlayArithmeticGame (1, controller.GetComponent<FoodController> ().foodNumber);
			controller.GetComponent<FoodController> ().TurnOnQuiz ();
			GameObject.Find ("Score").GetComponent<Score> ().score = 0;
			temp = other.gameObject;
			temp.SetActive (false);
		}
			
		if (other.gameObject.tag == "ArithmeticQuizCoin") {
			if (other.gameObject.GetComponent<FoodObject> ().correct == true) {
				if (ArithmeticQuizCount == 10) {
					GameObject.Find ("GoodSound").GetComponent<AudioSource> ().Play ();
					controller.GetComponent<FoodController> ().TurnOffQuiz ();
					temp.SetActive(true);
					ArithmeticQuizCount = 0;
				}
				controller.GetComponent<FoodController> ().RelocateItem (other.gameObject);
				controller.GetComponent<ArithmeticQuiz> ().SetArithmeticAnswers (RandomQuestion());
				ArithmeticQuizCount++;
				this_Snake.AddSnakeTail ();
				if (this_Snake.speed >= 0.2f) {
					this_Snake.speed -= 0.01f;
					//////debug.Log (snake.GetComponent<SnakeController> ().snake.speed);
				}	
				controller.GetComponent<Timer> ().ResetTimer();
				GameObject.Find ("Score").GetComponent<Score> ().IncrementScore(1, 10);
				GameObject.Find ("GoodSound").GetComponent<AudioSource> ().Play ();
			} else {
				StopSnake ();
			}
			print (ArithmeticQuizCount);

		}


//		if (other.gameObject.tag == "right") {
//			Destroy (other.gameObject);
//			this_Snake.AddSnakeTail ();
//			if (this_Snake.speed >= 0.2f) {
//				this_Snake.speed -= 0.01f;
//			}
////			snake.GetComponent<SnakeController> ().DestroyCoins ();
//			GameObject.Find ("Text").GetComponent<ChooseQuestion> ().RandomQuestion ();
//			GameObject.Find("Floor").GetComponent<Timer>().timer = 10;
//			GameObject.Find ("Score").GetComponent<Score> ().score += 1;
//			GameObject.Find ("GoodSound").GetComponent<AudioSource> ().Play ();
//		} 
		if (other.gameObject.tag == snakeBodyTag && this_Snake.firstDone && other.gameObject.transform.parent.gameObject == transform.parent.gameObject) {
			//this_Snake.Death ();
			StopSnake ();
			print ("Game Over you hit yourself");
			////////debug.Log (other.gameObject);
		}

//		if (other.gameObject.tag == "wrong") {
//			StartCoroutine(Death ());
//			this_Snake.Death ();
//		}
//
//		if (other.gameObject.tag == "stone") {
//			StartCoroutine(Death ());
//			this_Snake.Death ();
//		}

		if (GameObject.Find ("Score") != null) {
			if (GameObject.Find ("Score").GetComponent<Score> ().score > DataManager.control.levels.levels [SceneManager.GetActiveScene ().buildIndex].high_Score) {
				DataManager.control.levels.levels [SceneManager.GetActiveScene ().buildIndex].high_Score = GameObject.Find ("Score").GetComponent<Score> ().score;
				DataManager.control.Save2 ();
				DataManager.control.LoadFunction ();
			} else if (GameObject.Find ("Score").GetComponent<Score> ().score > DataManager.control.player.high_Score) {
				DataManager.control.player.high_Score = GameObject.Find ("Score").GetComponent<Score> ().score;
				DataManager.control.Save2 ();
				DataManager.control.LoadFunction ();
			}
		}

		if (other.GetComponent<SpriteRenderer> ().color == Color.white) {
			if (other.gameObject.tag == "blue") {
				other.GetComponent<SpriteRenderer> ().color = Color.blue;
				controller.GetComponent<GraphQuiz> ().bluesDone += 1;
			}
			if (other.gameObject.tag == "red") {
				other.GetComponent<SpriteRenderer> ().color = Color.red;
				controller.GetComponent<GraphQuiz> ().redsDone += 1;
			}
			if (other.gameObject.tag == "yellow") {
				other.GetComponent<SpriteRenderer> ().color = Color.yellow;
				controller.GetComponent<GraphQuiz> ().yellowsDone += 1;
			}
		}

		if (this_Snake.previousPos.Count <= 3) {
			if (other.gameObject.name == "LevelArithmetic") {
				SceneManager.LoadScene (1);
				DataManager.control.Save2 ();

			} else if (other.gameObject.name == "LevelQuad") {
				SceneManager.LoadScene (2);
				DataManager.control.Save2 ();

			} else if (other.gameObject.name == "LevelGraph") {
				SceneManager.LoadScene (3);
				DataManager.control.Save2 ();

			} else if (other.gameObject.name == "LevelCustom") {
				SceneManager.LoadScene (5);
				DataManager.control.Save2 ();

			} else if (other.gameObject.name == "LevelTrig") {
				SceneManager.LoadScene (4);
				DataManager.control.Save2 ();

			} else if (other.gameObject.name == "LevelClassic") {
				SceneManager.LoadScene (0);
				DataManager.control.Save2 ();

			}
		}
			
	}

	void StopSnake(){
		this_Snake.Death ();
		controller.GetComponent<SnakeController> ().deadSnakes++;
		if (controller.GetComponent<SnakeController> ().deadSnakes == controller.GetComponent<SnakeController> ().snakes.Count) {
			StartCoroutine (Death());
		} 
//		DataManager.control.Save ();
		DataManager.control.Save2 ();
	}


	IEnumerator Death()
	{
		GameObject.Find ("BadSound").GetComponent<AudioSource> ().Play ();
		if (controller.GetComponent<Timer> ().floor != null) {
			controller.GetComponent<Timer> ().death = true;
		}
		yield return new WaitForSeconds (0.5f);
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

//	void OnTriggerExit2D(Collider2D other){
//		print (other.gameObject.name);
//		if (other.gameObject.name == "Floor") {
////			StartCoroutine (Death ());
////			this_Snake.Death ();
//			if (GetComponentInParent<Snake2> ().timer > 0) {
//				GameObject.Find ("Floor").GetComponent<Timer> ().timer = 10;
//				print (new Vector2 (GetComponentInParent<Snake2> ().previousPos [0].x - 4, GetComponentInParent<Snake2> ().previousPos [0].y));
//				GetComponentInParent<Snake2> ().previousPos [0] = new Vector2 (GetComponentInParent<Snake2> ().previousPos [0].x - 10f, GetComponentInParent<Snake2> ().previousPos [0].y);
//			}
//		}
//	}

	float ReturnRounded(float input, float roundedMultiplier){
		float val = (Mathf.Floor((input / roundedMultiplier)) * roundedMultiplier);
		return val;
	}


//	void ReloacateItem(Collider2D other){
////		int index = Random.Range (0, squareGrid2.Count - 1);
////		squareGrid2 = snake.GetComponent<SquareGrid> ().squareGrid2;
////		other.gameObject.transform.position = new Vector2 (ReturnRounded(squareGrid2 [index].position.x, data.moveDistance), ReturnRounded(squareGrid2 [index].position.y, data.moveDistance));
////		squareGrid2 [index].ToggleOccupied ();
//		Vector2 pos = new Vector2 (ReturnRounded (Random.Range (-1.5f, 2), data.moveDistance), ReturnRounded (Random.Range (-3, 0.5f), data.moveDistance));
//		other.gameObject.transform.position = pos;
//		print (pos);
//	}

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
