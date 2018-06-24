using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {

	private static GameSession session = null;
	private static bool isSessionActive;



	//Simon and Player's turn
	
	//Simon and Player lists
	
	//Player points

	//Player chances

	public GameSession(){
		session = new GameSession();
	}

	public bool isGameStarted() {
		if(session != null)
			return true;
		return false;
	}

	private void Update() {
		
	}
}
