using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGrid : MonoBehaviour {
	private float radius = 3f;
	private float x;
	private float y;
	float xOffset = .15f;
	float yOffset = .5f;
	private float multiple = 0.45f;
	float div;
	Vector2 coord;

	public List<float> xCoords;
	public List<float> yCoords;
	public GameObject coin;
	public SnakeScriptableObject data;

	public List<Vector2> coords;

	// Use this for initialization
	void Start () {
		
		x = -radius;
		y = -radius;
		multiple = data.moveDistance;
		div = Mathf.Floor((radius * 2) / multiple);

		for (int i = 0; i <= div; i++) {
			xCoords.Add (x + (multiple * i));
		}

		for (int i = 0; i <= div; i++) {
			yCoords.Add (y + (multiple * i));
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Vector2 RandomPosition(){
		int t = Random.Range (0, xCoords.Count - 1);
		int s = Random.Range (0, yCoords.Count - 1);
		if ((Mathf.Pow (xCoords [t], 2) + Mathf.Pow (yCoords [s], 2)) < Mathf.Pow(radius, 2)) {
//			coords.Add (new Vector2(xCoords[t], yCoords[s]));
//			coords.Add (new Vector2(ReturnRounded(xCoords[t], 0.01f), ReturnRounded(yCoords[s], 0.01f)));
			coord = new Vector2(xCoords[t] - xOffset, yCoords[s] - yOffset);
		}
		return coord;
	}

	float ReturnRounded(float input, float roundedMultiplier){
		float val = (Mathf.Floor((input / roundedMultiplier)) * roundedMultiplier);
		return val;
	}
}
