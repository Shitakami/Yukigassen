using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShot : MonoBehaviour {

	public GameObject ball;
	public float maxTime;
	public float minTime;
	public float randX;
	public float randY;
	public float randZ;
	public float speed;
	
	// Use this for initialization
	void Start () {
		StartCoroutine(MyUpdate());
	}
	
	IEnumerator MyUpdate()
	{
		float x, y;

		while (true)
		{
			x = Random.Range(-randX, randX);
			y = Random.Range(-randY, randY);

			GameObject newBall = Instantiate(ball);
			newBall.transform.position = new Vector3(x, y + transform.position.y, transform.position.z);
			newBall.GetComponent<Rigidbody>().velocity = Vector3.back * speed;
			yield return new WaitForSeconds(Random.Range(minTime, maxTime));

		}


		



	}
}
