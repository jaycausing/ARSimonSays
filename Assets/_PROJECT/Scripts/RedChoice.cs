using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
#if UNITY_EDITOR
    using Input = GoogleARCore.InstantPreviewInput;
#endif

public class RedChoice : Choice {

	int choiceNum;
	bool isActiveToPlayer;

    void Awake() {
		isActiveToPlayer = false;
    }

    void Update(){
        this.transform.Rotate(Vector3.up, Time.deltaTime * 20.0f);
    }

    public override void setChoiceNum(int i) {
		choiceNum = i;
    }

	// sets if choice is active to the player
    public override void enableChoice() {
		isActiveToPlayer = true;
    }

	public override void disableChoice()
    {
		isActiveToPlayer = false;
	}

    public override void glow()
    {
        //choiceColor = Color.white;
        //choiceColor = Color.red;
    }
}