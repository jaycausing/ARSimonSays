using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
#if UNITY_EDITOR
using Input = GoogleARCore.InstantPreviewInput;
#endif

public abstract class Choice : MonoBehaviour {

		private int choiceNum;
		private Color choiceColor;
		private bool isActiveToPlayer;

		public Choice(){}

		public Choice(int i, Color c){
			choiceNum = i;
			choiceColor = c;
			isActiveToPlayer = false;
		}

        public abstract void selectChoice();

		public abstract void setChoiceNum( int i );
		public abstract void setPlayerEnable();
		public abstract void setPlayerDisable();

	}
