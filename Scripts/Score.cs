using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {
	public int score = 0;
	public GameObject highScore;
	float t = 0;
	bool run = false;

	float a, b;
	public GameObject snake;

	// Use this for initialization
	void Start () {
		snake = GameObject.FindGameObjectsWithTag ("snake") [0];
		highScore.GetComponent<Text> ().text = DataManager.control.levels.levels[SceneManager.GetActiveScene().buildIndex].high_Score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
//		GetComponent<Text> ().text = score.ToString ();
//		if(run){
//			t += Time.deltaTime*2;
//			if (t >= 1) {
//				t = 1;
//				run = false;
//			}
//			float lerpedPosition = Mathf.Lerp (a, b, t);
//			GetComponent<Text> ().text = Mathf.Round(lerpedPosition).ToString ();
//		}
		if (GameObject.Find ("TargetScore") != null) {
			GameObject.Find ("TargetScore").GetComponent<Text> ().text = DataManager.control.levels.levels [SceneManager.GetActiveScene().buildIndex].score_target.ToString();
		}
	}

	public void IncrementScore(int data, int x){
		t = 0;
		score += x; 
		run = true;
		if (data == 1) {
//			DataManager.control.levels [SceneManager.GetActiveScene ().buildIndex].total_Points += x;
//			DataManager.control.level.total_Points += x;
			if (DataManager.control.levels.levels[SceneManager.GetActiveScene ().buildIndex] != null) {
				DataManager.control.levels.levels [SceneManager.GetActiveScene ().buildIndex].total_Points += x;
				DataManager.control.player.total_points += x;
				if (snake.GetComponentInChildren<Col>().score >= DataManager.control.levels.levels [SceneManager.GetActiveScene ().buildIndex].score_target) {
					snake.GetComponentInChildren<Col> ().score = 0;
					DataManager.control.levels.levels [SceneManager.GetActiveScene ().buildIndex].score_target += 10;
					DataManager.control.Save2 ();
					DataManager.control.LoadFunction ();
				}
			}
		}
//		print (snake.GetComponentInChildren<Col>().score);
		GetComponent<Text> ().text = Mathf.Round(score).ToString ();
		if (GameObject.Find ("CumScore") != null) {
			GameObject.Find ("CumScore").GetComponent<Text> ().text = snake.GetComponentInChildren<Col> ().score.ToString ();
		}
	}

	void SmoothTransition(float a, float b){
		t += 0.001f;
		if (t >= 1) {
			t = 1;
		}
		float lerpedPosition = Mathf.Lerp (a, b, t);
		GetComponent<Text> ().text = Mathf.Round(lerpedPosition).ToString ();
	}
}
