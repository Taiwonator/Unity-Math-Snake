using UnityEngine;

public enum Swipe { None, Up, Down, Left, Right };

public class Swipe2 : MonoBehaviour
{
	public SnakeScriptableObject data;
	public GameObject snake;

	public float minSwipeLength = 1f;
	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;

	Vector2 firstClickPos;
	Vector2 secondClickPos;

	public static Swipe swipeDirection;

	void Update ()
	{
		DetectSwipe ();
	}

	public void DetectSwipe ()
	{
		int d = snake.GetComponent<SnakeController> ().snake.direction;

		if (Input.touches.Length > 0) {
			Touch t = Input.GetTouch(0);

			if (t.phase == TouchPhase.Began) {
				firstPressPos = new Vector2(t.position.x, t.position.y);
				//Debug.Log (firstPressPos);
			}
			if (t.phase == TouchPhase.Stationary) {
				firstPressPos = new Vector2(t.position.x, t.position.y);
				//Debug.Log (firstPressPos);
			}

			if (t.phase == TouchPhase.Moved) {
				secondPressPos = new Vector2(t.position.x, t.position.y);
				currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

				// Make sure it was a legit swipe, not a tap
				if (currentSwipe.magnitude < minSwipeLength) {
					swipeDirection = Swipe.None;
					return;
				}

				currentSwipe.Normalize();
//				currentSwipe.x *= 10;
//				currentSwipe.y *= 10;

				if ((currentSwipe.x > -0.7f && currentSwipe.x < 0.7f) && (currentSwipe.y > 0.7f) && d != 2) {
				//UP
					data.SwipeDirection = 0;

				}

				if ((currentSwipe.x > 0.7f && currentSwipe.x < 1f) && (currentSwipe.y > -0.7f && currentSwipe.y < 0.7f) && d != 3) {
				//Down
					data.SwipeDirection = 1;

				}

				if ((currentSwipe.x > -0.7f && currentSwipe.x < 0.7f) && (currentSwipe.y > -1f && currentSwipe.y < -0.7f) && d != 0) {
				//Left
					data.SwipeDirection = 2;

				}

				if ((currentSwipe.x > -1f && currentSwipe.x < -0.7f) && (currentSwipe.y > -0.7f && currentSwipe.y < 0.7f) && d != 1) {
					data.SwipeDirection = 3;

				}

			}
		} else {

			if (Input.GetMouseButtonDown(0)) {
				firstClickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			} else {
				swipeDirection = Swipe.None;
				//Debug.Log ("None");
			}
			if (Input.GetMouseButtonUp (0)) {
				secondClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
				currentSwipe = new Vector2 (secondClickPos.x - firstClickPos.x, secondClickPos.y - firstClickPos.y);

				// Make sure it was a legit swipe, not a tap
				if (currentSwipe.magnitude < minSwipeLength) {
					swipeDirection = Swipe.None;
					return;
				}

				currentSwipe.Normalize ();

				//Swipe directional check
				// Swipe up
				if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
					swipeDirection = Swipe.Up;
					data.SwipeDirection = 0;

				} else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
					swipeDirection = Swipe.Down;
					data.SwipeDirection = 2;
				} else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
					swipeDirection = Swipe.Left;
					data.SwipeDirection = 3;
					// Swipe right
				} else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
					swipeDirection = Swipe.Right;
					data.SwipeDirection = 1;
				}
			}

		}
	}
}
