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
         // TODO: popup showing "Thanks for playing!"
         // and number of rounds won
    }

    public void GameRestart(){
        // TODO: popup asking if player wants to restart game
         GameEnd();
         GameStart(activePlane);
    }

    public void GameQuit(){
        // TODO: popup asking if player wants to quit
        // execute rest of GameQuit if Yes
        if(activeSession != null)
            Destroy(activeSession);
        Application.Quit();
    }
     
}