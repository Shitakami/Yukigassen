using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour {

	public GameObject P;

	string tagName = "ball";
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == tagName)
		{
			GameObject p = Instantiate(P);
			p.transform.position = collision.transform.position;
			Destroy(collision.gameObject);
			Destroy(p, 5f);


		}
	}

}
