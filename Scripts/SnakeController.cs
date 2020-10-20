using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SnakeController : MonoBehaviour {
	public SnakeScriptableObject data;
	public GameObject snakeOne;
	public Snake2 snake;
	public GameObject Questions;
//	public List<Vector2> coinPositions;
//	public List<Vector2> prevCoinPositions;
	public GameObject NormalCoin;

	private GameObject coin;
	private GameObject rightCoin;
	bool first = true;

	public bool overlap;
	public bool top, bottom, left, right;

	public List<GameObject> snakes;
	public int deadSnakes = 0;

	public int currentSceneIndex;

	public bool paused = false;

	public GameObject LevelArithmetic;
	public GameObject LevelClassic;
	public GameObject LevelTrig;
	public GameObject LevelCustom;
	public GameObject LevelGraph;
	public GameObject LevelQuad;

	// Use this for initialization
	void Awake(){
 	}

	void Start () {
		InitialiseSnakeOne ();
//		print (DataManager.control.levels.levels[1].experience_needed);
		CheckActive(LevelArithmetic, 1);
		CheckActive(LevelClassic, 0);
		CheckActive(LevelCustom, 5);
		CheckActive(LevelQuad, 2);
		CheckActive(LevelGraph, 3);
		CheckActive(LevelTrig, 4);
//		coin = data.coin;
//		rightCoin = data.coin;
//		for (int i = 0; i < 3; i++) {
//			prevCoinPositions.Add (new Vector2(0, 0));
//		}
		int x = GameObject.FindGameObjectsWithTag("snake").Length;
		for(int i = 0; i < x; i++){
			snakes.Add (GameObject.FindGameObjectsWithTag("snake")[i]);
		}
	}

	void CheckActive(GameObject level, int index){
		if(level != null && DataManager.control.levels.levels[index] != null){
			Color color = level.GetComponent<SpriteRenderer> ().color;
			if (DataManager.control.player.experience < DataManager.control.levels.levels [index].experience_needed || DataManager.control.player.experience == 0) {
				level.GetComponent<CircleCollider2D> ().enabled = false;
				level.GetComponent<SpriteRenderer> ().color = color;
				level.GetComponent<SpriteRenderer> ().sprite = data.inactiveLevel;
			} else {
//				print (DataManager.control.levels.levels [index].experience_needed);
				level.GetComponent<CircleCollider2D> ().enabled = enabled;
				level.GetComponent<SpriteRenderer> ().color = new Color32 (255, 255, 255, 255);
				level.GetComponent<SpriteRenderer> ().sprite = data.activeLevel;
			}
		}
	}
		

	public bool turnedOn = false;
	
	// Update is called once per frame
	void Update () {
//		if (snake.previousPos.Count > 4 && !turnedOn) {
//			TurnOnOverlap ();
//		}

//		if (deadSnakes == snakes.Count) {
//			StartCoroutine (Death());
//		}

	}

	public void TurnOnOverlap(){
		top = true;
		bottom = true;
		left = true;
		right = true;
		turnedOn = true;
	}

	void InitialiseSnakeOne()
	{
//		snake = snakeOne.AddComponent<Snake2> ();
	}

	bool deathStart = false;

	public IEnumerator Death()
	{
		if (!deathStart) {
			deathStart = true;
			GameObject.Find ("BadSound").GetComponent<AudioSource> ().Play ();
			if (GameObject.Find ("Floor") != null) {
				GetComponent<Timer> ().death = true;
			}
			yield return new WaitForSeconds (0.5f);
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
	}

	public void TestMode(){
		int lowestNum = 0;
		int index = 0;
		for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
			if(DataManager.control.levels.levels[i].experience <= lowestNum) {
				index = i;
			}	
		}
		SceneManager.LoadScene (index);
	}


//	public void DestroyCoins()
//	{
//		for (int i = 0; i < GameObject.FindGameObjectsWithTag ("wrong").Length; i++) {
//			Destroy (GameObject.FindGameObjectsWithTag ("wrong") [i]);
//		}
//	}

//	public void SpawnCoins(int amount, int answer)
//	{
//		for (int i = 0; i < amount - 1; i++) {
//			coin.GetComponentInChildren<Text> ().text = Random.Range (1, 20).ToString ();
//			while(coin.GetComponentInChildren<Text> ().text == answer.ToString()) {
//				coin.GetComponentInChildren<Text> ().text = Random.Range (1, 20).ToString ();
//			}
//			GameObject temp = Instantiate (coin, new Vector2(Random.Range(-1.5f, 2), Random.Range(-3, 0.5f)), Quaternion.identity);
//			temp.tag = "wrong";
//		}
//		rightCoin.GetComponentInChildren<Text> ().text = Questions.GetComponent<ChooseQuestion> ().answer.ToString ();
//		GameObject temp2 = Instantiate (rightCoin, new Vector2(Random.Range(-1.5f, 2), Random.Range(-3, 0.5f)), Quaternion.identity);
//		temp2.tag = "right";
//		Questions.GetComponent<ChooseQuestion> ().done = false;
//
//	}
//
//	public void newSpawnCoins(int amount, int answer){
//
//			prevCoinPositions = new List<Vector2> (coinPositions);
//			coinPositions.Clear ();
//
//
//		Vector2 tempPos2;
//		tempPos2 = gameObject.GetComponent<CircleGrid> ().RandomPosition ();
//		//		for (int i = 0; i < amount; i++) {
//		//			while (DistanceApart(tempPos2, prevCoinPositions[i]) <= 1 || DistanceApart(tempPos2, coinPositions[i]) <= 1) {
//		//				tempPos2 = gameObject.GetComponent<CircleGrid> ().RandomPosition ();
//		//			}
//		//		}
//		rightCoin.GetComponentInChildren<Text> ().text = Questions.GetComponent<ChooseQuestion> ().answer.ToString ();
//		GameObject temp2 = Instantiate (rightCoin, tempPos2, Quaternion.identity);
//		coinPositions.Add (tempPos2);
//		//		GameObject temp2 = Instantiate (rightCoin, new Vector2(ReturnRounded(Random.Range(-1.5f, 2), data.moveDistance), ReturnRounded(Random.Range(-3, 0.5f), data.moveDistance)), Quaternion.identity);
//		temp2.tag = "right";
//		Questions.GetComponent<ChooseQuestion> ().done = false;
//
//		for (int i = 0; i < amount - 1; i++) {
//			coin.GetComponentInChildren<Text> ().text = Random.Range (1, 20).ToString ();
//
//			while(coin.GetComponentInChildren<Text> ().text == answer.ToString()) {
//				coin.GetComponentInChildren<Text> ().text = Random.Range (1, 20).ToString ();
//			}
//			Vector2 tempPos;
//			tempPos = gameObject.GetComponent<CircleGrid> ().RandomPosition ();
////			if (coinPositions.Count == 1) {
////				while (DistanceApart (tempPos, prevCoinPositions [0]) <= 1) {
////					tempPos = gameObject.GetComponent<CircleGrid> ().RandomPosition ();
////				}
////			}
//			while(tempPos == new Vector2(0, 0)){
//				tempPos = gameObject.GetComponent<CircleGrid> ().RandomPosition ();
//			}
//			coinPositions.Add (tempPos);
////			GameObject temp = Instantiate (rightCoin, new Vector2(ReturnRounded(Random.Range(-1.5f, 2), data.moveDistance), ReturnRounded(Random.Range(-3, 0.5f), data.moveDistance)), Quaternion.identity);
//			GameObject temp = Instantiate (rightCoin, tempPos, Quaternion.identity);
//			temp.tag = "wrong";
//		}
//
//		first = false;
//	}

	float ReturnRounded(float input, float roundedMultiplier){
		float val = (Mathf.Floor((input / roundedMultiplier)) * roundedMultiplier);
		return val;
	}

	float DistanceApart(Vector2 a, Vector2 b){
		float distance = Vector2.Distance (a, b);
		return distance;
	}

	bool start = false;

//	public void NormalCoins(int x){
//		for(int i = 0; i < x; i++){
//			Vector2 pos = new Vector2 (ReturnRounded (Random.Range (-1.5f, 2), data.moveDistance), ReturnRounded (Random.Range (-3, 0.5f), data.moveDistance));
//		    GameObject temp2 = Instantiate (NormalCoin, pos, Quaternion.identity);
//			temp2.GetComponent<FoodObject> ().pos = pos;
//		}
//		if (!start) {
//			this.gameObject.GetComponent<FoodController> ().FillFoodList ();
//			this.gameObject.GetComponent<FoodController> ().SetAnswers ("sub");
//			start = true;
//		}
//	}

}
