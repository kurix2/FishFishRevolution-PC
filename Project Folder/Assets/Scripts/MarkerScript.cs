using UnityEngine;
using System.Collections;

public class MarkerScript : MonoBehaviour {
    private GUIStyle style;
    private float boatDistance;
    private GameObject origin;
    private GameObject bobber;
    public Texture2D marker;

	// Use this for initialization
	void Start () {
        GameObject origin = GameObject.Find("Fishing Boat");
       // GameObject bobber = GameObject.Find("Bobber");
 
	}
	
	// Update is called once per frame
	void Update () {
        GameObject origin = GameObject.Find("Fishing Boat");
        GameObject bobber = GameObject.Find("Bobber");
        boatDistance = Vector3.Distance(origin.transform.position, bobber.transform.position);
        //Debug.Log(boatDistance);

	}

    void OnGUI() {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (boatDistance >= 10)
        {
            //WorldToScreenPoint
           // Debug.Log(screenPos);
           
            GUI.Label(new Rect(
                Mathf.Clamp((screenPos.x) - 5, 0, Screen.width),
                Mathf.Clamp((Screen.height - screenPos.y) - 20, 0, Screen.height),
               15, 15),
                marker);

            GUI.contentColor = Color.black;

            GUI.Label(new Rect(
              Mathf.Clamp((screenPos.x) - 8, 0, Screen.width),
              Mathf.Clamp((Screen.height - screenPos.y) - 35, 0, Screen.height),
             50, 25), "" + Mathf.Ceil(boatDistance) + "m");
        }


    }
}
