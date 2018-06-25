using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {

	// current session reference
	private static GameSession session;
	private static int roundNum;
	private Round round;
	private List<Choice> availableChoices;

	//Simon and Player's turn
	
	//Simon and Player lists
	
	//Player points

	//Player chances

	public GameSession(){
		roundNum = 0;
	}

	public bool isGameStarted() {
		if(session != null)
			return true;
		return false;
	}

	// used to create Round instances
	public void StartRound() {
		
	}
	
	// used to end Round instances before starting a new one
	public void NextRound() {

	}

	public void RestartRound() {
		
	}

	public void StartGame() {
		session = new GameSession();
	}
}
