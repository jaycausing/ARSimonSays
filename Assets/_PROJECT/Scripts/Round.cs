using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;

public class Round : MonoBehaviour {
 
	// UI Objects
	string defautTurnText;
	
	//Simon and Player's turn
	PlayerTurn playerTurn;
	SimonTurn simonTurn;

	public static List<GameObject> SimonChoiceHistory;
	private static Round currentRound;
	
	void OnEnable(){
		currentRound = this;
	}

	void OnDisable(){
		if(GameObject.Find("CurrentTurnText") != null)
			GameObject.Find("CurrentTurnText").GetComponent<Text>().text = defautTurnText;
		Destroy(currentRound);
	}

	void Start () {
		defautTurnText = GameObject.Find("CurrentTurnText").GetComponent<Text>().text;
		StartSimonTurn();
	}
	
	void Update () {
		
	}

	void StartSimonTurn(){
		GameObject.Find("CurrentTurnText").GetComponent<Text>().text =
		defautTurnText + "Simon";
		
		simonTurn = gameObject.AddComponent<SimonTurn>() as SimonTurn;

		GameObject.Find("CurrentTurnText").GetComponent<Text>().text = defautTurnText + "Simon";
	}
	void EndSimonTurn(List<GameObject> simonsChoices){
		SimonChoiceHistory = simonsChoices;
		StartPlayerTurn();
	}
	void StartPlayerTurn(){
		GameObject.Find("CurrentTurnText").GetComponent<Text>().text =
		defautTurnText + "You";
		playerTurn = gameObject.AddComponent<PlayerTurn>() as PlayerTurn;
		GameObject.Find("CurrentTurnText").GetComponent<Text>().text = defautTurnText + "You";
	}
	public void EndPlayerTurn(List<GameObject> playerChoices){
		StartCoroutine(IsPlayerCorrect(playerChoices));
	}

	private IEnumerator RestartRound(){
		Debug.Log("Restarting round " + GameSession.RoundNum);
		yield return StartCoroutine	(simonTurn.RestartTurn());
		yield return new WaitForSeconds(5);
		Debug.Log("Player's turn starting again");
		playerTurn.RestartTurn();
	}

	private IEnumerator IsPlayerCorrect(List<GameObject> playerChoices){
		bool correct = true;
		for(int i = 0; i < playerChoices.Count; i++){
			if(!playerChoices[i].Equals(SimonChoiceHistory[i]))
				correct = false;
		}

		if(correct){
			Text EntityChoices = GameObject.Find("EntityChoices").GetComponent<Text>();
			EntityChoices.text = "Correct! On to the next round!";
			yield return new WaitForSeconds(5.0f);
			EntityChoices.text = "";
			gameObject.SendMessageUpwards("EndRound", SimonChoiceHistory);
		} else {
			gameObject.SendMessageUpwards("GameOver");
		}
	}

	public void CancelRound(){
		StartCoroutine(CancelRoundCoroutine());
	}

	public IEnumerator CancelRoundCoroutine(){
		if(playerTurn){
			playerTurn.StopAllCoroutines();
			playerTurn.CancelInvoke();
			Debug.Log("Player turn functions are cancelled");
			yield return new WaitForSeconds(1);
			Destroy(playerTurn);
		}
		simonTurn.StopAllCoroutines();
		simonTurn.CancelInvoke();
		Debug.Log("Simon turn functions are cancelled");
		Destroy(simonTurn);
		yield return new WaitForSeconds(1);
		CancelInvoke();
		StopAllCoroutines();
		yield return new WaitForSeconds(1);
	}
}
