using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class SimonTurn : Turn
{

	//TODO: on creation of new turn, reference list of available choices
	private Turn currentTurn;
	private Round round;
	private List<GameObject> currentChoices;
	List<GameObject> pastChoices;
	int currentRound;
	//bool turnActive;
	
    void Awake()
    {
		pastChoices = GameSession.SimonChoiceHistory;
		currentChoices = pastChoices;
		currentRound = GameSession.RoundNum;
		//turnActive = false;
		currentTurn = this;
    }

    public override void EndTurn()
    {
		//turnActive = false;
		gameObject.SendMessageUpwards("EndSimonTurn", currentChoices);
        throw new System.NotImplementedException();
    }

    public override void PlaybackChoices(List<GameObject> choices)
    {
        //TODO: playback animation

		Debug.Log("Simon has chosen...");
		StartCoroutine(PrintChoicesInLog(choices));
		
		EndTurn();
		//turnActive = false;
    }

	// DELETE ME WHEN YOU CREATE PLAYBACK ANIMS!!!
	private IEnumerator PrintChoicesInLog(List<GameObject> choices){
		foreach(GameObject choice in choices){
			Debug.Log(choice.name);
			yield return new WaitForSeconds(2);
		}
	}

    public override void RestartTurn()
    {
		//turnActive = true;
		PlaybackChoices(currentChoices);
        throw new System.NotImplementedException();
    }

    public override List<GameObject> SelectChoices()
    {
        if(currentRound == 0){
			//TODO: Select three Choice objects at random
			for(int i = 0; i < 3; i++){
				currentChoices.Add(GameSession.availableChoicesSpawned[RandomChoice()]);
			}
			return currentChoices;
		}
		currentChoices.Add(GameSession.availableChoicesSpawned[RandomChoice()]);
		return currentChoices;
    }

	private int RandomChoice(){
		int choice = Mathf.RoundToInt(Random.Range(0.0f, 4.0f));
		return choice;
	}

    public override void StartTurn(List<GameObject> choiceHistory)
    {
		//turnActive = true;
		PlaybackChoices(SelectChoices());
    }

    public override void StartTurn()
    {
		Debug.Log("App tried calling wrong StartTurn()");
        throw new System.NotImplementedException();
    }

    public override List<GameObject> GetCurrentChoices()
    {
		Debug.Log("App tried calling the wrong SelectChoices()");
        throw new System.NotImplementedException();
    }

}
