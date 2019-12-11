using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinController : MonoBehaviour {



	private Transform targetPosition;
	private bool isReachable = false;
	public float movementSpeed;
	private CoinText coinScore;


	void Start(){

		coinScore = GameObject.FindGameObjectWithTag ("CoinScore").GetComponent<CoinText> ();

		if (targetPosition != null)
			return;
		targetPosition = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();

	}

	void Update(){


		if (isReachable) {

			MoveTowardPlayer ();
		}


	}



	public void MoveTowardPlayer(){

		transform.position = Vector2.MoveTowards(transform.position, targetPosition.position, movementSpeed * Time.deltaTime);

	}


	void OnTriggerEnter2D(Collider2D other){



		if (other.gameObject.CompareTag ("Player")) {

			isReachable = true;
		}
	}

	void OnCollisionEnter2D(Collision2D other){

		if (other.gameObject.CompareTag ("Player")) {

			coinScore.coinScore++;
			Destroy (gameObject);
		}
	}

}
