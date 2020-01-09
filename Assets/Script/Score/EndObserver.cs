using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndObserver : MonoBehaviour {

	GameObject[] enemys;
	GameObject scoreManager;
	// Use this for initialization
	void Start () {
		scoreManager = GameObject.FindGameObjectWithTag("ScoreManager");
	}
	
	// Update is called once per frame
	void Update () {

		enemys = GameObject.FindGameObjectsWithTag("Enemy");

		if(enemys.Length == 0)
		{
			if (scoreManager != null)
				scoreManager.GetComponent<ScoreManager>().CalcScore();
			Destroy(this.gameObject);
		}

	}
}
