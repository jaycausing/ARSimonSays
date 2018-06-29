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
    private GameObject GameStartUI;
    private GameObject RestartBtn;
    private GameObject RestartConfirmPopup;
    private GameObject GameOverMessage;

    Text scoreText;

    private GameManager GameManagerObj;

    void Awake(){
        SearchingUI = GameObject.Find("SearchingUI");
        GameManageUI = GameObject.Find("GameManageUI");
        GameStartUI = GameObject.Find("GameStart");
        RestartBtn = GameObject.Find("RestartBtn");
        RestartConfirmPopup = GameObject.Find("RestartConfirmPopup");
        GameOverMessage = GameObject.Find("GameOverMessage");

        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();

        GameManagerObj = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start(){
        SearchingUI.SetActive(false);
        GameManageUI.SetActive(false);
        GameStartUI.SetActive(false);
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
	}

    public void ShowGameStartMessage(){
        GameStartUI.SetActive(true);
        StartCoroutine(DeactivateUIAfterWait(GameStartUI));
    }

    public void ShowGameOverMessage(int score){
        GameOverMessage.SetActive(true);
        scoreText.text = scoreText.text + score;
    }

    /// Restart Game Buttons ///

    public void GameRestartConfirm(){
         RestartBtn.SetActive(false);
         RestartConfirmPopup.SetActive(true);
    }

    public void GameRestart(){
        RestartConfirmPopup.SetActive(false);
        StartCoroutine(RestartingGame());
    }

    public IEnumerator RestartingGame(){
         Debug.Log("Restarting game");

        if(GameStartUI != null){
            if(IsInvoking("DeleteUIAfterWait"))
                StopCoroutine(DeactivateUIAfterWait(GameStartUI));
            GameStartUI.SetActive(false);
        }
        if(GameOverMessage != null){
            GameOverMessage.SetActive(false);
            scoreText.text = scoreText.text;
        }

         yield return StartCoroutine(GameManagerObj.GameEnd());
         //Debug.Log("Game ended");
         //yield return new WaitForSeconds(3);
         Debug.Log("Game restarting");
         GameManagerObj.GameStart();
         RestartBtn.SetActive(true);
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

    public IEnumerator DeactivateUIAfterWait(GameObject UIObject){
        yield return new WaitForSeconds(5);
        UIObject.SetActive(false);
    }
}