using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadSceneManager : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R))
		{
			StartCoroutine(ReloadScene());
		}
	}

	IEnumerator ReloadScene()
	{
		AsyncOperation async = Application.LoadLevelAsync("Stage1");
		async.allowSceneActivation = false;

		while(async.progress > 0.9f)
		{
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForSeconds(1f);
		async.allowSceneActivation = true;
	}
}
