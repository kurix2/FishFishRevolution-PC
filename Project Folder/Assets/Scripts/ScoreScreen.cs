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
	public int totalScoreInt;




    public GameObject[] fishIcons;


    void Awake()
    {
        SScreen = this;
    }


	void Start () {
        startPos = transform.position;
        outPos = new Vector3(startPos.x, startPos.y - 1200, startPos.z);
        transform.position = outPos;
		totalScore = 0;
       

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
		totalScoreText.text = "Total Score: " + totalScore.ToString("0");


		int totalScoreInt = (int) Math.Round (totalScore);
	
			
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
