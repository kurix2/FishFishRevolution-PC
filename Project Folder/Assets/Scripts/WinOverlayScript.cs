using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinOverlayScript : MonoBehaviour {

	private RodStatus status;
	public GameObject rod;

	private  GameObject container;

	private Vector3 startPos;
	private Vector3 onScreenPos;

	private bool showingUI;



	void Start()
	{

		container = GameObject.Find("Win Overlay");
		//rod = GameObject.Find("NetworkView_TestObject");
		status = RodStatus.rodstatus;
		showingUI = false;


		startPos = container.transform.position;
		onScreenPos = new Vector3(container.transform.position.x, (Screen.height/3)*1.5f, container.transform.position.z);


	}
	
	void Update(){
		if (rod != null) {
			if(status.getStatus() == "reeling up" && !showingUI){
				Show();
			}
			else if(status.getStatus() != "reeling up" && showingUI) {
				Hide();
			}
		}

	}


	public void Show()
	{
		showingUI = true;
		iTween.MoveTo(container, iTween.Hash("position", onScreenPos, "time", 0.5f, "easeType", iTween.EaseType.easeOutSine));
	}
	
	public void Hide()
	{
		showingUI = false;
		iTween.MoveTo(container, iTween.Hash("position", startPos, "time", 0.5f, "easeType", iTween.EaseType.easeInSine));
	}

}
