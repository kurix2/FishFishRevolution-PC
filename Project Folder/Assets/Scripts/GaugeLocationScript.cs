using UnityEngine;
using System.Collections;

public class GaugeLocationScript : MonoBehaviour {
    public Vector3 pressedPosition;
	public Vector3 pressedPosition2;

    public Vector3 notPressedPosition;

	public bool buttonSwitchOn;
	public bool buttonSwitchOff;

    public bool castButtonPressed;

	public GameObject gauge;
	public GameObject gauge2;


	// Use this for initialization
	void Start () {
		buttonSwitchOff = false;
		buttonSwitchOn = true;

		gauge = GameObject.Find("CastFront");
		gauge2 = GameObject.Find ("CastBack");

		notPressedPosition = new Vector3((Screen.width/10)*12, (Screen.height/5)*2.0f,0f);
		pressedPosition = new Vector3((Screen.width/10) * 9.5f, (Screen.height/5)*2.0f,0f);
		pressedPosition2 = new Vector3((Screen.width/10) * 9.5f, (Screen.height/5)*2.0f - 5,0f);

	}
	
	// Update is called once per frame
	void Update () {
        


		if (castButtonPressed == false)
        {
			if(!buttonSwitchOff)
			{
				iTween.MoveTo(gauge,iTween.Hash("position",notPressedPosition,"time",0.5f,"easeType",iTween.EaseType.easeInSine));
				iTween.MoveTo(gauge2,iTween.Hash("position",notPressedPosition,"time",0.5f,"easeType",iTween.EaseType.easeInSine));
				buttonSwitchOff = true;
				buttonSwitchOn = false;
			}
        }

		else
        {
			if(!buttonSwitchOn)
			{
				iTween.MoveTo(gauge,iTween.Hash("position",pressedPosition,"time",0.5f,"easeType",iTween.EaseType.easeOutSine));
				iTween.MoveTo(gauge2,iTween.Hash("position",pressedPosition2,"time",0.5f,"easeType",iTween.EaseType.easeOutSine));
				buttonSwitchOn = true;
				buttonSwitchOff = false;
			}



        }
	}
}
