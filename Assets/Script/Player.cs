using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;


public class Player : MonoBehaviour {

	public Image damageImage;

	public GameObject leftController;
	public GameObject rightController;

	int damage = 0;
	TestThrow leftScript;
	TestThrow rightScript;

	AudioSource damageSE;

	// Use this for initialization
	void Start () {
		leftScript = leftController.GetComponent<TestThrow>();
		rightScript = rightController.GetComponent<TestThrow>();

		damageSE = GetComponent<AudioSource>();

		damageImage.color = new Color(1, 0, 0, 0);

		this.OnTriggerEnterAsObservable()
			.Where(x => x.gameObject.tag == "Enemy")
			.Subscribe(x =>
			{

				x.GetComponent<EnemyStatus>().DecrementHP(5);
				damage += 3;
				

			});

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		
		if(other.gameObject.tag == "EnemyAttack" || other.gameObject.tag == "Enemy")
		{
			
			damage += 1;
			if (other.gameObject.tag == "EnemyAttack")
				Destroy(other.gameObject);

			StartCoroutine(leftScript.Vibration(2000, 40));
			StartCoroutine(rightScript.Vibration(2000, 40));

			damageSE.Play();

			StartCoroutine(DamageEffect());

		}

	}

	IEnumerator DamageEffect()
	{

		float da = 0.01f;
		Color color = new Color(1, 0, 0, 0);

		for(int i = 0; i < 50; i++)
		{
			color.a += da;
			damageImage.color = color;
			yield return new WaitForSeconds(0.01f);
		}
		

		for(int i = 0; i < 50; i++)
		{
			color.a -= da;
			damageImage.color = color;
			yield return new WaitForSeconds(0.01f);
		}


	}

	public int GetDamage()
	{
		return damage;
	}



}
