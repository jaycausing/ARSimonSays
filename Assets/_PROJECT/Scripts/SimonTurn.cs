using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;

public class SimonTurn : Turn
{

	//TODO: on creation of new turn, reference list of available choices
	private Turn currentTurn;
	//private Round round;
	private List<GameObject> currentChoices;
	
	Text EntityChoices;
	Text ColorChoiceText;

    void OnEnable() {
		EntityChoices = GameObject.Find("EntityChoices").GetComponent<Text>();
		ColorChoiceText = GameObject.Find("EntityChoices").GetComponent<Text>();
		currentChoices = GameSession.SimonChoiceHistory;
		currentTurn = this;
    }

	void OnDisable() {
		EntityChoices.text = "";
		ColorChoiceText.text = "";
	}

	void Start(){
		StartCoroutine(StartTurn());
	}

	private IEnumerator StartTurn(){
		yield return StartCoroutine(SelectChoices());
		yield return StartCoroutine(PrintChoices(currentChoices));
		EndTurn();
	}

    public override void EndTurn() {
		EntityChoices.text = "";
		ColorChoiceText.text = "";
		gameObject.SendMessageUpwards("EndSimonTurn", currentChoices);
    }


	private IEnumerator PrintChoices(List<GameObject> choices){
		EntityChoices.text = "Simon's choices are...";
		yield return new WaitForSeconds(3);
		foreach(GameObject choice in choices){
			ColorChoiceText.text = choice.name;
			yield return new WaitForSeconds(2);
			ColorChoiceText.text = "";
			yield return new WaitForSeconds(0.1f);
		}
		yield return new WaitForSeconds(3);
	}

    public IEnumerator RestartTurn()
    {
		yield return StartCoroutine(PrintChoices(currentChoices));
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
        throw new System.NotImplementedException();
    }

}
