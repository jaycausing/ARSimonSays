using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class Round : MonoBehaviour {
 
	//Simon and Player's turn
	//Turn playerTurn;
	//Turn simonTurn;

	private static Round currentRound;
	private int roundNum;
	
	void Awake(){
		roundNum = GameSession.roundNum;
		currentRound = this;
	}
	void Start () {
		
	}
	
	void Update () {
		
	}

	void StartSimonTurn(){

	}
	void EndSimonTurn(){

	}
	void StartPlayerTurn(){
		
	}
	void EndPlayerTurn(){

	}
}
