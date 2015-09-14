using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoseOverlayScript : MonoBehaviour {
	
	private RodStatus status;
	public GameObject rod;
	
	private  GameObject container;
	
	private Vector3 startPos;
	private Vector3 onScreenPos;
	public static LoseOverlayScript loseOverlay;
	
	private bool showingUI;

	void Start()
	{
		
		container = GameObject.Find("Lose Overlay");
		//rod = GameObject.Find("NetworkView_TestObject");
		status = RodStatus.rodstatus;
		showingUI = false;
		
		
		startPos = container.transform.position;
		onScreenPos = new Vector3(container.transform.position.x, (Screen.height/2), container.transform.position.z);
		container.transform.position = startPos;
		
		
	}
	
	void Update(){
		if (rod != null) {
			if (status.getStatus () == "reset" && !showingUI) {
				Show ();
				Hide ();
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
		iTween.MoveTo(container, iTween.Hash("position", startPos, "time", 0.5f, "easeType", iTween.EaseType.easeInSine,"delay",4.0f));
	}
	
}
