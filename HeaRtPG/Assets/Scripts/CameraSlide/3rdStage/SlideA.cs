using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideA : MonoBehaviour {
	
	private CameraSlideA sm;
	public Transform playerPosition;
	public Vector3 playerEndPosition;
	private float speed = 8f;
	private PlayerController pController;
	public GameObject[] lockDoor;



	void Start(){

		sm = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraSlideA> ();
		if (pController != null)
			return;
		pController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
	}

	void Update(){



		if (!pController.hasControl3) {

			ForcePlayerDirection ();
		}



	}

	void ForcePlayerDirection(){

		playerPosition.position = Vector3.MoveTowards (new Vector3(playerPosition.position.x, 
			playerPosition.position.y,transform.position.z), playerEndPosition, speed * Time.deltaTime );

	}

	void ResetPlayerController(){

		StartCoroutine (ResetController ());

	}

	IEnumerator ResetController(){

		yield return new WaitForSeconds (1.5f);
		pController.hasControl3 = true;

		for (int i = 0; i < lockDoor.Length; i++) {

			lockDoor [i].SetActive (true);
			
		}



	}


	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.CompareTag ("Player") && !sm.hasSlideRight) {

			other.gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			sm.hasSlideRight = true;
			pController.hasControl3 = false;
			ResetPlayerController ();
			sm.ResetRightPosition ();

		} 




	}
}
