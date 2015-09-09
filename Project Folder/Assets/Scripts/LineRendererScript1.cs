using UnityEngine;
using System.Collections;

public class LineRendererScript1 : MonoBehaviour {

	public string status;
	public GameObject start;
	public GameObject end;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		status = GameObject.Find("NetworkView_TestObject(Clone)").GetComponent<RodStatus>().getStatus();

		if(!GameObject.Find("Bobber"))return;
		else{
			GameObject start = GameObject.Find("BobLauncher");
			LineRenderer lineY = GameObject.Find ("LineRendererObject").GetComponent<LineRenderer> ();
			lineY.SetPosition(0, start.transform.position);

			//If not reeling up, set the line end to the bobber. If reeling up, set the line end to the fish's mouth.
			if(status != "reeling up"){
				GameObject end = GameObject.Find("Bobber");
				lineY.SetPosition(1, end.transform.position);
			}
			else if (status == "reeling up"){
				GameObject end = GameObject.Find("MouthPoint");
				lineY.SetPosition(1, end.transform.position);	
			}

		}
	}
}
