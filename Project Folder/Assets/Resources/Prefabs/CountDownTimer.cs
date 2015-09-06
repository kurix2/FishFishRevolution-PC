 using UnityEngine;
 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour {

	 public Text timerText;
	 public Image timerBackground;
     public float m_startingMinutes = 3.0f; //in seconds
     public bool m_startTimer = false;
     public Text m_timerLabel; //drag GUIText in the scene here
 
     private float m_miliseconds;
     private float m_seconds;
     private float m_mins;
     private float m_totalmiliseconds;

     private Vector3 timerPos;
	 public static CountDownTimer timer;
     public bool inited;

     public Vector3 posOffset;
 
 
     // Use this for initialization
     void Start()
     {
		timerText.enabled = false;
		timerBackground.enabled = false;
		 inited = false;
//         timerPos = transform.position;
//         transform.position = new Vector3(999.0f, 999.0f, 999.0f);
         //this.Init(m_startingMinutes);
     }

    private bool gameover;

     // Update is called once per frame
     void Update()
     {
//         if (inited) {
//			   
////             Vector3 pos = new Vector3(Screen.width - posOffset.x, Screen.height - posOffset.y, 0 );
////             transform.position = pos;//timerPos; 
//         }
         if (m_startTimer && m_totalmiliseconds >= 0)
         {
             if (m_miliseconds <= 0)
             {
                 if (m_seconds <= 0)
                 {
                     m_mins--;
                     m_seconds = 59;
                 }
                 else
                 {
                     m_seconds--;
                 }
 
                 m_miliseconds = 99;
             }
 
             m_miliseconds -= Time.deltaTime * 100;
             m_totalmiliseconds -= Time.deltaTime * 100;
         }
         else if (m_totalmiliseconds <= 0)
         {
             m_miliseconds = 0.0f;
             m_seconds = 0.0f;
             m_mins = 0.0f;
         }
 
         if ((int)m_miliseconds > 9)
         {
             m_timerLabel.text = string.Format("{0}:{1}:{2}", m_mins, m_seconds, (int)m_miliseconds);
         }
         else
         {
             m_timerLabel.text = string.Format("{0}:{1}:0{2}", m_mins, m_seconds, (int)m_miliseconds);
         }

        if (inited && m_mins == 0 && m_seconds == 0 && m_miliseconds == 0 && !gameover)
        {
            gameover = true;
            RodStatus.rodstatus.setStatus("gameover");
        }
            

     }
 
//    void OnGUI()
//    {
//        GUI.skin.label.fontSize = 32;
//        if (inited)
//          GUI.Label(new Rect(Screen.width - 140, 20, 300, 50), "" + m_timerLabel.text);
//    }
     /// <summary>
     /// Public function to initialize the timer
     /// </summary>
     /// <param name="p_startingTime"></param>
     public void Init(float p_startingTime)
     {
		 timerText.enabled = true;
		 timerBackground.enabled = true;
         inited = true;
         //On the note of PlayerPrefs, you may want to read them in here on the initialize      
         m_totalmiliseconds = p_startingTime * (60/*seconds*/) * (100/*miliseconds*/);
         m_mins = p_startingTime;
         m_startTimer = true;
     }
 
     /// <summary>
     /// Public function to pause the timer
     /// </summary>
     /// <param name="p_pause"></param>
     public void PauseTimer(bool p_pause)
     {
         m_startTimer = p_pause;
     }

	void Awake(){
		if (timer == null) {
			timer = this;
		}

	}
 

 }