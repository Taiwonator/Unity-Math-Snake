using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InstantiateLine : MonoBehaviour {
	public SnakeScriptableObject data;
	float x = -3;
	float y;

	public GameObject blues;
	public GameObject reds;
	public GameObject yellows;

	public List<GameObject> bluesList;
	public List<GameObject> redsList;
	public List<GameObject> yellowsList;

	public int bluesDone;
	public int redsDone;
	public int yellowsDone;

	public int bluesNum;
	public int redsNum;
	public int yellowsNum;

	public bool run = false;

	//public float num;
	//public int dp;

	// Use this for initialization
	void Start () {

		bluesList = new List<GameObject> ();
		redsList = new List<GameObject> ();
//		yellowsList = new List<GameObject> ();

//		CreateLines ();

	}
	
	// Update is called once per frame
	void Update () {
		if (run) {
			CheckIfDone ();
		}
	}

	float RoundTo(float num, int dp)
	{
		float num2 = num * Mathf.Pow(10, dp);
		num = Mathf.Round (num2) / Mathf.Pow(10, dp);
		return num;
	}

	public bool blueDone = false;
	public bool redDone = false;
//	public bool yellowDone = false;

	void CheckIfDone()
	{
		if (bluesDone == bluesNum) {
			blueDone = true;
		} 
		if (redsDone == redsNum) {
			redDone = true;
		} 
//		if (yellowsDone == yellowsNum) {
//			yellowDone = true;
//		} 
	}

	public void CreateLines()
	{
		for(x = -Mathf.PI; x <= Mathf.PI; x+=0.1f)
		{
			y = Mathf.Sin (x);				
			GameObject temp = Instantiate (data.blue, new Vector2(x, y - 2), Quaternion.identity);
			temp.transform.parent = blues.transform;
			bluesList.Add (temp);
			bluesNum += 1;
		}


		for(x = -Mathf.PI; x <= Mathf.PI; x+=0.1f)
		{
			y = Mathf.Cos (x);				
			GameObject temp = Instantiate (data.red, new Vector2(x, y - 2), Quaternion.identity);
			temp.transform.parent = reds.transform;
			redsList.Add (temp);
			redsNum += 1;
		}


//		for(x = -Mathf.PI; x <= Mathf.PI; x+=0.1f)
//		{
//			y = Mathf.Tan (x);	
//			if (y < 2 && y > -2) {
//				GameObject temp = Instantiate (data.yellow, new Vector2 (x, y - 2), Quaternion.identity);
//				temp.transform.parent = yellows.transform;
//				yellowsList.Add (temp);
//				yellowsNum += 1;
//			}
//		}
	}

	public void TurnWhite()
	{
		for (int i = 0; i < bluesList.Count; i++) {
			bluesList [i].GetComponent<SpriteRenderer> ().color = Color.white;
		}
		for (int i = 0; i < redsList.Count; i++) {
			redsList [i].GetComponent<SpriteRenderer> ().color = Color.white;
		}
//		for (int i = 0; i < yellowsList.Count; i++) {
//			yellowsList [i].GetComponent<SpriteRenderer> ().color = Color.white;
//		}
	}
}
