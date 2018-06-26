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
	private static int roundNum;
	private Round round;
	private List<Choice> availableChoices;

	//Simon and Player's turn
	
	//Simon and Player lists
	
	//Player points

	//Player chances

	void Awake(){
		roundNum = 0;
		playArea = GameManager.activePlane;
		playAreaAnchor = playArea.CreateAnchor(playArea.CenterPose);
		session = this;
	}

	void Start(){
		StartRound();
	}


	// used to create Round instances
	public void StartRound() {
		//FIXME: orb spawns at player position
		//FIXME: also theres two???
		GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		orb.transform.position = playAreaAnchor.transform.position;
		orb.transform.Translate(0.25f, 0.0f, 1.5f);
		Debug.Log("Orb spawned at: " + orb.transform.position);
		Debug.Log("Player position at time of spawn: " + GameObject.FindWithTag("Player").transform.position);
	}
	
	// used to end Round instances before starting a new one
	public void NextRound() {

	}

	public void RestartRound() {
		
	}

}
