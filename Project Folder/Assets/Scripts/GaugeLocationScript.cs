using UnityEngine;
using System.Collections;

public class GaugeLocationScript : MonoBehaviour {
    public Vector3 pressedPosition;
    public Vector3 notPressedPosition;


	
    public bool castButtonPressed;

	public GameObject gauge;
	public GameObject gauge2;


	// Use this for initialization
	void Start () {

		castButtonPressed = false;

		gauge = GameObject.Find("CastFront");
		gauge2 = GameObject.Find ("CastBack");

        notPressedPosition = new Vector3(1000.0f, 10000.0f,0f);
		pressedPosition = new Vector3(700.0f, 33.0f,0f);
	}
	
	// Update is called once per frame
	void Update () {
        if (castButtonPressed == false)
        {
            gauge.transform.position = notPressedPosition;
			gauge2.transform.position = notPressedPosition;
		

        }
        else
        {
			gauge.transform.position = pressedPosition;
			gauge2.transform.position = pressedPosition;
        }
	}
}
