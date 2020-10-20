using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodObject : MonoBehaviour {

	public Vector2 pos;
	public Vector2 prevPos;
	public bool basic;
	public int num;
	public string label;
	public bool correct;

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "body") {
//			if (other.gameObject.transform.position.x >= 0 || other.gameObject.transform.position.x < -3.15f || other.gameObject.transform.position.y > 2.7f || other.gameObject.transform.position.y < -3.15f) {
//				GameObject.Find ("Controller").GetComponent<FoodController>().RelocateItem(other.gameObject);
//			}
//			MoveFromTo (transform.gameObject, transform.position, new Vector2(transform.position.x + 0.45f, transform.position.y));
//			transform.position = new Vector2(transform.position.x + 0.45f, transform.position.y);
		}
			
//		if(other.gameObject.GetComponent<FoodObject> ().correct){
//			other.gameObject.transform.position = new Vector2(other.gameObject.transform.position.x + 0.9f, other.gameObject.transform.position.y);
//		}
	}


	void MoveFromTo(GameObject obj, Vector2 from, Vector2 to)
	{
		Vector2 o = Vector2.zero;
		Vector2 pos = Vector2.SmoothDamp(from, to, ref o, 0.15f, 75f, Time.deltaTime);
		obj.transform.position = pos;
	}

}
