using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;

public class PlayerTurn : Turn
{

	//TODO: on creation of new turn, reference list of available choices
	//private Round round;
    private Turn currentTurn;
	private List<GameObject> currentChoices;
	//int currentRound;
	bool turnActive;

    Text EntityChoices;
	Text ColorChoiceText;
    Text PlayerChoiceText;

    void OnEnable() {
        EntityChoices = GameObject.Find("EntityChoices").GetComponent<Text>();
		ColorChoiceText = GameObject.Find("EntityChoices").GetComponent<Text>();
		PlayerChoiceText = GameObject.Find("PlayerChoiceText").GetComponent<Text>();
        currentChoices = new List<GameObject>();
		currentTurn = this;
    }

    void OnDisable(){
        EntityChoices.text = "";
		ColorChoiceText.text = "";
        PlayerChoiceText.text = "";
    }

    void Start(){
        StartCoroutine(StartTurn());
	}

    public override void EndTurn() {
        EntityChoices.text = "";
		ColorChoiceText.text = "";
        PlayerChoiceText.text = "";
        gameObject.SendMessageUpwards("EndPlayerTurn", currentChoices);
    }

    private IEnumerator StartTurn(){
		yield return StartCoroutine(SelectChoices());
		yield return StartCoroutine(PrintChoicesInLog(currentChoices));
		EndTurn();
	}

    IEnumerator SelectChoices(){
        PlayerChoiceText.text = "Tap on a cube to select it";
        //TODO: Touch object to select it
        yield return new WaitUntil(() => currentChoices.Count == Round.SimonChoiceHistory.Count);
    }

	private IEnumerator PrintChoicesInLog(List<GameObject> choices){
		EntityChoices.text = "You have chosen...";
        yield return new WaitForSeconds(3);
		foreach(GameObject choice in choices){
			ColorChoiceText.text = choice.name;
			yield return new WaitForSeconds(3);
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
