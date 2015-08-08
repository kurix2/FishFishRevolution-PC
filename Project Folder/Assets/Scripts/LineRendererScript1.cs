using UnityEngine;
using System.Collections;

public class LineRendererScript1 : MonoBehaviour {

	private GameObject start;
	private GameObject end;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!GameObject.Find("Bobber"))return;
		else{
		GameObject start = GameObject.Find("BobLauncher");
		GameObject end = GameObject.Find("Bobber");
		LineRenderer lineY = GameObject.Find ("LineRendererObject").GetComponent<LineRenderer> ();

		lineY.SetPosition(0, start.transform.position);
		lineY.SetPosition(1, end.transform.position);
		}
	}
}
