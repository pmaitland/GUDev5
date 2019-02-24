using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
	private Transform transform;

	public float moveSpeed = 0.5f;

	public enum Direction{ Right, Left, Up, Down };
	public Direction direction;

	private float lifetime = 2000;
	private float timeAlive = 0;

	void Start()
	{
		transform = gameObject.transform;
	}

    void Update()
    {
		switch (direction) {
			case Direction.Right:
				transform.position = new Vector2(transform.position.x + moveSpeed, transform.position.y);
				break;
			case Direction.Down:
				transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed);
				break;
			case Direction.Left:
				transform.position = new Vector2(transform.position.x - moveSpeed, transform.position.y);
				break;
			case Direction.Up:
				transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed);
				break;
			default:
				break;
		}

		timeAlive++;
		if (timeAlive >= lifetime) Destroy(gameObject);
    }

	void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy(gameObject);
	}

	public void setDirection(ArrowGunShoot.Direction d) {
		switch (d) {
		case ArrowGunShoot.Direction.Right:
			direction = Direction.Right;
			break;

		case ArrowGunShoot.Direction.Down:
			direction = Direction.Down;
			break;

		case ArrowGunShoot.Direction.Left:
			direction = Direction.Left;
			break;

		case ArrowGunShoot.Direction.Up:
			direction = Direction.Up;
			break;

		default:
			direction = Direction.Up;
			break;
		}
	}
}
