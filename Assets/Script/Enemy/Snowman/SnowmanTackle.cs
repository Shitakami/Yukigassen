using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanTackle : MonoBehaviour {

	public GameObject player;


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
		Debug.Log ("Enter");
		Quaternion rotation = Quaternion.LookRotation (player.transform.position - transform.position);
		rotation.x = 0;
		rotation.z = 0;

		int count = 0;

		while (true) {
			transform.rotation = Quaternion.RotateTowards (transform.rotation, rotation, Time.deltaTime * 400);

			yield return null;

			if (Mathf.Abs (transform.rotation.y - rotation.y) < 0.001f)
				break;

			count++;
			if (count > 50)
				break;

		}

		transform.LookAt (player.transform);

		Debug.Log ("Start Attack");

		_animator.SetBool ("Arrive", true);

		yield return new WaitForSeconds (3f);

		GetComponent<Rigidbody> ().velocity = transform.forward * 15f;

		yield return new WaitForSeconds(10f);

		Destroy(this.gameObject);

	}

}
