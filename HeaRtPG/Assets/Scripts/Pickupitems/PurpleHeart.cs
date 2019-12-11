using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleHeart : MonoBehaviour {

	private PlayerController player;

	void Start(){

		player = FindObjectOfType<PlayerController> ();


	}


	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.CompareTag ("Player")) {

			player.PurpleHeart.SetActive (true);
			player.hasPurpleHeart = true;
			Destroy (this.gameObject);
		}

	}
}
