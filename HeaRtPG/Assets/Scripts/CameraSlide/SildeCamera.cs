using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SildeCamera : MonoBehaviour {

	public Vector2 startPoint;
	public Vector3 endPoint,nextPoint;
	public Vector3 topPoint, bottomPoint;
	public float speed;
	public bool hasSlideRight,hasSlideLeft;
	public bool hasSlideBottom, hasSlideTop;

	void Start () 
	{
		startPoint = transform.position;
		hasSlideRight = false;
		hasSlideLeft = false;
		hasSlideTop = false;
		hasSlideBottom = false;

	}

	void Update ()
	{


		if (hasSlideTop) {

			TopCameraPosition ();
		}

		if (hasSlideBottom) {

			BottomCameraPosition ();
		}


		if (hasSlideRight) {
			
			EndPosition ();
		}

		if (hasSlideLeft) {

			NextPosition ();
		}

	
	}



	void TopCameraPosition(){

		transform.position = Vector3.MoveTowards (new Vector3(transform.position.x, 
			transform.position.y,transform.position.z), topPoint, speed * Time.deltaTime );
	}

	public void ResetTopCamera(){

		StartCoroutine (ResetTopPosition ());

	}

	IEnumerator ResetTopPosition(){

		yield return new WaitForSeconds (1f);
		hasSlideTop = false;

	}


	void BottomCameraPosition(){

		transform.position = Vector3.MoveTowards (new Vector3(transform.position.x, 
			transform.position.y,transform.position.z), bottomPoint, speed * Time.deltaTime );
	}

	public void ResetBottomCamera(){

		StartCoroutine (ResetBottomPosition ());

	}

	IEnumerator ResetBottomPosition(){

		yield return new WaitForSeconds (1f);
		hasSlideBottom = false;

	}



	void EndPosition ()
	{

		transform.position = Vector3.MoveTowards (new Vector3(transform.position.x, 
			transform.position.y,transform.position.z), endPoint, speed * Time.deltaTime );
		
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


	public void ResetLeftPosition(){

		StartCoroutine (ResetNextposition ());

	}

	IEnumerator ResetNextposition(){

		yield return new WaitForSeconds (1.5f);
		hasSlideLeft = false;

	}
}
