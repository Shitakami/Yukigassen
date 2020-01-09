using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanThrow : MonoBehaviour {

	public GameObject player;
	public GameObject Snowball;

	public GameObject leftHand;
	public GameObject rightHand;

	Animator _animator;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("MainCamera");
		_animator = GetComponent<Animator> ();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator Attack () {
//		Debug.Log ("Enter");
		int hand = -1;
		Quaternion rotation = Quaternion.LookRotation (player.transform.position - transform.position);
		rotation.x = 0;
		rotation.z = 0;

		int count = 0;

		while (true) {
			transform.rotation = Quaternion.RotateTowards (transform.rotation, rotation, Time.deltaTime * 400);

			yield return null;

			if (Mathf.Abs (transform.rotation.y - rotation.y) < 0.0005f)
				break;

			count++;
			if (count > 50)
				break;

		}

		transform.LookAt (player.transform);
		transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);

//		Debug.Log ("Start Attack");

		_animator.SetBool ("Arrive", true);
		yield return new WaitForSeconds (0.1f);

		while (true) {

			GameObject newSnowBall = Instantiate (Snowball);

			if (hand == 1)
				newSnowBall.transform.position = leftHand.transform.position;
			else
				newSnowBall.transform.position = rightHand.transform.position;

			Vector3 velocity = transform.forward * 15f;

			velocity.x += Random.Range (-3f, 3f);
			velocity.y += Random.Range (0, 4f);
			velocity.z += Random.Range (-3f, 3f);

			newSnowBall.GetComponent<Rigidbody> ().velocity = velocity;

			Destroy (newSnowBall, 3f);



			hand *= -1;

			yield return new WaitForSeconds (0.469f);

			transform.LookAt (player.transform);
			transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);
		}
		

	}

}
