using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideControllerD : MonoBehaviour {

	private SildeCamera sm;
	public Transform playerPosition;
	public Vector3 playerEndPosition;
	private float speed = 5f;
	private PlayerController pController;
	public GameObject TopBox;



	void Start(){

		sm = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<SildeCamera> ();
		if (pController != null)
			return;
		pController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
	}

	void Update(){


			if (!pController.hasControl2) {

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
		pController.hasControl2 = true;
		TopBox.SetActive (true);


	}


	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.CompareTag ("Player") && !sm.hasSlideBottom) {

			other.gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			sm.hasSlideBottom = true;
			sm.hasSlideTop = false;
			pController.hasControl2 = false;
			TopBox.SetActive (false);
			ResetPlayerController ();
			sm.ResetBottomCamera ();

		} 




	}
}

