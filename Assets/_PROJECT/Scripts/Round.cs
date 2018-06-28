using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class Round : MonoBehaviour {
 
	//Simon and Player's turn
	PlayerTurn playerTurn;
	SimonTurn simonTurn;

	public static List<GameObject> SimonChoiceHistory;
	private static Round currentRound;
	private int RoundNum;
	
	void Awake(){
		RoundNum = GameSession.RoundNum;
		currentRound = this;
	}
	void Start () {
		StartSimonTurn();
	}
	
	void Update () {
		
	}

	void StartSimonTurn(){
		// TODO: deactivate choices from player input
		simonTurn = Instantiate(new SimonTurn(), transform);
	}
	void EndSimonTurn(List<GameObject> simonsChoices){
		SimonChoiceHistory = simonsChoices;
		
		// TODO: activate choices from player input
		StartPlayerTurn();
	}
	void StartPlayerTurn(){
		playerTurn = Instantiate(new PlayerTurn(), transform);
	}
	public void EndPlayerTurn(List<GameObject> playerChoices){
		IsPlayerCorrect(playerChoices);
	}

	private void RestartRound(){
		simonTurn.RestartTurn();
	}

	private void IsPlayerCorrect(List<GameObject> playerChoices){
		if(playerChoices.Equals(SimonChoiceHistory))
			gameObject.SendMessageUpwards("EndRound", SimonChoiceHistory);
		else
			RestartRound();
	}
}
