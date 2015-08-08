using UnityEngine;
using System.Collections;

public class GaugeLocationScript : MonoBehaviour {
    public Vector3 pressedPosition;
    public Vector3 notPressedPosition;
    public bool castButtonPressed;

    public GameObject timerGauge;
	// Use this for initialization
	void Start () {
        notPressedPosition = new Vector3(99999.0f, 99999.0f, 0.0f);
        pressedPosition = new Vector3(-2.1f, 1, -0.7f);
	}
	
	// Update is called once per frame
	void Update () {
        if (castButtonPressed == false)
        {
            timerGauge.transform.position = notPressedPosition;
        }
        else
        {
            timerGauge.transform.position = pressedPosition;
        }
	}
}
