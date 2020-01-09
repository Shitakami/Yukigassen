using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : MonoBehaviour {

	public List<GameObject> Enemys;
	public GameObject ClearString;

	// Use this for initialization
	void Start () {
		ClearString.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log(Enemys.Count);

		if (0 == Enemys.Count)
			ClearString.SetActive(true);

	}
}
