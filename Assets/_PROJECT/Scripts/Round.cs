using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class Round : MonoBehaviour {
 
	//Simon and Player's turn objs
	//GameObject playerTurnObj;
	//GameObject simonTurnObj;
	
	//Simon and Player's turn
	PlayerTurn playerTurn;
	SimonTurn simonTurn;

	public static List<GameObject> SimonChoiceHistory;
	private static Round currentRound;
	//private int RoundNum;
	
	void OnEnable(){
		//RoundNum = GameSession.RoundNum;
		currentRound = this;
	}

	void OnDisable(){
		Destroy(currentRound);
	}

	void Start () {
		StartSimonTurn();
	}
	
	void Update () {
		
	}

	void StartSimonTurn(){
		// TODO: deactivate choices from player input
		// TODO: UI indicating Simon's turn

		simonTurn = gameObject.AddComponent<SimonTurn>() as SimonTurn;

		//simonTurnObj = Instantiate((new GameObject("Simon's Turn")), this.transform);
		//simonTurn = simonTurnObj.AddComponent<SimonTurn>() as SimonTurn;
		//simonTurn = Instantiate(new SimonTurn(), transform);
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

		//playerTurnObj = Instantiate((new GameObject("Player's Turn")), this.transform);
		playerTurn = gameObject.AddComponent<PlayerTurn>() as PlayerTurn;
		//playerTurn = Instantiate(new PlayerTurn(), transform);
	}
	public void EndPlayerTurn(List<GameObject> playerChoices){
		Debug.Log("Player's turn end");
		IsPlayerCorrect(playerChoices);
	}

	private IEnumerator RestartRound(){
		Debug.Log("Restarting round " + GameSession.RoundNum);
		//Debug.Log("Restarting round " + RoundNum);
		yield return StartCoroutine
		(simonTurn.RestartTurn());
		yield return new WaitForSeconds(5);
		Debug.Log("Player's turn starting again");
		playerTurn.RestartTurn();
	}

	private IEnumerator IsPlayerCorrect(List<GameObject> playerChoices){
		if(playerChoices.Equals(SimonChoiceHistory)){
			Debug.Log("Correct! On to the next round!");
			yield return new WaitForSeconds(5.0f);
			gameObject.SendMessageUpwards("EndRound", SimonChoiceHistory);
		} else {
			//testing game ending first
			gameObject.SendMessageUpwards("GameOver");
			//RestartRound();
		}
	}
}
