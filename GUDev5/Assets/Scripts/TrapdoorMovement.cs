using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapdoorMovement : MonoBehaviour
{

	private Transform transform;

	public float moveSpeed = 0.05f;
	public float moveDistance = 2f;

	private Vector2 openPosition;
	private Vector2 closedPosition;

	private bool closing = false;
	private bool opening = false;

	void Start()
	{
		transform = gameObject.transform;

		// open position is initial position
		openPosition = new Vector3(transform.position.x, transform.position.y);
		// closed position is to right of open position
		closedPosition = new Vector3(transform.position.x + moveDistance, transform.position.y);
	}

	void Update() {
		if (closing) {
			if (transform.position.x < closedPosition.x)
				transform.position = new Vector3(transform.position.x + moveSpeed, transform.position.y, transform.position.z);
			else if (transform.position.x >= openPosition.x)
				closing = false;
		}
		else if (opening) {
			if (transform.position.x > openPosition.x)
				transform.position = new Vector3(transform.position.x - moveSpeed, transform.position.y, transform.position.z);
			else if (transform.position.x <= openPosition.x)
				opening = false;
		}
	}

	public void Close()
	{
		closing = true;
		opening = false;
	}

	public void Open()
	{
		closing = false;
		opening = true;
	}
}
