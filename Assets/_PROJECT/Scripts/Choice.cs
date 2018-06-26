using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
#if UNITY_EDITOR
using Input = GoogleARCore.InstantPreviewInput;
#endif

public abstract class Choice : MonoBehaviour {

	int choiceNum;
	bool isActiveToPlayer;
	void Awake(){}
	void Update(){}

    public abstract void setChoiceNum(int i);
	public abstract void enableChoice();
	public abstract void disableChoice();
	public abstract void glow();

}
