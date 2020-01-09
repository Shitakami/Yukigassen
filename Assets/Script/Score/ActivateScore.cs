using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateScore : MonoBehaviour {

	public GameObject scoreManager;

	// Use this for initialization
	void Start () {
		scoreManager.GetComponent<ScoreManager>().CalcScore();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
