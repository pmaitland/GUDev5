using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTextOnTrigger : MonoBehaviour
{
	public string helpText = "Placeholder";

	void OnTriggerEnter2D(Collider2D col)
	{
		UsefulCanvas.Instance.infoPanel.Show(helpText);
	}
}
