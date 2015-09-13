﻿using UnityEngine;
using System.Collections;

public class HighScoreScene : MonoBehaviour {
	
	public static HighScoreScene HSScreen;
	
	private Vector3 startPos;
	private Vector3 outPos;
	
	void Awake()
	{
		HSScreen = this;
	}
	
	void Start()
	{
		
		startPos = transform.position;
		outPos = new Vector3(startPos.x, startPos.y - 500, startPos.z);
		transform.position = outPos;
		
		for (int i = 0; i <
		     transform.childCount; ++i)
		{
			transform.GetChild(i).gameObject.SetActive(false);
		}
	}
	
	
	public void Show()
	{
		for (int i = 0; i < transform.childCount; ++i)
		{
			transform.GetChild(i).gameObject.SetActive(true);
		}
		
		foreach (Behaviour childCompnent in this.gameObject.GetComponentsInChildren<Behaviour>())
			childCompnent.enabled = true;
		
		iTween.MoveTo(this.gameObject, iTween.Hash("position", startPos, "time", 0.5f, "easeType", iTween.EaseType.easeOutSine));
	}
	
	public void Hide()
	{
		outPos = new Vector3(startPos.x, startPos.y + 500, startPos.z);
		iTween.MoveTo(this.gameObject, iTween.Hash("position", outPos, "time", 0.5f, "easeType", iTween.EaseType.easeOutSine));
	}
}