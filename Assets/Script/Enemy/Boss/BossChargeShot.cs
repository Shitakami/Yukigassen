using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChargeShot : MonoBehaviour {

	public float biggerSize;
	public float biggerTime;

	float time = 0;
	Vector3 dScale;
	// Use this for initialization
	void Start () {
		this.gameObject.transform.localScale = Vector3.zero;
		dScale = new Vector3(biggerSize, biggerSize, biggerSize);
	}
	
	// Update is called once per frame
	void Update () {

		if (time < biggerTime)
			this.transform.localScale += dScale;

	}
}
