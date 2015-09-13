using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;



public class ScoreScreen : MonoBehaviour {

    public static ScoreScreen SScreen;

    private Vector3 startPos;
    private Vector3 outPos;
	public float totalScore;

	public Text totalScoreText;
	public float totalScoreInt;

	//for high score
	public float highScore;
	public float[] highScores = new float[5];
	public string highScoreKey;
	public Text highScoreText;




    public GameObject[] fishIcons;


    void Awake()
    {
        SScreen = this;
    }


	void Start () {


		//Get the highScore from player prefs if it is there, 0 otherwise. might be highscore
		highScoreKey = "HighScore";
		highScore = PlayerPrefs.GetFloat(highScoreKey,0);    

        startPos = transform.position;
        outPos = new Vector3(startPos.x, startPos.y - 1200, startPos.z);
        transform.position = outPos;
       

        for (int i = 0; i < 
            transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void Show()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        foreach (Behaviour childCompnent in this.gameObject.GetComponentsInChildren<Behaviour>())
            childCompnent.enabled = true;

        foreach (GameObject go in fishIcons)
        {
            go.GetComponent<ScoreScreenFishData>().Setup();
			totalScore += go.GetComponent<ScoreScreenFishData>().GetScore();

        }



		//Prints the score to the screen
		totalScoreText.text = "Total Score: " + totalScore.ToString("00000");
	
	

		for (int i = 0; i<highScores.Length; i++){
			
			//Get the highScore from 1 - 5
			highScoreKey = "HighScore"+(i+1).ToString();
			highScore = PlayerPrefs.GetFloat(highScoreKey,0);
			
			//if score is greater, store previous highScore
			//Set new highScore
			//set score to previous highScore, and try again
			//Once score is greater, it will always be for the
			//remaining list, so the top 5 will always be 
			//updated
			
			if(totalScore>highScore){
				float temp = highScore;
				PlayerPrefs.SetFloat(highScoreKey,totalScore);
					totalScore = temp;
			}
		}

		for (int i = 0; i<highScores.Length; i++){
			highScoreKey = "HighScore"+(i+1).ToString();
			highScores[i] = PlayerPrefs.GetFloat(highScoreKey,0);
			
			Debug.Log ("High Score " + (i+1) + " is " + highScores[i]);
			highScoreText.text += "Rank " +(i+1) + ": " + highScores[i].ToString("0000") + "\n\n";
		}




	
			
	    iTween.MoveTo(this.gameObject, iTween.Hash("position", startPos, "time", 0.5f, "easeType", iTween.EaseType.easeOutSine));

    }

    public void Hide()
    {
        outPos = new Vector3(startPos.x, startPos.y + 500, startPos.z);
        iTween.MoveTo(this.gameObject, iTween.Hash("position", outPos, "time", 0.5f, "easeType", iTween.EaseType.easeOutSine));
    }

	public float GetTotalScore(){
		return totalScore;
	}

    // Update is called once per frame
    void Update () {
	
	}
}
