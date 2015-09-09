using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartScreenScript : MonoBehaviour {

    public Image mainTitleImg;

	// Use this for initialization
	void Start () {
        mainTitleImg.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (mainTitleImg.enabled && NetworkManager.nManager.gameStarted)
        {
            mainTitleImg.enabled = false;
            this.enabled = false;
        }
            
	}
}
