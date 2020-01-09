using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour {

	public float verticalSpeed = 1;
	public float horizontalSpeed = 1;
	public float verticalTime = 1;
	public float horizontalTime = 3;

	// Use this for initialization
	void Start () {
		StartCoroutine(VerticalMove());
		StartCoroutine(HorizontalMove());
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	IEnumerator VerticalMove()
	{

		float time = 0;
		int d = 1;
		while (true)
		{

			while (time < verticalTime)
			{
				Vector3 pos = transform.position;
				pos.y += verticalSpeed * Time.deltaTime * d;
				transform.position = pos;

				time += Time.deltaTime;

				yield return null;

			}

			time = 0;
			d *= -1;

		}
	}

	IEnumerator HorizontalMove()
	{

		float time = 0;
		int d = 1;

		while (true)
		{

			while (time < horizontalTime)
			{
				Vector3 pos = transform.position;
				pos.x += horizontalSpeed * Time.deltaTime * d;
				transform.position = pos;

				time += Time.deltaTime;

				yield return null;

			}
			time = 0;
			d *= -1;

		}
	}

}
