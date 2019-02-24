using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzsawMovement : MonoBehaviour
{
	private Transform transform;

	public float moveSpeed;
	public float moveDistance;
	public int startingDirection;
	public float pauseDuration;

	private float distanceMoved;
	private int currentDirection;
	private bool paused = false;
	private float currentPauseTime = 0;

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
			case 0:
				transform.position = new Vector2(transform.position.x + moveSpeed, transform.position.y);
				break;
			case 1:
				transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed);
				break;
			case 2:
				transform.position = new Vector2(transform.position.x - moveSpeed, transform.position.y);
				break;
			case 3:
				transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed);
				break;
			default:
				break;
		}
		distanceMoved += moveSpeed;
		if (distanceMoved >= moveDistance) {
			currentDirection = (currentDirection + 2) % 4;
			distanceMoved = 0;
			paused = true;
		}
    }
}
