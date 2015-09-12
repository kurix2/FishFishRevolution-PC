using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartScreenScript : MonoBehaviour {

    public Image mainTitleImg;
	public Image fishingGirl;
	CanvasRenderer titleRenderer;
	CanvasRenderer girlRenderer;

	public float fadeTime = 2f;
	private float remainingTime;

	// Use this for initialization
	void Start () {

		remainingTime = fadeTime;
        mainTitleImg.enabled = true;
		fishingGirl.enabled = true;
		girlRenderer = fishingGirl.GetComponent<CanvasRenderer>();
		titleRenderer = mainTitleImg.GetComponent<CanvasRenderer>();
	}


	
	// Update is called once per frame
	void Update () {


        if (mainTitleImg.enabled && NetworkManager.nManager.gameStarted)
        {
			remainingTime -= Time.deltaTime;
			if(remainingTime <= 0f){
				fishingGirl.enabled = false;
				mainTitleImg.enabled = false;
				this.enabled = false;
			}

			float alpha = remainingTime/fadeTime;
			girlRenderer.SetAlpha(alpha);
			titleRenderer.SetAlpha(alpha);
        }
            
	}

}
