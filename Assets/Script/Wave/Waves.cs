using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.AI;

public class Waves : SingletonBehaviour<Waves> {

	[SerializeField]private GameObject[] m_instanceObjects;
	[SerializeField]private int m_snowmanCount = 0;
	private bool m_canTransitionNextScene = false;

	public GameObject[] InstanceObjects {
		get {
			return m_instanceObjects;
		}
		set { }
	}

	[SerializeField]private GameObject[] m_enemies;
	// [0]...normal, [1]...tackle, [2]...fire
	public GameObject[] Enemies {
		get {
			return m_enemies;
		}
		set { }
	}

	[SerializeField]private GameObject[] m_targets;

	public GameObject[] Targets {
		get {
			return m_targets;
		}
		set { }
	}

	[SerializeField]private GameObject[] m_clearGameObject;

	public GameObject[] ClearGameObject {
		get {
			return m_clearGameObject;
		}
		set { }
	}

	WaveManager waveManager;

	void Start () {
		waveManager = WaveManager.instance;
		waveManager.currentSection.Play ();

		this.UpdateAsObservable ()
			.Subscribe (_ => m_snowmanCount = FindObjectsOfType<NavMeshAgent> ().Length);

		this.ObserveEveryValueChanged (_ => m_snowmanCount)
			.Where (x => x >= 1)
			.Subscribe (_ => m_canTransitionNextScene = true);
		
		this.ObserveEveryValueChanged (_ => m_snowmanCount)
			.Where (x => x == 0)
			.Where (_ => m_canTransitionNextScene)
			.Subscribe (_ => {
	
			m_canTransitionNextScene = false;
			waveManager.currentSection.Play ();
			waveManager.Next ();

		});

	}
}