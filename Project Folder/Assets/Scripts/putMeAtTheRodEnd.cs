using UnityEngine;
using System.Collections;

public class putMeAtTheRodEnd : MonoBehaviour {
    private GameObject rodEnd;
	// Use this for initialization
	void Start () {
      
        
	}
	
	// Update is called once per frame
    void Update()
    {
        if (GameObject.Find("BobLauncher"))
        {
            rodEnd = GameObject.Find("BobLauncher");
            this.transform.position = rodEnd.transform.position;
        }
        else return;
        
       
	}
}
