using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCamera : MonoBehaviour
{
  private int currentBlock = 0;
  private const float deltaX = 16;
  private float x = 0;
  private float z =0;
  private const float speed = 2;
  private Vector3 a, b;

private void Awake() {
    a = transform.position;
    b = a;
    z = a.z;
}
  private void Update()
  {
      if(Input.GetKeyDown("q"))
      PrevBlock();
      if(Input.GetKeyDown("e"))
      NextBlock();
    x += Time.deltaTime * speed;
    transform.position = Vector3.Lerp(a, b, x);
  }
  public void NextBlock()
  {
    a = transform.position;
    a.x = currentBlock * deltaX;
    a.z = z;
    b = a;
    b.x = (currentBlock + 1) * deltaX;
    b.z = z;
    x = 0;
    currentBlock++;
  }

  public void PrevBlock()
  {
    a = transform.position;
    a.x = currentBlock * deltaX;
    a.z = z;
    b = a;
    b.x = (currentBlock - 1) * deltaX;
    b.z = z;
    x = 0;
    currentBlock--;
  }
}
