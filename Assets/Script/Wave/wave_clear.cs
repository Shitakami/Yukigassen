using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

[CreateAssetMenu]
public class wave_clear : Wave {

	public override void Play () {

		Waves.instance.ClearGameObject [0].SetActive (true);
		Instantiate (Waves.instance.ClearGameObject [1], Waves.instance.Targets [1].transform.position, Quaternion.identity);
		Instantiate (Waves.instance.ClearGameObject [2], Waves.instance.Targets [1].transform.position + new Vector3 (0.0f, 1.0f, 0.0f), Quaternion.identity);

	}

}
