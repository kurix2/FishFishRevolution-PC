using UnityEngine;
using System.Collections;

public class CameraAnimationScript : MonoBehaviour {

    public static CameraAnimationScript CamAnimSript;
    public string scene;

    void Awake()
    {
        if (CamAnimSript == null)
            CamAnimSript = this;
    }

    public void NextAnimation()
    {
        GetComponent<Animator>().speed = 1;
    }


    public void SetScene(string s)
    {
        scene = s;
        if (scene == "ScoreScreen")
            ScoreScreen.SScreen.Show();
    }


    public void PauseAnim()
    {
        GetComponent<Animator>().speed = 0;
    }


    void OnGUI()
    {   
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

    }
}
