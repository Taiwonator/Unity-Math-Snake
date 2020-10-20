using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccupiedSpace : MonoBehaviour {
	[SerializeField]
	public List<Vector2> occupied;
	public GameObject snakeOne;
	[SerializeField]
	private List<Vector2> prevPos;

	// Use this for initialization
	void Start () {
		occupied = new List<Vector2> ();
	}
	
	// Update is called once per frame
	void Update () {
//		prevPos = snakeOne.GetComponent<Snake2> ().previousPos;
//		if (occupied.Count - 1 < prevPos.Count) {
//			occupied.Add (new Vector2(0, 0));
//		}
//		for (int i = 0; i < prevPos.Count; i++) {
//			occupied [i] = prevPos [i];
//		}
//		occupied[prevPos.Count]
	}
}
