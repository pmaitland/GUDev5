using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeDetector : MonoBehaviour
{
  public EdgeTrigger left, right, top, bottom;
  private GameObject colLeft, colRight, colTop, colBottom;
  // private bool hitLeft,hitRight,hitBottom,hitTop;
  private Dictionary<EdgeTrigger, HashSet<GameObject>> hittingObjects;
  private Dictionary<EdgeTrigger, bool> hit;
  private void Awake()
  {
    hittingObjects = new Dictionary<EdgeTrigger, HashSet<GameObject>>();
    hittingObjects[left] = new HashSet<GameObject>();
    hittingObjects[right] = new HashSet<GameObject>();
    hittingObjects[top] = new HashSet<GameObject>();
    hittingObjects[bottom] = new HashSet<GameObject>();

    hit = new Dictionary<EdgeTrigger, bool>();
    hit[left] = false;
    hit[right] = false;
    hit[top] = false;
    hit[bottom] = false;
  }
  public void Trigger(EdgeTrigger trigger, Collider2D collider)
  {
    HashSet<GameObject> objects = hittingObjects[trigger];
    objects.Add(collider.gameObject);
  }

  private void Clean(EdgeTrigger trigger)
  {
    HashSet<GameObject> objects = hittingObjects[trigger];
    List<GameObject> toRemove = new List<GameObject>();
    Collider2D trigCol = trigger.GetComponent<Collider2D>();

    foreach (GameObject o in objects)
    {
      Collider2D oCol = o.GetComponent<Collider2D>();
      if (trigCol.IsTouching(oCol) == false)
        toRemove.Add(o);
    }

    foreach (GameObject o in toRemove)
    {
      objects.Remove(o);
    }

    hit[trigger] = objects.Count != 0;
  }
  public bool HitLeft()
  {
    Clean(left);
    return hit[left];
  }
  public bool HitTagLeft(string tag)
  {
    Clean(left);
    return HitTag(left, tag);
  }

  public bool HitRight()
  {
    Clean(right);
    return hit[right];
  }
  public bool HitTagRight(string tag)
  {
    Clean(right);
    return HitTag(right, tag);
  }

  public bool HitTop()
  {
    Clean(top);
    return hit[top];
  }
  public bool HitTagTop(string tag)
  {
    Clean(top);
    return HitTag(top, tag);
  }

  public bool HitBottom()
  {
    Clean(bottom);
    return hit[bottom];
  }
  public bool HitTagBottom(string tag)
  {
    Clean(bottom);
    return HitTag(bottom, tag);
  }
  public bool HitTag(EdgeTrigger trigger, string tag)
  {
    foreach (GameObject o in hittingObjects[trigger])
    {
      if (o.tag == tag)
        return true;
    }
    return false;
  }
  public bool HitTag(string tag)
  {
    foreach (KeyValuePair<EdgeTrigger, HashSet<GameObject>> pairs in hittingObjects)
    {
      foreach (GameObject o in pairs.Value)
      {
        if (o.tag == tag)
          return true;
      }
    }
    return false;
  }
  public bool NoHit()
  {
    return !HitBottom() && !HitTop() && !HitLeft() && !HitRight();
  }

  public List<GameObject> GetLeft()
  {
    return new List<GameObject>(hittingObjects[left]);
  }
  public List<GameObject> GetRight()
  {
    return new List<GameObject>(hittingObjects[right]);
  }

  public List<GameObject> GetTop()
  {
    return new List<GameObject>(hittingObjects[top]);
  }
  public List<GameObject> GetBottom()
  {
    return new List<GameObject>(hittingObjects[bottom]);
  }
}
