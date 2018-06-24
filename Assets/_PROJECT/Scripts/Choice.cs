namespace Choices
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using GoogleARCore;
	#if UNITY_EDITOR
		using Input = GoogleARCore.InstantPreviewInput;
	#endif

	public abstract class Choice {

        public abstract void selectChoice();
		public abstract void setActive();

	}

    public class RedChoice : Choice {

		int choiceNum;
		Color choiceColor;
		GameObject choicePrefab;
		Trackable trackable;
        protected RedChoice(int i, Color c, GameObject p, Trackable t) {
			choiceNum = i;
			choiceColor = c;
			choicePrefab = p;
			trackable = t;
        }

        public override void selectChoice() {
        }

        public override void setActive() {
        }
    }
}
