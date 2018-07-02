#if UNITY_EDITOR
	using Input = GoogleARCore.InstantPreviewInput;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using GoogleARCore;

public class PlayerTurn : Turn
{

	//TODO: on creation of new turn, reference list of available choices
	//private Round round;
    private Turn currentTurn;
	private List<GameObject> currentChoices;
    private static int layerMask = 1 << 9;
	
    Text EntityChoices;
	Text ColorChoiceText;
    Text PlayerChoiceText;

    Camera playerCam;

    bool isTracking;

    void OnEnable() {
        playerCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        EntityChoices = GameObject.Find("EntityChoices").GetComponent<Text>();
		ColorChoiceText = GameObject.Find("EntityChoices").GetComponent<Text>();
		PlayerChoiceText = GameObject.Find("PlayerChoiceText").GetComponent<Text>();
        currentChoices = new List<GameObject>();
        isTracking = false;
		currentTurn = this;
    }

    void OnDisable(){
        EntityChoices.text = "";
		ColorChoiceText.text = "";
        PlayerChoiceText.text = "";
    }

    void Start(){
        StartCoroutine(StartTurn());
	}

    void Update(){
        if(isTracking && Input.touchCount > 0){
            StartCoroutine(CheckTouch(Input.touches));
        }
    }

    private IEnumerator CheckTouch(Touch[] touches){
        RaycastHit hit;
        foreach(Touch touch in touches) {
            if(touch.phase == TouchPhase.Began) {
                Ray ray = playerCam.ScreenPointToRay(touch.position);

                if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)){
                    if (hit.collider != null){
                        if(hit.transform.parent != null) {
                            Transform choiceObj = hit.transform.parent;
                            currentChoices.Add(choiceObj.gameObject);
                            Debug.Log("Choice added: " + choiceObj.name);
                            yield return new WaitForSeconds(0.1f);
                        }
                    }
                }
            }
        }
    }

    public override void EndTurn() {
        EntityChoices.text = "";
		ColorChoiceText.text = "";
        PlayerChoiceText.text = "";
        gameObject.SendMessageUpwards("EndPlayerTurn", currentChoices);
    }

    private IEnumerator StartTurn(){
		yield return StartCoroutine(SelectChoices());
		yield return StartCoroutine(PrintChoicesInLog(currentChoices));
		EndTurn();
	}

    IEnumerator SelectChoices(){
        PlayerChoiceText.text = "Tap on a cube to select it";
        isTracking = true;
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => currentChoices.Count == Round.SimonChoiceHistory.Count);
    }

	private IEnumerator PrintChoicesInLog(List<GameObject> choices){
		EntityChoices.text = "You have chosen...";
        yield return new WaitForSeconds(3);
		foreach(GameObject choice in currentChoices){
			ColorChoiceText.text = choice.name;
			yield return new WaitForSeconds(3);
            ColorChoiceText.text = "";
		}
	}

    /*private IEnumerator WaitBuffer(float time){
        yield return new WaitForSeconds(time);
    }*/

    public void RestartTurn()
    {
        currentChoices.Clear();
		StartCoroutine(StartTurn());
    }


    public override List<GameObject> GetCurrentChoices()
    {
		return currentChoices;
        //throw new System.NotImplementedException();
    }

}
