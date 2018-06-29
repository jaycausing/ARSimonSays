using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class PlayerTurn : Turn
{

	//TODO: on creation of new turn, reference list of available choices
	//private Round round;
    private Turn currentTurn;
	private List<GameObject> currentChoices;
	//int currentRound;
	bool turnActive;

    void OnEnable()
    {
        //round = gameObject.GetComponentInParent<Round>();
		currentChoices = new List<GameObject>();
		//currentRound = GameSession.RoundNum;
		//turnActive = false;
		currentTurn = this;
    }

    // FIXME: App crashes when Player's turn starts
    void Start(){
        StartCoroutine(StartTurn());
		//StartTurn();
	}

    public override void EndTurn()
    {
        gameObject.SendMessageUpwards("EndPlayerTurn", currentChoices);
    }

    private IEnumerator StartTurn(){
		yield return StartCoroutine(SelectChoices());
		yield return StartCoroutine(PrintChoicesInLog(currentChoices));
		EndTurn();
	}

    IEnumerator SelectChoices(){
        yield return new WaitUntil(() => currentChoices.Count == Round.SimonChoiceHistory.Count);
    }

	private IEnumerator PrintChoicesInLog(List<GameObject> choices){
		Debug.Log("Player has chosen...");
		foreach(GameObject choice in choices){
			Debug.Log(choice.name);
			yield return new WaitForSeconds(2);
		}
	}

    public void RestartTurn()
    {
        currentChoices.Clear();
		StartCoroutine(StartTurn());
    }


    public override List<GameObject> GetCurrentChoices()
    {
		return currentChoices;
        //throw new System.NotImplementedException();
    }

}
