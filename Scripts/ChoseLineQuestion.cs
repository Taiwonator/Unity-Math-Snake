using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChoseLineQuestion : MonoBehaviour {
	public GameObject Grid;
	public GameObject snake;
	string question;

	// Use this for initialization
	void Start () {
		question = "START";
//		gameObject.transform.GetComponent<Text> ().text = "START";
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Text> ().text = question;
		CheckIfRight ();
	}

	void CheckIfRight()
	{
		if (question == "sin(x)") {
			if (Grid.GetComponent<InstantiateLine> ().blueDone) {
				Reset ();
				snake.GetComponent<Snake2> ().AddSnakeTail ();
				GameObject.Find ("GoodSound").GetComponent<AudioSource> ().Play ();
//			} else if (Grid.GetComponent<InstantiateLine> ().yellowDone || Grid.GetComponent<InstantiateLine> ().blueDone) {
			} else if (Grid.GetComponent<InstantiateLine> ().redDone) {
				SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
			}
		}
		if (question == "cos(x)") {
			if (Grid.GetComponent<InstantiateLine> ().redDone) {
				Reset ();
				snake.GetComponent<Snake2> ().AddSnakeTail ();
				GameObject.Find ("GoodSound").GetComponent<AudioSource> ().Play ();
//			} else if (Grid.GetComponent<InstantiateLine> ().yellowDone || Grid.GetComponent<InstantiateLine> ().redDone) {
			} else if (Grid.GetComponent<InstantiateLine> ().blueDone) {
				SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
			}
		}
//		if (question == "tan(x)") {
//			if (Grid.GetComponent<InstantiateLine> ().yellowDone) {
//				Reset ();
//				snake.GetComponent<Snake2> ().AddSnakeTail ();
//				GameObject.Find ("GoodSound").GetComponent<AudioSource> ().Play ();
//			} else if (Grid.GetComponent<InstantiateLine> ().redDone || Grid.GetComponent<InstantiateLine> ().blueDone) {
//				SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
//			}
//		}
	}
		
	public void Reset()
	{
		Grid.GetComponent<InstantiateLine> ().TurnWhite ();
		Grid.GetComponent<InstantiateLine> ().redsDone = 0;
		Grid.GetComponent<InstantiateLine> ().bluesDone = 0;
		Grid.GetComponent<InstantiateLine> ().yellowsDone = 0;
		Grid.GetComponent<InstantiateLine> ().redDone = false;
		Grid.GetComponent<InstantiateLine> ().blueDone = false;
//		Grid.GetComponent<InstantiateLine> ().yellowDone = false;

		int rand = Random.Range (0, 2);
		if (rand == 0) {
			question = "sin(x)";
		} else if (rand == 1) {
			question = "cos(x)";
		} 
//		else if (rand == 2) {
//			question = "tan(x)";
//		}
	}
}
