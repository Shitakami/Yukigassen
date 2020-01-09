using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanFire : MonoBehaviour {

	public GameObject player;

	public GameObject fire;
	public GameObject decisionObject;
	Animator _animator;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("MainCamera");
		_animator = GetComponent<Animator> ();
		fire.SetActive (false);
	}

	// Update is called once per frame
	void Update () {

	}

	public IEnumerator Attack () {
		Debug.Log ("Enter");
		Quaternion rotation = Quaternion.LookRotation (player.transform.position - transform.position);
		rotation.x = 0;
		rotation.z = 0;

		while (true) {
			transform.rotation = Quaternion.RotateTowards (transform.rotation, rotation, Time.deltaTime * 400);

			yield return null;

			if (Mathf.Abs (transform.rotation.y - rotation.y) < 0.001f)
				break;
		}

		

		Debug.Log ("Start Attack");
		transform.LookAt (player.transform);
		_animator.SetBool ("Arrive", true);
		yield return new WaitForSeconds (0.1f);


		int count = 1;
		float time = 0;
		fire.SetActive (true);

		while (true) {

			transform.Rotate (0, 0.2f * count, 0);

			if (time >= 2.35f)
				break;
			time += Time.deltaTime;

			yield return null;
		}

		StartCoroutine (FireDecision ());

		while (true) {

			time = 0;
			count *= -1;

			while (true) {

				transform.Rotate (0, 0.2f * count, 0);				

				if (time >= 4.7f)
					break;
				time += Time.deltaTime;

				yield return null;
			}



			
			
		}


	}

	IEnumerator FireDecision () {
		while (true) {

			GameObject fire = Instantiate (decisionObject);
			fire.transform.position = transform.position;
			fire.GetComponent<Rigidbody> ().velocity = transform.forward * 10;
			Destroy (fire, 2);

			yield return new WaitForSeconds (0.3f);

		}


	}

}
