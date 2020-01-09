using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveEnemy : MonoBehaviour {

	public GameObject target;
	public int splitNum = 3;

	public GameObject MakeEnemy;
	public List<GameObject> nextEnemys;
	public string nextEnemysName;
	public int nextEnemysCount;


	MakeEnemy makeEnemyScript;
	Queue<Vector3> curvePoint;

	SnowmanFire SnowmanFireScript;
	SnowmanTackle SnowmanTackleScript;
	SnowmanThrow SnowmanThrowScript;

	AudioSource brakeSE;

	NavMeshAgent agent;
	// Use this for initialization
	void Start () {

		makeEnemyScript = MakeEnemy.GetComponent<MakeEnemy> ();
		brakeSE = GetComponent<AudioSource> ();

		Vector3 targetPos = target.transform.position;
		Destroy (target);

		Vector3 startPos = transform.position;
		Vector3 splitVec = (targetPos - startPos) / splitNum;
		int direction;

		curvePoint = new Queue<Vector3> ();
		agent = GetComponent<NavMeshAgent> ();
		// agent.destination = targetPos;

		if (Mathf.Abs (transform.position.x - targetPos.x) < 10)
			direction = 0;
		else if (Mathf.Abs (transform.position.z - targetPos.z) < 10)
			direction = 1;
		else if (targetPos.x - transform.position.x > 0 && targetPos.z - transform.position.z > 0)
			direction = 2;
		else if (targetPos.x - transform.position.x > 0 && targetPos.z - transform.position.z < 0)
			direction = 3;
		else if (targetPos.x - transform.position.x < 0 && targetPos.z - transform.position.z > 0)
			direction = 3;
		else
			direction = 2;


		float bias = (targetPos - transform.position).sqrMagnitude / 2000;

		for (int i = 0; i < splitNum - 1; i++) {

			Vector3 pos = startPos + ((i + 1) * splitVec);

			switch (direction) {

				case 0:
					pos.x += i % 2 == 0 ? 5 : -5;
					pos.x *= bias;
					break;
				case 1:
					pos.z += i % 2 == 0 ? 5 : -5;
					pos.z *= bias;
					break;
				case 2:
					pos.x += i % 2 == 0 ? 3 : -3;
					pos.z += i % 2 == 0 ? -3 : 3;
					break;
				case 3:
					pos.x += i % 2 == 0 ? 3 : -3;
					pos.z += i % 2 == 0 ? 3 : -3;
					break;
				
			}

			curvePoint.Enqueue (pos);


		}

		curvePoint.Enqueue (targetPos);

		agent.destination = curvePoint.Dequeue ();

		StartCoroutine (MyUpdate ());

	}
	
	// Update is called once per frame
	IEnumerator MyUpdate () {

		while (true) {
			if (curvePoint.Count == 0)
				break;

			if ((agent.destination - transform.position).sqrMagnitude < 5)
				agent.destination = curvePoint.Dequeue ();

			

			if (curvePoint.Count == 1 && (agent.destination - transform.position).sqrMagnitude < 10) {
				agent.speed = 8;
			}
			
			yield return null;
		}

		yield return new WaitWhile (() => (transform.position - agent.destination).sqrMagnitude > 3f);

		brakeSE.Play ();

		if ((SnowmanFireScript = GetComponent<SnowmanFire> ()) != null)
			StartCoroutine (SnowmanFireScript.Attack ());
		else if ((SnowmanTackleScript = GetComponent<SnowmanTackle> ()) != null)
			StartCoroutine (SnowmanTackleScript.Attack ());
		else if ((SnowmanThrowScript = GetComponent<SnowmanThrow> ()) != null)
			StartCoroutine (SnowmanThrowScript.Attack ());


	}


	public void MyDestroy () {
		if (nextEnemys.Count != 0)
			makeEnemyScript.Make (nextEnemys, nextEnemysName, nextEnemysCount);

		Destroy (this.gameObject);


	}

	

	private void OnTriggerEnter (Collider collider) {

		/*
		if (collider.gameObject.tag == "Attack")
			MyDestroy();
		*/

		

	}

}
