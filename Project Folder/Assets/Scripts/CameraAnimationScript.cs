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
    }


    public void PauseAnim()
    {
        GetComponent<Animator>().speed = 0;
    }


    void OnGUI()
    {   
        if (scene == "GameOver")
            if (GUI.Button(new Rect(Screen.width / 2 + 300, Screen.height / 2, 200, 60), "Restart"))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
    }
}
