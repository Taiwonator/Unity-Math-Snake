using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

	private Color lerpedColor = Color.white;
	private float timer;
	public float startTimer = 10;
	public bool death = false;
	public GameObject floor;
	public Color startColor;
	public Color color;

	public GameObject Text;

	public bool paused = false;
	public bool startPause = true;

	// Use this for initialization
	void Start () {
		startColor = floor.GetComponent<SpriteRenderer> ().color;
		color = startColor;
		timer = startTimer;
	}
	
	// Update is called once per frame
	void Update () {
		TimerFunction ();
		if (timer <= 5 && timer > 0) {
			lerpedColor = Color.Lerp (color, Color.red, Mathf.PingPong (Time.time, 1));
			floor.GetComponent<SpriteRenderer> ().color = lerpedColor;
		} else {
			floor.GetComponent<SpriteRenderer> ().color = color;
		}

		if (death) {
			lerpedColor = Color.Lerp (color, Color.black, Mathf.PingPong (Time.time*4, 1));
			floor.GetComponent<SpriteRenderer> ().color = lerpedColor;
		}
		if (timer >= 0) {
			Text.GetComponent<Text> ().text = Mathf.RoundToInt (timer).ToString ();
		} else {
			Text.GetComponent<Text> ().text = "0";
		}
	}

	void TimerFunction()
	{
		if (!paused && !startPause) {
			timer -= Time.deltaTime;
		}
//		if (Mathf.Round(timer *100) / 100 == 0) {
//			GameObject.Find ("BadSound").GetComponent<AudioSource> ().Play ();
//		}
		if (timer <= 0) {
			StartCoroutine(GetComponent<SnakeController> ().Death ());
		}
	}

	public void ResetTimer(){
		timer = startTimer;
	}
}
