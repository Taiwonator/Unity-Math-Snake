using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateGrid : MonoBehaviour {
	public SnakeScriptableObject data;
	public Vector2 gridDimensions;
	public float distApart;

	// Use this for initialization
	void Start () {
		for (float i = 0; i <= gridDimensions.x; i += distApart) {
			for (float o = 0; o <= gridDimensions.y; o += distApart) {
				Instantiate (data.stone, new Vector2(i - gridDimensions.x / 2, o - gridDimensions.y / 2), Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
