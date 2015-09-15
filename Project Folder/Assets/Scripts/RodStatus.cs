﻿using UnityEngine;
using System.Collections;

// Handles the States of the rod

[RequireComponent(typeof(AudioSource))]

public class RodStatus : MonoBehaviour
{
	//Audio Sources & Clips
    public AudioSource release;
	public AudioSource keep;
    public AudioSource reelSoundSource;
	public AudioClip keepSE;
	public AudioClip releaseSE;

	public bool reelSoundSwitchOn;
	public bool reelSoundSwitchOff;


    private bool gameOver;

    public static RodStatus rodstatus;
    public NetworkView nView;
    private GameObject fishManager;
    private string status;

    private bool catchSwitch;
    private float currVel;

    // Time to react before fish gets away
    private float pullTimeMax = 1.5f;
    private float pullTimeLeft;
    private float pullSplashScale;

    public GameObject fish;
    private Fish hookedFish;
    private int fishCount = 0;
    private int reelSpeed = 5;
    private bool reelSwitch;
    public int speed = 500;
    public float distance;
    public bool fishAttached;

    public float rodPullFactor;
    public float swimSpeed;
    private int randomTrigger;
    private float randomDir;

    public float linePower;

    private Vector3 prevPos;
    private RigidbodyConstraints orginalConstraint;

    // UI
    private bool showBiteSplash;
    private bool androidUIEnabled;

	//Boat
	private Vector3 boatPos;
	private float boatDistance;

    // Bobber
    public GameObject bobber;
    private Rigidbody rb;
    private GameObject hookHinge;
    public GameObject rodEnd;
    private GameObject rodTip;
    private LineRenderer line;
    public Transform reel;

    // For demo only ============
    private float biteCoolDown;
    public float TimeBetweenBites = 5f;
    private int biteTimer;
    private Time biteTime;
    // ==========================


    // Get the current Status
    public string getStatus()
    {
        return status;
    }

    public void setStatus(string sts)
    {
        status = sts;
        // Send new status to phone
    }

    void Awake()
    {
        rodstatus = this;
    }

    // Use this for initialization
    void Start()
    {
		boatPos = GameObject.Find ("Fishing Boat").transform.position;
		reelSoundSwitchOff = true;
		reelSoundSwitchOn = false;
        nView = GetComponent<NetworkView>();
        NetworkManager.nView = nView;
        fishManager = GameObject.Find("Fish Manager");
        bobber = GameObject.Find("Bobber");
        rodEnd = GameObject.Find("BobLauncher");
        rodTip = GameObject.Find("tipCluster");
        fish = GameObject.Find("fish");
        line = GameObject.Find("LineRendererObject").GetComponent<LineRenderer>();
        rb = bobber.GetComponent<Rigidbody>();
         
        pullTimeLeft = pullTimeMax;
        line.GetComponent<Renderer>().enabled = false;

        pullTimeLeft = pullTimeMax;
        biteCoolDown = TimeBetweenBites;

        status = "standby";
        StartCoroutine(CalcVelocity());
        line.GetComponent<Renderer>().enabled = false;

        // for demo only
        //biteTimer = TimeBetweenBites;
        biteCoolDown = TimeBetweenBites;

        /* if (!nView.isMine)
         {
             enabled = false;
         }
        */
    }

    IEnumerator CalcVelocity()
    {
        while (Application.isPlaying)
        {
            //Set previous position
            prevPos = rodEnd.transform.position;

            //Wait for the end of the frame
            yield return new WaitForEndOfFrame();

            //Calculate velocity: Velocity = DeltaPosition /DeltaTime
            currVel = Vector3.Distance(prevPos, rodEnd.transform.position) / Time.deltaTime;
            //Debug.Log("current vel. " +currVel);
        }
    }

 

