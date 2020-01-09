using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;


public class Ball : MonoBehaviour {

	public GameObject explosion;
	public int damage = 1;

	bool throwing = false;
	GameObject scoreManager;

	
	// Use this for initialization
	void Start () {

		scoreManager = GameObject.FindGameObjectWithTag("ScoreManager");

		this.OnTriggerEnterAsObservable()
			.Where( x => x.gameObject.tag == "Plane" || x.gameObject.tag == "Enemy")
			.Subscribe( x => {

				GameObject newEffect = Instantiate(explosion);
				newEffect.transform.position = this.transform.position;

				scoreManager.GetComponent<ScoreManager>().BallCountIncrement();

				if (x.gameObject.tag == "Enemy")
					scoreManager.GetComponent<ScoreManager>().HitBallCountIncrement();

				Destroy(this.gameObject);

		});


		this.OnTriggerEnterAsObservable()
			.Where(_ => throwing == true)
			.Where(x => x.gameObject.tag == "wall" || x.gameObject.tag == "Untagged")
			.Subscribe(_ =>
			{


				Destroy(this.gameObject);

			});

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	/*
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Plane")
		{
			GameObject effect = Instantiate(explosion);
			effect.transform.position = transform.position;

			if (other.gameObject.tag == "Enemy")
				other.gameObject.GetComponent<MoveEnemy>().MyDestroy();


			Destroy(this.gameObject);
		}
		else if(other.gameObject.tag == "wall")
		{
			Destroy(this.gameObject);
		}
	}
	*/

	public int GetDamage()
	{
		return damage;
	}

	public void Throw()
	{
		throwing = true;
	}

}
