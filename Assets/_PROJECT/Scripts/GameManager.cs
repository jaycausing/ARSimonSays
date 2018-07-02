using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.Common;

public class GameManager : MonoBehaviour {

    private GameSession session;
    public static DetectedPlane activePlane;
    private UIManager UIManagerObj;
    private bool isGameEnd;
    
    
    void Start(){
        session = GetComponentInChildren<GameSession>();
        UIManagerObj = GameObject.Find("UIManager").GetComponent<UIManager>();
        isGameEnd = false;
    }

    void Update() {
        if(ActivePlaneGenerator.GetActivePlaneStatus()){
            if(!session.isActiveAndEnabled && !isGameEnd){
                activePlane = ActivePlaneGenerator.GetActivePlane();
                GameStart();
            }
        }
    }

    public void GameOver(){
        isGameEnd = true;
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