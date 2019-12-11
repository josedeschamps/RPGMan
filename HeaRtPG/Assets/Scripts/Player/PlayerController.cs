using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour {

	public int health = 1;
	public float moveSpeed;
	private Animator slashAnim;
	public GameObject attackRadius;
	public GameObject dropHeart;
	public GameObject basicHeartUI;
	public bool hasHeart = false;
	public bool holdingHeart = false;
	public bool hasPurpleHeart = false;
	public bool hasControl, hasControl2, hasControl3 = true;
	private SpriteRenderer SR;
	private Rigidbody2D RB2D;
	public Transform[] DropLocation;
	private int spawnPoint;


	public Transform fireboltLocation;
	public GameObject fireBolt;


	public GameObject PurpleHeart;
	public GameOverManager gm;

	void Start(){
		
		slashAnim = GetComponent<Animator> ();
		SR = GetComponent<SpriteRenderer> ();
		RB2D = GetComponent<Rigidbody2D> ();
		hasHeart = false;
		hasPurpleHeart = false;
		holdingHeart = false;
		hasControl = true;
		hasControl2 = true;
		hasControl3 = true;
	}




	void Update () {

		if (health < 0) {

			gm.LoadLevel ();
			Destroy (gameObject);

		}

		//keyboard and mobile movement
		//Attacks
		if (hasControl && hasControl2 && hasControl3) {
			KeyboardController ();
			KeyboardAttackController ();
			KeyboardFireBoltAttackController ();
			MobileJoyStickController ();


		}
	}



	void GameOver(){

		SceneManager.LoadScene ("GameOver");
	}
		

	void KeyboardController(){

		//The Movement for a 2D Top down gameplay
//		float moveHorizontal = Input.GetAxisRaw ("Horizontal") * moveSpeed * Time.deltaTime;
//		float moveVertical = Input.GetAxisRaw ("Vertical") * moveSpeed * Time.deltaTime;
//
//		//the movement.
//		transform.Translate  (new Vector3 (moveHorizontal, moveVertical));

		RB2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, Input.GetAxisRaw("Vertical") *moveSpeed);

	
	}

	void KeyboardAttackController(){

		if (Input.GetKeyDown (KeyCode.Space) && hasHeart) {

			Attack ();
		}
	}


	void KeyboardFireBoltAttackController(){

		if (Input.GetKeyDown (KeyCode.B) && hasPurpleHeart) {

			FireboltAttack ();

		}

	}



	void MobileJoyStickController(){

		transform.Translate (new Vector2 (CrossPlatformInputManager.GetAxis ("Horizontal"), 
			CrossPlatformInputManager.GetAxis ("Vertical")) * moveSpeed * Time.deltaTime);
	}



	public void Attack(){

		if (hasControl && hasControl2 && hasControl3) {
			attackRadius.SetActive (true);
			slashAnim.SetBool ("SetAttack", true);
			StartCoroutine (ResetAttack ());

		}
	}


	IEnumerator ResetAttack(){

		yield return new WaitForSeconds (.4f);
		slashAnim.SetBool ("SetAttack", false);
		attackRadius.SetActive (false);


	}

	public void FireboltAttack(){

		if (hasControl && hasControl2 && hasControl3) {

			Instantiate (fireBolt, fireboltLocation.transform.position, Quaternion.identity);

		}

	}

	void DropTheHeart(){

		Instantiate (dropHeart, DropLocation[spawnPoint].transform.position , Quaternion.identity);




	}


	void TakingDamage(){


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






	void OnCollisionEnter2D(Collision2D other){

		if (!hasPurpleHeart) {

			if (other.gameObject.CompareTag ("Enemy") && holdingHeart) {

				spawnPoint = Random.Range (0, 3);
				DropTheHeart ();
				basicHeartUI.SetActive (false);
				hasHeart = false;
				holdingHeart = false;
				health++;


			}
			

			if (other.gameObject.CompareTag ("Enemy") && !holdingHeart) {

				TakingDamage ();
				health--;
				hasHeart = false;
				holdingHeart = false;

			}


		}



		if(hasPurpleHeart){

			if (other.gameObject.CompareTag ("Enemy")) {

				TakingDamage ();
				hasPurpleHeart = false;
				PurpleHeart.SetActive (false);
				health--;
			
			}

		}


	}




}
