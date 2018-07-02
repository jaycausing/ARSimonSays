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
        isGameEnd = false;
        session = GetComponentInChildren<GameSession>();
        UIManagerObj = GameObject.Find("UIManager").GetComponent<UIManager>();
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
        UIManagerObj.ShowGameOverMessage(GameSession.RoundNum);
        StartCoroutine(GameEnd());
    }

    public void GameStart(){
        UIManagerObj.ShowGameStartMessage();
        isGameEnd = false;
        session.enabled = true;
    }

    public IEnumerator GameEnd(){
        isGameEnd = true;
        yield return StartCoroutine(session.DisablingSession());
        Debug.Log("Session disabled");
        session.enabled = false;
    }

    public bool GetGameStatus(){
        return isGameEnd;
    }

    // TODO: add UI event listener to invoke this
    
     
}