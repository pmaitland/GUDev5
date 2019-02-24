using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesMovement : MonoBehaviour
{
	private Transform transform;

	public float moveSpeed;
	public float moveDistance;

	public enum Direction {Right, Left, Up, Down};
	public Direction startingDirection;

	public float pauseDuration;

	private float distanceMoved;
	private Direction currentDirection;
	private bool paused = false;
	public float currentPauseTime = 0;

	// Start is called before the first frame update
	void Start()
	{
		transform = gameObject.transform;

		currentDirection = startingDirection;
	}

	// Update is called once per frame
	void Update()
	{
		if (paused) {
			currentPauseTime++;
			if (currentPauseTime >= pauseDuration) {
				paused = false;
				currentPauseTime = 0;
			}
			return;
		}

		switch (currentDirection) {
		case Direction.Right:
			transform.position = new Vector2(transform.position.x + moveSpeed, transform.position.y);
			break;
		case Direction.Left:
			transform.position = new Vector2(transform.position.x - moveSpeed, transform.position.y);
			break;
		case Direction.Up:
			transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed);
			break;
		case Direction.Down:
			transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed);
			break;
		default:
			break;
		}
		distanceMoved += moveSpeed;
		if (distanceMoved >= moveDistance) {
			switch (currentDirection) {
			case Direction.Right:
				currentDirection = Direction.Left;
				break;
			case Direction.Left:
				currentDirection = Direction.Right;
				break;
			case Direction.Up:
				currentDirection = Direction.Down;
				break;
			case Direction.Down:
				currentDirection = Direction.Up;
				break;
			default:
				break;
			}
			distanceMoved = 0;
			paused = true;
		}
	}
}
