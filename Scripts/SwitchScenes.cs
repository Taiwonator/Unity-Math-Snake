using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour {
	public bool x;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (x) {
			SceneManager.LoadScene (0);
			x = false;
		} else {
			SceneManager.LoadScene (1);
			x = true;
		}
	}
}
