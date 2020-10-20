using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareGrid : MonoBehaviour {
	[SerializeField]
	private List<bool> OccupationsList;
//	public List<Vector2> squareGrid;
	public List<GridPosition> squareGrid2;
	public SnakeScriptableObject data;
	private float moveDistance;

	public float lowX = -2;
	public float highX = 2;
	public float lowY = -2 ;
	public float highY = 2;

	private float iterationNumX;
	private float iterationNumY;

	// Use this for initialization
	void Start () {
		
//		squareGrid = new List<Vector2>();
		squareGrid2 = new List<GridPosition>();
		OccupationsList = new List<bool> (new bool[80]);
		moveDistance = data.moveDistance;
	
		iterationNumX = Mathf.Floor((highX - lowX) / moveDistance);
		iterationNumY = Mathf.Floor((highY - lowY) / moveDistance);

//		for (int i = 0; i <= iterationNumX; i++) {
//			for (int j = 0; j <= iterationNumY; j++) {
//				float x = lowX + (i * moveDistance);
//				float y = lowY + (j * moveDistance);
//				squareGrid.Add (new Vector2(x,y));
//			}
//		}

		for (int i = 0; i <= iterationNumX; i++) {
			for (int j = 0; j <= iterationNumY; j++) {
				float x = lowX + (i * moveDistance);
				float y = lowY + (j * moveDistance);
				squareGrid2.Add (new GridPosition(new Vector2(x, y), false));
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < squareGrid2.Count; i++) {
			OccupationsList[i] = squareGrid2 [i].occupied;
		}
	}


}
