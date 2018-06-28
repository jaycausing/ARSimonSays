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
    public static GameSession ActiveSession = null;
    public GameObject SessionPrefab;
    private GameObject activeSessionObj;
    //public static bool IsGameStarted;
    public static DetectedPlane activePlane;
    
    void Awake(){
        //IsGameStarted = false;
    }
    void Update() {
        if(ActivePlaneGenerator.GetActivePlaneStatus() == true &&
        ActiveSession == null){
           GameStart(ActivePlaneGenerator.GetActivePlane());
        }
    }

    public void GameStart(DetectedPlane dp){
        activePlane = dp;
        activeSessionObj = Instantiate(SessionPrefab, transform);
        ActiveSession = activeSessionObj.GetComponent<GameSession>();
        //IsGameStarted = true;
    }

    public void GameOver(){
        // TODO: popup showing "Thanks for playing!"
         // and number of rounds won
         // and option to play again or quit

         GameEnd();
    }

    public void GameEnd(){
         Destroy(activeSessionObj);
         ActiveSession = null;
    }

    // TODO: add UI event listener to invoke this
    public void GameRestart(){
        // TODO: popup asking if player wants to restart game
         GameEnd();
         GameStart(activePlane);
    }

    // TODO: add UI event listener to invoke this
    public void GameQuit(){
        // TODO: popup asking if player wants to quit
        // execute rest of GameQuit if Yes
        if(ActiveSession != null)
            GameEnd();
        Application.Quit();
    }
     
}