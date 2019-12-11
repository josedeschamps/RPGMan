using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSlideA : MonoBehaviour {

	public Vector2 startPoint;
	public Vector3 endPoint,nextPoint;
	public float speed;
	public bool hasSlideRight,hasSlideLeft;


	void Start () 
	{
		startPoint = transform.position;
		hasSlideRight = false;
		hasSlideLeft = false;


	}

	void Update ()
	{


	

		if (hasSlideRight) {

			NextPosition ();
		}


	}

	public void ResetRightPosition(){

		StartCoroutine (ResetEndposition ());

	}

	IEnumerator ResetEndposition(){

		yield return new WaitForSeconds (1.5f);
		hasSlideRight = false;

	}

	void NextPosition(){

		transform.position = Vector3.MoveTowards (new Vector3(transform.position.x, 
			transform.position.y,transform.position.z), nextPoint, speed * Time.deltaTime );
	}



}
