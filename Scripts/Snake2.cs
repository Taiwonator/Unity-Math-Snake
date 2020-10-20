using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Snake2 : MonoBehaviour {

	[SerializeField]
	private float xPos;
	[SerializeField]
	private float yPos;

	public SnakeScriptableObject data;
	[SerializeField]
	private int directionCommand; 
	[SerializeField]
	public List<GameObject> snakeBody;
	[SerializeField]
	public List<Vector2> previousPos;
	[SerializeField]
//	public List<bool> disabled;
	public GameObject snakeController;

	public GameObject head;
	public Vector2 nextPos;
	public bool alive = true;

	bool creationDone;
	public bool firstDone = false;
	[SerializeField]
	public int direction = -1;

	public float speed;
	public bool keys = false;
	public bool isPlayer;
	public GameObject target;
	public GameObject snakePrefab;

	public GameObject[] skins;

	bool start = true;

	int keysDirection = -1;
	int swipeDirection = -1;
	public float minSwipeLength = 1f;
	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;
	Vector2 firstClickPos;
	Vector2 secondClickPos;

	// Use this for initialization
	void Awake(){
		direction = -1;	
	}

	void Start () {
		if(GetComponent<Player>() != null) {
			isPlayer = true;
		}
		speed = data.speed;
		data.SwipeDirection = 0;
		previousPos = new List<Vector2> (new Vector2[data.snakeLength]);
//		disabled = new List<bool> (new bool[data.snakeLength]);
		snakeController = GameObject.Find ("Controller");
//		int rand = Random.Range (0, 2);
//		if (rand == 0) {
//			snakePrefab = data.prefab;
//		} else {
//			snakePrefab = data.prefab2;
//		}
		SetSkin();
		previousPos[0] = data.startingPos;
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
			keys = false;
		} else {
			keys = true;
		}
		if (GameObject.Find ("FoodObject(1)") != null) {
			target = GameObject.Find ("FoodObject(1)");
		}
	}
	
	// Update is called once per frame
	void Update () {
//		isPlayer = DataManager.control.player.AI_control;
		if (!creationDone) {
			CreateBody ();
		}
		if (isPlayer) {
			if (alive && !snakeController.GetComponent<SnakeController> ().paused) {
				if (!start) {
					Move ();
					Timer ();
					snakeController.GetComponent<Timer> ().startPause = false;
				} else {
					snakeController.GetComponent<Timer> ().startPause = true;
				}
				if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
					DetectSwipe ();
				} else {
					DetectKeys ();
				}
			}
		} else {
			if (alive && !snakeController.GetComponent<SnakeController> ().paused) {
				if (!start) {
					Move ();
					Timer ();
					snakeController.GetComponent<Timer> ().startPause = false;
				} else {
					snakeController.GetComponent<Timer> ().startPause = true;
				}
				CheckDistance ();
			}
		}
		if (!isPlayer) {
			speed = data.speed * 2f;
		} else {
			speed = data.speed;
		}



		if (!finished) {
			MoveIndividualBlock ();
		}

		for (int i = 1; i < previousPos.Count; i++) {
			if (Vector2.Distance (previousPos [i], previousPos [(i-1)]) > 3) {
				StartCoroutine(DisableHead (snakeBody [i]));
			}

		}

		if (snakeBody.Count > DataManager.control.player.longest_snake) {
			DataManager.control.player.longest_snake = snakeBody.Count;
//			DataManager.control.Save2 ();
//			DataManager.control.LoadFunction ();
		}

	}

	void CheckDistance() {
		Vector2 vPosUp = new Vector2(0, 0);
		Vector2 vPosRight = new Vector2(0, 0);
		Vector2 vPosDown = new Vector2(0, 0);
		Vector2 vPosLeft = new Vector2(0, 0);

		float distanceUp = 0;
		float distanceRight = 0;
		float distanceDown = 0;
		float distanceLeft = 0;
		float distance = 0;

		List<float> potentials = new List<float>();

		vPosUp = new Vector2 (snakeBody [0].transform.position.x, snakeBody [0].transform.position.y + data.moveDistance);
		distanceUp = Vector2.Distance (vPosUp, target.transform.position);

		vPosRight = new Vector2 (snakeBody [0].transform.position.x + data.moveDistance, snakeBody [0].transform.position.y);
		distanceRight = Vector2.Distance (vPosRight, target.transform.position);

		vPosDown = new Vector2 (snakeBody [0].transform.position.x, snakeBody [0].transform.position.y - data.moveDistance);
		distanceDown = Vector2.Distance (vPosDown, target.transform.position);

		vPosLeft = new Vector2 (snakeBody [0].transform.position.x - data.moveDistance, snakeBody [0].transform.position.y);
		distanceLeft = Vector2.Distance (vPosLeft, target.transform.position);

		if (snakeController.GetComponent<FoodController> ().grid.Contains(ReturnRoundedVetor2(vPosUp, 0.45f)) || ReturnRoundedVetor2(vPosUp, 0.45f) == new Vector2(target.transform.position.x, target.transform.position.y)) {
			potentials.Add (distanceUp);
		}
		if (snakeController.GetComponent<FoodController> ().grid.Contains(ReturnRoundedVetor2(vPosRight, 0.45f))  || ReturnRoundedVetor2(vPosRight, 0.45f) == new Vector2(target.transform.position.x, target.transform.position.y)) {
			potentials.Add (distanceRight);
		}
		if (snakeController.GetComponent<FoodController> ().grid.Contains(ReturnRoundedVetor2(vPosDown, 0.45f))  || ReturnRoundedVetor2(vPosDown, 0.45f) == new Vector2(target.transform.position.x, target.transform.position.y)) {
			potentials.Add (distanceDown);
		}
		if (snakeController.GetComponent<FoodController> ().grid.Contains(ReturnRoundedVetor2(vPosLeft, 0.45f))  || ReturnRoundedVetor2(vPosLeft, 0.45f) == new Vector2(target.transform.position.x, target.transform.position.y)) {
			potentials.Add (distanceLeft);
		}
//		directionCommand = returnDirectionOfShortest(distanceUp, distanceRight, distanceDown, distanceLeft);
		int i = 0;
		int dir;
		if (potentials.Count == 0) {
			dir = 0;
//			if (vPosUp.y > 2) {
//				dir = 1;
//				if (vPosRight.x > 2) {
//					dir = 2;
//					if (vPosDown.y < -2) {
//						dir = 3;
//						if (vPosLeft.x < -2) {
//							print ("lol");
//						}
//					}
//				}
//			}
		} else {
			dir = returnDirectionOfShortest(distanceUp, distanceRight, distanceDown, distanceLeft, potentials)[i];
			if (potentials.Count == 1) {
				dir = returnDirectionOfShortest (distanceUp, distanceRight, distanceDown, distanceLeft, potentials) [0];
			} else {
				while (dir == (direction + 2) % 4) {
					i++;
					dir = returnDirectionOfShortest (distanceUp, distanceRight, distanceDown, distanceLeft, potentials) [i];
				}
			}
		}
		directionCommand = dir;
		start = false;
	}

	int[] returnDirectionOfShortest(float distanceUp, float distanceRight, float distanceDown, float distanceLeft, List<float> potentials){
		List<float> list = new List<float>();
		for (int i = 0; i < potentials.Count; i++) {
			list.Add (potentials[i]);
		}
//		list.Add (distanceUp);
//		list.Add (distanceRight);
//		list.Add (distanceDown);
//		list.Add (distanceLeft);
		list.Sort ();
		List<float> orderedList = new List<float> ();
		orderedList = new List<float>(list);
//		int[] directionOrder = new int[4];
//		print (potentials.Count);
		int[] directionOrder = new int[potentials.Count];
		if (list.Contains (distanceUp)) {
			directionOrder [orderedList.IndexOf (distanceUp)] = 0;
		}
		if (list.Contains (distanceRight)) {
			directionOrder [orderedList.IndexOf (distanceRight)] = 1;
		}
		if (list.Contains (distanceDown)) {
			directionOrder [orderedList.IndexOf (distanceDown)] = 2;
		}
		if (list.Contains (distanceLeft)) {
			directionOrder [orderedList.IndexOf (distanceLeft)] = 3;
		}
		return directionOrder;
	}

	void SetSkin(){
		skins = new GameObject[7];
		skins [0] = data.prefab;
		skins [1] = data.prefab2;
		skins [2] = data.prefab3;
		skins [3] = data.prefab4;
		skins [4] = data.prefab5;
		skins [5] = data.prefab6;
		skins [6] = data.prefab7;
		if (DataManager.control.player.currentSkin != -1 && isPlayer) {
			snakePrefab = skins [DataManager.control.player.currentSkin];
		} else {
			snakePrefab = data.defaultSkin;
		}
	}

	IEnumerator DisableHead(GameObject Object){
		Object.GetComponent<CircleCollider2D> ().enabled = false;
		Color color = Object.GetComponent<SpriteRenderer> ().color;
		Object.GetComponent<SpriteRenderer> ().color = new Color(0.5f, 0.5f, 0.5f);
		yield return new WaitForSeconds (0.5f);
		Object.GetComponent<CircleCollider2D> ().enabled = true;
		Object.GetComponent<SpriteRenderer> ().color = new Color(1, 1, 1);
	}


	float t = 1;

	public void AddSnakeTail()
	{
		GameObject temp;
		if (snakeBody.Count == 1) {
			temp = Instantiate (snakePrefab, new Vector2(0, 0), Quaternion.identity);
		} else {
			temp = Instantiate (snakePrefab, previousPos[previousPos.Count - 1], Quaternion.identity);
		}
		temp.transform.localScale = new Vector2 (data.size * t, data.size * t);
		temp.transform.parent = this.gameObject.transform;
		temp.AddComponent<CircleCollider2D> ();
		temp.GetComponent<CircleCollider2D> ().isTrigger = true;
		temp.GetComponent<CircleCollider2D> ().radius = 0.15f;
		snakeBody.Add (temp);
		temp.tag = "body";
		temp.GetComponent<SpriteRenderer> ().sortingOrder = 10;
		if (snakeBody.Count != 2) {
			previousPos.Add (previousPos [previousPos.Count - 1]);
		} else {
			previousPos.Add (previousPos[previousPos.Count - 1] + new Vector2(-1, 1));
		}
//		disabled.Add (disabled[disabled.Count - 1]);
		if (t >= 0.8f) {
			t -= 0.1f;
		}
	}

	void CreateBody()
	{
		snakeBody = new List<GameObject> ();
		InstantiateBody ();
	}

	void InstantiateBody()
	{
		int i = 0;
		while(i < data.snakeLength) {
			GameObject temp = Instantiate (snakePrefab, data.startingPos, Quaternion.identity);
			temp.transform.localScale = new Vector2 (data.size, data.size);
		    temp.transform.parent = this.gameObject.transform;
			temp.tag = "body";
			temp.AddComponent<CircleCollider2D> ();
			temp.GetComponent<CircleCollider2D> ().isTrigger = true;
			temp.GetComponent<SpriteRenderer> ().sortingOrder = 10;
			snakeBody.Add (temp);
//			if (SceneManager.GetActiveScene ().name == "SchoolSnakeMultiplayer") {
//				temp.AddComponent<NetworkIdentity> ();
//				temp.GetComponent<NetworkIdentity> ().localPlayerAuthority = true;
//				temp.AddComponent<NetworkTransform> ();
//				temp.GetComponent<NetworkTransform> ().transformSyncMode = NetworkTransform.TransformSyncMode.SyncTransform;
//			}
			i++;
		}
		if (i == snakeBody.Count) {
			head = snakeBody [0];
			if (data.snakeLength != 1) {
				snakeBody [1].tag = "Untagged";
			}
			head.gameObject.tag = "head";
//			head.AddComponent<CircleCollider2D> ();
			head.GetComponent<CircleCollider2D> ().radius = 0.15f;
			head.AddComponent<Rigidbody2D> ();
//			head.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
//			head.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
			head.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Kinematic;
			head.AddComponent<Col> ();
			head.GetComponent<Col>().tag = "coin";
			head.GetComponent<Col>().snakeBodyTag = "body";
			head.GetComponent<Col> ().controller = snakeController;
			head.GetComponent<Col> ().data = data;
			creationDone = true;
		}
	}

	bool finished;
