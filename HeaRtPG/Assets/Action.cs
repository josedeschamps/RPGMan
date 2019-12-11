using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour {
	private Animator anim; 

	// Use this for initialization
	void Start () {

	anim = GetComponent<Animator> ();

		
	}
	
	// Update is called once per frame
	public void Update () {
		if (Input.GetButtonDown("Jump")) {
			anim.SetBool ("Attack", true); 
		}else{
			anim.SetBool("Attack", false); 
		}
		
	}
	void FixedUpdate(){
		
	}
}