    // Update is called once per frame
    void Update()
    {
		boatDistance = Vector3.Distance(boatPos, bobber.transform.position);
        // Status switch for handeling fishing states
        switch (status)
        {
            // Waiting for player to cast
            case "standby":
                break;

            case "precasting":
                GameObject.Find("GaugeUI").GetComponent<GaugeLocationScript>().castButtonPressed = true;
                break;

            // The hook is in the air
            case "casting":
                if (rb.GetComponent<Bobber>().hasLanded())
                    status = "hooking";
                break;

            case "hooking":
                
                // Waiting for fish to bite
                if (!catchSwitch)
                {
                    
                    // Random disabled for demo
                    // int randCatchChance = Random.Range(0, catchChanceRange);
                    //Debug.Log("random number: " + randCatchChance);
                    // if (randCatchChance == 0)
                    biteCoolDown -= Time.deltaTime;
                    if (biteCoolDown <= 0)
                    {
                        catchSwitch = true;
                        showBiteSplash = true;
                        nView.RPC("vibrate", RPCMode.All);
                    }
                }
                else
                {
                    pullTimeLeft -= Time.deltaTime;
                    pullSplashScale = pullTimeLeft / pullTimeMax;
                   // Debug.Log("timeLeft " + pullTimeLeft);
                    if (pullTimeLeft < 0)
                    {
                        Debug.Log("GG, fish escaped");
                        catchSwitch = false;
                        pullTimeLeft = pullTimeMax;
                        showBiteSplash = false;
                        biteCoolDown = TimeBetweenBites;
                    }
                    else
                    {
                        // INSERT VELOSITY FROM ROD HERE
                        if (currVel > 25.0f && catchSwitch)
                        {
                            Debug.Log("CATCH!! " + currVel);
                            showBiteSplash = false;
                            status = "reeling in";
                        }
                    }
                }

                break;

            // Hook has landed on ocean floor, player can now reel in
            case "reeling in":
                float step = (rodPullFactor / 30) * Time.deltaTime;
                //Put Rod Sound Switch HERE If this rodpullFactor isnt zero
			if(rodPullFactor > 0 && !reelSoundSwitchOn){
				reelSoundSwitchOn = true;
				reelSoundSwitchOff = false;
				reelSoundSource.Play ();
			}
			else if(rodPullFactor <= 0 && !reelSoundSwitchOff){
				reelSoundSwitchOff = true;
				reelSoundSwitchOn = false;
				reelSoundSource.Stop ();
			}
                linePower += step*10;
                if (linePower >= 0.9f)
                    linePower-=0.9f;

                if (rodPullFactor >= 0.1f)
                    rodPullFactor -= 0.1f;

                // Fish hooked
                if (catchSwitch)
                {
					
                    if (randomTrigger == 1)
                    {
                        randomDir = Random.Range(-90.0f, 90.0f);
                    }

                    if (!fishAttached)
                    {
                        Debug.Log("Caught a random fish");

                        fishAttached = true;
						Gauge.fishGauge.enabled = true;

                        hookedFish = fishManager.GetComponent<FishManager>().hookFish();
						hookedFish.setDistance(boatDistance);
						
					   hookHinge = Instantiate(Resources.Load("Prefabs/CharacterHook"), rb.position, Quaternion.identity) as GameObject;
                          

					   //hookHinge = Instantiate(Resources.Load("Prefabs/HookHinges3"), rb.position, Quaternion.identity) as GameObject;
                       //GameObject Connecter = GameObject.Find("HookJoint");
                       //HingeJoint hj = Connecter.GetComponent<HingeJoint>();
                        //hj.connectedBody = rb;

                        string fishPrefab = hookedFish.getPrefab();

					fish = Instantiate(Resources.Load("Prefabs/Fish/" + fishPrefab), new Vector3(rb.position.x,rb.position.y-0.1f,rb.position.z), Quaternion.identity) as GameObject;
					//fish = Instantiate(Resources.Load("Prefabs/Fish/" + fishPrefab), rb.position, Quaternion.identity) as GameObject;
//                        GameObject hookJointGO = GameObject.Find("FishJoint");
//                        HingeJoint fishJoint = fish.GetComponent<HingeJoint>();

					    GameObject hookOfHook = GameObject.Find("CharacterHookHook"); 
						hookOfHook.GetComponent<CharacterJoint>().connectedBody = fish.GetComponent<Rigidbody>();
					    
						hookHinge.transform.parent = rb.transform;
						//hookHinge = fish.GetComponent<Rigidbody>();

                        rb.useGravity = false;
                        swimSpeed = hookedFish.getSpeed();
                    }
                }

                randomTrigger = Random.Range(0, 50);

                Quaternion rotation2 = Quaternion.LookRotation(rb.position - rodEnd.transform.position);
                Quaternion rotation3 = Quaternion.Euler(rotation2.x, rotation2.y + randomDir, rotation2.z);
                rb.rotation = rotation3;

                Vector3 towardsRod = Vector3.MoveTowards(rb.position, rodEnd.transform.position, step);
                Vector3 swimVec = -rb.transform.forward * (swimSpeed * Time.deltaTime);
                Vector3 newPos = towardsRod - swimVec;

                rb.position = newPos;

                rodPullFactor = 0;

                if (bobber.GetComponent<Bobber>().reelMeUp == true || Vector3.Distance(bobber.transform.position,boatPos) < 6.0f)
                    status = "reeling up";

                break;

            // Hook has reached the boat, reel now goes up towards rodEnd
            case "reeling up":
				
                

                float distance = Vector3.Distance(rb.position, rodEnd.transform.position);
                if (distance < 0.01f)
                {
                    bobber.transform.parent = rodTip.transform;
                    if (!androidUIEnabled)
                    {
                        nView.RPC("enableCaughtUI", RPCMode.All,hookedFish.getName(),hookedFish.getWeight());
                        androidUIEnabled = true;
                    }

                    if (Input.GetKeyDown("space"))
                        status = "reset";
                }

                else
                {
                    bobber.transform.GetComponent<Renderer>().enabled = false;
                    rb.velocity = new Vector3(0, 0, 0);
                    rb.constraints = orginalConstraint;
                    rb.useGravity = false;
                    rb.isKinematic = true;
                    rb.position = Vector3.MoveTowards(rb.position, rodTip.transform.position, reelSpeed * Time.deltaTime);
                }

                break;

            // Fish has been caught, reset all values and remove the fish
            case "reset":
			    Gauge.fishGauge.enabled = false;    


                line.GetComponent<Renderer>().enabled = false;
                Destroy(hookHinge);
                Destroy(fish);
                rb.GetComponent<Bobber>().reset();
                rb.isKinematic = false;
                fishAttached = false;
                catchSwitch = false;
                androidUIEnabled = false;
                biteCoolDown = 5f;

                rb.velocity = new Vector3(0, 0, 0);
                rb.constraints = orginalConstraint;
                bobber.transform.position = rodTip.transform.position;
                bobber.transform.parent = rodEnd.transform;
                nView.RPC("showCastButton", RPCMode.All);
                status = "standby";

                break;

            case "gameover":
                if (!gameOver)
                    CameraAnimationScript.CamAnimSript.NextAnimation();
                nView.RPC("showEndScreen", RPCMode.All);
                gameOver = true;
                // ???
                /// Application.LoadLevel(Application.loadedLevel);
                break;

            // Default value, this should never be called
            default:
                Debug.Log("Default, something went wrong");
                break;
        }
    }