//	bool disableIncoming = false;
	void DetectKeys(){
		if (Input.GetKeyDown (KeyCode.UpArrow) && direction != 2) {
			keysDirection = 0;
			start = false;
			DataManager.control.player.total_swipes++;
		} else if (Input.GetKeyDown (KeyCode.RightArrow) && direction != 3) {
			keysDirection = 1;
			start = false;
			DataManager.control.player.total_swipes++;
		} else if (Input.GetKeyDown (KeyCode.DownArrow) && direction != 0) {
			keysDirection = 2;
			start = false;
			DataManager.control.player.total_swipes++;
		} else if (Input.GetKeyDown (KeyCode.LeftArrow) && direction != 1) {
			keysDirection = 3;
			start = false;
			DataManager.control.player.total_swipes++;
		}
	}

	void Move()
	{
		if (isPlayer) {
			if (keys) {
				if (keysDirection == 0 && direction != 2) {
					directionCommand = 0;
				} else if (keysDirection == 1 && direction != 3) {
					directionCommand = 1;
				} else if (keysDirection == 2 && direction != 0) {
					directionCommand = 2;
				} else if (keysDirection == 3 && direction != 1) {
					directionCommand = 3;
				}
		
			} else {
				if (swipeDirection == 0 && direction != 2) {
					directionCommand = 0;
				} else if (swipeDirection == 1 && direction != 3) {
					directionCommand = 1;
				} else if (swipeDirection == 2 && direction != 0) {
					directionCommand = 2;
				} else if (swipeDirection == 3 && direction != 1) {
					directionCommand = 3;
				}
			}
		}

			if (timer >= speed) {
				switch (directionCommand) {
				case 0:
					nextPos = new Vector2 (snakeBody [0].transform.position.x, snakeBody [0].transform.position.y + data.moveDistance); 
					yPos += 0.5f;
					direction = 0;
					break;
				case 1: 
					nextPos = new Vector2 (snakeBody [0].transform.position.x + data.moveDistance, snakeBody [0].transform.position.y);
					xPos += 45f;
					direction = 1;
					break;
				case 2:
					nextPos = new Vector2 (snakeBody [0].transform.position.x, snakeBody [0].transform.position.y - data.moveDistance);
					yPos -= 0.5f;
					direction = 2;
					break;
				case 3:
					nextPos = new Vector2 (snakeBody [0].transform.position.x - data.moveDistance, snakeBody [0].transform.position.y);
					xPos -= 45f;
					direction = 3;
					break;
				}
			    GridPositions (nextPos, snakeBody.Count);
				previousPos.Insert (0, nextPos);
				previousPos.Remove (previousPos[previousPos.Count - 1]);
//			disableIncoming = false;
			if (snakeController.GetComponent<SnakeController> ().overlap) {
				if (previousPos [0].x > 3.6f && snakeController.GetComponent<SnakeController> ().right) {
					previousPos [0] = new Vector2 (-3.6f, previousPos [0].y);
					StartCoroutine (DisableHead(head));
//					disableIncoming = true;
				}

				if (previousPos [0].x < -3.6f && snakeController.GetComponent<SnakeController> ().left) {
					previousPos [0] = new Vector2 (3.6f, previousPos [0].y);
					StartCoroutine (DisableHead(head));
//					disableIncoming = true;
				}

				if (previousPos [0].y > 3.15f && snakeController.GetComponent<SnakeController> ().top) {
					previousPos [0] = new Vector2 (previousPos [0].x, -3.6f);
					StartCoroutine (DisableHead(head));
//					disableIncoming = true;
				}

				if (previousPos [0].y < -3.6f && snakeController.GetComponent<SnakeController> ().bottom) {
					previousPos [0] = new Vector2 (previousPos [0].x, 3.15f);
					StartCoroutine (DisableHead(head));
//					disableIncoming = true;
				}
			} 

//			disabled.Insert (0, disableIncoming);
//			disabled.Remove (disabled[disabled.Count - 1]);

				finished = false;
				if (!firstDone) {
					firstDone = true;
				}
				timer = 0;

			}
	}

	public void GridPositions(Vector2 pos, int snake_size){
		if (snakeController.GetComponent<FoodController> () != null) {
			StartCoroutine (Countdown (pos, snake_size));
		}
	}

	public IEnumerator Countdown(Vector2 pos, int multiplier) {
		float startTime = ongoingTimer;
		if (snakeController.GetComponent<FoodController> ().grid.Contains (ReturnRoundedVetor2 (pos, 0.45f))) {
			snakeController.GetComponent<FoodController> ().grid.Remove (ReturnRoundedVetor2(pos, 0.45f));
			//		print ("Removed at Start: " + startTime);
			//		print (snakeController.GetComponent<FoodController> ().grid.Count);
			yield return new WaitUntil (()=>(ongoingTimer - startTime) >= speed * (multiplier + 3));
			snakeController.GetComponent<FoodController> ().grid.Add (ReturnRoundedVetor2(pos, 0.45f));

		}
	}

	public float timer = 0;
	public float ongoingTimer = 0;

	void Timer()
	{
		timer += Time.deltaTime;
		ongoingTimer += Time.deltaTime;
	}

	void MoveIndividualBlock()
	{
		int i = 0;
		for (i = 0; i <= snakeBody.Count - 1; i++) {
		    MoveFromTo(snakeBody[i], snakeBody[i].transform.position, previousPos[i]);
		}
		if (i == snakeBody.Count - 1) {
			finished = true;
		}
	}

	void MoveFromTo(GameObject obj, Vector2 from, Vector2 to)
	{
		Vector2 o = Vector2.zero;
		Vector2 pos = Vector2.SmoothDamp(from, to, ref o, speed, 75f, timer);
//		Vector2 pos = Vector2.SmoothDamp(from, to, ref o, 0.3f, 75f, timer);
		obj.transform.position = pos;
	}

	void ControlSize()
	{
		snakeBody [0].transform.localScale = new Vector2 (data.size, data.size);
		for (int i = 1; i <= snakeBody.Count - 1; i++) {
			snakeBody[i].transform.localScale = new Vector2 (data.size/1, data.size/1);
		}
	}

	Vector2 ReturnRoundedVetor2(Vector2 input, float roundedMultiplier){
		float xVal = Mathf.Round((input.x / roundedMultiplier)) * roundedMultiplier;
		float yVal = Mathf.Round((input.y / roundedMultiplier)) * roundedMultiplier;
		Vector2 val = new Vector2 (xVal, yVal);
		return val;
	}
