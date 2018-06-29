using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class SimonTurn : Turn
{

	//TODO: on creation of new turn, reference list of available choices
	private Turn currentTurn;
	//private Round round;
	private List<GameObject> currentChoices;
	//List<GameObject> pastChoices;
	//int currentRound;
	//bool turnActive;
	
    void OnEnable()
    {
		//pastChoices = GameSession.SimonChoiceHistory;
		currentChoices = GameSession.SimonChoiceHistory;;
		//currentRound = GameSession.RoundNum;
		currentTurn = this;
    }

	void Start(){
		StartCoroutine(StartTurn());
	}

	private IEnumerator StartTurn(){
		yield return StartCoroutine(SelectChoices());
		yield return StartCoroutine(PrintChoicesInLog(currentChoices));
		EndTurn();
	}

    public override void EndTurn()
    {
		//turnActive = false;
		gameObject.SendMessageUpwards("EndSimonTurn", currentChoices);
        //throw new System.NotImplementedException();
    }


	// DELETE ME WHEN YOU CREATE PLAYBACK ANIMS!!!
	private IEnumerator PrintChoicesInLog(List<GameObject> choices){
		Debug.Log("Simon has chosen...");
		yield return new WaitForSeconds(3);
		foreach(GameObject choice in choices){
			Debug.Log(choice.name);
			yield return new WaitForSeconds(2);
		}
		yield return new WaitForSeconds(3);
	}

    public IEnumerator RestartTurn()
    {
		yield return StartCoroutine(PrintChoicesInLog(currentChoices));
		EndTurn();
    }

	// DELETE ME WHEN FIGURED OUT CAUSE OF CRASH
	IEnumerator SelectChoices(){
		Debug.Log("Choosing objects...");
        if(GameSession.RoundNum == 0){
			currentChoices.Add(GameSession.availableChoicesSpawned[RandomChoice()]);
			yield return new WaitForSeconds(3);
			currentChoices.Add(GameSession.availableChoicesSpawned[RandomChoice()]);
			yield return new WaitForSeconds(3);
			currentChoices.Add(GameSession.availableChoicesSpawned[RandomChoice()]);
			yield return new WaitForSeconds(3);
		} else {
			currentChoices.Add(GameSession.availableChoicesSpawned[RandomChoice()]);
			yield return new WaitForSeconds(3);
		}
	}

	private int RandomChoice(){
		int choice = Mathf.RoundToInt(Random.Range(0.0f, 4.0f));
		return choice;
	}

    public override List<GameObject> GetCurrentChoices()
    {
		Debug.Log("App tried calling the wrong SelectChoices()");
        throw new System.NotImplementedException();
    }

}
