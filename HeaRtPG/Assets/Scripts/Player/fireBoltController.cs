using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBoltController : MonoBehaviour {

	public float speed;
	private Rigidbody2D RB2D;
	//public GameObject effect;

	void Start () {

		RB2D = GetComponent<Rigidbody2D> ();
		Destroy (gameObject, 4f);

	}


	void Update () {

		RB2D.velocity = (new Vector2(speed, 0));


	}



	void OnCollisionEnter2D(Collision2D other){


		if(other.collider.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Box"))  {

		//	Instantiate (effect, transform.position, Quaternion.identity);
			Destroy (gameObject);

		}

	}
}
