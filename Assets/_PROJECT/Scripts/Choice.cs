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
		bool isActiveToPlayer;

		public Choice(){}

		public Choice(int i, Color c, GameObject p){
			choiceNum = i;
			choiceColor = c;
			choicePrefab = p;
			isActiveToPlayer = false;
		}

        public abstract void selectChoice();
		public abstract void setPlayerActive();

	}

    public class RedChoice : Choice {

		int choiceNum;
		Color choiceColor;
		GameObject choicePrefab;
		bool isActiveToPlayer;

        public RedChoice(int i, Color c, GameObject p) {
			choiceNum = i;
			choiceColor = c;
			choicePrefab = p;
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