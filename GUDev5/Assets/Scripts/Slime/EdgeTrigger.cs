using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeTrigger : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other) {
    //    print(other.transform.parent.parent.name + " > " + other.transform.parent.name + " > " + other.gameObject.name);
    //    if(other.isTrigger && other.gameObject.tag != "TinySlimes")
        // return;
        if(other.isTrigger)
        return;
       GetComponentInParent<EdgeDetector>().Trigger(this,other);
   }
}
