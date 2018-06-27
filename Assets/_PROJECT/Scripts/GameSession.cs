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
	
	//TODO: remember to replace prototype
	// list with actual list
	//private List<Choice> availableChoices;
	public GameObject[] availableChoices;
	private Color[] choiceColors = new Color[] { 
	Color.red, Color.yellow, Color.green, 
	Color.blue, Color.magenta };
	private Vector3[] spawnPositions = new Vector3[]{
		new Vector3(0,0.35f,0.35f),
		new Vector3(0.25f,0.25f,0.25f),
		new Vector3(0.1f,0.075f,0.075f),
		new Vector3(-0.1f,0.075f,0.075f),
		new Vector3(-0.25f,0.25f,0.25f)
	};

	private GameObject player;
	
	//Simon and Player's turn
	//Turn playerTurn;
	//Turn simonTurn;

	//Simon and Player lists
	

	//Player points = round completed
	protected int points;
	//Player chances

	void Awake(){
		roundNum = 0;
		playArea = GameManager.activePlane;
		playAreaAnchor = Session.CreateAnchor(playArea.CenterPose, playArea);
		player = GameObject.FindWithTag("Player");

		session = this;
	}

	void Start(){
		//float distance = 0f;
		for(int i = 0; i < availableChoices.Length; i++){
			/*Vector3 pos = playAreaAnchor.transform.position;
			pos.z = 1.0f;
			pos.y = distance;*/
			Instantiate(availableChoices[i], spawnPositions[i], Quaternion.identity, playAreaAnchor.transform);
			Renderer r = availableChoices[i].GetComponentInChildren<Renderer>(true);
			r.material.color = choiceColors[i];
			// TODO: need to make custom materials for all choices
		}

		StartRound();
	}


	// used to create Round instances
	public void StartRound() {
		//FIXME: orb spawns at player position
		/*GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		orb.transform.localScale -= new Vector3(0.75f,0.75f,0.75f);
		orb.transform.LookAt(player.transform);
		orb.transform.parent = playAreaAnchor.transform;
		orb.transform.Translate(0.0f, 0.0f, 0.75f);
		Debug.Log("Orb spawned at: " + orb.transform.position);
		Debug.Log("Player position at time of spawn: " + GameObject.FindWithTag("Player").transform.position);*/
	}
	
	// used to end Round instances before starting a new one
	public void NextRound() {

	}

	public void RestartRound() {
		
	}

}
