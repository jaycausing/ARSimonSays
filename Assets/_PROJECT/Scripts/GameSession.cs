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
	public static List<GameObject> availableChoicesSpawned;
	private Vector3[] spawnPositions;

	private GameObject player;

	//Simon's list
	public static List<GameObject> SimonChoiceHistory;

	//Player chances
	// TODO: Player chances

	//Game Round object
	private GameObject roundObj;

	private bool isRestarting;

	void Awake(){
		player = GameObject.FindWithTag("Player");
		this.enabled = false;
	}
	
	void OnEnable(){

		spawnPositions = new Vector3[]{
			new Vector3(0,0.35f,0.35f),
			new Vector3(0.25f,0.25f,0.25f),
			new Vector3(0.1f,0.075f,0.075f),
			new Vector3(-0.1f,0.075f,0.075f),
			new Vector3(-0.25f,0.25f,0.25f)
		};

		availableChoicesSpawned = new List<GameObject>();

		isRestarting = false;
		if(playArea == null){
			playArea = GameManager.activePlane;
		}
		playAreaAnchor = Session.CreateAnchor(playArea.CenterPose, playArea);
		RoundNum = 0;
		SimonChoiceHistory = new List<GameObject>();
		
		Debug.Log("Game Session Start");

		for(int i = 0; i < availableChoices.Length; i++){
			GameObject choiceObj = Instantiate(availableChoices[i], spawnPositions[i], Quaternion.identity, playAreaAnchor.transform);
			availableChoicesSpawned.Add(choiceObj);
		}

		session = this;
		Debug.Log("Session set");
		roundObj = Instantiate((new GameObject("Round")), this.transform);
		StartRound();

	}

	void OnDisable() {
		if(session != null)
			session = null;
	}

	public IEnumerator DisablingSession(){
		if(roundObj != null){
			
			yield return StartCoroutine(round.CancelRoundCoroutine());
			Debug.Log("Round has been cancelled");
			for(int i = 0; i < availableChoicesSpawned.Count; i++){
				Destroy(availableChoicesSpawned[i]);
			}
			availableChoicesSpawned.Clear();
			playAreaAnchor = null;
			yield return new WaitForSeconds(1);
			Destroy(roundObj);
			SimonChoiceHistory.Clear();
			Debug.Log("SimonChoiceHistory cleared");
			session = null;
			//Destroy(playAreaAnchor);
			//Destroy(playArea);
		}
	}

	void Start(){
		
	}


	// used to create Round instances
	public void StartRound() {
		//round = gameObject.AddComponent<Round>() as Round;
		


		round = roundObj.AddComponent<Round>() as Round;
		Debug.Log("Round begin");
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
