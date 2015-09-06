using UnityEngine;
using System.Collections;

public class Cast : MonoBehaviour {

    public int castCounter;

    public GameObject rod;
    private GameObject barUI;
    private GameObject pingPongBar;
    public float gaugeValue;

    private GameObject bobber;
    private GameObject rodEnd;
    private GameObject launchPoint;
    private LineRenderer line;
    public string status;
    private Rigidbody rb;
    private RigidbodyConstraints orginalConstraint;


	// Use this for initialization
	void Start () {
      //  timerObject = GameObject.Find("Timer Object");
        castCounter = 0;
        barUI = GameObject.Find("GaugeUI");
        pingPongBar = GameObject.Find("Bar");
        barUI.SetActive(true);

        launchPoint = GameObject.Find("BobberLaunchPoint");	
        bobber = GameObject.Find ("Bobber");		
        rodEnd = GameObject.Find ("BobLauncher");
        line = GameObject.Find("LineRendererObject").GetComponent<LineRenderer>();
      
        status = "standby";

	}


    [RPC]
    public void castGauge()
    {
        Debug.Log("Guage Initiated");
        if (status == "standby") {
            rod.GetComponent<RodStatus>().setStatus("precasting");
          //  status = "precasting";
            barUI.SetActive(true);
           
        }
    }

	[RPC]
	public void cast()
    {
		if (castCounter == 0) {

			CountDownTimer.timer.Init(3.0f);
		} 

        castCounter++;
       // timerObject.GetComponent<CountDownTimer>.

        Debug.Log("Trying to cast");
       // Debug.Log("gauge Value was " + gaugeValue);
        
        if (rod.GetComponent<RodStatus>().getStatus() == "precasting")
        {
            rod.GetComponent<RodStatus>().setStatus("casting");
            line.GetComponent<Renderer>().enabled = true;
		    bobber.transform.position = rodEnd.transform.position;
		    rb = bobber.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;

            if (orginalConstraint == null)
                orginalConstraint = rb.constraints;
            else
                rb.constraints = orginalConstraint;

            rodEnd.transform.DetachChildren();
        
		    bobber.GetComponent<Rigidbody>().useGravity = true;
            rb.velocity = launchPoint.transform.forward * (gaugeValue * 60);
        }
	}
  

	// Update is called once per frame
    void Update()
    {

        if (barUI)
        {
            gaugeValue = pingPongBar.GetComponent<PingPongMove>().value;
        }
        else return;

        if (rod.GetComponent<RodStatus>().getStatus() == "precasting")
        {
            barUI.GetComponent<GaugeLocationScript>().castButtonPressed = true;
        }
        else
            barUI.GetComponent<GaugeLocationScript>().castButtonPressed = false;
    }
}
 