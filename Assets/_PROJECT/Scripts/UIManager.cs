using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using GoogleARCore;
using GoogleARCore.Examples.Common;

#if UNITY_EDITOR
	using Input = GoogleARCore.InstantPreviewInput;
#endif


public class UIManager : MonoBehaviour {

    private GameObject SearchingUI;
    private GameObject GameManageUI;
    private GameObject RestartBtn;
    private GameObject RestartConfirmPopup;
    private GameObject GameOverMessage;

    private GameManager GameManagerObj;

    void Awake(){
        SearchingUI = GameObject.Find("SearchingUI");
        GameManageUI = GameObject.Find("GameManageUI");
        RestartBtn = GameObject.Find("RestartBtn");
        RestartConfirmPopup = GameObject.Find("RestartConfirmPopup");
        GameOverMessage = GameObject.Find("GameOverMessage");

        GameManagerObj = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start(){
        SearchingUI.SetActive(false);
        GameManageUI.SetActive(false);
        RestartBtn.SetActive(false);
        RestartConfirmPopup.SetActive(false);
        GameOverMessage.SetActive(false);
    }

    /// Start Game Button ///

    public void StartTracking(){
		Destroy(GameObject.Find("StartBtn"));
		ActivePlaneGenerator.StartTracking = true;
        Debug.Log("Is tracking active? " + ActivePlaneGenerator.StartTracking);
        Debug.Log("Session status: " + Session.Status);
        SearchingUI.SetActive(true);
        GameManageUI.SetActive(true);
        RestartBtn.SetActive(true);
        StartCoroutine(DeleteUIAfterWait(GameObject.Find("GameStart")));
	}

    public void ShowGameOverMessage(int score){
        GameOverMessage.SetActive(true);
        Text scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreText.text = scoreText.text + score;
    }

    /// Restart Game Buttons ///

    public void GameRestartConfirm(){
         RestartBtn.SetActive(false);
         RestartConfirmPopup.SetActive(true);
    }
    public void GameRestart(){
         Debug.Log("Restarting game");
         RestartBtn.SetActive(false);
         RestartConfirmPopup.SetActive(true);

         GameManagerObj.GameEnd();
         GameManagerObj.GameStart();
         //SessionPrefab.SetActive(true);
         //GameStart(activePlane);
    }
    public void GameRestartCancel(){
         RestartBtn.SetActive(true);
         RestartConfirmPopup.SetActive(false);
    }

    /// UI Remove Coroutines ///

    public IEnumerator FadeUI(GameObject UIObject){
        yield return new WaitForSeconds(3);
        // Get UI Color
        // for loop
        // lower color opacity
        // yield return new WaitForSeconds(0.1f);
    }

    public IEnumerator DeleteUIAfterWait(GameObject UIObject){
        yield return new WaitForSeconds(3);
        Destroy(UIObject);
    }
}