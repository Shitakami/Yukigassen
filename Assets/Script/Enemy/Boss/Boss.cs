using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	// ballShot用変数
	public GameObject rBallShotPos;
	public GameObject lBallShotPos;
	public GameObject ball;
	public float ballSpeed;
	public float ballShotTime;
	public int ballShotCount;

	// チャージショット用変数
	public GameObject chargeShotPos;
	public GameObject chargeBall;
	public float chargeShotSpeed;
	public int chargeShotCount;
	Animator _animator;

	// 召喚用変数
	public GameObject summonsPos;
	public GameObject summonsEnemy;
	public GameObject summonsEnemyTarget;
	int count = 0;

	GameObject player;
	float frame;

	string state = "state";
	string animSpeed = "speed";

	// Use this for initialization
	void Start () {

		_animator = GetComponent<Animator>();
		StartCoroutine(MyUpdate());
		player = GameObject.FindGameObjectWithTag("MainCamera");
	}

	private void Update()
	{
		this.transform.LookAt(player.transform);


	}

	// Update is called once per frame
	IEnumerator MyUpdate () {

		int pattern;

		while (true)
		{

			_animator.SetInteger(state, 0);
			_animator.SetFloat(animSpeed, 0.5f);
			yield return new WaitForSeconds(3f);

			pattern = Random.Range(0, 3);


			switch (pattern)
			{
				case 0:
					for(int i = 0; i < ballShotCount; i++)
						yield return StartCoroutine(BallShot());
					break;

				case 1:
					for(int i = 0; i < chargeShotCount; i++)
						yield return StartCoroutine(ChargeShot());
					break;

				case 2:
					yield return StartCoroutine(Summons());
					break;

			}

		}



	}


	IEnumerator BallShot()
	{

		_animator.SetTrigger("BossBallShot");

		yield return new WaitWhile(() => !_animator.GetCurrentAnimatorStateInfo(0).IsName("BossBallShot"));


		int count = 0;
		int status = 0;

		while (true)
		{
			AnimatorStateInfo animState = _animator.GetCurrentAnimatorStateInfo(0);


			if(animState.normalizedTime > 0.28 && status == 0)
			{
				status = 1;
				GameObject newBall = Instantiate(ball);
				newBall.transform.position = lBallShotPos.transform.position;
				newBall.transform.LookAt(player.transform);
				Vector3 velocity = newBall.transform.forward * ballSpeed;
				velocity.x += Random.Range(-2f, 2f);
				velocity.y += Random.Range(-2f, 2f);
				velocity.z += Random.Range(-2f, 2f);
				newBall.GetComponent<Rigidbody>().velocity = velocity;
				count++;
				Debug.Log("status = 1");
			}
			else if(animState.normalizedTime > 0.78 && status == 1)
			{
				status = 2;
				GameObject newBall = Instantiate(ball);
				newBall.transform.position = rBallShotPos.transform.position;
				newBall.transform.LookAt(player.transform);
				Vector3 velocity = newBall.transform.forward * ballSpeed;
				velocity.x += Random.Range(-2f, 2f);
				velocity.y += Random.Range(-2f, 2f);
				velocity.z += Random.Range(-2f, 2f);
				newBall.GetComponent<Rigidbody>().velocity = velocity;
				count++;
			}
			


			if (!animState.IsName("BossBallShot"))
				yield break;

			yield return null;


		}




	}


	IEnumerator ChargeShot()
	{

		int status = 0;
		GameObject newChargeBall = null;

		_animator.SetTrigger("BossChargeShot");
		
		yield return new WaitWhile(() => !_animator.GetCurrentAnimatorStateInfo(0).IsName("BossChargeShot"));

		while (true)
		{

			AnimatorStateInfo animState = _animator.GetCurrentAnimatorStateInfo(0);

			if(status == 0 && animState.normalizedTime > 0.15f)
			{
				newChargeBall = Instantiate(chargeBall, transform, chargeShotPos.transform);
				newChargeBall.transform.position = chargeShotPos.transform.position;
				
				
				status = 1;

			}
			else if(status == 1 && animState.normalizedTime > 0.5f)
			{
				chargeBall.transform.LookAt(player.transform);
				newChargeBall.GetComponent<Rigidbody>().velocity = newChargeBall.transform.forward * chargeShotSpeed;
				status = 2;
				newChargeBall.transform.parent = null;
				Destroy(newChargeBall, 5f);
			}


			if (!animState.IsName("BossChargeShot"))
				yield break;

			yield return null;

		}
	}


	IEnumerator Summons()
	{
		if (count >= 4)
			yield break;

		int status = 0;
		_animator.SetTrigger("BossSummons");
		yield return new WaitWhile(() => !_animator.GetCurrentAnimatorStateInfo(0).IsName("BossSummons"));

		while (true)
		{
			AnimatorStateInfo animState = _animator.GetCurrentAnimatorStateInfo(0);

			if(status == 0 && animState.normalizedTime > 0.5f)
			{
				GameObject newEnemy = Instantiate(summonsEnemy);
				Vector3 pos = summonsPos.transform.position;
				pos.y = 0;
				newEnemy.transform.position = pos;
				status = 1;
				count++;
			}
			

			if (!animState.IsName("BossSummons"))
				yield break;

			yield return null;

		}

		
	}

}
