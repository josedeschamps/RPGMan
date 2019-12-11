using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

	public string sceneName;
	public Animator fader;





	public void LoadLevel(){

		fader.SetBool ("SetFader", true);
		StartCoroutine (FadeScene ());
	}

	IEnumerator FadeScene(){

		yield return new WaitForSeconds (.5f);
		SceneManager.LoadScene (sceneName);

	}
}
