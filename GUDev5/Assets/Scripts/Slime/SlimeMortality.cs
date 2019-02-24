using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlimeMortality : MonoBehaviour
{
  private EdgeDetector edge;
  private Animator anim;
  private bool dead = false;
  public bool isMama = false;
  private void Awake()
  {
    if (!isMama)
      edge = GetComponentInChildren<EdgeDetector>();
    
    if(!isMama)
    anim = GetComponentInChildren<Animator>();
    else
    anim = GetComponent<Animator>();
  }
  private bool restart = false;
  private void Update()
  {
    if (dead)
      return;

    if (!isMama)
    {
      if (edge.HitTag("Danger"))
      {
        StartCoroutine(DieRoutine());
        dead = true;
      }
    }else
    {
        if(Input.GetKeyDown("r")){
      StartCoroutine(DieRoutine());
      dead = true;
      restart = true;
      }

    }
  }
  private void OnCollisionEnter2D(Collision2D other)
  {
    if (!isMama)
      return; if (other.gameObject.tag == "Danger")
    {
      StartCoroutine(DieRoutine());
      dead = true;
    }
  }
  private IEnumerator DieRoutine()
  {
    //todo
    if (JoyconManager.Instance != null)
      JoyconManager.Instance.OnApplicationQuit();

    anim.Play("Die");
    InvokeRepeating("Flash", 1, 0.03f);
    if (!isMama)
    {
      TinySlimeControl control = GetComponent<TinySlimeControl>();
      if (control != null)
        Destroy(control);
    }
    else
    {
      Destroy(GetComponent<BigSlimeMovement>());
    }
    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    if (rb != null)
    {
      rb.velocity = Vector2.up * 10;
      rb.gravityScale = 2;
    }

    foreach (Collider2D col in GetComponentsInChildren<Collider2D>())
    {
      col.isTrigger = true;
    }

    if (isMama)
      GetComponent<Collider2D>().isTrigger = true;

    yield return new WaitForSeconds(2);

    if (UsefulCanvas.Instance != null)
      if (UsefulCanvas.Instance.curtain.openning == true)
        UsefulCanvas.Instance.curtain.Close();

    yield return new WaitForSeconds(2);
    if(restart)
    SceneManager.LoadScene("Level1");
    else
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  private void Flash()
  {
    SpriteRenderer s;
    if (isMama)
      s = GetComponent<SpriteRenderer>();
    else
      s = GetComponentInChildren<SpriteRenderer>();
    s.enabled = !s.enabled;
  }
}
