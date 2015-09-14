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

	 public static CountDownTimer timer;
     public bool inited;

	private Vector3 startPos;
	private Vector3 offscreenPos;
	
	private bool showingUI;

    public Vector3 posOffset;
	public GameObject container;
 
     // Use this for initialization
     void Start()
     {
		timerText.enabled = true;
		timerBackground.enabled = true;
		 inited = false;
//         timerPos = transform.position;
//         transform.position = new Vector3(999.0f, 999.0f, 999.0f);
         //this.Init(m_startingMinutes);

		startPos = container.transform.position;
		offscreenPos = new Vector3(container.transform.position.x + 500, container.transform.position.y, container.transform.position.z);
		container.transform.position = offscreenPos;


     }

    private bool gameover;

     // Update is called once per frame
     void Update()
     {
         if (!inited) {
			m_mins = 3.0f;
         }
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
            
//
		if (CameraAnimationScript.CamAnimSript.scene == "Game" && !showingUI)
			Show();
		else if (CameraAnimationScript.CamAnimSript.scene != "Game" && showingUI)
			Hide();

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

	public void Show()
	{
		showingUI = true;
		iTween.MoveTo(container, iTween.Hash("position", startPos, "time", 0.5f, "easeType", iTween.EaseType.easeOutSine));
	}
	
	public void Hide()
	{
		showingUI = false;
		iTween.MoveTo(container, iTween.Hash("position", offscreenPos, "time", 0.5f, "easeType", iTween.EaseType.easeInSine));
		PauseTimer(false);
	}
 

 }