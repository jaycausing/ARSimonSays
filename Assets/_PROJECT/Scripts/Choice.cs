	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using GoogleARCore;
	#if UNITY_EDITOR
		using Input = GoogleARCore.InstantPreviewInput;
	#endif

	public abstract class Choice {

		int choiceNum;
		Color choiceColor;
		GameObject choicePrefab;
		Trackable trackable;
		bool isActiveToPlayer;

        public Choice(){}
		public Choice(int i, Color c, GameObject p, Trackable t){
			choiceNum = i;
			choiceColor = c;
			choicePrefab = p;
			trackable = t;
			isActiveToPlayer = false;
		}

        public abstract void selectChoice();
		public abstract void setPlayerActive();

	}

    public class RedChoice : Choice {

		int choiceNum;
		Color choiceColor;
		GameObject choicePrefab;
		Trackable trackable;
		bool isActiveToPlayer;

        public RedChoice(int i, Color c, GameObject p, Trackable t) {
			choiceNum = i;
			choiceColor = c;
			choicePrefab = p;
			trackable = t;
			isActiveToPlayer = false;
        }

        public override void selectChoice() {
			// TODO: add this object to the list of choices
        }

		// sets if choice is active to the player
        public override void setPlayerActive() {
			isActiveToPlayer = true;
        }
    }