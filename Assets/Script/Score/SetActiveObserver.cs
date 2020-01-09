using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveObserver : MonoBehaviour {

	public GameObject endObserver;

	// Use this for initialization
	void Start () {
		Instantiate(endObserver);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
