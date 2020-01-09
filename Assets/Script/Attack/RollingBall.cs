using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;

public class RollingBall : MonoBehaviour {

	public float biggerRate = 0.0001f;
	public float speedDownRate = 0.000001f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {


		Vector3 newSize = this.transform.localScale;
		newSize.x += biggerRate;
		newSize.y += biggerRate;
		newSize.z += biggerRate;
		this.transform.localScale = newSize;

	}

	IEnumerator MyUpdate()
	{

		while (true)
		{




			this.GetComponent<Rigidbody>().velocity *= (1 - speedDownRate);

			yield return new WaitForSeconds(0.2f);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		

		if(collision.gameObject.tag == "Enemy")
		{
			Destroy(collision.gameObject);
		}

	}

}