    [RPC]
    public void showCastButton()
    {
        Debug.Log("Showing Cast Button");
    }


    // Move these to another file
    [RPC]
    public void reeling(float reelSpd)
    {
        if (status == "reeling in") {
			if (reelSpd > 0) {
				reel.rotation = Quaternion.AngleAxis (reelSpd, Vector3.right);
				rodPullFactor = reelSpd;
				rb.constraints = RigidbodyConstraints.FreezePositionY;
			}
		} 
    }


    [RPC]
    public void keepFish()
    {

        Debug.Log("Adding fish to List!");
        fishManager.GetComponent<FishManager>().keepFish(hookedFish);
        fishCount++;
		keepSEFunc ();
        if (fishCount == 3)
        {
            nView.RPC("enableRestartButton", RPCMode.All);
            status = "gameover";
        }
        else
            status = "reset";
    }



    [RPC]
    public void releaseFish()
    {

        Debug.Log("Releasing fish");
		releaseSEFunc ();
        status = "reset";
    }


    [RPC]
    public void enableCaughtUI(string name, float weight)
    {
        Debug.Log("Enabling keep or release ui elements");
        //caughtFish = true;
    }

    [RPC]
    public void giveUp()
    {
        status = "gameover";
        nView.RPC("showEndScreen", RPCMode.All);
    }

    [RPC]
    public void showEndScreen()
    {
        // shows endscreen on phone
    }

    [RPC]
    public void enableRestartButton()
    {
        // enable the restart button on phone
    }

    [RPC]
    public void enableHighScore()
    {
        CameraAnimationScript.CamAnimSript.showHighScore();
    }
    public Texture2D biteSplash;
    public void OnGUI()
    {
        if (showBiteSplash)
        {
            GUI.Label(new Rect(Screen.width / 2 - biteSplash.width / 2, Screen.height / 2 - biteSplash.height / 2, biteSplash.width * pullSplashScale, biteSplash.height * pullSplashScale), biteSplash);
        }
    }

  
    [RPC]
    public void inTransition()
    {
      // tells phone theres a animation transit going
    }

    [RPC]
    public void transitionOver()
    {
        // tells phone theres a animation transit is over
    }

    void OnCollisionEnter(Collision col)
    {
		if (col.gameObject.name == "Ocean") {
			Debug.Log ("hit ocean");
			GameObject.Find ("Bobber");
			rb.velocity = new Vector3 (0, 0, 0);
		}
	}

	public void keepSEFunc(){
		keep.PlayOneShot (keepSE, 1);
	}

	public void releaseSEFunc(){
		release.PlayOneShot (releaseSE, 1);
	}
}