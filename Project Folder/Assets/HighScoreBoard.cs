using UnityEngine;
using System.Collections;

public class HighScoreBoard : MonoBehaviour {

	public float[] highScores = new float[5];
	string highScoreKey = "";
	
	void Start(){
		for (int i = 0; i<highScores.Length; i++){
			highScoreKey = "HighScore"+(i+1).ToString();
			highScores[i] = PlayerPrefs.GetFloat(highScoreKey,0);

			Debug.Log ("High Score " + (i+1) + " is " + highScores[i]);
		}
			
	}
}