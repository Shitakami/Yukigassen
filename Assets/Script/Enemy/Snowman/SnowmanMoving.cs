using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UniRx;
using UniRx.Triggers;

public class SnowmanMoving : MonoBehaviour {

	public GameObject m_target;
	private int m_splitNum = 3;
	private NavMeshAgent m_agent;
	public AudioSource m_audioSource;
	Queue<Vector3> curvePoint = new Queue<Vector3> ();
	SnowmanFire SnowmanFireScript;
	SnowmanTackle SnowmanTackleScript;
	SnowmanThrow SnowmanThrowScript;
	private int m_numOfEnemies;

	void Start () {
		m_audioSource = GetComponent<AudioSource> ();
		this.UpdateAsObservable ()
			.TakeWhile (_ => m_target == null)
			.Subscribe (_ => {
			
			m_numOfEnemies = FindObjectsOfType<NavMeshAgent> ().Length;
			switch (m_numOfEnemies) {
				case 1:
					m_target = Waves.instance.Targets [0];
					break;

				case 2:
					m_target = Waves.instance.Targets [1];
					break;

				case 3:
					m_target = Waves.instance.Targets [2];
					break;

				case 4:
					m_target = Waves.instance.Targets [3];
					break;
			}
		});

		Observable.Timer (System.TimeSpan.FromSeconds (0.3f))
			.Subscribe (_ => {
			
			m_agent = GetComponent<NavMeshAgent> ();
			Vector3 m_startPos = transform.position;
			Vector3 splitVec = (m_target.transform.position - m_startPos) / m_splitNum;
			int direction;
			if (Mathf.Abs (transform.position.x - m_target.transform.position.x) < 15)
				direction = 0;
			else if (Mathf.Abs (transform.position.z - m_target.transform.position.z) < 15)
				direction = 1;
			else if (m_target.transform.position.x - transform.position.x > 0 && m_target.transform.position.z - transform.position.z > 0)
				direction = 2;
			else if (m_target.transform.position.x - transform.position.x > 0 && m_target.transform.position.z - transform.position.z < 0)
				direction = 3;
			else if (m_target.transform.position.x - transform.position.x < 0 && m_target.transform.position.z - transform.position.z > 0)
				direction = 3;
			else
				direction = 2;
			
//			Debug.Log ("direction = " + direction);

			float bias = (m_target.transform.position - transform.position).sqrMagnitude / 2000;
			for (int i = 0; i < m_splitNum - 1; i++) {

				Vector3 pos = m_startPos + ((i + 1) * splitVec);
				
				int onAxis = 5;
				int outAxis = 3;
				
				switch (direction) {

					case 0:
						pos.x += i % 2 == 0 ? onAxis : -onAxis;
						pos.x *= bias;
						break;
					case 1:
						pos.z += i % 2 == 0 ? onAxis : -onAxis;
						pos.z *= bias;
						break;
					case 2:
						pos.x += i % 2 == 0 ? outAxis : -outAxis;
						pos.z += i % 2 == 0 ? -outAxis : outAxis;
						break;
					case 3:
						pos.x += i % 2 == 0 ? outAxis : -outAxis;
						pos.z += i % 2 == 0 ? outAxis : -outAxis;
						break;
				}
				//Debug.Log (pos);
				curvePoint.Enqueue (pos);


			}

			curvePoint.Enqueue (m_target.transform.position);

			m_agent.destination = curvePoint.Dequeue ();
			StartCoroutine (MyUpdate ());
		});
		


	}

	IEnumerator MyUpdate () {

		while (true) {
			if (curvePoint.Count == 0)
				break;

			if ((m_agent.destination - transform.position).sqrMagnitude < 5) {
				m_agent.destination = curvePoint.Dequeue ();
//				Debug.Log (m_agent.destination);
			}

			if (curvePoint.Count == 1 && (m_agent.destination - transform.position).sqrMagnitude < 10) {
				m_agent.speed = 8;
			}

			yield return null;
		}

		yield return new WaitWhile (() => (transform.position - m_agent.destination).sqrMagnitude > 3f);

		m_audioSource.Play ();

		if ((SnowmanFireScript = GetComponent<SnowmanFire> ()) != null)
			StartCoroutine (SnowmanFireScript.Attack ());
		else if ((SnowmanTackleScript = GetComponent<SnowmanTackle> ()) != null)
			StartCoroutine (SnowmanTackleScript.Attack ());
		else if ((SnowmanThrowScript = GetComponent<SnowmanThrow> ()) != null)
			StartCoroutine (SnowmanThrowScript.Attack ());

	}

	public void SetAgent(GameObject pos)
	{

		m_target = pos;


	}

		
}
