using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicHeart : MonoBehaviour {



	private PlayerController player;

	void Start(){

		player = FindObjectOfType<PlayerController> ();
	

	}
	

	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.CompareTag ("Player")) {

			player.basicHeartUI.SetActive (true);
			player.hasHeart = true;
			player.holdingHeart = true;
			Destroy (this.gameObject);
		}

	}

}
