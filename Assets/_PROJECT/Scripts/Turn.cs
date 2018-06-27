using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public abstract class Turn : MonoBehaviour {
	
	public List<Choice> choiceOptions;
	Turn currentTurn;
	List<Choice> currentChoices;
	int currentRound;

	public Turn(){}
	
	public abstract void StartTurn(List<Choice> choiceHistory, int round);
	public abstract void EndTurn();
	public abstract void RestartTurn();

	public abstract List<Choice> SelectChoices();
	public abstract void PlaybackChoices();

	// TODO:
	// if player loses, this.BroadcastMessage("GameEnd");

}

public class SimonTurn : Turn
{

	//TODO: on creation of new turn, reference list of available choices
	public List<Choice> choiceOptions;
	Turn currentTurn;
	List<Choice> currentChoices;
	List<Choice> pastChoices;
	int currentRound;
    public SimonTurn(List<Choice> choiceHistory, int round)
    {
		choiceOptions = 
		currentChoices = new List<Choice>();
		pastChoices = choiceHistory;
		currentRound = round;
    }

    public override void EndTurn()
    {
		PlaybackChoices();
		Destroy(currentTurn);
        throw new System.NotImplementedException();
    }

    public override void PlaybackChoices()
    {
        throw new System.NotImplementedException();
    }

    public override void RestartTurn()
    {
        throw new System.NotImplementedException();
    }

    public override List<Choice> SelectChoices()
    {
        if(currentRound == 1){
			//TODO: Select three Choice objects at random
		}
		return currentChoices;
    }

    public override void StartTurn(List<Choice> choiceHistory, int round)
    {
		currentTurn = new SimonTurn(choiceHistory, round);
    }
}
