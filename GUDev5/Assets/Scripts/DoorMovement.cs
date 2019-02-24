using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : MonoBehaviour
{

	private Transform transform;

	public float moveSpeed = 0.05f;
	public float moveDistance = 2f;

	private Vector2 closedPosition;
	private Vector2 openPosition;

	private bool opening = false;
	private bool closing = false;

	void Start()
	{
		transform = gameObject.transform;

		// closed position is initial position
		closedPosition = new Vector3(transform.position.x, transform.position.y);
		// open position is above closed position
		openPosition = new Vector3(transform.position.x, transform.position.y + moveDistance);
	}

	void Update() {
		if (opening) {
			if (transform.position.y < openPosition.y)
				transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed, transform.position.z);
			else if (transform.position.y >= openPosition.y)
				opening = false;
		}
		else if (closing) {
			if (transform.position.y > closedPosition.y)
				transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed, transform.position.z);
			else if (transform.position.y <= openPosition.y)
				closing = false;
		}
	}

	public void Open()
	{
		opening = true;
		closing = false;
	}

	public void Close()
	{
		opening = false;
		closing = true;
	}
}
