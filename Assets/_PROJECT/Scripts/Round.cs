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
		// TODO: UI indicating Simon's turn
		simonTurn = Instantiate(new SimonTurn(), transform);
		Debug.Log("Simon's turn start");
	}
	void EndSimonTurn(List<GameObject> simonsChoices){
		SimonChoiceHistory = simonsChoices;
		Debug.Log("Simon's turn end");
		// TODO: activate choices from player input
		StartPlayerTurn();
	}
	void StartPlayerTurn(){
		Debug.Log("Player's turn start");
		// TODO: UI indicating player's turn
		playerTurn = Instantiate(new PlayerTurn(), transform);
	}
	public void EndPlayerTurn(List<GameObject> playerChoices){
		Debug.Log("Player's turn end");
		IsPlayerCorrect(playerChoices);
	}

	private void RestartRound(){
		Debug.Log("Restarting round " + RoundNum);
		simonTurn.RestartTurn();
	}

	private void IsPlayerCorrect(List<GameObject> playerChoices){
		if(playerChoices.Equals(SimonChoiceHistory)){
			Debug.Log("Correct! On to the next round!");
			gameObject.SendMessageUpwards("EndRound", SimonChoiceHistory);
		} else {
			//testing game ending first
			gameObject.SendMessageUpwards("GameOver");
			//RestartRound();
		}
	}
}
