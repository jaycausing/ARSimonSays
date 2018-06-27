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
    public static GameSession activeSession = null;
    public GameObject SessionPrefab;
    bool isGameStarted;
    public static DetectedPlane activePlane;
    
    void Awake(){
        isGameStarted = false;
    }
    void Update() {
        if(ActivePlaneGenerator.GetActivePlaneStatus() == true &&
        activeSession == null){
           GameStart(ActivePlaneGenerator.GetActivePlane());
        }
    }

    public void GameStart(DetectedPlane dp){
        activePlane = dp;
        activeSession = Instantiate(SessionPrefab, transform).GetComponent<GameSession>();
    }

    public void GameEnd(){
         Destroy(activeSession);
    }

    public void GameRestart(){
         GameEnd();
         GameStart(activePlane);
    }
     
}