//
//	void OnTriggerEnter2D(Collider2D other)
//	{
//		Debug.Log ("dd");
//	}
//
//	void OnCollisionEnter2D(Collision2D other)
//	{
//		Debug.Log ("dd");
//	}
//
	void HeadDirection()
	{
		if (direction == 0) {
			head.GetComponent<SpriteRenderer> ().sprite = data.up;
		} else if (direction == 1) {
			head.GetComponent<SpriteRenderer> ().sprite = data.right;
		} else if (direction == 2) {
			head.GetComponent<SpriteRenderer> ().sprite = data.down;
		} else if(direction == 3){
			head.GetComponent<SpriteRenderer> ().sprite = data.left;
		}
	}


	public void Death(){
		alive = false;
	}

//	float clickTimer = 20;
//	bool firstClick = false;
//	bool secondClick = false;
//	bool countdown = false;
//
//	void DoubleClick(){
//		if (Input.GetKeyDown (KeyCode.UpArrow)) {
//			firstClick = true;
//			countdown = true;
//		}
//
//		if (countdown) {
//			clickTimer--;
//			if (Input.GetKeyDown (KeyCode.UpArrow) && clickTimer < 18) {
//				secondClick = true;
//			}
//			if (clickTimer <= 0) {
//				countdown = false;
//				clickTimer = 20;
//			}
//		}
//
//
//		Debug.Log (secondClick);
//	}

	public void DetectSwipe ()
	{
		int d = direction;

		if (Input.touches.Length > 0) {
			Touch t = Input.GetTouch (0);

			if (t.phase == TouchPhase.Began) {
				firstPressPos = new Vector2 (t.position.x, t.position.y);
			}
			if (t.phase == TouchPhase.Stationary) {
				firstPressPos = new Vector2 (t.position.x, t.position.y);
			}

			if (t.phase == TouchPhase.Moved) {
				secondPressPos = new Vector2 (t.position.x, t.position.y);
				currentSwipe = new Vector2 (secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

				if (currentSwipe.magnitude < minSwipeLength) {
					swipeDirection = swipeDirection;
				}
				start = false;
				currentSwipe.Normalize ();

				if ((currentSwipe.x > -0.7f && currentSwipe.x < 0.7f) && (currentSwipe.y > 0.7f) && d != 2) {
					//UP
					swipeDirection = 0;
					print ("Up");
					DataManager.control.player.total_swipes++;
				}

				if ((currentSwipe.x > 0.7f && currentSwipe.x < 1f) && (currentSwipe.y > -0.7f && currentSwipe.y < 0.7f) && d != 3) {
					//Down
					swipeDirection = 1;
					print ("Right");
					DataManager.control.player.total_swipes++;
				}

				if ((currentSwipe.x > -0.7f && currentSwipe.x < 0.7f) && (currentSwipe.y > -1f && currentSwipe.y < -0.7f) && d != 0) {
					//Left
					swipeDirection = 2;
					print ("Down");
					DataManager.control.player.total_swipes++;
				}

				if ((currentSwipe.x > -1f && currentSwipe.x < -0.7f) && (currentSwipe.y > -0.7f && currentSwipe.y < 0.7f) && d != 1) {
					swipeDirection = 3;
					print ("Left");
					DataManager.control.player.total_swipes++;
				}

			}
		} else {
			if (Input.GetMouseButtonDown (0)) {
				firstClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			} 

			if (Input.GetMouseButtonUp (0)) {
				secondClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
				currentSwipe = new Vector2 (secondClickPos.x - firstClickPos.x, secondClickPos.y - firstClickPos.y);
				start = false;
				if (currentSwipe.magnitude < minSwipeLength) {
					swipeDirection = swipeDirection;
					return;
				}

				currentSwipe.Normalize ();

				//Swipe directional check
				// Swipe up
				if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
					swipeDirection = 0;
		
				} else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
					swipeDirection = 2;

				} else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
					swipeDirection = 3;

					// Swipe right
				} else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
					swipeDirection = 1;

				}
			}
		}
	}
				
}
