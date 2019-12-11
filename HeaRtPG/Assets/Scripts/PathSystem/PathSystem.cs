using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;


[RequireComponent(typeof(Rigidbody2D))]
public class PathSystem : MonoBehaviour {


	///Call my namespace
	Enemy.BaseStats stats = new Enemy.BaseStats ();



/// <summary>
/// Low budget pathingfinding for now.
/// </summary>
	public Transform[] pathPoints;
	//public float movementSpeed = 1f;
	private int waypoint = 0;
	public int maxWaypoint;
	Transform targetWayPoint;
	private bool followPath = true;

	public float timeToChase;
	private float timeToChaseCount;
	/// <summary>
	/// Ends here
	/// </summary>
	public GameObject purpleHeart;
	public Transform targetPosition;
	private bool chasePlayer, stillChasingPlayer = false;
	private SpriteRenderer SR;

	public GameObject[] doorLocked;





	void Start(){
		
		SR = GetComponent<SpriteRenderer> ();
		timeToChaseCount = timeToChase;

	}


	void Update () {




		if (stats.SkeletonHealth <= 0) {

			Instantiate (purpleHeart, transform.position, Quaternion.identity);
			UnlockedDoor ();
			Destroy (gameObject);
		}


		if (chasePlayer) {

			ChasePlayer ();


		}

		if (stillChasingPlayer) {


			timeToChaseCount -= Time.deltaTime;


		}

		if (timeToChaseCount < 0f) {

			timeToChaseCount = timeToChase;
			chasePlayer = false;
			followPath = true;
			stillChasingPlayer = false;

		}



		if (followPath) {
	
			Vector3 moveDirection = transform.position - pathPoints [waypoint].transform.position;
			; 

			//enemies checking to see where to move toward.
			if (waypoint < this.pathPoints.Length) {

				if (targetWayPoint == null)
					targetWayPoint = pathPoints [waypoint];
				FollowPath ();

				float angle = Mathf.Atan2 (moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + 250;
				transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);

			}


		}
	
			
	}




	void FollowPath(){
		//Move to the toward to my pathPoints.
		transform.position = Vector2.MoveTowards (transform.position, targetWayPoint.position, stats.movementSpeed * Time.deltaTime);

		if (transform.position == targetWayPoint.position) {  //check my waypoint position.

			waypoint++;
			targetWayPoint = pathPoints [waypoint];  //this make the enemy follow the path using my array.
		}


		if (waypoint == maxWaypoint) {

			waypoint = 0;
		}
	
	}


	void ChasePlayer(){

//		if (targetPosition.position == null) {
//			return;
//		}

		Vector3 moveDirection = transform.position - targetPosition.position;

		float angle = Mathf.Atan2 (moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + 250;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);


		transform.position = Vector2.MoveTowards(transform.position, targetPosition.position, stats.movementSpeed * Time.deltaTime);

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

	void UnlockedDoor(){

		for (int i = 0; i < doorLocked.Length; i++) {

			doorLocked [i].SetActive (false);
		}

	}


	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.CompareTag ("Player")) {

			chasePlayer = true;
			followPath = false;
			stillChasingPlayer = false;

		}
			

	}



	void OnTriggerExit2D(Collider2D other){

		if (other.gameObject.CompareTag ("Player")) {

			stillChasingPlayer = true;
		}

	}


	void OnCollisionEnter2D(Collision2D other){

		if(other.gameObject.CompareTag("Sword") || other.gameObject.CompareTag("FireBolt")){

			stats.SkeletonHealth--;
			DamageFromPlayer ();
			Debug.Log ("Hit");

		}


		if (other.gameObject.CompareTag ("EnemyBoarder")) {


			followPath = true;
			chasePlayer = false;
			stillChasingPlayer = false;
		}


	}



}
