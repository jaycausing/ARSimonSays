using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class SimonTurn : Turn
{

	//TODO: on creation of new turn, reference list of available choices
	private Turn currentTurn;
	private List<GameObject> currentChoices;
	List<GameObject> pastChoices;
	int currentRound;
	bool turnActive;
    void Awake()
    {
		currentChoices = new List<GameObject>();
		pastChoices = GameSession.SimonChoiceHistory;
		currentRound = GameSession.roundNum;
		turnActive = false;
		currentTurn = this;
    }

    /*public override void EndTurn()
    {
		Destroy(currentTurn);
        throw new System.NotImplementedException();
    }*/

    public override void PlaybackChoices(List<GameObject> choices)
    {
        //TODO: playback animation

		turnActive = false;
    }

    public override void RestartTurn()
    {
		turnActive = true;
		PlaybackChoices(currentChoices);
        throw new System.NotImplementedException();
    }

    public override List<GameObject> SelectChoices()
    {
        if(currentRound == 1){
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
		turnActive = true;
		PlaybackChoices(SelectChoices());
    }

    public override List<GameObject> GetCurrentChoices()
    {
		return currentChoices;
        throw new System.NotImplementedException();
    }
}
