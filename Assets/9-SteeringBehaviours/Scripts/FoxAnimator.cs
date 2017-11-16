using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FoxAnimator : MonoBehaviour
{
    private Animator anim;
    private Vector3 prevPos;
	
	void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	
	void Update ()
    {
        Vector3 currPos = transform.position;

        float movementSpeed = (prevPos - currPos).magnitude;
        anim.SetFloat("moveSpeed", movementSpeed);

        prevPos = currPos;	
	}
}
