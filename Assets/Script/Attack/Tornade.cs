using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornade : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector3 pos = this.transform.position;
		pos.y = 0;
		this.transform.position = pos;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
