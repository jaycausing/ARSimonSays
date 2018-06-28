using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class PlayerTurn : Turn
{

	//TODO: on creation of new turn, reference list of available choices
	private Round round;
    private Turn currentTurn;
	private List<GameObject> currentChoices;
	int currentRound;
	bool turnActive;
    void Awake()
    {
        round = gameObject.GetComponentInParent<Round>();
		currentChoices = new List<GameObject>();
		currentRound = GameSession.RoundNum;
		turnActive = false;
		currentTurn = this;
    }

    public override void EndTurn()
    {
        gameObject.SendMessageUpwards("EndPlayerTurn", currentChoices);
    }

    void Update(){
        while(turnActive){
            if(currentChoices.Count == Round.SimonChoiceHistory.Count){
                turnActive = false;
                PlaybackChoices(currentChoices);
            }
        }
        EndTurn();
    }

    public override void PlaybackChoices(List<GameObject> choices)
    {
        //TODO: playback animation
        Debug.Log("You have chosen...");
		StartCoroutine(PrintChoicesInLog(choices));
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
		currentChoices.Clear();
        StartTurn();
        throw new System.NotImplementedException();
    }

    /*public override List<GameObject> SelectChoices()
    {
        // do i even need this?
    }*/

    public override void StartTurn()
    {
		turnActive = true;
        //SelectChoices();
    }

    public override List<GameObject> GetCurrentChoices()
    {
		return currentChoices;
        throw new System.NotImplementedException();
    }

    public override List<GameObject> SelectChoices()
    {
        Debug.Log("App tried calling the wrong SelectChoices()");
        throw new System.NotImplementedException();
    }
}
