using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishBoxTrigger : MonoBehaviour
{
	private int numCols = 0;
	public string nextLevel;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.transform.gameObject.layer == 8 || col.transform.gameObject.layer == 9)
			numCols++;

		if (numCols >= 5)
			SceneManager.LoadScene(nextLevel);
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.transform.gameObject.layer == 8 || col.transform.gameObject.layer == 9)
			numCols--;
	}
}
