using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class GameSession : MonoBehaviour {

	// current session reference
	private static GameSession session;
	private DetectedPlane playArea;
	private Pose hitSpawn;
	private bool gameStatus;
	private Anchor playAreaAnchor;
	private static int roundNum;
	private Round round;
	private List<Trackable> availableChoices = new List<Trackable>();

	//Simon and Player's turn
	
	//Simon and Player lists
	
	//Player points

	//Player chances

	void Awake(){
		roundNum = 0;
		playArea = GameManager.activePlane;
		playAreaAnchor = GameManager.spawnHit.Trackable.CreateAnchor(GameManager.spawnHit.Pose);
		session = this;
	}

	void Start(){
		StartRound();
	}


	// used to create Round instances
	public void StartRound() {
		//Anchor anchor = playArea.CreateAnchor(playArea.CenterPose);
		GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		orb.transform.localScale -= new Vector3(0.75f, 0.75f, 0.75f);
		orb.transform.position = GameManager.spawnHit.Pose.position;
		orb.transform.SetParent(playAreaAnchor.transform, false);
		//TODO: spawn five Choice objects within a circle
		//playAreaAnchor.transform.localPosition = new Vector3(50.0f, 5.0f, 5.0f);
		Debug.Log("Orb spawned at: " + orb.transform.position);
		Debug.Log("Anchor position at: " + playAreaAnchor.transform.position);
		Debug.Log("Player position at time of spawn: " + GameObject.FindWithTag("Player").transform.position);
		Debug.Log("Orb tracking state: " + playAreaAnchor.TrackingState);
	}
	
	// used to end Round instances before starting a new one
	public void NextRound() {

	}

	public void RestartRound() {
		
	}

}
