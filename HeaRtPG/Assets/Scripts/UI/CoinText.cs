using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CoinText : MonoBehaviour {


	private Text coinText;

	public int coinScore = 0;

	void Start () {

		coinText = GetComponent<Text> ();

		coinText.text = "SMACK COINS : 0";
		
	}
	

	void Update () {

		coinText.text = "SMACK COINS :  " + coinScore ;
		
	}
}
