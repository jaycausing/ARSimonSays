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

    GameSession session;
    public static DetectedPlane activePlane;
    
    void Awake(){
        session = GetComponentInChildren<GameSession>();
    }

    void Update() {
        if(ActivePlaneGenerator.GetActivePlaneStatus() == true &&
        !session.isActiveAndEnabled){
        //ActiveSession == null){
            activePlane = ActivePlaneGenerator.GetActivePlane();
            session.enabled = true;
            //SessionPrefab.SetActive(true);
           //GameStart(ActivePlaneGenerator.GetActivePlane());
        }
    }

    /*public void GameStart(DetectedPlane dp){
        
        activeSessionObj = Instantiate(SessionPrefab, this.transform);
        ActiveSession = activeSessionObj.GetComponent<GameSession>();
        //IsGameStarted = true;
    }*/

    // TODO: OnGUI events for Restart and Quit
    /*void OnGUI() {
        
    }*/

    public void GameOver(){
        // TODO: popup showing "Thanks for playing!"
         // and number of rounds won
         // and option to play again or quit
        int score = GameSession.RoundNum;
        Debug.Log("Game over! Final score: " + score);
        GameEnd();
    }

    public void GameEnd(){
         session.enabled = false;
         //SessionPrefab.SetActive(false);
         //Destroy(activeSessionObj);
         //ActiveSession = null;
    }

    // TODO: add UI event listener to invoke this
    public void GameRestart(){
        // TODO: popup asking if player wants to restart game
         Debug.Log("Restarting game");
         GameEnd();
         session.enabled = true;
         //SessionPrefab.SetActive(true);
         //GameStart(activePlane);
    }

    // TODO: add UI event listener to invoke this
    public void GameQuit(){
        // TODO: popup asking if player wants to quit
        // execute rest of GameQuit if Yes
        Debug.Log("Quitting application");
        if(session.isActiveAndEnabled)
            GameEnd();
        Destroy(session);
        Application.Quit();
    }
     
}