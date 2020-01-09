using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLoring : MonoBehaviour {

	public GameObject ball;


	bool flag = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerStay(Collider other)
	{

		var trackedObject = GetComponent<SteamVR_TrackedObject>();
		var device = SteamVR_Controller.Input((int)trackedObject.index);

		if (device.velocity.x * device.velocity.x + device.velocity.z * device.velocity.z > 4 && device.GetPress(SteamVR_Controller.ButtonMask.Trigger) && flag)
		{
			GameObject newBall = Instantiate(ball);
			newBall.transform.position = this.transform.position;
			newBall.GetComponent<Rigidbody>().velocity = device.velocity * 4;
		//	newBall.GetComponent<Rigidbody>().angularVelocity = device.angularVelocity;
		//	newBall.GetComponent<Rigidbody>().maxAngularVelocity = newBall.GetComponent<Rigidbody>().angularVelocity.magnitude;
			flag = false;
			StartCoroutine(Interval());
		}


	}

	IEnumerator Interval()
	{
		yield return new WaitForSeconds(1f);
		flag = true;


	}
}
