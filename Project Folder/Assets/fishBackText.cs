using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class fishBackText : MonoBehaviour {
	public Text MainText;
	public Text BackText;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
		BackText.text = MainText.text;
	}
}
