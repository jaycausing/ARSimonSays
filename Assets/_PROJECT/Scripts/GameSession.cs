using System.Collections;
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

	void Awake(){
		RoundNum = 0;

		playArea = GameManager.activePlane;
		playAreaAnchor = Session.CreateAnchor(playArea.CenterPose, playArea);
		
		player = GameObject.FindWithTag("Player");
		
		SimonChoiceHistory = new List<GameObject>();

		session = this;
	}

	void Start(){
		//float distance = 0f;
		for(int i = 0; i < availableChoices.Length; i++){
			Instantiate(availableChoices[i], spawnPositions[i], Quaternion.identity, playAreaAnchor.transform);
			availableChoicesSpawned[i] = availableChoices[i];
		}

		StartRound();
	}


	// used to create Round instances
	public void StartRound() {
		round = Instantiate(new Round(), transform);
	}
	
	// used to end Round instances before starting a new one
	public void EndRound(List<GameObject> simonChoices){
		// TODO: UI or anim event showing player is correct
		// and moving on to the next round
		SimonChoiceHistory = simonChoices;
		Destroy(round);
		RoundNum++;
		StartRound();
	}

}
