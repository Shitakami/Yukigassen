using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

	public float HP = 6;
	public float addHP = 5;
	public GameObject endureEffect;
	public GameObject collapseEffect;

	GameObject controller;

	AudioSource hitSE;

	// Use this for initialization
	void Start() {
		hitSE = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update() {

	}

	private void OnTriggerEnter(Collider other)
	{
		if ((this.gameObject.tag == "wall" || this.gameObject.tag == "Untagged") && (other.gameObject.tag == "EnemyAttack" || other.gameObject.tag == "Enemy"))
		{
			hitSE.Play();

			GameObject e = Instantiate(endureEffect);
			e.transform.position = other.transform.position;

			Destroy(e, 1f);
			if (other.gameObject.tag == "EnemyAttack" || other.gameObject.tag == "Enemy")
				Destroy(other.gameObject);
			

			HP--;

			if (controller != null && HP != 0)
				StartCoroutine(controller.GetComponent<TestThrow>().Vibration());
			else if (controller != null && HP == 0)
				StartCoroutine(controller.GetComponent<TestThrow>().Vibration(2000));

			if (HP == 0)
			{
				GameObject newEffect = Instantiate(collapseEffect);
				newEffect.transform.position = this.transform.position;
				Destroy(newEffect, 1f);

				Destroy(this.gameObject);
			}
		}




	}





	public void GrabWall(GameObject con)
	{
		HP += addHP;
		controller = con;
	}

	IEnumerator Vibration()
	{
		var trackedObject = controller.GetComponent<SteamVR_TrackedObject>();
		var device = SteamVR_Controller.Input((int)trackedObject.index);

		for (int i = 0; i < 10; i++)
		{
			device.TriggerHapticPulse(1500);
			yield return null;
		}
	}

}
