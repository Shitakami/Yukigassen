using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeEnemy : MonoBehaviour {

	Dictionary<string, int> enemyMap;
	public GameObject ClearText;
	// Use this for initialization
	void Start () {
		enemyMap = new Dictionary<string, int>();
		ClearText.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Make(List<GameObject> enemys, string name, int count)
	{

		if (count == 1)
			EnemysSetActive(enemys);
		else if (enemyMap.ContainsKey(name))
		{
			enemyMap[name]--;

			if (enemyMap[name] == 0)
				EnemysSetActive(enemys);
			else if (enemyMap.ContainsKey("10th") && enemyMap["10th"] == 0)
				ClearText.SetActive(true);



		}
		else
		{
			enemyMap.Add(name, count - 1);
			Debug.Log(enemyMap.ContainsKey("10th")) ;

		}


	}

	void EnemysSetActive(List<GameObject> enemys)
	{
		foreach (GameObject enemy in enemys)
			enemy.SetActive(true);
	}

}
