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
        pressedPosition = new Vector3(3f, 0.2f, 3.23f);
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
