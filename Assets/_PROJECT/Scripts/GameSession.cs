﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class GameSession : MonoBehaviour {

	// current session reference
	private static GameSession session;
	private DetectedPlane playArea;
	private bool gameStatus;
	private Anchor playAreaAnchor;
	public static int RoundNum;
	private Round round;
	
	//TODO: remember to replace prototype
	// list with actual list
	//private List<Choice> availableChoices;
	public GameObject[] availableChoices;
	public static GameObject[] availableChoicesSpawned = new GameObject[5];
	private Vector3[] spawnPositions = new Vector3[]{
		new Vector3(0,0.35f,0.35f),
		new Vector3(0.25f,0.25f,0.25f),
		new Vector3(0.1f,0.075f,0.075f),
		new Vector3(-0.1f,0.075f,0.075f),
		new Vector3(-0.25f,0.25f,0.25f)
	};

	private GameObject player;

	//Simon's list
	public static List<GameObject> SimonChoiceHistory;

	//Player chances
	// TODO: Player chances

	//Game Round object
	private GameObject roundObj;

	void Awake(){
		player = GameObject.FindWithTag("Player");

		this.enabled = false;
	}
	
	void OnEnable(){
		if(playArea == null){
			playArea = GameManager.activePlane;
			playAreaAnchor = Session.CreateAnchor(playArea.CenterPose, playArea);
		}
		RoundNum = 0;
		SimonChoiceHistory = new List<GameObject>();
		for(int i = 0; i < availableChoices.Length; i++){
			Instantiate(availableChoices[i], spawnPositions[i], Quaternion.identity, playAreaAnchor.transform);
			availableChoicesSpawned[i] = availableChoices[i];
		}
	}

	void Start(){
		Debug.Log("Game Session Start");

		session = this;
		roundObj = Instantiate((new GameObject("Round")), this.transform);
		StartRound();
	}


	// used to create Round instances
	public void StartRound() {
		//round = gameObject.AddComponent<Round>() as Round;
		
		round = roundObj.AddComponent<Round>() as Round;
		//Instantiate(new Round(), transform);
		Debug.Log("Round " + RoundNum + " start");
	}
	
	// used to end Round instances before starting a new one
	public void EndRound(List<GameObject> simonChoices){
		// TODO: UI or anim event showing player is correct
		// and moving on to the next round
		SimonChoiceHistory = simonChoices;
		Destroy(round);
		//Destroy(roundObj);
		RoundNum++;
		StartRound();
	}

}
