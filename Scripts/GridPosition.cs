using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPosition {

	public Vector2 position;
	public bool occupied = false;

	public GridPosition(Vector2 pos, bool occ){
		this.position = pos;
		this.occupied = occ;
	}

	public void ToggleOccupied(){
		if (occupied == false) {
			occupied = true;
		} else {
			occupied = false;
		}
	}
}
