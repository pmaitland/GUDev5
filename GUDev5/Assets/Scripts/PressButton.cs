using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButton : MonoBehaviour
{
	// target object controlled by button
	public GameObject[] targets;

	public Sprite actuatedSprite;
	public Sprite unactuatedSprite;

	public enum SlimeColor{ Red, Blue };
	public enum ButtonColor{ White, Red, Blue };
	public ButtonColor buttonColor;

	private SpriteRenderer spriteRenderer;

	private int numOfCols = 0;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		if (spriteRenderer.sprite == null) spriteRenderer.sprite = unactuatedSprite;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		numOfCols++;

		// ignore any collisions which can't tell us the colour of the slime
		if (!col.transform.parent.gameObject.GetComponent<TinySlimeControl>()) return;

		SlimeColor slimeColor = (SlimeColor)col.transform.parent.gameObject.GetComponent<TinySlimeControl>().slimeColor;

		// if button isn't white and slime and button are different colours, don't push the button
		if (!(buttonColor.ToString() == "White") && slimeColor.ToString() != buttonColor.ToString()) return;

		spriteRenderer.sprite = actuatedSprite;

		foreach (GameObject target in targets) {
			switch (target.tag) {
				case "Door":
					target.GetComponent<DoorMovement>().Open();
					break;

				case "Trapdoor":
					target.GetComponent<TrapdoorMovement>().Close();
					break;

				default:
					Debug.Log("Unkown target type for button");
					break;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		numOfCols--;
		if (numOfCols > 0) return;

		spriteRenderer.sprite = unactuatedSprite;

		foreach (GameObject target in targets) {
			switch (target.tag) {
				case "Door":
					target.GetComponent<DoorMovement>().Close();
					break;

				case "Trapdoor":
					target.GetComponent<TrapdoorMovement>().Open();
					break;

				default:
					Debug.Log("Unkown target type for button");
					break;
			}
		}
	}
}
