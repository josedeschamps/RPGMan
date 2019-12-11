using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideControllerB : MonoBehaviour {


	private SildeCamera sm;
	public Transform playerPosition;
	public Vector3 playerEndPosition;
	private float speed = 8f;
	private PlayerController pController;
	public GameObject leftBox;

	void Start(){

		sm = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<SildeCamera> ();
		if (pController != null)
			return;
		pController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
	}

	void Update(){




			if (!pController.hasControl) {

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
	pController.hasControl = true;
	leftBox.SetActive (true);


	}


	void OnTriggerEnter2D(Collider2D other){


	if (other.gameObject.CompareTag ("Player") && !sm.hasSlideRight) {
		
		other.gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		sm.hasSlideLeft = true;
		sm.hasSlideRight = false;
		pController.hasControl = false;
		leftBox.SetActive (false);
		ResetPlayerController ();
		sm.ResetLeftPosition ();
	}
}

}
