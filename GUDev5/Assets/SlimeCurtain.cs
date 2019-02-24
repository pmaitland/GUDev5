using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCurtain : MonoBehaviour
{
  public RectTransform endCurtain, startCurtain;
  private Vector2 originalEnd, originalStart;
  public bool openning = true;
  private void Awake() {
      originalEnd = endCurtain.anchoredPosition;
      originalStart = startCurtain.anchoredPosition;
  }
  void Update()
  {
      if(Input.GetKeyDown("o"))
          Open();
      if(Input.GetKeyDown("c"))
          Close();
  }

private const float speed = 1000;
  public void Open()
  {
    StopAllCoroutines();
    openning = true;
    startCurtain.gameObject.SetActive(true);
    endCurtain.gameObject.SetActive(false);
    startCurtain.anchoredPosition = originalStart;
    startCurtain.GetComponent<Rigidbody2D>().velocity = Vector2.left*speed;
    StartCoroutine(Reset());
    
  }

  public void Close()
  {
    StopAllCoroutines();
    openning = true;
    startCurtain.gameObject.SetActive(false);
    endCurtain.gameObject.SetActive(true);
    endCurtain.anchoredPosition = originalEnd;
    endCurtain.GetComponent<Rigidbody2D>().velocity = Vector2.left*speed;
    StartCoroutine(Reset());
  }

  public void Hide()
  {
    StopAllCoroutines();
    startCurtain.gameObject.SetActive(false);
    endCurtain.gameObject.SetActive(false);
  }

private IEnumerator Reset(){
    yield return new WaitForSeconds(0.9f);
    // endCurtain.SetPositionAndRotation(originalEnd, Quaternion.identity);
    // startCurtain.SetPositionAndRotation(originalStart, Quaternion.identity);
    startCurtain.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    endCurtain.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
}
//   private IEnumerator SlideCurtain(GameObject curtain)
//   {
//     RectTransform[] shadows = curtain.GetComponentsInChildren<RectTransform>();
//     foreach(Transform t in shadows)
//         t.gameObject.SetActive(false);

//     for (int i = 0; i < shadows.Length; i++)
//     {
//       for (int j = i; j < shadows.Length - 1; j++)
//       {
//         if (shadows[j].anchoredPosition.x > shadows[j + 1].anchoredPosition.x)
//         {
//           RectTransform t = shadows[j];
//           shadows[j] = shadows[j + 1];
//           shadows[j + 1] = t;
//         }
//       }
//     }
//     for (int i = 0; i < shadows.Length; i++)
//     {
//       shadows[i].gameObject.SetActive(true);
//       yield return new WaitForSeconds(0.1f);
//     }

//   }
}
