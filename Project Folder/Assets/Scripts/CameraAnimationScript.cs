using UnityEngine;
using System.Collections;

public class CameraAnimationScript : MonoBehaviour {

    public static CameraAnimationScript CamAnimSript;
    public string scene;

    private NetworkView nView;

    void Awake()
    {
        if (CamAnimSript == null)
            CamAnimSript = this;
    }

	private bool skipFirst;
    public void NextAnimation()
    {
		GetComponent<Animator> ().speed = 1;
		if (!skipFirst) {
			if (nView == null) {
				nView = GameObject.Find ("NetworkView_TestObject(Clone)").GetComponent<NetworkView> ();
			}
			if (nView)
				nView.RPC ("inTransition", RPCMode.All);
		}
		skipFirst = true;
	}

	private int check = 0;
    public void SetScene(string s)
    {
		scene = s;
		if (scene == "ScoreScreen")
			ScoreScreen.SScreen.Show ();
		if (check > 0) {
			if (nView == null) {
				nView = GameObject.Find ("NetworkView_TestObject(Clone)").GetComponent<NetworkView> ();
			}
			if (scene != "TransitGameOver")
				nView.RPC ("transitionOver", RPCMode.All);
		}
		check++;
	}


    public void PauseAnim()
    {
        GetComponent<Animator>().speed = 0;
    }

    public void showHighScore()
    {
        ScoreScreen.SScreen.Hide();
        HighScoreScene.HSScreen.Show();
        NextAnimation();
    }

    void OnGUI()
    {   /*
        if (scene == "ScoreScreen")
            if (GUI.Button(new Rect(Screen.width / 2 + 200, Screen.height / 2, 200, 60), "High Score"))
            {
                ScoreScreen.SScreen.Hide();
                HighScoreScene.HSScreen.Show();
                NextAnimation();
            }

        if (scene == "HighScoreScreen")
            if (GUI.Button(new Rect(Screen.width / 2 + 200, Screen.height / 2 + 70, 200, 60), "Restart"))
            {
                HighScoreScene.HSScreen.Hide();
                SceneFade.SceneFader.EndScene();
            }
            */
    }
}
