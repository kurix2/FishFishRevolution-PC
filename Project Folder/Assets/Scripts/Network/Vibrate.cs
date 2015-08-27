using UnityEngine;
using System.Collections;

public class Vibrate : MonoBehaviour {

	// Use this for initialization
    NetworkView nView;
   
    void Start()
    {
        nView = GetComponent<NetworkView>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space")) {
          //  Debug.Log("space key was pressed");
            nView.RPC("vibrate", RPCMode.All);
            
        }
    }
}
