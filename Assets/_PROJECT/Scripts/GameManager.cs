using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.Common;

public class GameManager : MonoBehaviour {
    /*
    GameManager handles the following
    - creating and destroying game sessions
    - allocating data between Player/Simon and session
    - player score
     */
    
    //ITS CAUSE THIS NEVER BECAME A THING
    //public static GameSession ActiveSession = null;
    //public GameObject SessionPrefab;
    //private GameObject activeSessionObj;
    //public static bool IsGameStarted;

    private GameSession session;
    public static DetectedPlane activePlane;
    private UIManager UIManagerObj;
    
    
    void Start(){
        session = GetComponentInChildren<GameSession>();
        UIManagerObj = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    void Update() {
        if(ActivePlaneGenerator.GetActivePlaneStatus()){
            if(!session.isActiveAndEnabled){
                activePlane = ActivePlaneGenerator.GetActivePlane();
                GameStart();
            }
        }
    }

    public void GameOver(){
        UIManagerObj.ShowGameOverMessage(GameSession.RoundNum);
        StartCoroutine(GameEnd());
    }

    public void GameStart(){
        UIManagerObj.ShowGameStartMessage();
        session.enabled = true;
    }

    public IEnumerator GameEnd(){
        yield return StartCoroutine(session.DisablingSession());
        Debug.Log("Session disabled");
        session.enabled = false;
    }

    // TODO: add UI event listener to invoke this
    
     
}