using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeCollision : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D collision)
	{
		switch (collision.gameObject.tag) {
			case "BigSlime":
				Destroy(collision.gameObject);
				// TODO end game on mama death
				break;

			// TODO baby slime death

			default:
				break;
		}
	}
}
