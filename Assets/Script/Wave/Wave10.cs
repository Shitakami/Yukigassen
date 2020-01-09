using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

[CreateAssetMenu]
public class Wave10 : Wave {

	public override void Play () {
		Debug.Log ("Scene : 10");
		Instantiate (Waves.instance.Enemies [0], Waves.instance.InstanceObjects [0].transform.position, Quaternion.identity);
		Observable.Timer (System.TimeSpan.FromSeconds (0.4f))
			.Subscribe (_ => Instantiate (Waves.instance.Enemies [0], Waves.instance.InstanceObjects [1].transform.position, Quaternion.identity));
		Observable.Timer (System.TimeSpan.FromSeconds (0.8f))
			.Subscribe (_ => Instantiate (Waves.instance.Enemies [1], Waves.instance.InstanceObjects [2].transform.position, Quaternion.identity));
		Observable.Timer (System.TimeSpan.FromSeconds (1.2f))
			.Subscribe (_ => Instantiate (Waves.instance.Enemies [2], Waves.instance.InstanceObjects [3].transform.position, Quaternion.identity));
	}

}
