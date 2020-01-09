using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class EnemyStatus : MonoBehaviour {

	public IntReactiveProperty HP;
	public int MaxHP = 3;

	public GameObject deathEffect;
	public int tornadeDamage = 1;

	

	bool canHitTornado = true;

	// Use this for initialization
	void Start () {
		HP = new IntReactiveProperty(MaxHP);

		

		HP.Where((x) => x <= 0)
		  .Subscribe(_ =>
		  {

			  GameObject newEffect = Instantiate(deathEffect);
		
			  Vector3 pos = transform.position;
			  pos.y += 1;
			  newEffect.transform.position = pos;

			  // ボスが撃破された場合すべての敵をDestroy
			  if(GetComponent<DestroyAllEnemys>() != null)
				  GetComponent<DestroyAllEnemys>().DestroyAll();
			  

			  Destroy(newEffect, 1f);
			  Destroy(this.gameObject);

		  }
		  );

		this.OnTriggerStayAsObservable()
			.Where(x => x.gameObject.tag == "Tornade")
			.Subscribe(_ => {

				if (!canHitTornado)
					return;

				HP.Value -= tornadeDamage;

				StartCoroutine(TornadeInterval());

	});

		this.OnTriggerEnterAsObservable()
			.Where(x => x.gameObject.tag == "Attack")
			.Subscribe(x =>
			{

				HP.Value -= x.gameObject.GetComponent<Ball>().GetDamage();


			});



	}
	
	// Update is called once per frame
	void Update () {
		
	}



	IEnumerator TornadeInterval()
	{

		canHitTornado = false;

		yield return new WaitForSeconds(1f);

		canHitTornado = true;


	}

	public void DecrementHP(int d)
	{
		HP.Value -= d;
	}

	public int GetHP()
	{
		return HP.Value;
	}

}
