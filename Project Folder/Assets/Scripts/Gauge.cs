using UnityEngine;
using System.Collections;

public class Gauge : MonoBehaviour {

    private RodStatus status;
    private GameObject bobber;
    private GameObject rodEnd;
    public GameObject compass;

    public float barSize = 200;
    private float barSizeMin = 100;
    private float barSizeMax = 300;
    public float barX = 200;
    public float barY = 115;
    public float barHeight = 50;
    private float defaultSize;

    public Texture2D fishIcon;
    private float fishIconX;
    private float fishIconY;
    private float fishIconOffset;


    private float angle;
    public float tolerance;

    private bool aligned;

    private bool fasterThanFish;

    private bool showUI;

    private bool snap;
    // Use this for initialization
    void Start () {
        status = RodStatus.rodstatus;
        bobber = status.bobber;
        rodEnd = status.rodEnd;
        defaultSize = barSize;
        compass = Instantiate(compass, transform.position, Quaternion.identity) as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
        
        Vector3 compassPos = new Vector3(transform.position.x, bobber.transform.position.y, transform.position.z);
        compass.transform.position = compassPos;

        if (status.getStatus() == "reeling in") {

            showUI = true;

            Vector3 lookPos = rodEnd.transform.position - compass.transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            compass.transform.rotation = rotation;


            Vector3 targetDir = bobber.transform.position - compass.transform.position;
            Vector3 forward = compass.transform.forward;
            angle = Vector3.Angle(targetDir, forward);

        
            if (angle <= tolerance)
            {
                aligned = true;
                if (barSize <= barSizeMax)
                    barSize += 0.1f;
            }
            
            else
            { 
                aligned = false;
                if (barSize >= barSizeMin)
                    barSize -= 0.1f;
            }
            
            fishIconX = barX - (fishIcon.width / 2) + fishIconOffset;
            fishIconY = barY - 6;

            
            float dangerZoneL = (barX - barSize / 2) - 4;
            float dangerZoneR = (barX + barSize / 2) - (fishIcon.width - 10);

            if (fishIconX > dangerZoneR || fishIconX < dangerZoneL)
            {
                Debug.Log("Line Snap!");
                status.setStatus("reset");
                barSize = defaultSize;
                fishIconOffset = 0;
            }
        
            if (status.linePower > 1.1f)
                {
                    fasterThanFish = true;
                    fishIconOffset -= 0.05f + (status.linePower / 100);
                }
            else
                {
                    fasterThanFish = false;
                    fishIconOffset += 0.15f;
                }
        } else
        {
            showUI = false;
        }

    }

    void OnGUI()
    {
        if (showUI) { 
           

            GUI.Button(new Rect(barX - (barSize / 2), barY, barSize, barHeight), "");
            GUI.Label(new Rect(fishIconX, fishIconY, fishIcon.width, fishIcon.height), fishIcon);
            /*
          // Debugging
          GUI.Label(new Rect(30, 90, 300, 30), "fish speed: " + status.swimSpeed);
          GUI.Label(new Rect(30, 120, 300, 30), "reel speed: " + status.linePower);

             Debugging buttons
          if (GUI.Button(new Rect(30, 200, 75, 35), "Bar+"))
              barSize++;
          if (GUI.Button(new Rect(30, 240, 75, 35), "Bar-"))
              barSize--;
          if (GUI.Button(new Rect(30, 280, 75, 35), "Fish+"))
              fishIconOffset++;
          if (GUI.Button(new Rect(30, 320, 75, 35), "Fish-"))
              fishIconOffset--;
           GUI.Label(new Rect(30, 360, 300, 30), "snap: " + snap);
       */


        }
    }
}
