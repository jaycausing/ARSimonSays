using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public abstract class Turn : MonoBehaviour {
	
	private Turn currentTurn;
	private Round round;
	private List<GameObject> currentChoices;
	int currentRound;
	bool turnActive;

	public abstract void EndTurn();
	public abstract void StartTurn();
	public abstract void RestartTurn();

	public abstract List<GameObject> SelectChoices();
	public abstract void PlaybackChoices(List<GameObject> choices);

	public abstract List<GameObject> GetCurrentChoices();
	// TODO:
	// if player loses, this.BroadcastMessage("GameEnd");

}
