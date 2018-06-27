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
	private GameObject[] availableChoices;
	private Color[] choiceColors = new Color[] { 
	Color.red, Color.yellow, Color.green, 
	Color.blue, Color.magenta };
	private Choice[] choiceComps = new Choice[]{};

	public GameObject player;
	//Simon and Player's turn
	
	//Simon and Player lists
	
	//Player points

	//Player chances

	void Awake(){
		roundNum = 0;
		playArea = GameManager.activePlane;
		playAreaAnchor = Session.CreateAnchor(playArea.CenterPose, playArea);
		player = GameObject.FindWithTag("Player");
		availableChoices = new GameObject[5];
		for(int i = 0; i < choiceColors.Length; i++){
			string objName = "choice" + i;
			availableChoices[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			availableChoices[i].name = objName;
			//availableChoices[i].AddComponent
			availableChoices[i].GetComponent<Renderer>().material.color = choiceColors[i];
		}
		session = this;
	}

	void Start(){
		StartRound();
	}


	// used to create Round instances
	public void StartRound() {
		//FIXME: orb spawns at player position
		GameObject orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		orb.transform.localScale -= new Vector3(0.75f,0.75f,0.75f);
		orb.transform.LookAt(player.transform);
		orb.transform.parent = playAreaAnchor.transform;
		orb.transform.Translate(0.0f, 0.0f, 0.75f);
		Debug.Log("Orb spawned at: " + orb.transform.position);
		Debug.Log("Player position at time of spawn: " + GameObject.FindWithTag("Player").transform.position);
	}
	
	// used to end Round instances before starting a new one
	public void NextRound() {

	}

	public void RestartRound() {
		
	}

}
