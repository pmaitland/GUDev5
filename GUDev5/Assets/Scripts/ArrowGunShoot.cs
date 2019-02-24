using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGunShoot : MonoBehaviour
{
	private Collider2D collider;

	public Transform arrow;

	public enum Direction{ Right, Left, Up, Down };
	public Direction direction;

	public float shootingCooldown = 100;
	private float countSinceLastShot = 0;

	void Start()
	{
		collider = GetComponent<Collider2D>();
	}

    void Update()
    {
		countSinceLastShot++;
		if (countSinceLastShot >= shootingCooldown) {
			Vector2 arrowPosition = transform.position;

			switch (direction) {
				case Direction.Right:
					arrowPosition = new Vector2(transform.position.x + collider.bounds.size.x, transform.position.y);
					break;
				case Direction.Down:
					arrowPosition = new Vector2(transform.position.x, transform.position.y - collider.bounds.size.y);
					break;
				case Direction.Left:
					arrowPosition = new Vector2(transform.position.x - collider.bounds.size.x, transform.position.y);
					break;
				case Direction.Up:
					arrowPosition = new Vector2(transform.position.x, transform.position.y + collider.bounds.size.y);
					break;
				default:
					break;
			}

			Transform a = Instantiate(arrow, arrowPosition, transform.rotation);
			a.gameObject.GetComponent<ArrowMovement>().setDirection(direction);
			countSinceLastShot = 0;
		}
    }
}
