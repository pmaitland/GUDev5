using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSlimeMovement : MonoBehaviour
{
    
	private Transform transform;
	private Rigidbody2D rb;
	private Animator anim;

	public float moveSpeed = 0.01f;

private void Awake() {
	anim = GetComponent<Animator>();
	rb = GetComponent<Rigidbody2D>();
}
	void Start()
	{
		transform = gameObject.transform;
	}
    
    void Update()
    {
		// move right
		transform.position = new Vector3(transform.position.x + moveSpeed, transform.position.y, transform.position.z);
    
		
			UpdateAnimationParam();
			}

		void UpdateAnimationParam(){
			anim.SetFloat("prevVeloY", anim.GetFloat("veloY"));
			anim.SetFloat("veloY", rb.velocity.y);
		}
}
