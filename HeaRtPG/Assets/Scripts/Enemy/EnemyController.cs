using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour {

	Enemy.BaseStats stats = new Enemy.BaseStats ();
	public float moveSpeed = 2f;
	public float timeBetweenMove = 2f;
	private float timeBetweenMoveCounter;
	public float timeToMove = 1f;
	private float timeToMoveCounter;
	public GameObject itemPrefab;
	private Vector3 moveDirection;
	private Rigidbody2D RB2D;
	private Animator enemyAnim;
	private SpriteRenderer SR;
	private bool moving;


	void Start(){
		
		SR = GetComponent<SpriteRenderer> ();
		enemyAnim = GetComponent<Animator> ();
		RB2D = GetComponent<Rigidbody2D> ();
		timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
		timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);

	}


	void Update(){

		if (stats.SpiderHealth <=0) {
			SpawnItem ();
			Destroy (gameObject);
		}


		EnemyMovement ();
	}



	void EnemyMovement(){

		if (moving) {

			timeToMoveCounter -= Time.deltaTime;
			RB2D.velocity = moveDirection;
			enemyAnim.SetBool ("SetAnim", true);

			if (timeToMoveCounter < 0f) {

				moving = false;
				timeBetweenMoveCounter = timeBetweenMove;
				enemyAnim.SetBool ("SetAnim", false);
			}
		} 

		else {

			timeBetweenMoveCounter -= Time.deltaTime;
			RB2D.velocity = Vector2.zero;
			if (timeBetweenMoveCounter < 0f) {
				moving = true;
				timeToMoveCounter = timeToMove;
				moveDirection = new Vector3 (Random.Range (-1f, 1f)* moveSpeed, Random.Range (-1f, 1f) * moveSpeed, 0f);


			}

		}
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



	void SpawnItem(){

		Instantiate (itemPrefab, transform.position, Quaternion.identity);
	}


	void OnCollisionEnter2D(Collision2D other){

		if (other.gameObject.CompareTag ("Sword") || other.gameObject.CompareTag("FireBolt")) {

			stats.SpiderHealth--;
			DamageFromPlayer ();
		}

	}


}
