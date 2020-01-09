using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

[CreateAssetMenu]
public class Wave1 : Wave {

	public override void Play () {
		Debug.Log ("Scene : 1");
		Instantiate (Waves.instance.Enemies [0], Waves.instance.InstanceObjects [0].transform.position, Quaternion.identity);
		Observable.Timer (System.TimeSpan.FromSeconds (0.4f))
			.Subscribe (_ => Instantiate (Waves.instance.Enemies [0], Waves.instance.InstanceObjects [1].transform.position, Quaternion.identity));
		WaveManager.instance.Next ();
	}

}
