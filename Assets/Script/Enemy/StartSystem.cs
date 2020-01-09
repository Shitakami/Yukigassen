using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSystem : MonoBehaviour {

	public List<GameObject> obj;
	public GameObject scoreManger;
	// Use this for initialization
	void Start () {
		for(int i = 0; i < obj.Count; i++)
		obj[i].SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			for (int i = 0; i < obj.Count; i++)
				obj[i].SetActive(true);
			scoreManger.GetComponent<ScoreManager>().SetStartTime();
		}
	}
}
