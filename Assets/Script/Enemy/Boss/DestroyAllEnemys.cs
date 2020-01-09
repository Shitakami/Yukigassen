using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAllEnemys : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void DestroyAll()
	{
		GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
		for (int i = 0; i < enemys.Length; i++)
			Destroy(enemys[i].gameObject);
	}

}
