using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class ChaseEnemy : MonoBehaviour {

	///Call my namespace
	Enemy.BaseStats stats = new Enemy.BaseStats ();

	public Transform targetPosition;
	private bool chasePlayer = false;
	public GameObject itemPrefab;
	private SpriteRenderer SR;
	private Rigidbody2D RB2D;
	private Animator anim;





	void Start(){

		SR = GetComponent<SpriteRenderer> ();
		RB2D = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		targetPosition = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();

	}


	void Update () {




		if (stats.EvilSkeletonHealth <= 0) {

			SpawnItem ();
			Destroy (gameObject);
		}


		if (chasePlayer) {

			ChasePlayer ();


		}
			


	}




	void SpawnItem(){

		Instantiate (itemPrefab, transform.position, Quaternion.identity);
	}







	void ChasePlayer(){

		Vector3 moveDirection = transform.position - targetPosition.position;
		float angle = Mathf.Atan2 (moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + 250;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		Vector2 velocity = new Vector2((transform.position.x - targetPosition.position.x) * stats.Speed, (transform.position.y - targetPosition.position.y) * stats.Speed);
		RB2D.velocity = -velocity;

	}


	void DamageFromPlayer(){


		StartCoroutine (DamageFlash ());

	}


	IEnumerator DamageFlash(){


		Color32 whateverColor = new Color32(244,63,63,255);
		for(int i = 0; i < 3; i++)
		{
			SR.material.color = Color.white;
			yield return new WaitForSeconds (.1F);
			SR.material.color = whateverColor;
			yield return new WaitForSeconds (.1F);
		}
		SR.material.color = Color.white;

	}




	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.CompareTag ("Player")) {

			chasePlayer = true;
			anim.SetBool ("SetAnim", true);

		}


	}





	void OnCollisionEnter2D(Collision2D other){

		if (other.gameObject.CompareTag ("Sword") || other.gameObject.CompareTag ("FireBolt")) {

			stats.EvilSkeletonHealth--;
			DamageFromPlayer ();


		}


	}



}
