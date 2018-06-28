using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
#if UNITY_EDITOR
using Input = GoogleARCore.InstantPreviewInput;
#endif

public class GreenChoice : Choice {

	private int choiceNum;
	private Color choiceColor;
	private bool isActiveToPlayer;

	public GreenChoice(){
		choiceNum = 0;
		choiceColor = new Color(0,1,0,1);
		isActiveToPlayer = false;
	}

  public GreenChoice(int i, Color c) {
		choiceNum = i;
		choiceColor = c;
		isActiveToPlayer = false;
  }

  public override void selectChoice() {
		gameObject.SendMessageUpwards("AddChoiceToList", choiceNum);
  }

  public override void setPlayerEnable() {
		isActiveToPlayer = true;
  }

	public override void setPlayerDisable() {
    isActiveToPlayer = false;
  }

  public override void setChoiceNum(int i) {
      choiceNum = i;
  }
}